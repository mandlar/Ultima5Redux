﻿using System;
using System.Collections.Generic;
using System.IO;
using Ultima5Redux.Data;
using Ultima5Redux.DayNightMoon;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.PlayerCharacters;
using Ultima5Redux.PlayerCharacters.Inventory;

namespace Ultima5Redux
{
    public class GameState
    {
        /// <summary>
        ///     Data chunks for each of the save game sections
        /// </summary>
        public enum DataChunkName
        {
            Unused, NPC_ISALIVE_TABLE, NPC_ISMET_TABLE, N_PEOPLE_PARTY,  CURRENT_YEAR, CURRENT_MONTH, CURRENT_DAY,
            CURRENT_HOUR, CURRENT_MINUTE, NPC_TYPES, NPC_MOVEMENT_LISTS, NPC_MOVEMENT_OFFSETS, NPC_SPRITE_INDEXES,
            CHARACTER_ANIMATION_STATES, CHARACTER_STATES, MOONSTONE_X_COORDS,
            MOONSTONE_Y_COORDS, MOONSTONE_BURIED, MOONSTONE_Z_COORDS, ACTIVE_CHARACTER, 
            MONSTERS_AND_STUFF_TABLE
        }

        /// <summary>
        ///     2D array of flag indicating if an NPC is dead [MasterMap][npc#]
        /// </summary>
        private readonly bool[][] _npcIsDeadArray;

        /// <summary>
        ///     2D array of flag indicating if an NPC is met [MasterMap][npc#]
        /// </summary>
        private readonly bool[][] _npcIsMetArray;

        private readonly DataChunks<OverlayChunkName> _overworldOverlayDataChunks;

        /// <summary>
        ///     A random number generator - capable of seeding in future
        /// </summary>
        private readonly Random _ran = new Random();

        private readonly DataChunks<OverlayChunkName> _underworldOverlayDataChunks;

        private readonly ImportedGameState _importedGameState;

        // all initial loaded state information
        private readonly SmallMapReferences.SingleMapReference.Location _location;
        private readonly int _nInitialFloor;
        private readonly int _nInitialX;
        private readonly int _nInitialY;
        private readonly LargeMap.Maps _initialMap;
        
        
        public ushort Food { get; set; }
        public ushort Gold { get; set; }
        public int Keys { get; set; }
        public int Gems { get; set; }
        public int Torches { get; set; }
        public int SkullKeys { get; set; }
        public bool HasGrapple { get; set; }
        public int TurnsToExtinguish { get; set; }
        public bool IsTorchLit => TurnsToExtinguish > 0;
        
        //public MapUnitPosition InitialPosition

