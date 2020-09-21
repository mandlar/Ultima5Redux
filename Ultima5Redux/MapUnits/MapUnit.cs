﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Ultima5Redux.DayNightMoon;
using Ultima5Redux.External;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.PlayerCharacters;

namespace Ultima5Redux.MapUnits
{
    public abstract class MapUnit
    {
        protected TileReferences TileReferences { get; set; }

        /// <summary>
        /// All the movements for the map character
        /// </summary>
        internal MapUnitMovement Movement { get; private protected set; }
        
        /// <summary>
        /// the state of the animations
        /// </summary>
        public MapUnitState TheMapUnitState { get; protected set; }

        /// <summary>
        /// Get's the appropriate sprite index when the Avatar occupies the same tile and comes from the given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public abstract TileReference GetTileReferenceWithAvatarOnTile(VirtualMap.Direction direction);
        
        
        /// <summary>
        /// The location state of the character
        /// </summary>
        internal SmallMapCharacterState TheSmallMapCharacterState { get; }

        private readonly MapUnitPosition _mapMapUnitPosition = new MapUnitPosition();
        
        /// <summary>
        /// Gets the TileReference of the keyframe of the particular MapUnit (typically the first frame)
        /// </summary>
        public TileReference KeyTileReference
        {
            get
            {
                if (NPCRef != null) return TileReferences.GetTileReference(NPCRef.NPCKeySprite);

                return TileReferences.GetTileReferenceOfKeyIndex(TheMapUnitState.Tile1Ref.Index);
            }
            set
            {
                TheMapUnitState.Tile1Ref = value;
                TheMapUnitState.Tile2Ref = value;
            }
        }

        /// <summary>
        /// The characters current position on the map
        /// </summary>
        internal MapUnitPosition MapUnitPosition
        {
            get => _mapMapUnitPosition;
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                // this is a bit redundant but we have a backing field and also store the XY positions
                // in the TheMapUnitState and TheSmallMapCharacterState, but we have to do this because the .XY
                // of the MapUnitPosition is often edited directly
                _mapMapUnitPosition.X = value.X;
                _mapMapUnitPosition.Y = value.Y;
                _mapMapUnitPosition.Floor = value.Floor;
                
                TheMapUnitState.X = (byte) value.X;
                TheMapUnitState.Y = (byte) value.Y;
                TheMapUnitState.Floor = (byte) value.Floor;

                if (TheSmallMapCharacterState == null) return;
                TheSmallMapCharacterState.TheMapUnitPosition.X = value.X;
                TheSmallMapCharacterState.TheMapUnitPosition.Y = value.Y;
                TheSmallMapCharacterState.TheMapUnitPosition.Floor = value.Floor;
            }
        }

        /// <summary>
        /// How many iterations will I force the character to wander?
        /// </summary>
        internal int ForcedWandering { get; set; }
        
        public int MovementAttempts = 0;
        /// <summary>
        /// Reference to current NPC (if it's an NPC at all!)
        /// </summary>
        public NonPlayerCharacterReference NPCRef { get; }

        public SmallMapReferences.SingleMapReference.Location MapLocation { get; set; }

        /// <summary>
        /// Is the character currently active on the map?
        /// </summary>
        public bool IsInParty
        {
            get; set; 
        }

        /// <summary>
        /// Is the map character currently an active character on the current map
        /// </summary>
        public abstract bool IsActive { get; }
        public virtual void CalculateNextPath(VirtualMap virtualMap, TimeOfDay timeOfDay, int nMapCurrentFloor)
        {
            // by default the thing doesn't move on it's own
        }

        public virtual void CompleteNextMove(VirtualMap virtualMap, TimeOfDay timeOfDay)
        {
            // by default the thing doesn't move on it's own
        }

        public string GetDebugDescription(TimeOfDay timeOfDay)
        {
            if (NPCRef != null)
            {
                return ("Name=" + NPCRef.FriendlyName
                       + " " + MapUnitPosition + " Scheduled to be at: " +
                       NPCRef.Schedule.GetCharacterDefaultPositionByTime(timeOfDay) +
                       " with AI Mode: " +
                       NPCRef.Schedule.GetCharacterAiTypeByTime(timeOfDay) +
                       " <b>Movement Attempts</b>: " + MovementAttempts + " " +
                       this.Movement);
            }
            
            return ("MapUnit "+ KeyTileReference.Description  
                    + " " + MapUnitPosition + " Scheduled to be at: " 
                    + " <b>Movement Attempts</b>: " + MovementAttempts + " "
                    + this.Movement);
        }

        /// <summary>
        /// empty constructor if there is nothing in the map character slot
        /// </summary>
        protected MapUnit()
        {
            NPCRef = null;
            TheMapUnitState = null;
            TheSmallMapCharacterState = null;
            Movement = null;
        }

