﻿using System;
using System.Diagnostics.CodeAnalysis;
using Ultima5Redux.Maps;

namespace Ultima5Redux
{
    [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")] 
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")]
    public class Point2DFloat
    {
        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public Point2DFloat(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public bool WithinN(Point2DFloat xy, float nWithin)
        {
            bool bWithinX = Math.Abs(xy.X - X) <= nWithin;
            bool bWithinY = Math.Abs(xy.Y - Y) <= nWithin;
            return bWithinX && bWithinY;
        }

        public double DistanceBetween(Point2DFloat xy)
        {
            return Math.Sqrt(Math.Pow(X - xy.X, 2) + Math.Pow(Y - xy.Y, 2));
        }

        public Point2DFloat Copy()
        {
            return new Point2DFloat(X, Y);
        }

        public override bool Equals(object obj) => obj is Point2DFloat point2DFloat && Equals(point2DFloat);

        public bool Equals(Point2DFloat other)
        {
            if (other == null) return false;
            if (Math.Abs(X - other.X) > 0.0000001f)
                return false;

            return Math.Abs(Y - other.Y) < 0.0000001f;
        }

        public override string ToString()
        {
            return "X=" + X + ",Y=" + Y;
        }

        public static bool operator ==(Point2DFloat point1, Point2DFloat point2)
        {
            if (ReferenceEquals(point1, null)) return ReferenceEquals(point2, null);

            return point1.Equals(point2);
        }

        public static bool operator !=(Point2DFloat point1, Point2DFloat point2)
        {
            return !(point1 == point2);
        }
    }

    public class Point2D
    {
        /// <summary>
        ///     4 way direction
        /// </summary>
        public enum Direction { Up, Down, Left, Right, None }

        
        public Point2D(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public Point2D GetPoint2DOrNullOutOfRange(int nMaxX, int nMaxY, int nMinX = 0, int nMinY = 0)
        {
            return IsOutOfRange(nMaxX, nMaxY, nMinX, nMinY) ? null : this;
        }
        
        public bool IsOutOfRange(int nMaxX, int nMaxY, int nMinX = 0, int nMinY = 0)
        {
            return (X < nMinX || X > nMaxX || Y < nMinY || Y > nMaxY);
        }
        
        public Point2D GetAdjustedPosition(Direction direction, int nMaxX, int nMaxY, int nMinX = 0, int nMinY = 0)
        {
            Point2D adjustedPos = GetAdjustedPosition(direction);
            return adjustedPos.IsOutOfRange(nMaxX, nMaxY, nMinX, nMinY) ? null : adjustedPos;
        }

        public Point2D GetAdjustedPosition(Direction direction, int nSpaces = 1)
        {
            Point2D adjustedPos = Copy();

            switch (direction)
            {
                case Direction.None:
                    // no movement
                    break;
                case Direction.Right:
                    adjustedPos.X += nSpaces;
                    break;
                case Direction.Up:
                    adjustedPos.Y -= nSpaces;
                    break;
                case Direction.Left:
                    adjustedPos.X -= nSpaces;
                    break;
                case Direction.Down:
                    adjustedPos.Y += nSpaces;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }

            return adjustedPos;
        }

        public void AdjustXAndYToMax(int nMax)
        {
            X %= nMax;
            if (X < 0) X += nMax;
            Y %= nMax;
            if (Y < 0) Y += nMax;
        }
        
        public bool IsWithinN(Point2D xy, int nWithin)
        {
            bool bWithinX = Math.Abs(xy.X - X) <= nWithin;
            bool bWithinY = Math.Abs(xy.Y - Y) <= nWithin;
            return bWithinX && bWithinY;
        }

        // is the point given point in one of the four directions given?
        public bool IsWithinNFourDirections(Point2D xy)
        {
            return Math.Abs(DistanceBetween(xy) - 1) < 0.01;
        }

        public double DistanceBetween(Point2D xy)
        {
            return Math.Sqrt(Math.Pow(X - xy.X, 2) + Math.Pow(Y - xy.Y, 2));
        }

        public Point2D Copy()
        {
            return new Point2D(X, Y);
        }


        public override bool Equals(object obj)
        {
            return obj is Point2D point2D && Equals(point2D);
        }

        // ReSharper disable once MemberCanBePrivate.Global
        public bool Equals(Point2D other)
        {
            if (other == null) return false;
            if (X != other.X)
                return false;

            return Y == other.Y;
        }

        [SuppressMessage("ReSharper", "NonReadonlyMemberInGetHashCode")] 
        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return "X=" + X + ",Y=" + Y;
        }

        public static bool operator ==(Point2D point1, Point2D point2)
        {
            return point1?.Equals(point2) ?? ReferenceEquals(point2, null);
        }

        public static bool operator !=(Point2D point1, Point2D point2)
        {
            return !(point1 == point2);
        }

    }
}