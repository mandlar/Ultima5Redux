﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Ultima5Redux
{
    [DataContract]  
    public class TileReference
    {
        [DataMember]
        public int Index;
        [DataMember]
        public string Name;
        [DataMember]
        public string Description;
        [DataMember]
        public bool IsWalkingPassable; 
        [DataMember]
        public bool IsBoatPassable;
        [DataMember]
        public bool IsSkiffPassable;
        [DataMember]
        public bool IsCarpetPassable;
        [DataMember]
        public bool IsOpenable;
        [DataMember]
        public bool IsPartOfAnimation;
        [DataMember]
        public int AnimationIndex;
        [DataMember]
        public bool IsUpright;
        [DataMember]
        public int FlatTileSubstitionIndex;
        [DataMember]
        public string FlatTileSubstitionName;
        [DataMember]
        public bool IsEnemy;
        [DataMember]
        public bool IsNPC;
        [DataMember]
        public bool IsBuilding;
        [DataMember]
        public bool DontDraw;
        [DataMember]
        public int SpeedFactor;
        [DataMember]
        public bool IsKlimable;
        [DataMember] 
        public bool IsPushable;


        public override string ToString()
        {
            return this.Name;
        }

        public bool IsNPCCapableSpace => (this.IsWalkingPassable || this.IsOpenable);


        public bool IsSolidSpriteButNotDoor => (!this.IsWalkingPassable && !this.IsOpenable);

        public bool IsSolidSprite => (!IsWalkingPassable);
    }
}
