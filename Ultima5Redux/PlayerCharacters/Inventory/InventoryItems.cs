﻿using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Ultima5Redux.Data;

namespace Ultima5Redux.PlayerCharacters.Inventory
{
    public abstract class InventoryItems<TEnumType, T>
    {
        //[IgnoreDataMember] protected readonly DataOvlReference DataOvlRef;
        [IgnoreDataMember] protected readonly List<byte> GameStateByteArray;

        protected InventoryItems(List<byte> gameStateByteArray)
        {
            //DataOvlRef = dataOvlRef;
            GameStateByteArray = gameStateByteArray;
        }

        [DataMember] public abstract Dictionary<TEnumType, T> Items { get; }

        [IgnoreDataMember] public virtual IEnumerable<InventoryItem> GenericItemList => Items.Values.Cast<InventoryItem>().ToList();
    }
}