        /// <summary>
        ///     Construct the GameState
        /// </summary>
        /// <param name="u5Directory">Directory of the game State files</param>
        /// <param name="dataOvlRef"></param>
        public GameState(string u5Directory, DataOvlReference dataOvlRef)
        {
            DataOvlReference dataRef = dataOvlRef;

            string saveFileAndPath = Path.Combine(u5Directory, FileConstants.SAVED_GAM);

            _importedGameState = new ImportedGameState(u5Directory, dataOvlRef);
            
            DataChunks = new DataChunks<DataChunkName>(saveFileAndPath, DataChunkName.Unused);

            List<byte> gameStateByteArray = Utils.GetFileAsByteList(saveFileAndPath);

            // one time copy of all imported state information
            CharacterRecords = _importedGameState.CharacterRecords;
            _location = _importedGameState.Location;
            _nInitialFloor = _importedGameState.Floor;
            _initialMap = _importedGameState.InitialMap;
            _nInitialX = _importedGameState.X;
            _nInitialY = _importedGameState.Y;
            
            Food = _importedGameState.Food;
            Gold = _importedGameState.Gold;
            Keys = _importedGameState.Keys;
            Gems = _importedGameState.Gems;
            Torches = _importedGameState.Torches;
            SkullKeys = _importedGameState.SkullKeys;
            HasGrapple = _importedGameState.HasGrapple;
            TurnsToExtinguish = _importedGameState.TorchTurnsLeft;
            
            // moonstones and moongates
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte,
                "0-0xFF Moonstone X Coordinates (valid only if buried)", 0x28A, 0x08, 0x00,
                DataChunkName.MOONSTONE_X_COORDS);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte,
                "0-0xFF Moonstone Y Coordinates (valid only if buried)", 0x292, 0x08, 0x00,
                DataChunkName.MOONSTONE_Y_COORDS);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "0=buried,0xFF=Inventory Moonstone Flags", 0x29A,
                0x08, 0x00, DataChunkName.MOONSTONE_BURIED);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte,
                "0=Britannia,0xFF=Underworld Moonstone Z Coordinates (valid only if buried)", 0x2A2, 0x08, 0x00,
                DataChunkName.MOONSTONE_Z_COORDS);

            // misc
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "Active Character - 0-5,0xFF=None", 0x2D5, 0x01,
                0x00, DataChunkName.ACTIVE_CHARACTER);

            // time and date
            DataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16, "Current Year", 0x2CE, 0x02, 0x00,
                DataChunkName.CURRENT_YEAR);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "Current Month", 0x2D7, 0x01, 0x00,
                DataChunkName.CURRENT_MONTH);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "Current Day", 0x2D8, 0x01, 0x00,
                DataChunkName.CURRENT_DAY);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "Current Hour", 0x2D9, 0x01, 0x00,
                DataChunkName.CURRENT_HOUR);
            // 0x2DA is copy of 2D9 for some reason
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Byte, "Current Minute", 0x2DB, 0x01, 0x00,
                DataChunkName.CURRENT_MINUTE);

            //dataChunks.AddDataChunk()
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Bitmap, "NPC Killed Bitmap", 0x5B4, 0x80, 0x00,
                DataChunkName.NPC_ISALIVE_TABLE);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.Bitmap, "NPC Met Bitmap", 0x634, 0x80, 0x00,
                DataChunkName.NPC_ISMET_TABLE);
            DataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Number of Party Members", 0x2B5, 0x1, 0x00,
                DataChunkName.N_PEOPLE_PARTY);

            DataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "NPC Type InitialMap", 0x5B4, 0x20, 0x00,
                DataChunkName.NPC_TYPES);
            //List<byte> chunks = DataChunks.GetDataChunk(DataChunkName.NPC_TYPES).GetAsByteList();

            // get the NPCs movement list - 0x20 NPCs, with 0x10 movement commands each, consisting of 0x1 direction byte + 0x1 repetitions
            DataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "NPC Movement List", 0xBB8,
                0x20 * 0x10 * sizeof(byte) * 2, 0x00, DataChunkName.NPC_MOVEMENT_LISTS);
            // bajh: Jan 12 2020, moved from BB8 to BBA to test a theory that it actually begins a few bytes after the original documentation indicates
            //dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "NPC Movement List", 0xBBA, 0x20 * 0x10 * (sizeof(byte) * 2), 0x00, DataChunkName.NPC_MOVEMENT_LISTS);
            // get the offsets to the current movement instructions of the NPCs
            DataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List, "NPC Movement Offset Lists", 0xFB8,
                0x20 * sizeof(byte) * 2, 0x00, DataChunkName.NPC_MOVEMENT_OFFSETS);

            // we will need to add 0x100 for now, but cannot because it's read in as a bytelist
            DataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "NPC Sprite (by smallmap)", 0xFF8, 0x20, 0x00,
                DataChunkName.NPC_SPRITE_INDEXES);

            // Initialize the table to determine if an NPC is dead
            List<bool> npcAlive = DataChunks.GetDataChunk(DataChunkName.NPC_ISALIVE_TABLE).GetAsBitmapBoolList();
            _npcIsDeadArray = Utils.ListTo2DArray(npcAlive, NonPlayerCharacterReferences.NPCS_PER_TOWN, 0x00,
                NonPlayerCharacterReferences.NPCS_PER_TOWN *
                SmallMapReferences.SingleMapReference.TOTAL_SMALL_MAP_LOCATIONS);

            // Initialize a table to determine if an NPC has been met
            List<bool> npcMet = DataChunks.GetDataChunk(DataChunkName.NPC_ISMET_TABLE).GetAsBitmapBoolList();
            // these will map directly to the towns and the NPC dialog #
            _npcIsMetArray = Utils.ListTo2DArray(npcMet, NonPlayerCharacterReferences.NPCS_PER_TOWN, 0x00,
                NonPlayerCharacterReferences.NPCS_PER_TOWN *
                SmallMapReferences.SingleMapReference.TOTAL_SMALL_MAP_LOCATIONS);

            // this stores monsters, party, objects and NPC location info and other stuff too (apparently!?)
            DataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Character Animation States - Current Environment", 0x6B4, 0x100, 0x00,
                DataChunkName.CHARACTER_ANIMATION_STATES);

            // this stores monsters, party, objects and NPC location info and other stuff too (apparently!?)
            DataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List, "Character States - including xyz", 0x9B8,
                0x200, 0x00, DataChunkName.CHARACTER_STATES);

            // load the overworld and underworld overlays
            // they are stored in the saved.ool file - but also the brit.ool and under.ool file - not quite sure why it's stored in both...
            // string overworldOverlayPath = Path.Combine(u5Directory, FileConstants.BRIT_OOL);
            // string underworldOverlayPath = Path.Combine(u5Directory, FileConstants.UNDER_OOL);
            string savedOolFile = Path.Combine(u5Directory, FileConstants.SAVED_OOL);
            string overworldOverlayPath = savedOolFile;
            string underworldOverlayPath = savedOolFile;

            _overworldOverlayDataChunks =
                new DataChunks<OverlayChunkName>(overworldOverlayPath, OverlayChunkName.Unused);
            _underworldOverlayDataChunks =
                new DataChunks<OverlayChunkName>(underworldOverlayPath, OverlayChunkName.Unused);

            // _overworldOverlayDataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Character Animation States - including xyz", 0x00, 0x100, 0x00, OverlayChunkName.CHARACTER_ANIMATION_STATES);
            // _underworldOverlayDataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Character Animation States - including xyz", 0x00, 0x100, 0x00, OverlayChunkName.CHARACTER_ANIMATION_STATES);
            _overworldOverlayDataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Character Animation States - Overworld", 0x00, 0x100, 0x00,
                OverlayChunkName.CHARACTER_ANIMATION_STATES);
            _underworldOverlayDataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Character Animation States - Underworld", 0x100, 0x100, 0x00,
                OverlayChunkName.CHARACTER_ANIMATION_STATES);

            TheMoongates = new Moongates(GetDataChunk(DataChunkName.MOONSTONE_X_COORDS),
                GetDataChunk(DataChunkName.MOONSTONE_Y_COORDS),
                GetDataChunk(DataChunkName.MOONSTONE_BURIED), GetDataChunk(DataChunkName.MOONSTONE_Z_COORDS));

            TheTimeOfDay = new TimeOfDay(DataChunks.GetDataChunk(DataChunkName.CURRENT_YEAR),
                DataChunks.GetDataChunk(DataChunkName.CURRENT_MONTH),
                DataChunks.GetDataChunk(DataChunkName.CURRENT_DAY), DataChunks.GetDataChunk(DataChunkName.CURRENT_HOUR),
                DataChunks.GetDataChunk(DataChunkName.CURRENT_MINUTE));

            // import the players inventory
            PlayerInventory = new Inventory(gameStateByteArray, dataRef, new MoonPhaseReferences(dataRef), TheMoongates,
                this);
        }

        /// <summary>
        ///     All player character records
        /// </summary>
        public PlayerCharacterRecords CharacterRecords { get; }

        internal DataChunk CharacterAnimationStatesDataChunk =>
            DataChunks.GetDataChunk(DataChunkName.CHARACTER_ANIMATION_STATES);

        internal DataChunk CharacterStatesDataChunk => DataChunks.GetDataChunk(DataChunkName.CHARACTER_STATES);
        internal DataChunk NonPlayerCharacterMovementLists => DataChunks.GetDataChunk(DataChunkName.NPC_MOVEMENT_LISTS);

        internal DataChunk NonPlayerCharacterMovementOffsets =>
            DataChunks.GetDataChunk(DataChunkName.NPC_MOVEMENT_OFFSETS);

        internal DataChunk NonPlayerCharacterKeySprites => DataChunks.GetDataChunk(DataChunkName.NPC_SPRITE_INDEXES);

        internal DataChunk OverworldOverlayDataChunks =>
            _overworldOverlayDataChunks.GetDataChunk(OverlayChunkName.CHARACTER_ANIMATION_STATES);

        internal DataChunk UnderworldOverlayDataChunks =>
            _underworldOverlayDataChunks.GetDataChunk(OverlayChunkName.CHARACTER_ANIMATION_STATES);

        /// <summary>
        ///     The virtual map which includes the static map plus all things overlaid on it including NPCs
        /// </summary>
        public VirtualMap TheVirtualMap { get; private set; }

        /// <summary>
        ///     The current time of day
        /// </summary>
        public TimeOfDay TheTimeOfDay { get; }

        public Moongates TheMoongates { get; }

        /// <summary>
        ///     Game State raw data
        /// </summary>
        private DataChunks<DataChunkName> DataChunks { get; }

        public byte ActivePlayerNumber
        {
            get => DataChunks.GetDataChunk(DataChunkName.ACTIVE_CHARACTER).GetChunkAsByte();
            set => DataChunks.GetDataChunk(DataChunkName.ACTIVE_CHARACTER).SetChunkAsByte(value);
        }

        /// <summary>
        ///     Players current inventory
        /// </summary>
        public Inventory PlayerInventory { get; }


        /// <summary>
        ///     Users Karma
        /// </summary>
        public uint Karma { get; set; }

        /// <summary>
        ///     The name of the Avatar
        /// </summary>
        public string AvatarsName => CharacterRecords.Records[PlayerCharacterRecords.AVATAR_RECORD].Name;

        public DataChunk GetDataChunk(DataChunkName dataChunkName)
        {
            return DataChunks.GetDataChunk(dataChunkName);
        }

        /// <summary>
        ///     Take fall damage from klimbing mountains
        /// </summary>
        public void GrapplingFall()
        {
            // called when falling from a Klimb on a mountain
        }

        /// <summary>
        ///     Using the random number generator, provides 1 in howMany odds of returning true
        /// </summary>
        /// <param name="howMany">1 in howMany odds of returning true</param>
        /// <returns>true if odds are beat</returns>
        public bool OneInXOdds(int howMany)
        {
            // if ran%howMany is zero then we beat the odds
            int nextRan = _ran.Next();
            return nextRan % howMany == 0;
        }

        /// <summary>
        ///     Is NPC alive?
        /// </summary>
        /// <param name="npc">NPC object</param>
        /// <returns>true if NPC is alive</returns>
        public bool NpcIsAlive(NonPlayerCharacterReference npc)
        {
            // the array isDead because LB stores 0=alive, 1=dead
            // I think it's easier to evaluate if they are alive
            return _npcIsDeadArray[npc.MapLocationId][npc.DialogIndex] == false;
        }

        /// <summary>
        ///     Sets the flag to indicate the NPC is met
        /// </summary>
        /// <param name="npc"></param>
        public void SetMetNpc(NonPlayerCharacterReference npc)
        {
            _npcIsMetArray[npc.MapLocationId][npc.DialogIndex] = true;
        }


        /// <summary>
        ///     Has the NPC met the avatar yet?
        /// </summary>
        /// <param name="npc"></param>
        /// <returns></returns>
        public bool NpcHasMetAvatar(NonPlayerCharacterReference npc)
        {
            return _npcIsMetArray[npc.MapLocationId][npc.DialogIndex];
        }

        /// <summary>
        ///     DEBUG FUNCTION
        /// </summary>
        /// <param name="npc"></param>
        /// <param name="bHasMet"></param>
        public void SetNpcHasMetAvatar(NonPlayerCharacterReference npc, bool bHasMet)
        {
            _npcIsMetArray[npc.MapLocationId][npc.DialogIndex] = bHasMet;
        }

        /// <summary>
        ///     Initializes (one time) the virtual map component
        ///     Must be initialized pretty much after everything else has been loaded into memory
        /// </summary>
        /// <param name="smallMapReferences"></param>
        /// <param name="smallMaps"></param>
        /// <param name="overworldMap"></param>
        /// <param name="underworldMap"></param>
        /// <param name="tileReferences"></param>
        /// <param name="npcRefs"></param>
        /// <param name="inventoryReferences"></param>
        /// <param name="dataOvlReference"></param>
        internal void InitializeVirtualMap(SmallMapReferences smallMapReferences, SmallMaps smallMaps,
            LargeMap overworldMap, LargeMap underworldMap, TileReferences tileReferences, 
            NonPlayerCharacterReferences npcRefs, InventoryReferences inventoryReferences,
            DataOvlReference dataOvlReference)
        {
            SmallMapReferences.SingleMapReference mapRef =
                _location == SmallMapReferences.SingleMapReference.Location.Britannia_Underworld
                    ? null : smallMapReferences.GetSingleMapByLocation(_location, _nInitialFloor);

            TheVirtualMap = new VirtualMap(smallMapReferences, smallMaps, overworldMap,
                underworldMap, tileReferences, this, npcRefs, TheTimeOfDay, TheMoongates, 
                inventoryReferences, CharacterRecords, _initialMap, mapRef, dataOvlReference);
            // we have to set the initial xy, not the floor because that is part of the SingleMapReference
            // I should probably just add yet another thing to the constructor
            TheVirtualMap.CurrentPosition.XY = new Point2D(_nInitialX, _nInitialY);
        }

        private enum OverlayChunkName { Unused, CHARACTER_ANIMATION_STATES }
    }
}