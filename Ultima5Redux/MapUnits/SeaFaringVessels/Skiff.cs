﻿using System.Collections.Generic;
using Ultima5Redux.Data;
using Ultima5Redux.Maps;
using Ultima5Redux.PlayerCharacters;

namespace Ultima5Redux.MapUnits.SeaFaringVessels
{
    public class Skiff : SeaFaringVessel
    {
        private static Dictionary<SmallMapReferences.SingleMapReference.Location, int> Prices { get; } = new Dictionary<SmallMapReferences.SingleMapReference.Location, int>()
        {
            {SmallMapReferences.SingleMapReference.Location.East_Britanny, 250},
            {SmallMapReferences.SingleMapReference.Location.Minoc, 350},
            {SmallMapReferences.SingleMapReference.Location.Buccaneers_Den, 200},
            {SmallMapReferences.SingleMapReference.Location.Jhelom, 400}
        };
        
        public static int GetPrice(SmallMapReferences.SingleMapReference.Location location,
            PlayerCharacterRecords records)
        {
            return GetAdjustedPrice(records, Prices[location]);
        }

        public Skiff(MapUnitState mapUnitState, MapUnitMovement mapUnitMovement, 
            TileReferences tileReferences, SmallMapReferences.SingleMapReference.Location location,
            DataOvlReference dataOvlReference, VirtualMap.Direction direction) : 
            base(mapUnitState, null, mapUnitMovement, tileReferences, location, 
                dataOvlReference, direction)
        {
            
        }

        protected override Dictionary<VirtualMap.Direction, string> DirectionToTileName { get; } =
            new Dictionary<VirtualMap.Direction, string>()
            {
                {VirtualMap.Direction.None, "SkiffLeft"},
                {VirtualMap.Direction.Left, "SkiffLeft"},
                {VirtualMap.Direction.Down, "SkiffDown"},
                {VirtualMap.Direction.Right, "SkiffRight"},
                {VirtualMap.Direction.Up, "SkiffUp"},
            };

        protected override Dictionary<VirtualMap.Direction, string> DirectionToTileNameBoarded => DirectionToTileName;
        public override Avatar.AvatarState BoardedAvatarState => Avatar.AvatarState.Skiff;


        public override string BoardXitName => DataOvlRef.StringReferences.GetString(DataOvlReference.SleepTransportStrings.SKIFF_N).Trim();
    }
}