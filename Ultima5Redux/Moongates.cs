﻿using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Ultima5Redux
{
    public class Moongates
    {
        /// <summary>
        /// Saved X position
        /// </summary>
        private DataChunk XPos { get; }
        /// <summary>
        /// Saved Y position
        /// </summary>
        private DataChunk YPos { get; }
        /// <summary>
        /// Saved flag indicating if it's buried or in players inventory
        /// </summary>
        private DataChunk BuriedFlags { get; }
        /// <summary>
        /// Saved Z (floor) position
        /// </summary>
        private DataChunk ZPos { get; }
        
        /// <summary>
        /// Total number of moonstones in game
        /// </summary>
        private const int TOTAL_MOONSTONES = 8;
        
        /// <summary>
        /// All buried positions
        /// </summary>
        private readonly List<Point3D> _moongatePositions = new List<Point3D>(TOTAL_MOONSTONES);
        /// <summary>
        /// Are moonstones buried?
        /// </summary>
        private readonly List<bool> _moonstonesBuried = new List<bool>(TOTAL_MOONSTONES);
        /// <summary>
        /// Dictionary of all locations and if they are buried 
        /// </summary>
        private readonly Dictionary<Point3D, bool> _moongateBuriedAtPositionDictionary = new Dictionary<Point3D, bool>(TOTAL_MOONSTONES);
        
        /// <summary>
        /// Gets the position of an indexed moongate (in order from the save file)
        /// </summary>
        /// <param name="nMoongateIndex">0-7</param>
        /// <returns></returns>
        public Point3D GetMoongatePosition(int nMoongateIndex)
        {
            return _moongatePositions[nMoongateIndex];
        }

        /// <summary>
        /// Is the moonstone buried, indexed moongate (in order from the save file)
        /// </summary>
        /// <param name="nMoonstoneIndex"></param>
        /// <returns></returns>
        public bool IsMoonstoneBuried(int nMoonstoneIndex)
        {
            Debug.Assert(nMoonstoneIndex >= 0 && nMoonstoneIndex < TOTAL_MOONSTONES);
            return _moonstonesBuried[nMoonstoneIndex];
        }

        /// <summary>
        /// Is there a moonstone buried at a particular position?
        /// </summary>
        /// <param name="position"></param>
        /// <returns>true if one is buried</returns>
        public bool IsMoonstoneBuried(Point3D position)
        {
            if (!_moongateBuriedAtPositionDictionary.ContainsKey(position)) return false;
            return _moongateBuriedAtPositionDictionary[position];
        }
        
        /// <summary>
        /// Constructor. Built with DataChunk references from save file
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        /// <param name="buriedFlags"></param>
        /// <param name="zPos"></param>
        public Moongates(DataChunk xPos, DataChunk yPos, DataChunk buriedFlags, DataChunk zPos)
        {
            // save datachunks for saving later
            this.XPos = XPos;
            this.YPos = YPos;
            this.BuriedFlags = buriedFlags;
            this.ZPos = ZPos;
            List<byte> xPos = XPos.GetAsByteList();
            List<byte> yPos = YPos.GetAsByteList();
            List<byte> zPos = ZPos.GetAsByteList();
            List<byte> buried = buriedFlags.GetAsByteList();
            Debug.Assert(xPos.Count == TOTAL_MOONSTONES && yPos.Count == TOTAL_MOONSTONES && zPos.Count == TOTAL_MOONSTONES && buried.Count == TOTAL_MOONSTONES);
            
            for (int i = 0; i < TOTAL_MOONSTONES; i++)
            {
                Point3D moongatePos = new Point3D(xPos[i], yPos[i], zPos[i]);
                _moongatePositions.Add(moongatePos);
                _moonstonesBuried.Add(buried[i] == 0);
                _moongateBuriedAtPositionDictionary.Add(_moongatePositions[i],_moonstonesBuried[i]);
            }
        }
    }
}