        /// <summary>
        /// Builds a MpaCharacter from pre-instantiated objects - typically loaded from disk in advance
        /// </summary>
        /// <param name="npcRef"></param>
        /// <param name="mapUnitState"></param>
        /// <param name="smallMapTheSmallMapCharacterState"></param>
        /// <param name="mapUnitMovement"></param>
        /// <param name="timeOfDay"></param>
        /// <param name="playerCharacterRecords"></param>
        /// <param name="tileReferences"></param>
        /// <param name="location"></param>
        protected MapUnit(NonPlayerCharacterReference npcRef, MapUnitState mapUnitState, SmallMapCharacterState smallMapTheSmallMapCharacterState,
            MapUnitMovement mapUnitMovement, TimeOfDay timeOfDay, PlayerCharacterRecords playerCharacterRecords,
            TileReferences tileReferences, SmallMapReferences.SingleMapReference.Location location)
        {
            TileReferences = tileReferences;
            MapLocation = location;
            NPCRef = npcRef;
            TheMapUnitState = mapUnitState;
            TheSmallMapCharacterState = smallMapTheSmallMapCharacterState;
            Movement = mapUnitMovement;

            PlayerCharacterRecord record = null;
            
            // Debug.Assert(playerCharacterRecords != null);
            Debug.Assert(TheMapUnitState != null);
            Debug.Assert(Movement != null);
            
            // gets the player character record for an NPC if one exists
            // this is commonly used when meeting NPCs who have not yet joined your party 
            if (npcRef != null)
            {
                record = playerCharacterRecords.GetCharacterRecordByNPC(npcRef);
            }

            // is the NPC you are loading currently in the party?
            IsInParty = record != null && record.PartyStatus == PlayerCharacterRecord.CharacterPartyStatus.InParty;

            // set the characters position 
            MapUnitPosition = new MapUnitPosition(TheMapUnitState.X, TheMapUnitState.Y, TheMapUnitState.Floor);
        }

        /// <summary>
        /// move the character to a new position
        /// </summary>
        /// <param name="xy"></param>
        /// <param name="nFloor"></param>
        protected void Move(Point2D xy, int nFloor)
        {
            MapUnitPosition.XY = xy;
            MapUnitPosition.Floor = nFloor;
        }

        /// <summary>
        /// Move the character to a new position
        /// </summary>
        /// <param name="mapUnitPosition"></param>
        protected void Move(MapUnitPosition mapUnitPosition)
        {
            MapUnitPosition = mapUnitPosition;
        }

        /// <summary>
        /// Builds the actual path for the character to travel based on their current position and their target position
        /// </summary>
        /// <param name="currentMap"></param>
        /// <param name="mapUnit">where the character is presently</param>
        /// <param name="targetXy">where you want them to go</param>
        /// <returns>returns true if a path was found, false if it wasn't</returns>
        protected bool BuildPath(Map currentMap, MapUnit mapUnit, Point2D targetXy)
        {
            MapUnitMovement.MovementCommandDirection getCommandDirection(Point2D fromXy, Point2D toXy)
            {
                if (fromXy == toXy) return MapUnitMovement.MovementCommandDirection.None;
                if (fromXy.X < toXy.X) return MapUnitMovement.MovementCommandDirection.East;
                if (fromXy.Y < toXy.Y) return MapUnitMovement.MovementCommandDirection.South;
                if (fromXy.X > toXy.X) return MapUnitMovement.MovementCommandDirection.West;
                if (fromXy.Y > toXy.Y) return MapUnitMovement.MovementCommandDirection.North;
                throw new Ultima5ReduxException("For some reason we couldn't determine the path of the command direction in getCommandDirection");
            }

            Point2D vector2ToPoint2D(Vector2 vector)
            {
                return new Point2D((int)vector.X, (int)vector.Y);
            }

            if (mapUnit.MapUnitPosition.XY == targetXy)
            {
                throw new Ultima5ReduxException("Asked to build a path, but " + mapUnit.NPCRef.Name + " is already at " + targetXy.X + "," + targetXy.Y); //+ "," + targetXy.Floor);
            }

            // todo: need some code that checks for different floors and directs them to closest ladder or staircase instead of same floor position

            Stack<Node> nodeStack = currentMap.AStar.FindPath(new System.Numerics.Vector2(mapUnit.MapUnitPosition.XY.X, mapUnit.MapUnitPosition.XY.Y),
                new System.Numerics.Vector2(targetXy.X, targetXy.Y));

            MapUnitMovement.MovementCommandDirection prevDirection = MapUnitMovement.MovementCommandDirection.None;
            MapUnitMovement.MovementCommandDirection newDirection = MapUnitMovement.MovementCommandDirection.None;
            Point2D prevPosition = mapUnit.MapUnitPosition.XY;

            // temporary while I figure out why this happens
            if (nodeStack == null) return false;

            int nInARow = 0;
            // builds the movement list that is compatible with the original U5 movement instruction queue stored in the state file
            foreach (Node node in nodeStack)
            {
                Point2D newPosition = vector2ToPoint2D(node.Position);
                newDirection = getCommandDirection(prevPosition, newPosition);

                // if the previous direction is the same as the current direction, then we keep track so that we can issue a single instruction
                // that has N iterations (ie. move East 5 times)
                if (prevDirection == newDirection || prevDirection == MapUnitMovement.MovementCommandDirection.None)
                {
                    nInARow++;
                }
                else
                {
                    // if the direction has changed then we add the previous direction and reset the concurrent counter
                    mapUnit.Movement.AddNewMovementInstruction(new MapUnitMovement.MovementCommand(prevDirection, nInARow));
                    nInARow = 1;
                }
                prevDirection = newDirection;
                prevPosition = newPosition;
            }
            if (nInARow > 0) { mapUnit.Movement.AddNewMovementInstruction(new MapUnitMovement.MovementCommand(newDirection, nInARow)); }
            return true;
        }

    }
}
