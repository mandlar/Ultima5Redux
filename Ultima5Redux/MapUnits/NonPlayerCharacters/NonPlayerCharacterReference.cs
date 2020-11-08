﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Ultima5Redux.Dialogue;
using Ultima5Redux.Maps;

//using static Ultima5Redux.NonPlayerCharacterReferences;

namespace Ultima5Redux.MapUnits.NonPlayerCharacters
{
    /// <summary>
    /// A single non player character (NPC)
    /// </summary>
    public partial class NonPlayerCharacterReference
    {

        #region Private Variables
        private GameState _gameStateRef;
        private readonly bool _normalNPC;

        #endregion

        #region Constants, Sructures and Enums
        /// <summary>
        /// Original structure
        /// </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct NPCSchedule
        {
            public fixed byte AI_types[3];
            public fixed byte x_coordinates[3];
            public fixed byte y_coordinates[3];
            public fixed byte z_coordinates[3];
            public fixed byte times[4];
        };

        /// <summary>
        /// NPC Type, any other value is a specific character
        /// </summary>
        public enum NPCDialogTypeEnum
        {
            Blacksmith = 0x81, Barkeeper = 0x82, HorseSeller = 0x83, Shipwright = 0x84, Healer = 0x87,
            InnKeeper = 0x88, MagicSeller = 0x85, GuildMaster = 0x86, None = 0xFF
            // unknowns may be crown and sandlewood box
        };
        // public enum NPCDialogTypeEnum
        // {
        //     Custom = -1, Blacksmith = 0x81, Barkeeper = 0x82, HorseSeller = 0x83, Shipwright = 0x84, Healer = 0x87,
        //     InnKeeper = 0x88, MagicSeller = 0x85, GuildMaster = 0x86, Unknown = 0xFF, None = 0xFE, Avatar = 0x1C
        //     // unknowns may be crown and sandlewood box
        // };

        private enum NPCKeySpriteEnum
        {
            Custom = -1, Guard = 368, Merchant = 340, Healer = 320,
            UnknownX86 = 368, Bard = 324, Fighter = 328, Towny = 336, BardPlaying = 348, Jester = 344, Child = 360, Beggar = 364,
            Apparition = 372, BlackThorn = 376, LordBritish = 380, Unknown = 0xFF
        };

        // based on Xu4 AI = (0x0-fixed, 0x1-wander, 0x80-follow, 0xFF-attack)

        #endregion

        #region Public Static Methods
        /// <summary>
        /// Return true if the dialog is not part of a standard dialog tree like a guard or shopkeeper
        /// </summary>
        /// <param name="dialogType">The dialog type that yu want to compare</param>
        /// <returns></returns>
        public static bool IsSpecialDialogType(NPCDialogTypeEnum dialogType)
        {
            if (dialogType == 0) return true;
            //if (dialogType == NPCDialogTypeEnum.Avatar) return true;
            foreach (NPCDialogTypeEnum tempDialogType in (NPCDialogTypeEnum[])Enum.GetValues(typeof(NPCDialogTypeEnum)))
            {
                if (dialogType == tempDialogType)
                    return true;
            }
            return false;
        }
        #endregion

        #region Public Methods
        //public void Move(Point2D xy, int nFloor)
        //{
        //    CurrentMapPosition = xy;
        //    CurrentFloor = nFloor;
        //}
        #endregion

        #region Public Properties
        /// <summary>
        /// NPCs name
        /// </summary>
        public string Name
        {
            get
            {
                if (Script == null) return string.Empty;

                return Script.GetScriptLine(TalkScript.TalkConstants.Name).GetScriptItem(0).Str.Trim();
            }
        }

        /// <summary>
        /// friendlier version of name in case they are a profession and not named
        /// </summary>
        public string FriendlyName => (Name == "" ? NPCType.ToString() : Name);

        public bool IsShoppeKeeper => NPCType != NPCDialogTypeEnum.None;
        // public bool IsShoppeKeeper => NPCType != NPCDialogTypeEnum.Custom &&
        //                               NPCType != NPCDialogTypeEnum.Avatar &&
        //                               NPCType != NPCDialogTypeEnum.Unknown;

        /// <summary>
        /// The daily schedule of the NPC
        /// </summary>
        public NonPlayerCharacterSchedule Schedule { get; }
        /// <summary>
        /// The Dialog identifier
        /// </summary>
        public byte DialogNumber { get; }

        /// <summary>
        /// 0-31 index of it's position in the NPC arrays (used for saved.gam references)
        /// </summary>
        public int DialogIndex { get; }

        /// <summary>
        /// The byte representing the type of character
        /// </summary>
        private byte CharacterType { get; }

        /// <summary>
        /// The talk script the NPC will follow
        /// </summary>
        public TalkScript Script { get; }

