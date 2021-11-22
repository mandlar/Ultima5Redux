﻿using System.Collections.Generic;
using Newtonsoft.Json;
using Ultima5Redux.References;

namespace Ultima5Redux.Maps
{
    public abstract class RegularMap : Map
    {
        protected sealed override Dictionary<Point2D, TileOverrideReference> XYOverrides { get; set; }
        public SmallMapReferences.SingleMapReference CurrentSingleMapReference { get; }

        [JsonConstructor] protected RegularMap()
        {
        }

        protected RegularMap(SmallMapReferences.SingleMapReference singleSmallMapReference)
        {
            CurrentSingleMapReference = singleSmallMapReference;

            // for now combat maps don't have overrides
            XYOverrides = GameReferences.TileOverrideRefs.GetTileXYOverrides(singleSmallMapReference);
        }
    }
}