﻿using System;
using System.Collections.Generic;
using Ultima5Redux.Data;
using Ultima5Redux.DayNightMoon;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.MapUnits.SeaFaringVessels;
using Ultima5Redux.PlayerCharacters;

namespace Ultima5Redux.MapUnits
{
    public class Avatar : MapUnit
    {
        public Avatar(TileReferences tileReferences, MapUnitPosition avatarPosition, 
            SmallMapReferences.SingleMapReference.Location location, MapUnitMovement movement,
            MapUnitState mapUnitState = null) : base()
        {
            if (mapUnitState == null)
            {
                TheMapUnitState = MapUnitState.CreateAvatar(tileReferences, SmallMapReferences.GetStartingXYZByLocation(location));
            }
            else
            {
                TheMapUnitState = MapUnitState.CreateAvatar(tileReferences, SmallMapReferences.GetStartingXYZByLocation(location),
                    mapUnitState);
            }
            TileReferences = tileReferences;

            CurrentDirection = TheMapUnitState.Tile1Ref.GetDirection();
            CurrentAvatarState = CalculateAvatarState(TheMapUnitState.Tile1Ref);
            MapLocation = location;
            //TheMapUnitState = mapUnitState;
            // set the initial key tile
            // KeyTileReference = TheMapUnitState.Tile1Ref;
            // _ = KeyTileReference;
            Movement = movement;
        }

        public override TileReference KeyTileReference 
        {
            get =>
                IsAvatarOnBoardedThing
                    ? TileReferences.GetTileReferenceByName(DirectionToTileNameBoarded[CurrentDirection])
                    : TileReferences.GetTileReferenceByName(DirectionToTileName[CurrentDirection]);
            set => base.KeyTileReference = value;
        }

        private AvatarState CalculateAvatarState(TileReference tileReference)
        {
            if (tileReference.Name == "BasicAvatar") return AvatarState.Regular;
            if (tileReference.Name.StartsWith("Ship")) return AvatarState.Frigate;
            if (tileReference.Name.StartsWith("Skiff")) return AvatarState.Skiff;
            if (tileReference.Name.StartsWith("RidingMagicCarpet")) return AvatarState.Carpet;
            if (tileReference.Name.StartsWith("RidingHorse")) return AvatarState.Horse;
            throw new Ultima5ReduxException("Asked to calculate AvatarState of "+tileReference.Name + " but you can't do that, it's not a thing!");
        }

        protected override Dictionary<VirtualMap.Direction, string> DirectionToTileName { get; }
            = new Dictionary<VirtualMap.Direction, string>()
            {
                {VirtualMap.Direction.None, "BasicAvatar"},
                {VirtualMap.Direction.Left, "BasicAvatar"},
                {VirtualMap.Direction.Down, "BasicAvatar"},
                {VirtualMap.Direction.Right, "BasicAvatar"},
                {VirtualMap.Direction.Up, "BasicAvatar"},
            };

        protected override Dictionary<VirtualMap.Direction, string> DirectionToTileNameBoarded => DirectionToTileName;
        public override AvatarState BoardedAvatarState => AvatarState.Regular;

        public override string BoardXitName => "You can't board the Avatar you silly goose!";

        public override bool IsActive => true;

        public enum AvatarState { Regular, Carpet, Horse, Frigate, Skiff, Hidden }

        internal AvatarState CurrentAvatarState { get; private set; } = AvatarState.Regular;
        public VirtualMap.Direction PreviousDirection { get; private set; } = VirtualMap.Direction.None;
        public VirtualMap.Direction CurrentDirection { get; private set; } = VirtualMap.Direction.None;
        public bool AreSailsUp { get; set; } = false;
        /// <summary>
        /// The current MapUnit (if any) that the Avatar is occupying. It is expected that it is NOT in the active
        /// the current MapUnits object 
        /// </summary>
        public MapUnit CurrentBoardedMapUnit { get; private set; }
        /// <summary>
        /// Is the Avatar currently boarded onto a thing 
        /// </summary>
        public bool IsAvatarOnBoardedThing => CurrentAvatarState != AvatarState.Regular && CurrentAvatarState != AvatarState.Hidden;

        /// <summary>
        /// Describes if there are only left right sprites
        /// </summary>
        private readonly Dictionary<AvatarState, bool> _onlyLeftRight = new Dictionary<AvatarState, bool>()
        {
            {AvatarState.Carpet, true},
            {AvatarState.Frigate, false},
            {AvatarState.Hidden, false},
            {AvatarState.Horse, true},
            {AvatarState.Skiff, false},
            {AvatarState.Regular, false}
        };

        private TileReference GetCurrentTileReference()
        {
            if (CurrentAvatarState == AvatarState.Regular || CurrentAvatarState == AvatarState.Hidden)
                return NonBoardedTileReference;
            
            return CurrentBoardedMapUnit.BoardedTileReference;
        }
        
