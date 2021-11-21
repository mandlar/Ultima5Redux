﻿using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ultima5Redux.Maps;
using Ultima5Redux.References;

namespace Ultima5Redux.PlayerCharacters.Inventory
{
    /// <summary>
    ///     Instance represents a single reagent type
    /// </summary>
    public class Reagent : InventoryItem
    {
        //0x2AA 1 0-99 Sulfur Ash
        //0x2AB 1 0-99 Ginseng
        //0x2AC 1 0-99 Garlic
        //0x2AD 1 0-99 Spider Silk
        //0x2AE 1 0-99 Blood Moss
        //0x2AF 1 0-99 Black Pearl
        //0x2B0 1 0-99 Nightshade
        //0x2B1 1 0-99 Mandrake Root
        [JsonConverter(typeof(StringEnumConverter))] public enum ReagentTypeEnum
        {
            SulfurAsh = 0x2AA, Ginseng = 0x2AB, Garlic = 0x2AC, SpiderSilk = 0x2AD, BloodMoss = 0x2AE,
            BlackPearl = 0x2AF, NightShade = 0x2B0, MandrakeRoot = 0x2B1
        }

        private const int REAGENT_SPRITE = 259;

        [IgnoreDataMember] private readonly GameState _state;
        [IgnoreDataMember] public override int BasePrice => 0;

        [IgnoreDataMember] public override bool HideQuantity => false;

        [IgnoreDataMember] public override string InventoryReferenceString => ReagentType.ToString();
        [IgnoreDataMember] public override bool IsSellable => false;

        [DataMember] public override int Quantity
        {
            get => base.Quantity;
            set => base.Quantity = value > 99 ? 99 : value;
        }

        /// <summary>
        ///     Standard index/order of reagents in data files
        /// </summary>
        [IgnoreDataMember] public int ReagentIndex => (int)ReagentType - (int)ReagentTypeEnum.SulfurAsh;

        [DataMember] public ReagentTypeEnum ReagentType { get; }

        [JsonConstructor] private Reagent()
        {
        }
        
        /// <summary>
        ///     Create a reagent
        /// </summary>
        /// <param name="reagentType">The type of reagent</param>
        /// <param name="quantity">how many the party has</param>
        /// <param name="state"></param>
        public Reagent(ReagentTypeEnum reagentType, int quantity, GameState state) : base(quantity, REAGENT_SPRITE)
        {
            // capture the game state so we know the users Karma for cost calculations
            _state = state;
            ReagentType = reagentType;
        }

        /// <summary>
        ///     Get the correct price adjust for the specific location and
        /// </summary>
        /// <param name="records"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="Ultima5ReduxException"></exception>
        public override int GetAdjustedBuyPrice(PlayerCharacterRecords records,
            SmallMapReferences.SingleMapReference.Location location)
        {
            if (!GameReferences.ReagentReferences.IsReagentSoldAtLocation(location, ReagentType))
                throw new Ultima5ReduxException("Requested reagent " + LongName + " from " + location +
                                                " which is not sold here");

            // A big thank you to Markus Brenner (@minstrel_dragon) for digging in and figuring out the Karma calculation
            // price = Base Price * (1 + (100 - Karma) / 100)
            int nAdjustedPrice = GameReferences.ReagentReferences.GetPriceAndQuantity(location, ReagentType).Price *
                                 (1 + (100 - _state.Karma) / 100);
            return nAdjustedPrice;
        }

        /// <summary>
        ///     Get bundle quantity based on location
        ///     Different merchants sell in different quantities
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        /// <exception cref="Ultima5ReduxException"></exception>
        public override int GetQuantityForSale(SmallMapReferences.SingleMapReference.Location location)
        {
            if (!GameReferences.ReagentReferences.IsReagentSoldAtLocation(location, ReagentType))
                throw new Ultima5ReduxException("Requested reagent " + LongName + " from " + location +
                                                " which is not sold here");

            return GameReferences.ReagentReferences.GetPriceAndQuantity(location, ReagentType).Quantity;
        }



        /// <summary>
        ///     Does a particular location sell a particular reagent?
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool IsReagentForSale(SmallMapReferences.SingleMapReference.Location location)
        {
            return GameReferences.ReagentReferences.IsReagentSoldAtLocation(location, ReagentType);
        }

    }
}