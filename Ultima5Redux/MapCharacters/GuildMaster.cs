﻿using System.Collections.Generic;
using Ultima5Redux.Data;
using Ultima5Redux.DayNightMoon;
using Ultima5Redux.Dialogue;

namespace Ultima5Redux.MapCharacters
{
    public class GuildMaster : ShoppeKeeper
    {
        
        public GuildMaster(ShoppeKeeperDialogueReference shoppeKeeperDialogueReference, 
            ShoppeKeeperReference theShoppeKeeperReference, DataOvlReference dataOvlReference) : 
            base(shoppeKeeperDialogueReference, theShoppeKeeperReference, dataOvlReference)
        {
        }

        public override List<ShoppeKeeperOption> ShoppeKeeperOptions => new List<ShoppeKeeperOption>()
        {
            new ShoppeKeeperOption("Buy", ShoppeKeeperOption.DialogueType. BuyGuildMaster),
        };
        
        public override string GetHelloResponse(TimeOfDay tod = null, ShoppeKeeperReference shoppeKeeperReference = null)
        {
            int nIndex = ShoppeKeeperDialogueReference.GetRandomMerchantStringIndexFromRange(148, 151);
            if (shoppeKeeperReference != null)
                return "\"" + ShoppeKeeperDialogueReference.GetMerchantString(nIndex,
                    shoppeKeeperName: shoppeKeeperReference.ShoppeKeeperName,
                    shoppeName: shoppeKeeperReference.ShoppeName,
                    tod: tod);
            throw new Ultima5ReduxException("Can't get a hello response without a ShoppeKeeperReference");
        }

        public override string GetWhichWouldYouSee()
        {
            return DataOvlReference.StringReferences.GetString(
                DataOvlReference.ShoppeKeeperGeneral2Strings.YES_N_N_DQ__WE_SELL_COLON_N_N).Replace("Yes", "").TrimStart();
        }

        public override string GetThyInterest()
        {
            return DataOvlReference.StringReferences.GetString(DataOvlReference.ShoppeKeeperGeneral2Strings.THY_CONCERN_Q_DQ);
        }

        public override string GetForSaleList()
        {
            return DataOvlReference.StringReferences.GetString(DataOvlReference.ShoppeKeeperGeneral2Strings
                       .A_DOTS_KEYS_N)
                   + DataOvlReference.StringReferences.GetString(DataOvlReference.ShoppeKeeperGeneral2Strings
                       .B_DOTS_GEMS_N)
                   + DataOvlReference.StringReferences.GetString(DataOvlReference.ShoppeKeeperGeneral2Strings
                       .C_DOTS_TORCHES_N_N).Trim();
        }
    }
}