﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Policy;
using Ultima5Redux.Data;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits.CombatMapUnits;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.PlayerCharacters;
using Ultima5Redux.PlayerCharacters.CombatItems;
using Ultima5Redux.PlayerCharacters.Inventory;

namespace Ultima5Redux.MapUnits
{
    public class CombatPlayer : CombatMapUnit
    {
        public PlayerCharacterRecord Record { get; }

        public override string Name => Record.Name;

        public override CharacterStats Stats => Record.Stats;

        public CombatPlayer(PlayerCharacterRecord record, TileReferences tileReferences, Point2D xy, DataOvlReference dataOvlReference)
        {
            DataOvlRef = dataOvlReference;
            Record = record;
            TileReferences = tileReferences;
            TheMapUnitState = MapUnitState.CreateCombatPlayer(TileReferences, Record, 
                new MapUnitPosition(xy.X, xy.Y, 0));

            // set the characters position 
            MapUnitPosition = new MapUnitPosition(TheMapUnitState.X, TheMapUnitState.Y, TheMapUnitState.Floor);
        }

        public CombatPlayer()
        {
            
        }

        protected override Dictionary<Point2D.Direction, string> DirectionToTileName { get; }
        protected override Dictionary<Point2D.Direction, string> DirectionToTileNameBoarded { get; }
        public override Avatar.AvatarState BoardedAvatarState => Avatar.AvatarState.Hidden;
        public override string BoardXitName => "GET OFF ME YOU BRUTE!";
        public override bool IsActive => true;

        public override string ToString()
        {
            return Record.Name;
        }

        /// <summary>
        /// Gets the string used to describe all available weapons that will be outputted to user
        /// </summary>
        /// <returns></returns>
        public string GetAttackWeaponsString(Inventory inventory)
        {
            List<CombatItem> combatItems = GetAttackWeapons(inventory);

            if (combatItems == null) return "bare hands";

            string combatItemString  = "";
            for (int index = 0; index < combatItems.Count; index++)
            {
                CombatItem item = combatItems[index];
                if (index > 0)
                    combatItemString += ", ";
                combatItemString += item.LongName;
            }

            return combatItemString;
        }

        /// <summary>
        /// Gets a list of all weapons that are available for use by given player character. The list is ordered. 
        /// </summary>
        /// <returns>List of attack weapons OR null if none are available</returns>
        public List<CombatItem> GetAttackWeapons(Inventory inventory)
        {
            List<CombatItem> weapons = new List<CombatItem>();

            bool bBareHands = false;

            bool isAttackingCombatItem(DataOvlReference.Equipment equipment)
            {
                return equipment != DataOvlReference.Equipment.Nothing &&
                       inventory.GetItemFromEquipment(equipment) is CombatItem combatItem && combatItem.AttackStat > 0;
            }
            
            if (isAttackingCombatItem(Record.Equipped.Helmet))
                weapons.Add(inventory.GetItemFromEquipment(Record.Equipped.Helmet));

            if (isAttackingCombatItem(Record.Equipped.LeftHand))
                weapons.Add(inventory.GetItemFromEquipment(Record.Equipped.LeftHand));
            else
                bBareHands = true;

            if (isAttackingCombatItem(Record.Equipped.RightHand))
                weapons.Add(inventory.GetItemFromEquipment(Record.Equipped.RightHand));
            else
                bBareHands = true;

            if (weapons.Count != 0) return weapons;
            
            Debug.Assert(bBareHands);
            return null;
        }

    }
}