        /// <summary>
        /// They are either a merchant or they have a speaking role
        /// </summary>
        public bool NormalNPC => NPCType != NPCDialogTypeEnum.None || DialogNumber > 0;

        //        public Point2D CurrentMapPosition { get; private set; } = new Point2D(0, 0);
//        public int CurrentFloor { get; private set; }

        /// <summary>
        /// Which map is the NPC on?
        /// </summary>
        //public SmallMapReferences.SingleMapReference MapReference { get; }
        public SmallMapReferences.SingleMapReference.Location MapLocation { get; }
        public byte MapLocationId => (byte)(MapLocation - 1);

        /// <summary>
        /// What type of NPC are they? 
        /// </summary>
        public NPCDialogTypeEnum NPCType
        {
            get
            {
                // it's the Avatar
                if (NPCKeySprite == 256) return NPCDialogTypeEnum.None;
                // it's a merchant
               // if (NPCKeySprite == 340)
                {
                    foreach (int npctype in Enum.GetValues(typeof(NPCDialogTypeEnum)))
                    {
                        if (npctype == DialogNumber) //&& npctype != (int)NPCDialogTypeEnum.Custom)
                        {
                            return (NPCDialogTypeEnum)npctype;
                        }
                    }
                }
                foreach (int npctype in Enum.GetValues(typeof(NPCDialogTypeEnum)))
                {
                    if (npctype == CharacterType) //&& npctype != (int)NPCDialogTypeEnum.Custom)
                        // if (npctype == DialogNumber && npctype != (int)NPCDialogTypeEnum.Custom)
                    {
                        return (NPCDialogTypeEnum)npctype;
                    }
                }
                return NPCDialogTypeEnum.None;
            }
        }

        //public int NPCKeySprite
        //{
        //    get; private set;
        //}
        //private int _npcKeySprite;
        public int NPCKeySprite => CharacterType + 0x100;

        /// <summary>
        /// Returns true if the NPC knows/has met the Avatar
        /// </summary>
        public bool KnowTheAvatar
        {
            get
            {
                int nScriptLines = Script.NumberOfScriptLines;

                // two steps - first if the NPC Has met flag is flipped in saved.gam then we know they have met the Avatar
                // secondly, if the AskName command is not present in their entire script, then we can surmise that they must already know the Avatar (from the old days)

                if (_gameStateRef.NpcHasMetAvatar(this))
                {
                    return true;
                }

                for (int i = 0; i < nScriptLines; i++)
                {
                    if (Script.GetScriptLine(i).ContainsCommand(TalkScript.TalkCommand.AskName))
                    {
                        return false;
                    }
                }
                return true;
            }
            set => _gameStateRef.SetNpcHasMetAvatar(this, value);
        }
        #endregion

        #region Constructors

        /// <summary>
        /// Construct an NPC
        /// </summary>
        /// <param name="gameStateRef"></param>
        /// <param name="schedule">daily schedule</param>
        /// <param name="npcType">type of NPC they are</param>
        /// <param name="dialogNumber">dialog number referencing data OVL</param>
        /// <param name="dialogIndex">0-31 index of it's position in the NPC arrays (used for saved.gam references)</param>
        /// <param name="talkScript">their conversation script</param>
        /// <param name="location"></param>
        /// <param name="bNormalNPC"></param>
        public NonPlayerCharacterReference(SmallMapReferences.SingleMapReference.Location location, GameState gameStateRef, NPCSchedule schedule,
            byte npcType, byte dialogNumber, int dialogIndex, TalkScript talkScript, bool bNormalNPC)
        {
            Schedule = new NonPlayerCharacterSchedule(schedule);
            
            MapLocation = location;
            //NPCKeySprite = nKeySprite;

            CharacterType = npcType;
            DialogNumber = dialogNumber;
            Script = talkScript;
            //NormalNPC = bNormalNPC;
            DialogIndex = dialogIndex;
            _gameStateRef = gameStateRef;
            _normalNPC = bNormalNPC;

            // no schedule? I guess you're not real
            if (!IsEmptySchedule(schedule))
            {
                Debug.WriteLine(location + "     NPC Number: " + this.DialogNumber + " in " + location);
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Does the NPC have an empty schedule? If so, then they aren't actually an NPC
        /// </summary>
        /// <param name="schedule">daily schedule</param>
        /// <returns></returns>
        private static bool IsEmptySchedule(NonPlayerCharacterReference.NPCSchedule schedule)
        {
            unsafe
            {
                if (schedule.times[0] == 0 && schedule.times[1] == 0 && schedule.times[2] == 0 && schedule.times[3] == 0 && schedule.times[4] == 0)
                    return true;
            }
            return false;
        }
    }


    #endregion
}
