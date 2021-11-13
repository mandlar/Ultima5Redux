﻿using System.Collections.Generic;
using Ultima5Redux.Data;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.PlayerCharacters;
using Ultima5Redux.References;

namespace Ultima5Redux.MapUnits
{
    public sealed class Horse : MapUnit
    {
        public Horse(MapUnitMovement mapUnitMovement,
            SmallMapReferences.SingleMapReference.Location location,
            Point2D.Direction direction, NonPlayerCharacterState npcState,
            MapUnitPosition mapUnitPosition)
            : base( null, mapUnitMovement, location, direction, npcState,
                GameReferences.SpriteTileReferences.GetTileReferenceByName("HorseLeft"), 
                mapUnitPosition)
        {
            KeyTileReference = NonBoardedTileReference;
        }

        private static Dictionary<SmallMapReferences.SingleMapReference.Location, int> Prices { get; } =
            new Dictionary<SmallMapReferences.SingleMapReference.Location, int>
            {
                { SmallMapReferences.SingleMapReference.Location.Trinsic, 200 },
                { SmallMapReferences.SingleMapReference.Location.Paws, 320 },
                { SmallMapReferences.SingleMapReference.Location.Buccaneers_Den, 260 }
            };


        public override Avatar.AvatarState BoardedAvatarState => Avatar.AvatarState.Horse;

        public override bool IsActive => true;

        public override bool IsAttackable => false;

        public override string BoardXitName => GameReferences.DataOvlRef.StringReferences
            .GetString(DataOvlReference.SleepTransportStrings.HORSE_N).Trim();

        public override string FriendlyName => BoardXitName;

        protected override Dictionary<Point2D.Direction, string> DirectionToTileName { get; } =
            new Dictionary<Point2D.Direction, string>
            {
                { Point2D.Direction.None, "HorseLeft" },
                { Point2D.Direction.Left, "HorseLeft" },
                { Point2D.Direction.Down, "HorseLeft" },
                { Point2D.Direction.Right, "HorseRight" },
                { Point2D.Direction.Up, "HorseRight" }
            };

        protected override Dictionary<Point2D.Direction, string> DirectionToTileNameBoarded { get; } =
            new Dictionary<Point2D.Direction, string>
            {
                { Point2D.Direction.None, "RidingHorseLeft" },
                { Point2D.Direction.Left, "RidingHorseLeft" },
                { Point2D.Direction.Down, "RidingHorseLeft" },
                { Point2D.Direction.Right, "RidingHorseRight" },
                { Point2D.Direction.Up, "RidingHorseRight" }
            };

        protected override Dictionary<Point2D.Direction, string> FourDirectionToTileNameBoarded =>
            new Dictionary<Point2D.Direction, string>
            {
                { Point2D.Direction.None, "RidingHorseLeft" },
                { Point2D.Direction.Left, "RidingHorseLeft" },
                { Point2D.Direction.Down, "RidingHorseDown" },
                { Point2D.Direction.Right, "RidingHorseRight" },
                { Point2D.Direction.Up, "RidingHorseUp" }
            };

        public static int GetPrice(SmallMapReferences.SingleMapReference.Location location,
            PlayerCharacterRecords records)
        {
            return GetAdjustedPrice(records, Prices[location]);
        }

        private static int GetAdjustedPrice(PlayerCharacterRecords records, int nPrice)
        {
            const double IntelligenceFactor = 0.015;
            return (int)(nPrice - nPrice * IntelligenceFactor * records.AvatarRecord.Stats.Intelligence);
        }
    }
}