        /// <summary>
        /// Attempt to move the Avatar in a given direction
        /// It takes into account if the Avatar has boarded a vehicle (horse, skiff etc)
        /// </summary>
        /// <param name="direction">the direction </param>
        /// <returns>true if Avatar moved, false if they only changed direction</returns>
        public bool Move(VirtualMap.Direction direction)
        {
            bool bChangeTile = true;
            // if there are only left and right sprites then we don't switch directions unless they actually
            // go left or right, otherwise we maintain direction
            if (_onlyLeftRight[CurrentAvatarState])
            {
                if (direction != VirtualMap.Direction.Left && direction != VirtualMap.Direction.Right)
                {
                    bChangeTile = false;
                }
            }

            // we only track changes in tile if we are changing the direction of the sprite, otherwise we don't track
            // it and don't care - this makes sure carpets and horses don't change direction when going up
            // and down
            if (bChangeTile)
            {
                PreviousDirection = CurrentDirection;
                CurrentDirection = direction;
            }

            if (CurrentBoardedMapUnit != null) CurrentBoardedMapUnit.Direction = CurrentDirection;

            // did the Avatar change direction?
            bool bDirectionChanged = PreviousDirection != CurrentDirection;

            // set the new sprite to reflect the new direction
            if (bChangeTile)
            {
                TheMapUnitState.SetTileReference(GetCurrentTileReference());
            }

            // return false if the direction changed AND your on a Frigate
            // because you will just change direction
            return !(bDirectionChanged && CurrentAvatarState == AvatarState.Frigate);
        }

        /// <summary>
        /// Creates an Avatar MapUnit at the default small map position
        /// Note: this should never need to be called from a LargeMap since the values persist on disk
        /// </summary>
        /// <param name="tileReferences"></param>
        /// <param name="location"></param>
        /// <param name="movement"></param>
        /// <param name="mapUnitState"></param>
        /// <returns></returns>
        public static MapUnit CreateAvatar(TileReferences tileReferences, 
            SmallMapReferences.SingleMapReference.Location location, MapUnitMovement movement,
            MapUnitState mapUnitState = null)
        {
            Avatar theAvatar = new Avatar(tileReferences, SmallMapReferences.GetStartingXYZByLocation(location), 
                location, movement, mapUnitState);
            
            return theAvatar;
        }

        /// <summary>
        /// Show the Avatar that isn't boarded on top of anything
        /// </summary>
        public MapUnit UnboardedAvatar()
        {
            KeyTileReference = NonBoardedTileReference;// GetTileReferenceByName("BasicAvatar");
            CurrentAvatarState = AvatarState.Regular;
            MapUnit previouslyBoardedMapUnit = CurrentBoardedMapUnit;
            CurrentBoardedMapUnit.IsOccupiedByAvatar = false;
            CurrentBoardedMapUnit = null;
            return previouslyBoardedMapUnit;
        }

        public void BoardMapUnit(MapUnit mapUnit)
        {
            // note: since the Avatar does not control all MapUnits, we only add it our internal tracker
            // but do not release it from the world - that must be done outside this method
            KeyTileReference = mapUnit.KeyTileReference;
            CurrentAvatarState = mapUnit.BoardedAvatarState;
            CurrentBoardedMapUnit = mapUnit;
            CurrentBoardedMapUnit.IsOccupiedByAvatar = true;
            if (mapUnit is Frigate)
            {
                // if we are going onto a frigate, then we want to make sure the Avatar can start rowing
                // in the direction that it's already facing
                PreviousDirection = mapUnit.Direction;
                CurrentDirection = mapUnit.Direction;
            }
        }
        
        // public void SetBoardedCarpet(VirtualMap.Direction direction)
        // {
        //     KeyTileReference = TileReferences.GetTileReferenceByName(_tileIndexMap[AvatarState.Carpet][direction]); 
        //     CurrentAvatarState = AvatarState.Carpet;
        // }
        //
        // public void SetBoardedHorse(VirtualMap.Direction direction)
        // {
        //     KeyTileReference = TileReferences.GetTileReferenceByName(_tileIndexMap[AvatarState.Horse][direction]); 
        //     CurrentAvatarState = AvatarState.Horse;
        // }
        //
        // public void SetBoardedFrigate(VirtualMap.Direction direction, bool bSailsUp = false)
        // {
        //     KeyTileReference = TileReferences.GetTileReferenceByName(_tileIndexMap[AvatarState.Frigate][direction]);
        //     CurrentAvatarState = AvatarState.Frigate;
        // }
        //
        // public void SetBoardedSkiff(VirtualMap.Direction direction)
        // {
        //     KeyTileReference = TileReferences.GetTileReferenceByName(_tileIndexMap[AvatarState.Skiff][direction]);
        //     CurrentAvatarState = AvatarState.Skiff;
        // }
    }
}