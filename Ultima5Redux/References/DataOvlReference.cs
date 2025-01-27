﻿using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Ultima5Redux.Data;

// ReSharper disable InconsistentNaming

namespace Ultima5Redux.References
{
    /// <summary>
    ///     Class for quick access to the static contents of the Data.ovl file
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public class DataOvlReference
    {
        public enum AdditionalStrings
        {
            THE_SCEPTRE_IS_RECLAIMED_BANG_N,
            A_RING_HAS_VANISHED_BANG_N,
            STARS_CONFLICT_STARS_N_N
        }


        public enum Battle2Strings
        {
            N_BATTLE_IS_LOST_BANG,
            N_VICTORY_BANG_N,
            _TELEPORTS_BANG_N,
            THY_SWORD_HATH_SHATTERED_BANG_N,
            _VANISHES_BANG,
            _DIVIDES_BANG_N,
            _IS_POISONED_BANG_N,
            _REGURGITATED_BANG_N,
            Q_DQ,
            Q_DQ2,
            Q_DQ3,
            N_N_YOUR_RESPONSE_Q_N_COLON,
            N_N,
            N_N2,
            _IS_SLICED_IN_HALF_BANG_,
            _DIE_BANG_DQ_,
            N_N3,
            N_N4,
            N_THOU_ART_SUBDUED_AND_BLINDFOLDED_BANG,

            N_N_STRONG_GUARDS_DRAG_THEE_AWAY_BANG
            //     [0] = {string} "\nBATTLE IS LOST!"
            // [1] = {string} "\nVICTORY!\n"
            // [2] = {string} " teleports!\n"
            // [3] = {string} "Thy sword hath shattered!\n"
            // [4] = {string} " vanishes!"
            // [5] = {string} " divides!\n"
            // [6] = {string} " is poisoned!\n"
            // [7] = {string} " regurgitated!\n"
            // [8] = {string} "?""
            // [9] = {string} "?""
            // [10] = {string} "?""
            // [11] = {string} "\n\nYour response?\n:"
            // [12] = {string} "\n\n"
            // [13] = {string} "\n\n"
            // [14] = {string} " is sliced in half! "
            // [15] = {string} " die!" "
            // [16] = {string} "\n\n"
            // [17] = {string} "\n\n"
            // [18] = {string} "\nThou art subdued and blindfolded!"
            // [19] = {string} "\n\nStrong guards drag thee away!"
        }


        public enum BattleStrings
        {
            _FAILED_BANG_N,
            _MISSED_BANG_N,
            _POSSESSED_BANG_N,
            _REAPPEARS_BANG,
            _DISAPPEARS_BANG,
            _GATES_IN_A_DAEMON_BANG_N,
            _GRAZED_BANG_N,
            _KILLED_BANG_N,
            _SLEPT_BANG_N,
            _DRAGGED_UNDER_BANG_N,
            _HIT_BANG_N,
            _BARELY_WOUNDED_BANG_N,
            _LIGHTLY_WOUNDED_BANG_N,
            HEAVILY_WOUNDED_BANG_N,
            _CRITICAL_BANG_N,
            _INTERFERES_BANG_N,
            AIM_BANG_,
            AIM_BANG_2,
            NOTHING_BANG_N,
            COLON_N,
            ATTACK_DASH,

            ATTACK_DASH_2
            //     [0] = {string} "Failed!\n"
            // [1] = {string} " missed!\n"
            // [2] = {string} " possessed!\n"
            // [3] = {string} " reappears!"
            // [4] = {string} " disappears!"
            // [5] = {string} " gates in a daemon!\n"
            // [6] = {string} " grazed!\n"
            // [7] = {string} " killed!\n"
            // [8] = {string} " slept!\n"
            // [9] = {string} " dragged under!\n"
            // [10] = {string} " hit!\n"
            // [11] = {string} " barely wounded!\n"
            // [12] = {string} " lightly wounded!\n"
            // [13] = {string} " heavily wounded!\n"
            // [14] = {string} " critical!\n"
            // [15] = {string} " interferes!\n"
            // [16] = {string} "Aim! "
            // [17] = {string} "Aim! "
            // [18] = {string} "Nothing!\n"
            // [19] = {string} ":\n"
            // [20] = {string} "Attack-"
            // [21] = {string} "Attack-"
        }

        public enum ChitChatStrings
        {
            DOST_THOU_PAY,
            YES_BANG,
            NO_BANG,
            GET_HORSE_OUTTA_HERE,
            HALF_TO_CHARITY,
            GUARD_DEMANDS,
            XX_GP_TRIBUTE,
            GIVE_PASSWORD_BADGE,
            YOUR_RESPONSE_Q,
            PASS_FRIEND,
            GUARD_NO_RESPONSE,
            NO_RESPONSE,
            DONT_HURT_ME,
            MERCH_SEE_ME_AT_SHOP1,
            MERCH_SEE_ME_AT_SHOP2,
            NOBODY_HERE,
            ZZZ,

            N_NO_RESPONSE_N
            // [0] = {string} "\n\nDost thou pay?\n\n:"
            // [1] = {string} "Yes\n"
            // [2] = {string} "No!\n"
            // [3] = {string} "A merchant says:\n"GET THAT HORSE OUT OF HERE!"\n"
            // [4] = {string} "Thou wilt give\nhalf thy gold to\ncharity!"
            // [5] = {string} "A guard demands\na "
            // [6] = {string} " gp tribute\nto Blackthorn!"
            // [7] = {string} "Give now the\npassword, bearer\nof the Badge!"
            // [8] = {string} "\n\nYour response?\n"
            // [9] = {string} "Pass, friend!"
            // [10] = {string} "The guard offers\nno response!\n"
            // [11] = {string} "No response!\n"
            // [12] = {string} "Don't hurt me!\nPlease go away!"
            // [13] = {string} "A merchant says:\n"Come see me at\nmy shoppe, "
            // [14] = {string} "when\nit's open!"\n"
            // [15] = {string} "\nNobody's here!\n"
            // [16] = {string} "\n"Zzzzzz..."\n"
            // [17] = {string} "\nNo response!\n"
        }

        /// <summary>
        ///     Conversational phrase indexes
        /// </summary>
        public enum ChunkPhrasesConversation
        {
            CANT_JOIN_1 = 0x02,
            CANT_JOIN_2 = 0x03,
            MY_NAME_IS = 0x05,
            YOUR_INTEREST = 0x07,
            CANNOT_HELP = 0x09,
            YOU_RESPOND = 0x0A,
            WHAT_YOU_SAY = 0x0B,
            WHATS_YOUR_NAME = 0x0C,
            IF_SAY_SO = 0x0E,
            PLEASURE = 0x0F,
            YOU_SEE = 0x11,
            I_AM_CALLED = 0x12
        }

        /// <summary>
        ///     Chunk names specific to the Data.ovl file
        /// </summary>
        [SuppressMessage("ReSharper", "IdentifierTypo")]
        public enum DataChunkName
        {
            Unused = -1,
            TALK_COMPRESSED_WORDS,
            LOCATION_NAME_INDEXES,
            LOCATION_NAMES,
            PHRASES_CONVERSATION,
            LOCATIONS_X,
            LOCATIONS_Y,
            TRAVEL,
            WORLD,
            CHIT_CHAT,
            KEYPRESS_COMMANDS,
            VISION2,
            OPENING_THINGS_STUFF,
            KLIMBING,
            GET_THINGS,
            SPECIAL_ITEM_NAMES,
            SPECIAL_ITEM_NAMES2,
            WEAR_USE_ITEM,
            SHARDS,
            WORDS_OF_POWER,
            POTIONS,
            SPELLS,
            LONG_ARMOUR,
            SHORT_ARMOUR,
            DEFENSE_VALUES,
            ATTACK_VALUES,
            ATTACK_RANGE_VALUES,
            SPELL_ATTACK_RANGE,
            EQUIP_INDEXES,
            REQ_STRENGTH_EQUIP,
            EQUIPPING,
            ZSTATS,
            SLEEP_TRANSPORT,
            REAGENTS,
            EXCLAIMS,
            MOON_PHASES,
            THINGS_I_FIND,
            STORE_NAMES,
            SHOPPE_KEEPER_NAMES,
            EQUIPMENT_BASE_PRICE,
            WEAPONS_SOLD_BY_MERCHANTS,
            SHOPPE_KEEPER_NOT_ENOUGH_MONEY,
            SHOPPE_KEEPER_DO_YOU_WANT,
            SHOPPE_KEEPER_WHATS_FOR_SALE,
            SHOPPE_KEEPER_SELLING,
            SHOPPE_KEEPER_BLACKSMITH_WE_HAVE,
            SHOPPE_KEEPER_BLACKSMITH_HELLO,
            SHOPPE_KEEPER_BLACKSMITH_POS_EXCLAIM,
            SHOPPE_KEEPER_GENERAL,
            SHOPPE_KEEPER_INNKEEPER,
            SHOPPE_KEEPER_INNKEEPER_2,
            REAGENT_QUANTITES,
            REAGENT_BASE_PRICES,
            SHOPPE_KEEPER_GENERAL_2,
            SHOPPE_KEEPER_REAGENTS,
            SHOPPE_KEEPER_TOWNES_PROVISIONS,
            SHOPPE_KEEPER_TOWNES_REAGENTS,
            SHOPPE_KEEPER_TOWNES_HEALING,
            SHOPPE_KEEPER_TOWNES_TAVERN,
            SHOPPE_KEEPER_TOWNES_INN,
            SHOPPE_KEEPER_TOWNES_SHIPS,
            SHOPPE_KEEPER_TOWNES_HORSES,
            SHOPPE_KEEPER_HEALER,
            SHOPPE_KEEPER_HEALER2,
            HEALER_HEAL_PRICES,
            HEALER_CURE_PRICES,
            HEALER_RESURRECT_PRICES,
            X_DOCKS,
            Y_DOCKS,
            BAR_KEEP_GOSSIP_WORDS,
            BAR_KEEP_GOSSIP_PEOPLE,
            BAR_KEEP_GOSSIP_PLACES,
            SHOPPE_KEEPER_BAR_KEEP,
            BAR_KEEP_GOSSIP_MAP,
            SHOPPE_KEEPER_BAR_KEEP_2,
            INN_DESCRIPTION_INDEXES,
            INN_BED_X_COORDS,
            INN_BED_Y_COORDS,
            YELLING,
            WORLD2,
            MONSTER_NAMES_MIXED,
            MONSTER_NAMES_UPPER,
            ENEMY_FLAGS,
            ENEMY_ATTACK_RANGE,
            ENEMY_RANGE_THING,
            ENEMY_THING,
            ENEMY_STATS,
            ENEMY_FRIENDS,
            BATTLE,
            ADDITIONAL,
            BATTLE2,
            SEARCH_OBJECT_ID,
            SEARCH_OBJECT_QUALITY,
            SEARCH_OBJECT_LOCATION,
            SEARCH_OBJECT_FLOOR,
            SEARCH_OBJECT_X,
            SEARCH_OBJECT_Y,
            RESPONSE_TO_KEYSTROKE
        }


        public enum EnemyIndividualNamesMixed
        {
            MAGE,
            BARD,
            FIGHTER,
            AVATAR,
            VILLAGER,
            MERCHANT,
            JESTER,
            BARD_2,
            CHILD,
            BEGGAR,
            GUARD,
            WANDERER,
            BLACKTHORNE,
            LORD_BRITISH,
            SEA_HORSE,
            SQUID,
            SEA_SERPENT,
            SHARK,
            GIANT_RAT,
            BAT,
            GIANT_SPIDER,
            GHOST,
            SLIME,
            GREMLIN,
            MIMIC,
            REAPER,
            GAZER,
            CRAWLER, // appears to be unused asset, replaced by quest items 
            GARGOYLE,
            INSECT_SWARM,
            ORC,
            SKELETON,
            PYTHON,
            ETTIN,
            HEADLESS,
            WISP,
            DAEMON,
            DRAGON,
            SAND_TRAP,
            TROLL,
            MONGBAT,
            CORPSER,
            ROT_WORM,

            SHADOW_LORD
            //     [0] = {string} "Mage"
            // [1] = {string} "Bard"
            // [2] = {string} "Fighter"
            // [3] = {string} "Avatar"
            // [4] = {string} "Villager"
            // [5] = {string} "Merchant"
            // [6] = {string} "Jester"
            // [7] = {string} "Bard"
            // *
            // *
            // [8] = {string} "Child"
            // [9] = {string} "Beggar"
            // [10] = {string} "Guard"
            // [11] = {string} "Wanderer"
            // [12] = {string} "Blackthorn"
            // [13] = {string} "Lord British"
            // [14] = {string} "Sea Horse"
            // [15] = {string} "Squid"
            // [16] = {string} "Sea Serpent"
            // [17] = {string} "Shark"
            // [18] = {string} "Giant Rat"
            // [19] = {string} "Bat"
            // [20] = {string} "Giant Spider"
            // [21] = {string} "Ghost"
            // [22] = {string} "Slime"
            // [23] = {string} "Gremlin"
            // [24] = {string} "Mimic"
            // [25] = {string} "Reaper"
            // [26] = {string} "Gazer"
            // [27] = {string} "Crawler"
            // [28] = {string} "Gargoyle"
            // [29] = {string} "Insect Swarm"
            // [30] = {string} "Orc"
            // [31] = {string} "Skeleton"
            // [32] = {string} "Python"
            // [33] = {string} "Ettin"
            // [34] = {string} "Headless"
            // [35] = {string} "Wisp"
            // [36] = {string} "Daemon"
            // [37] = {string} "Dragon"
            // [38] = {string} "Sand Trap"
            // [39] = {string} "Troll"
            // *
            // *
            // [40] = {string} "Mongbat"
            // [41] = {string} "Corpser"
            // [42] = {string} "Rot Worm"
            // [43] = {string} "Shadow Lord"
        }


        public enum EnemyOutOfCombatNamesUpper
        {
            WIZARDS = 0,
            BARD,
            FIGHTER,
            X_AVATAR,
            VILLAGER,
            MERCHANT,
            JESTER,
            BARD_2,
            PIRATES,
            X,
            CHILD,
            BEGGAR,
            GUARD,
            X_WANDERER,
            BLACKTHORNE,
            LORD_BRITISH,
            SEA_HORSES,
            SQUIDS,
            SEA_SERPENTS,
            SHARKS,
            GIANT_RATS,
            BATS,
            SPIDERS,
            GHOSTS,
            SLIME,
            GREMLINS,
            MIMICS,
            REAPERS,
            GAZERS,
            X_QUEST_ITEMS,
            GARGOYLE,
            INSECTS,
            ORCS,
            SKELETONS,
            SNAKES,
            ETTINS,
            HEADLESSES,
            WISPS,
            DAEMONS,
            DRAGONS,
            SAND_TRAPS,
            TROLLS,
            X_FIELDS,
            X_WHIRLPOOL,
            MONGBATS,
            CORPSERS,
            ROT_WORMS,

            SHADOW_LORD
            //     [0] = {string} "WIZARDS"
            // [1] = {string} "BARD"
            // [2] = {string} "FIGHTER"
            // [3] = {string} "x"
            // [4] = {string} "VILLAGER"
            // [5] = {string} "MERCHANT"
            // [6] = {string} "JESTER"
            // [7] = {string} "BARD"
            // [8] = {string} "PIRATES"
            // [9] = {string} "x"
            // [10] = {string} "CHILD"
            // [11] = {string} "BEGGAR"
            // [12] = {string} "GUARDS"
            // [13] = {string} "x"
            // [14] = {string} "BLACKTHORN"
            // [15] = {string} "LORD BRITISH"
            // [16] = {string} "SEA HORSES"
            // [17] = {string} "SQUIDS"
            // [18] = {string} "SEA SERPENTS"
            // [19] = {string} "SHARKS"
            // [20] = {string} "GIANT RATS"
            // [21] = {string} "BATS"
            // [22] = {string} "SPIDERS"
            // [23] = {string} "GHOSTS"
            // [24] = {string} "SLIME"
            // [25] = {string} "GREMLINS"
            // [26] = {string} "MIMICS"
            // [27] = {string} "REAPERS"
            // [28] = {string} "GAZERS"
            // [29] = {string} "x"
            // [30] = {string} "GARGOYLE"
            // [31] = {string} "INSECTS"
            // [32] = {string} "ORCS"
            // [33] = {string} "SKELETONS"
            // [34] = {string} "SNAKES"
            // [35] = {string} "ETTINS"
            // [36] = {string} "HEADLESSES"
            // [37] = {string} "WISPS"
            // [38] = {string} "DAEMONS"
            // [39] = {string} "DRAGONS"
            // [40] = {string} "SAND TRAPS"
            // [41] = {string} "TROLLS"
            // [42] = {string} "x"
            // [43] = {string} "x"
            // [44] = {string} "MONGBATS"
            // [45] = {string} "CORPSERS"
            // [46] = {string} "ROTWORMS"
            // [47] = {string} "SHADOW LORD"
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum Equipment
        {
            BareHands = -2,
            LeatherHelm = 0,
            ChainCoif = 1,
            IronHelm = 2,
            SpikedHelm = 3,
            SmallShield = 4,
            LargeShield = 5,
            SpikedShield = 6,
            MagicShield = 7,
            JewelShield = 8,
            ClothArmour = 9,
            LeatherArmour = 10,
            Ringmail = 11,
            ScaleMail = 12,
            ChainMail = 13,
            PlateMail = 14,
            MysticArmour = 15,
            Dagger = 16,
            Sling = 17,
            Club = 18,
            FlamingOil = 19,
            MainGauche = 20,
            Spear = 21,
            ThrowingAxe = 22,
            ShortSword = 23,
            Mace = 24,
            MorningStar = 25,
            Bow = 26,
            Arrows = 27,
            Crossbow = 28,
            Quarrels = 29,
            LongSword = 30,
            TwoHHammer = 31,
            TwoHAxe = 32,
            TwoHSword = 33,
            Halberd = 34,
            SwordofChaos = 35,
            MagicBow = 36,
            SilverSword = 37,
            MagicAxe = 38,
            GlassSword = 39,
            JeweledSword = 40,
            MysticSword = 41,
            RingInvisibility = 42,
            RingProtection = 43,
            RingRegeneration = 44,
            AmuletOfTurning = 45,
            SpikedCollar = 46,
            Ankh = 47,
            FlamPor = 48,
            VasFlam = 49,
            InCorp = 50,
            UusNox = 51,
            UusZu = 52,
            UusFlam = 53,
            UusSanct = 54,
            Nothing = 0xFF
        }

        public enum EquippingStrings
        {
            ITEM_COLON,
            CANT_CHANGE_IN_BATTLE,
            AMBFDTPRS,
            NO_AMMO_BANG,
            REMOVE_HELM_FIRST_BANG,
            REMOVE_ARMOUR_FIRST_BANG,
            FREE_ONE_HAND_BANG,
            FREE_BOTH_HANDS_FIRST_BANG,
            REMOVE_THY_AMULET_BANG,
            ONLY_ONE_RING_BANG,
            NOT_STRONG_ENOUGH_BANG,
            N_N_RING_VANISHES_N,
            DONE_N,
            NONE_BANG_N,
            THOU_ART_EMPTY_N_HANDED_BANG_N,

            ITEM_COLON2
            //            [0] "Item: "	string
            //[1]	"Thou canst not change armour in heated battle!"	string
            //[2]	"AMBFDTPRS"	string
            //[3]	"Thou hast no ammunition for that weapon!"	string
            //[4]	"Remove first thy present helm!"	string
            //[5]	"Thou must first remove thine other armour!"	string
            //[6]	"Thou must free one of thy hands first!"	string
            //[7]	"Both hands must be free before thou canst wield that!"	string
            //[8]	"Thou must remove thine other amulet!"	string
            //[9]	"Only one magic ring may be worn at a time!"	string
            //[10]	"Thou art not strong enough!"	string
            //[11]	"\n\nRing vanishes!\n"	string
            //[12]	"Done\n"	string
            //[13]	"None!\n"	string
            //[14]	"Thou art empty-\nhanded!\n"	string
            //[15]	"Item: "	string
        }

        public enum ExclaimStrings
        {
            NO_EFFECT_BANG_N,
            PUSHED_BANG_N,
            PULLED_BANG_N,
            WONT_BUDGE_BANG_N,
            WONT_BUDGE_BANG_N_2,
            ESCAPE,
            DASH_NOT_HERE_BANG_N,
            DASH_NOT_YET_BANG_N,
            GOO_POOF_BANG_N,
            DISARMED_BANG_N,
            CHEST_OPENED_BANG_N,
            CREATURE_COLON,
            SPACE_CHARMED_BANG_N,
            CREATURE_COLON_2,
            CREATURE_COLON_3,
            TO_PHASE_COLON,
            MAGIC_ABSORBED_BANG_N,
            SPELL_NAME_COLON_N_COLON,
            NONE_BANG,
            NO_EFFECT_BANG,
            ABSORBED_BANG_N,
            NOT_HERE_BANG_N,
            NON_MIXED_BANG_N,
            MP_TOO_LOW_BANG_N,
            SUCCESS_BANG_N,
            FAILED_BANG_N,
            SCROLL_N_N,
            LIGHT_BANG_N,
            WIND_CHANGE_BANG_N,
            PROTECTION_BANG_N,
            NEGATE_MAGIC_BANG_N,
            VIEW_BANG_N,
            NOT_HERE_BANG_N_2,
            SUMMON_DAEMON_BANG_N,
            NOT_HERE_BANG_N_3,
            RESURRECTION_BANG_N,
            NOT_HERE_BANG,
            NO_EFFECT_BANG_N_2,
            NEGATE_TIME_BANG_N,
            POTION_N,
            HEALED_BANG_N,
            POISON_CURED_BANG_N,
            POISONED_BANG_N,
            SLEPT_BANG_N,
            POOF_BANG_N,
            INVISIBLE_BANG_N,
            N_NO_NOTICEABLE_EFFECT_NOW_BANG_N,
            MOONSTONE_SPACE,
            BURIED_BANG_N,
            CANNOT_BE_BURIED_HERE_BANG_N,

            GEM_SHARD_N_N_THOU_DOES_HOLD
            // [43] = {string} "Slept!\n"
            // [44] = {string} "Poof!\n"
            // [45] = {string} "Invisible!\n"
            // [46] = {string} "\nNo noticeable effect now!\n"
            // [47] = {string} "Moonstone "
            // [48] = {string} "buried!\n"
            // [49] = {string} "cannot be buried here!\n"
            // [50] = {string} "Gem Shard\n\nThou dost hold above t"
            // [0] = {string} "No effect!\n"
            // [1] = {string} "Pushed!\n"
            // [2] = {string} "Pulled!\n"
            // [3] = {string} "Won't budge!\n"
            // [4] = {string} "Won't budge\n"
            // [5] = {string} "Escape"
            // [6] = {string} "-Not here!\n"
            // [7] = {string} "-Not yet!\n"
            // [8] = {string} "5346POOF!\n"
            // [9] = {string} "Disarmed!\n"
            // [10] = {string} "Chest opened!\n"
            // [11] = {string} "Creature: "
            // [12] = {string} " charmed!\n"
            // [13] = {string} "Creature: "
            // [14] = {string} "Creature: "
            // [15] = {string} "To phase: "
            // [16] = {string} "Magic absorbed!\n"
            // [17] = {string} "Spell name:\n:"
            // [18] = {string} "None!\n"
            // [19] = {string} "No effect!\n"
            // [20] = {string} "Absorbed!\n"
            // [21] = {string} "Not here!\n"
            // [22] = {string} "None mixed!\n"
            // [23] = {string} "M.P. too low!\n"
            // [24] = {string} "Success!\n"
            // [25] = {string} "Failed!\n"
            // [26] = {string} "Scroll\n\n"
            // [27] = {string} "Light!\n"
            // [28] = {string} "Wind change!\n"
            // [29] = {string} "Protection!\n"
            // [30] = {string} "Negate magic!\n"
            // [31] = {string} "View!\n"
            // [32] = {string} "Not here!\n"
            // [33] = {string} "Summon Daemon!\n"
            // [34] = {string} "Not here!\n"
            // [35] = {string} "Resurrection!\n"
            // [36] = {string} "Not here!\n"
            // [37] = {string} "No effect!\n"
            // [38] = {string} "Negate time!\n"
            // [39] = {string} "Potion\n"
            // [40] = {string} "Healed!\n"
            // [41] = {string} "Poison cured!\n"
            // [42] = {string} "POISONED!\n"    
        }

        public enum GetThingsStrings
        {
            OPEN_IT_FIRST = 0,
            A_MOONSTONE,
            A_MAGIC_CARPET,
            S_FOOD,
            A_SANDLEWOOD_BOX,
            S_TORCH,
            BANG,
            ES_BANG,
            S_GEM,
            BANG2,
            S_BANG3,
            S_ODD_KEY,
            S_KEY,
            BANG4,
            S_BANG,
            PLANS_FOR_HMS_CAPE,
            A_SCROLL_COLON,
            BANG5,
            S_GOLD,
            A_SPACE,
            S_POTION,
            BANG6,
            THE_SHARD_OF,
            FALSEHOOD,
            HATRED,
            COWARDICE,
            CROWN_OF_LB,
            SCEPTRE_OF_LB,
            AMULET_OF_LB,
            NOTHING_TO_GET,
            GET,
            MUST_OPEN_FIRST,
            CONTENTS_OF_CHEST_YOU_FIND,
            NOT_HER,
            NEWLINE,
            BORROWED,
            CROPS_PICKED,
            MMMM_DOT,
            CANT_REACH_PLATE,
            MMM_DOT2,
            CANT_REACH_PLATE2,
            CANT_REACH_PLATE3,
            MMM_DOT3,
            NOTHING_TO_GET2
        }

        public enum KeypressCommandsStrings
        {
            BUFFER_O = 0,
            BUFFER_FF,
            BUFFER_N,
            SHEETS_IN_IRONS,
            PASS,
            BOARD,
            CAST_DOT,
            D_WHAT,
            ENTER_WHAT,
            FIRE,
            GET,
            HOLE_UP,
            ONLY_IN_BED,
            IGNITE_TORCH,
            JIMMY,
            KLIMB,
            LOOK,
            DOT_DOT_DOT,
            MIX_REAGENTS,
            NEW_ORDER,
            OPEN,
            PUSH_NOT_HER,
            PUSH,
            QUIT,
            READY,
            SEARCH,
            SEARCH_DOR,
            TALK,
            FUNNY_NO_RESPONSE,
            TALK_FUNNY_NO_RESPONSE,
            TALK2,
            USE_ITEM,
            VIEW_GEM,
            YOU_HAVE_NONE,
            WWHAT,
            XIT,
            YELL,
            ZSTATS_DOT,
            WHAT_Q,
            PASS_N,
            NORTH_N,
            SOUTH_N,
            WEST_N,
            EAST_N,
            HOLE_UP_AND,
            N_REPAIR_DOT,
            SAILS_MUST_BE,
            LOWERED,
            HULL_NOW,
            BANG_N_N,
            CAMP_N_N,
            ON_LAND_OR_SHIP,
            ON_FOOT,
            HOW_MANY_HOURS,
            WILT_THOU_WATCH,
            NO_N_N,
            YES_N_N,
            WHO_WILL_GUARD,
            NONE_POSTED,
            SET_ACTIVE_PLR,
            NONE_BANG
        }

        // [0] = {string} "Death vision!\n"
        // [1] = {string} "Strange vision!\n"
        // [2] = {string} "\nThou dost see\n"
        // [3] = {string} "\n"
        // [4] = {string} "You see:\ndarkness.\n"
        // [5] = {string} "You see:\n"
        // [6] = {string} "A sleep field.\n"
        // [7] = {string} "A poison gas field.\n"
        // [8] = {string} "A wall of fire.\n"
        // [9] = {string} "An electric field.\n"
        // [10] = {string} "An energy field.\n"
        // [11] = {string} "a dripping stalactite.\n"
        // [12] = {string} "a caved in passage.\n"
        // [13] = {string} "an unfortunate software pirate.\n"
        // [14] = {string} "a less fortunate adventurer.\n"
        // [15] = {string} "a passage.\n"
        // [16] = {string} "an up ladder.\n"
        // [17] = {string} "a down ladder.\n"
        // [18] = {string} "a ladder.\n"
        // [19] = {string} "a wooden chest.\n"
        // [20] = {string} "a fountain.\n"
        // [21] = {string} "a pit.\n"
        // [22] = {string} "an open chest.\n"
        // [23] = {string} "an energy field.\n"
        // [24] = {string} "nothing of note.\n"
        // [25] = {string} "a heavy door.\n"
        // [26] = {string} "a wall.\n"
        // [27] = {string} "SPEC WALL ERR.\n"
        // [28] = {string} "a wall.\n"
        // [29] = {string} "a heavy door.\n"
        // [30] = {string} "a heavy door.\n"
        public enum KlimbingStrings
        {
            WITH_WHAT = 0,
            ON_FOOT,
            IMPASSABLE,
            NOT_CLIMABLE,
            FELL
        }


        public enum LocationStrings
        {
            Moonglow = 0,
            Britain = 1,
            Jhelom = 2,
            Yew = 3,
            Minoc = 4,
            Trinsic = 5,
            Skara_Brae = 6,
            New_Magincia = 7,
            Fogsbane = 8,
            Stormcrow = 9,
            Greyhaven = 10,
            Waveguide = 11,
            Iolos_Hut = 12,
            Suteks_Hut = -1,
            SinVraals_Hut = -2,
            Grendels_Hut = -3,
            Lord_Britishs_Castle = -4,
            Palace_of_Blackthorn = -5,
            West_Britanny = 13,
            North_Britanny = 14,
            East_Britanny = 15,
            Paws = 16,
            Cove = 17,
            Buccaneers_Den = 18,
            Ararat = 19,
            Bordermarch = 20,
            Farthing = 21,
            Windemere = 22,
            Stonegate = 23,
            Lycaeum = 24,
            Empath_Abbey = 25,
            Serpents_Hold = 26,
            Deceit = 27,
            Despise = 28,
            Destard = 29,
            Wrong = 30,
            Covetous = 31,
            Shame = 32,
            Hythloth = 33,
            Doom = 34
        }


        public enum LongArmourString
        {
            LEATHER_HELM,
            SPIKED_HELM,
            SMALL_SHIELD,
            LARGE_SHIELD,
            SPIKED_SHIELD,
            MAGIC_SHIELD,
            JEWEL_SHIELD,
            CLOTH_ARMOUR,
            LEATHER_ARMOUR,
            SCALE_MAIL,
            CHAIN_MAIL,
            PLATE_MAIL,
            MYSTIC_ARMOUR
        }

        public enum OpeningThingsStrings
        {
            T = 0,
            HOU_DOST_FIND,
            A_HIDDEN_DOOR,
            KEY_BROKE,
            SUCCESS,
            KEY_BROKE2,
            NEWLINE,
            NO_KEYS,
            KEY_BROKE3,
            NO_KEYS2,
            CHEST_UNLOCKED,
            KEY_BROKE4,
            ALREADY_OPEN,
            WHAT,
            NO_KEYS3,
            KEY_BROKE6,
            UNLOCKED,
            KEY_BROKE5,
            NO_ONE_IS_THERE,
            KEY_BROKE7,
            COULDNT_FIND_NPC,
            N_N_I_THANK_THEE_N,
            UNLOCKED2,
            NO_LOCK,
            FOUND,
            CANT,
            NOTHING_TO_OPEN,
            TRAPPED,
            CHEST_EMPTY,
            CHEST_OPENED,
            ALREADY_OPEN2,
            WHAT2,
            ITS_OPEN,
            TOO_HEAVY,
            LOCKED_N,
            OPENED
        }

        public enum PotionsStrings
        {
            BLUE,
            YELLOW,
            RED,
            GREEN,
            ORANGE,
            PURPLE,
            BLACK,
            WHITE
        }

        public enum ReagentStrings
        {
            SULFUR_ASH,
            GINSENG,
            GARLIC,
            SPIDER_SILK,
            BLOOD_MOSS,
            BLACK_PEARL,
            NIGHTSHADE,
            MANDRAKE_ROOT
        }


        public enum ResponseToKeystroke
        {
            N_A_,
            _STOLE_SOME_FOOD_BANG_N,
            _ESCAPES_BANG_N,
            CANT_BANG_N,
            COMMA_,
            COMMA_ARMED_WITH_,
            BARE_HANDS,
            COLON,
            ARGH_BANG_N,
            ZZZZZ_DOT_DOT_DOT_N,
            BUFFER_0,
            FF_N,
            N_N,
            SOUNDS_,
            OFF_N,
            ON_N,
            CAST_DOT_DOT_DOT_N,
            ABSORBED_BANG_N,
            CANT_BANG_N2,
            GET_DASH,
            JIMMY_DASH,
            OPEN_DASH,
            PUSH_DASH,
            READY_DOT_DOT_DOT_N_N,
            SEARCH_DASH,
            USE_ITEM_N_N,
            YELL_,
            Z_DASH_STATS_DOT_DOT_DOT_N,
            PASS_N,
            SET_ACTIVE_PLR_COLON_N_NONE_BANG_N,
            BOARD,
            D_DASH_WHAT_Q_N,
            ENTER,
            FIRE,
            HOLE_UP,
            IGNITE_TORCH,
            LOOK,
            MIX,
            NEW_ORDER,
            QUIT,
            TALK,
            VIEW,
            W_DASH_WHAT_Q_N,
            X_DASH_IT,
            WHAT_Q_N


            // [0] = {string} "\nA "
            // [1] = {string} " stole some food!\n"
            // [2] = {string} " escapes!\n"
            // [3] = {string} "Can't!\n"
            // [4] = {string} ", "
            // [5] = {string} ", armed with "
            // [6] = {string} "bare hands"
            // [7] = {string} ":"
            // [8] = {string} "ARGH!\n"
            // [9] = {string} "Zzzzz...\n"
            // [10] = {string} "Buffer O"
            // [11] = {string} "ff\n"
            // [12] = {string} "n\n"
            // [13] = {string} "Sound "
            // [14] = {string} "Off\n"
            // [15] = {string} "On\n"
            // [16] = {string} "Cast...\n"
            // [17] = {string} "Absorbed!\n"
            // [18] = {string} "Can't!\n"
            // [19] = {string} "Get-"
            // [20] = {string} "Jimmy-"
            // [21] = {string} "Open-"
            // [22] = {string} "Push-"
            // [23] = {string} "Ready...\n\n"
            // [24] = {string} "Search-"
            // [25] = {string} "Use item\n\n"
            // [26] = {string} "Yell "
            // [27] = {string} "Z-stats...\n"
            // [28] = {string} "Pass\n"
            // [29] = {string} "Set active plr:\nNone!\n"
            // [30] = {string} "Board"
            // [31] = {string} "D-What?\n"
            // [32] = {string} "Enter"
            // [33] = {string} "Fire"
            // [34] = {string} "Hole up"
            // [35] = {string} "Ignite torch"
            // [36] = {string} "Look"
            // [37] = {string} "Mix"
            // [38] = {string} "New order"
            // [39] = {string} "Quit"
            // [40] = {string} "Talk"
            // [41] = {string} "View"
            // [42] = {string} "W-What?\n"
            // [43] = {string} "X-it"
            // [44] = {string} "What?\n"
        }

        public enum ShadowlordStrings
        {
            GEM_SHARD_THOU_HOLD_EVIL_SHARD,
            FALSEHOOD_DOT,
            HATRED_DOT,
            COWARDICE_DOT,
            N_N_NO_EFFECT,
            N_N_AND_CAST_INTO_FLAME,
            TRUTH,
            LOVE,
            COURAGE,
            THE_DOOM_OF_SHADOWLORD,
            FALSEHOOD_WORD,
            HATRED_WORD,
            COWARDICE_WORD,
            IS_WROUGHT_N
        }

        public enum ShardsStrings
        {
            FALSEHOOD,
            HATRED,
            COWARDICE
        }


        public enum ShoppeKeeperBarKeepStrings
        {
            DQ,
            OF_WHAT_WOULDST_N_THOU_HEAD_MY_N_LORE_COMMA_SP,
            Q_DQ_N_N_YOU_RESPOND_COLON_N,
            N_N,
            THAT_I_CANNOT_HELP_THEE_WITH_DOT_N_N,
            N_N_FAIR_NUFF_Q_DQ,
            NO_N_N,
            YES_N_N,
            SORRY_COMMA_SP,
            N_SAYS_SP,
            DOT_N_N,
            NO,
            YES_N_N_2,
            DQ_ANYTHING_ELSE_N_FOR_THEE_Q_DQ,
            NO_2,
            YES_N_N_3,
            YELLS_NPC_DOT_N,
            MILADY,
            SIR,

            Q_DQ
            // from 0x9f0e-9feb
            // [0] = {string} """
            // [1] = {string} "Of what wouldst\nthou hear my\nlore, "
            // [2] = {string} "?"\n\nYou respond:\n"
            // [3] = {string} "\n\n"
            // [4] = {string} ""That, I cannot help thee with.\n\n"
            // [5] = {string} "\n\nFair 'nuff?" "
            // [6] = {string} "No\n\n"
            // [7] = {string} "Yes\n\n"
            // [8] = {string} ""Sorry, "
            // [9] = {string} "\nsays "
            // [10] = {string} ".\n\n"
            // [11] = {string} "No"
            // [12] = {string} "Yes\n\n""
            // [13] = {string} ""Anything else\nfor thee?" "
            // [14] = {string} "No"
            // [15] = {string} "Yes\n\n""
            // [16] = {string} "yells $.\n"
            // [17] = {string} "milady"
            // [18] = {string} "sir"
            // [19] = {string} "?" "
            // [20] = {string} "Yes"
            // [21] = {string} "No"
            // [22] = {string} "F\n\n"
            // [23] = {string} "Yes"
            // [24] = {string} "No"
            // [25] = {string} "Yes\n\n"
            // [26] = {string} "No"
            // [27] = {string} "S\n\n"
            // [28] = {string} "Yes\n\n"
            // [29] = {string} "No"
            // [30] = {string} "Yes"
            // [31] = {string} "No"        
        }

        [SuppressMessage("ReSharper", "IdentifierTypo")]
        public enum ShoppeKeeperBarKeepStrings2
        {
            TWO,
            THREE,
            FOUR,
            FIVE,
            SIX,
            SIR,
            MILADY,
            THAT_WILL_BE_SP,
            SP_GOLD_FOR_THE_SP,
            S_OF_YE_COMMA_N,
            DQ_N_N_CANT_PAY_Q_B_BEAT_IT_BANG_DQ_N_YELLS_SP,
            N_ENJOY_BANG_DQ_N_N,
            N_N_DQ_I_BEG_THY_N_PARDON_COMMA_SP,
            COMMA_DQ_N_SAYS_SP,
            DOT_N_DQ_BUT_HAVENT_N_THY_HAD_ENOUGH_N_TO_DRINK_Q_DQ,
            YES_N_N,
            NO_BANG,
            OUR_WINE_LIST_COMMA_N,
            DOT_N_N,
            A_WINE_1_N,
            B_WINE_2_N,
            C_WINE_3_N,
            D_WINE_4_N,
            E_WINE_5_N,
            F_WINE_6_N,
            THY_CHOICE_Q_DQ,
            N_N_DQ_AH_A_FINE_CHOICE_COMMA_SP,
            DQ_N_N_DQ_CANT_PAY_Q_BEAT_IT_BANG_DQ_N_YELLS_SP,
            N_ENJOY_BANG_DQ,
            N_N_HOW_MANY_WOULDST_N_THOU_LIKE_Q_DQ_SP,
            N_N_DQ_HRUMPH_DOT_DQ,
            N_N,
            THOU_HAST_N_HEITHER_GOLD_NOR_N_NEED_BANG_OUT_BANG_DQ_N,
            YELLS_SP,
            DOT_N,
            DQ_THOU_CANST_AFFORD_ONLY_SP,
            BANG_DQ_N_N,
            N_N_2
        }


        public enum ShoppeKeeperBlacksmithHello
        {
            HAIL_FRIEND_BANG_BUY_OR_SELL_Q,

            GREETINGS_TRAVELLER_BUY_OR_SELL_Q
            // [0] = {string} "Hail, friend! Wouldst thou Buy or Sell?"
            // [1] = {string} "Greetings, traveller! Wish ye to Buy, or hast thou wares to Sell?"
        }

        public enum ShoppeKeeperBlacksmithPositiveExclamation
        {
            VERY_GOOD_BANG_N,
            EXCELLENT_BANG_N,
            FINE_FINE_BANG_N,

            BUT_OF_COURSE_BANG_N
            // [2] = {string} "Very good!\n"
            // [3] = {string} "Excellent!\n"
            // [4] = {string} "Fine, fine!\n"
            // [5] = {string} "But of course!\n"
        }

        public enum ShoppeKeeperBlacksmithWeHave
        {
            WE_HAVE_COLON,
            WE_STOCK_COLON,
            THOU_CANST_BUY_COLON,

            WEVE_GOT_COLON
            // [6] = {string} "We have:"
            // [7] = {string} "We stock:"
            // [8] = {string} "Thou canst buy:"
            // [9] = {string} "We've got:"
        }


        public enum ShoppeKeeperGeneral2Strings
        {
            N_N_COLON,
            N_COLON,
            N_N_DQ,
            N_N_DQ_2,
            SAYS_NAME_DOT_N,
            NO,
            YES,
            N_N_DQ_3,
            N_N_INTERESTED_Q_DQ,
            NO_N_N_DQ_WHAT_ELSE_Q_N_N,
            YES_N,
            N_DQ_SOLID_BANG_N_SAYS_NAME_N_N_DQ_WHAT_ELSE_N,
            M_LADY,
            M_LORD,
            Q_N_N,
            A_DOTS_KEYS_N,
            B_DOTS_GEMS_N,
            C_DOTS_TORCHES_N_N,
            THY_CONCERN_Q_DQ,
            YES_N_N_DQ__WE_SELL_COLON_N_N,
            NO_2,
            N_N_DQ_THOU_CANST_CARRY_BANG_DQ_N_N,
            N_N_DQ_4,
            IS_THIS_THY_NEED_Q_DQ,
            N_COLON_2,
            NO_N_N_DQ_WHAT_ELSE_Q_N_N_2,
            YES_N_2,
            N_DQ_I_THANK_THEE_BANG_DQ_N_SAYS_NAME__N,

            DQ_ANYTHING_ELSE_Q_N_N
            // [0] = {string} "\n\n:"
            // [1] = {string} "\n:"
            // [2] = {string} "\n\n""
            // [3] = {string} "\n\n""
            // [4] = {string} "says $.\n"
            // [5] = {string} "No"
            // [6] = {string} "Yes"
            // [7] = {string} "\n\n""
            // [8] = {string} "\n\nInterested?" "
            // [9] = {string} "No\n\n"What else, then?\n\n"
            // [10] = {string} "Yes\n"
            // [11] = {string} "\n"Sold!"\nsays $.\n\n"What else, \n"
            // [12] = {string} "m'lady"
            // [13] = {string} "m'lord"
            // [14] = {string} "?\n\n"
            // [15] = {string} "a.........Keys\n"
            // [16] = {string} "b.........Gems\n"
            // [17] = {string} "c......Torches\n\n"
            // [18] = {string} "Thy concern?" "
            // [19] = {string} "Yes\n\n"We sell:\n\n"
            // [20] = {string} "No"
            // [21] = {string} "\n\n"Thou canst not carry any more!"\n\n"
            // [22] = {string} "\n\n""
            // [23] = {string} " Is this thy need?" "
            // [24] = {string} "\n:"
            // [25] = {string} "No\n\n"What else?\n\n"
            // [26] = {string} "Yes\n"
            // [27] = {string} "\n"I thank thee!"\nsays $.\n"
            // [28] = {string} ""Anything else?\n\n"
        }

        public enum ShoppeKeeperGeneralStrings
        {
            DOT_DOT_DOT,
            N_THY_INTEREST_Q_QUOTE,
            YES_N_N_FINE_BANG_WE_SELL_COLON,
            NO,
            THE_STABLES_ARE_CLOSED_DOT_N,
            YES_N_N,
            N_N_DEAL_Q_QUOTE,
            NO_2,
            YES_BANG,
            N_N_QUOTE_THOU_COULDST_NOT_AFFORD_TO,
            FEED_IT_BANG_QUOTE_N_YELLS_SK_N,

            NO_3
            // [0] = {string} "..."
            // [1] = {string} "\nThy interest?" "
            // [2] = {string} "Yes\n\n"Fine! We sell:\n\n"
            // [3] = {string} "No"
            // [4] = {string} "The stables are closed.\n"
            // [5] = {string} "Yes\n\n""
            // [6] = {string} "\n\nDeal?" "
            // [7] = {string} "No"
            // [8] = {string} "Yes!"
            // [9] = {string} "\n\n"Thou couldst not afford to "
            // [10] = {string} "feed it!"\nyells $.\n"
            // [11] = {string} "No"
        }

        public enum ShoppeKeeperHealerStrings
        {
            N_N_DQ_WHO_NEEDS_MY_AID_Q_DQ,
            NO_ONE,
            FOR_GOLD_GOLD_DO_N_N_WILT_THO_N_PAY_Q_DQ,
            YES,
            NO,
            NO_2,
            YES_N_N,
            DQ_WE_HAVE_POWERS_TO_CURE_HEAL_RESURRECT_DOT_DQ_N,
            SAYS_NAME_DOT_N_N_DQ_WHAT_IS_THE_NATURE_OF_THY_NEED_Q_DQ,
            CURING,
            N_N,
            RECEIVE_NOW_THE_LIGHT_BANG_DQ,
            I_CAN_CURE_THY_POISONED_BODY,
            HEALING,
            N_N_2,
            RECEIVE_NOW_THE_LIGHT_BANG_DQ_2,
            I_CAN_HEAL_THEE,
            RESURRECT,
            N_N_3,
            I_CAN_RAISE_THIS_UNFORTUNATE_PERSON_FROM,
            THE_DEAD,
            NOTHING,
            N_N_DQ_IS_THERE_ANY_OTHER_WAY_IN_WHICH_I_MAY_N,

            AID_THEE_Q
            // [22] = {string} "\n\n"Who needs my aid?" "
            // [23] = {string} "No one"
            // [24] = {string} "for % gold.\n\nWilt thou\npay?" "
            // [25] = {string} "Yes"
            // [26] = {string} "No"
            // [27] = {string} "No"
            // [28] = {string} "Yes\n\n"
            // [29] = {string} ""We have powers to Cure, Heal, or Resurrect."\n"
            // [30] = {string} "says $.\n\n"What is the nature of thy need?" "
            // [31] = {string} "Curing"
            // [32] = {string} "\n\n""
            // [33] = {string} "Receive now the Light!""
            // [34] = {string} "I can cure thy poisoned body "
            // [35] = {string} "Healing"
            // [36] = {string} "\n\n""
            // [37] = {string} "Receive now the Light!""
            // [38] = {string} "I can heal thee "
            // [39] = {string} "Resurrect"
            // [40] = {string} "\n\n""
            // [41] = {string} "I can raise this unfortunate person from "
            // [42] = {string} "the dead "
            // [43] = {string} "Nothing"
            // [44] = {string} "\n\n"Is there any other way in which I may\n"
            // [45] = {string} "aid thee?" "
        }


        public enum ShoppeKeeperHealerStrings2
        {
            DQ_THOU_HAST_NO_NEED_OF_THIS_ART_BANG_DQ_SAYS_NAME
        }


        public enum ShoppeKeeperInnkeeper2Strings
        {
            ASKS_N_WHO_WILL_STAY_Q,
            NOBODY_N_N,
            N_N,
            THY_FRIEND,
            _WILL_NOT_LEAVE_THEE_BANG_N_N,
            DQ_THE_RATE_FOR_OUR_MOST_COMFORTABLE_ROOM_WILL_BE_,
            GLD_GOLD_PER_MONTH_DUE_AT_CHECKOUT_DOT,
            N_WILT_THOU_TAKE_IT_Q_DQ,
            N_N_2,
            I_THANK_THEE_DQ_N_SAYS_NAME_DOT_N_N,
            N_N_ONE_MUST_FIRST_BE_LEFT_BEHIND_BANG_N_N,
            N_N_DQ_NO_ONE_HERE_IS_FROM_THY_PARTY_BANG_DQ_N_SAYS_NAME_DOT_N_N,
            N_N_DQ_WHO_WILL_N_CHECK_OUT_Q_DQ,
            ___GUEST,
            __REGISTER_COLON_N_N,
            NO_ONE_N_N,
            N_N_DQ_THAT_WILL_BE_GLD_GOLD_PLEASE_DOT_DQ_N_N,
            THY_FRIEND_HAS_DIED_BY_THE_WAY_DOT_N,
            I_HOPE_THOU_HAST_THY_STAY_ENJOYABLE_COMMA_N,
            SAYS_NAME_DOT_N_N,
            NO,
            YES,
            N_N_NAME_ASKS_N_ART_THOU_HERE_TO_PICKUP_OR_N,
            LEAVE_A_COMPANION_OR_TO_REST_FOR_NIGHT_Q_DQ,

            IS_THERE_N_ANYTHING_MORE_N_I_CAN_DO_FOR_N_THEE_Q_DQ
            // [0] = {string} "$ asks,\n"Who will\nstay?" "
            // [1] = {string} "Nobody\n\n"
            // [2] = {string} "\n\n"
            // [3] = {string} "Thy friend"
            // [4] = {string} " will not leave thee!\n\n"
            // [5] = {string} ""The rate for\nour most comfortable room will be "
            // [6] = {string} "% gold per month, due at check-out."
            // [7] = {string} "\nWilt thou take\nit?" "
            // [8] = {string} "\n\n"
            // [9] = {string} ""I thank thee."\nsays $.\n\n"
            // [10] = {string} "\n\nOne must first be left behind!\n\n"
            // [11] = {string} "\n\n"No one here is from thy party!"\nsays $.\n\n"
            // [12] = {string} "\n\n"Who will\ncheck out?" "
            // [13] = {string} "    GUEST"
            // [14] = {string} "  REGISTER:\n\n"
            // [15] = {string} "No one\n\n"
            // [16] = {string} "\n\n"That will be % gold, please."\n\n""
            // [17] = {string} "Thy friend has died, by the way."\n"
            // [18] = {string} "I hope thou hast found thy stay enjoyable,"\n"
            // [19] = {string} "says $.\n\n"
            // [20] = {string} "No"
            // [21] = {string} "Yes"
            // [22] = {string} "\n\n$ asks,\n"Art thou here\nto Pick up or\n"
            // [23] = {string} "Leave a\ncompanion, or\nto Rest for the\nnight?" "
            // [24] = {string} ""Is there\nanything more\nI can do for\nthee?" "
        }


        public enum ShoppeKeeperInnkeeperStrings
        {
            I_AM_SORRY_COMMA_N,
            MILADY,
            SIR,
            COMMA_BUT_WE_HAVE_NO_ROOM_N_N,
            XXX_N_WILT_THOU_TAKE_N_IT_Q_DQ,
            N_N,
            HIGHWAYMAN_BANG_CHEAP_OUT_BANG,
            SCREAMS_N_NAME_N,
            HAVE_A_PLEASENT_NIGHT_COMMA,
            MILADY_2,
            SIR_2,
            BANG_N_SAYS_NAME_DOT_N_N,
            ZZZZ_DOTS_N_N,
            MORNING_BANG_N,

            _HAS_N_PASSED_AWAY_DOT_N
            // [0] = {string} ""I am sorry,\n"
            // [1] = {string} "milady"
            // [2] = {string} "sir"
            // [3] = {string} ", but we\nhave no room\navailable."\n\n"
            // [4] = {string} "\nWilt thou take\nit?" "
            // [5] = {string} "\n\n"
            // [6] = {string} ""Highwaymen!\nCheap, at that!\nOUT!" "
            // [7] = {string} "screams\n$.\n"
            // [8] = {string} ""Have a pleasant\nnight, "
            // [9] = {string} "milady"
            // [10] = {string} "sir"
            // [11] = {string} "!"\nsays $.\n\n"
            // [12] = {string} "Zzzzzz....\n\n"
            // [13] = {string} "Morning!\n"
            // [14] = {string} " has\npassed away.\n"
        }


        public enum ShoppeKeeperReagentStrings
        {
            SULFUR_ASH,
            GINSENG,
            GARLIC,
            SPIDER_SILK,
            BLOOD_MOSS,
            BLACK_PEARL,
            NIGHTSAHDE,

            MANDRAKE
            // [0] = {string} "Sulfur Ash"
            // [1] = {string} "Ginseng"
            // [2] = {string} "Garlic"
            // [3] = {string} "Spider Silk"
            // [4] = {string} "Blood Moss"
            // [5] = {string} "Black Pearl"
            // [6] = {string} "Nightshade"
            // [7] = {string} "Mandrake"        
        }


        public enum ShoppeKeeperSellingStrings
        {
            DONT_DEAL_AMMO_GROWL_NAME,
            N_N,
            N_NDEAL_Q,
            NO,
            YES_DONE_SAYS_NAME,
            I_CANNOT_BUY_FROM_THEE_NAME
        }

        public enum ShortArmourString
        {
            LEATHER_HELM,
            SPIKED_HELM,
            SMALL_SHIELD,
            LARGE_SHIELD,
            SPIKED_SHIELD,
            MAGIC_SHIELD,
            JEWEL_SHIELD,
            CLOTH_ARMOUR,
            LEATHER_ARMOUR,
            SCALE_MAIL,
            CHAIN_MAIL,
            PLATE_MAIL,
            MYSTIC_ARMOUR
        }

        public enum SleepTransportStrings
        {
            ZZZZZ_N_N,
            AMBUSHED_BANG_N,
            PARTY_RESTED_BANG_N,
            NO_EFFECT_DOTS_N,
            FOR_HOW_MANY_HOURS,
            ZZZZZZ_DOTS_N,
            THROWN_OUT_OF_BED_N,
            N_ON_FOOT_N,
            N_ON_FOOT_N_2,
            N_NOT_HER_BANG_N,
            NAY_BANG_N,
            HORSE_N,
            CARPET_N,
            SKIFF_N,
            SHIP_N,
            N_DANGER_SHIP_BADLY_DAMAGED_N,
            M_WARNING_NO_SKIFFS_N,
            WHAT_N,
            WHAT_N_2,
            FIRE_BROADSIDE_ONLY_BANG_N,
            WHAT_N_3,
            WHAT_N_4,
            BOOM_BANG_N,
            DOOR_DESTROYED_BANG_N,
            NONE_OWNED_BANG_N,
            N_N_SWAP_SPACE,
            NOBODY_BANG_N,
            N_N,
            SPACE_MUST_LEAD_BANG_N,
            N_WITH_SPACE,
            NOBODY_BANG_N_2,
            N_N_2,
            SPACE_MUST_LEAD_BANG_N_2,
            BANG_N,
            N_NOT_HERE_BANG_N,
            WHAT_N_LOWERCASE,
            N_UNDER_SAIL_BANG_N,
            CARPET_BANG_N,
            N_NO_LAND_NEARBY_BANG_N,
            N_NOT_HERE_BANG_N_3,
            HORSE_BANG_N,
            N_NO_LAND_NEARBY_BANG_N_3,
            N_NOT_HERE_BANG_N_4,
            HORSE_N_2,
            N_NO_LAND_NEARBY_BANG_N_5,
            N_NOT_HERE_BANG_N_6,
            SKIFF_BANG_N_LOWERCASE,
            SHIP_BANG_N_LOWERCASE,

            N_NO_SKIFFS_ON_BOARD_BANG_N_2
            //        [0]	"Zzzzzz...\n\n"	string
            //[1]	"Ambushed!\n\n"	string
            //[2]	"Party rested!\n"	string
            //[3]	"No effect...\n"	string
            //[4]	"For how many hours? "	string
            //[5]	"Zzzzzzz...\n"	string
            //[6]	"Thrown out of bed!\n"	string
            //[7]	"\nOn foot\n"	string
            //[8]	"\nOn foot\n"	string
            //[9]	"\nNot here!\n"	string
            //[10]	"\"Nay!\"\n"	string
            //[11]	"horse\n"	string
            //[12]	"carpet\n"	string
            //[13]	"skiff\n"	string
            //[14]	"Ship\n"	string
            //[15]	"\nDANGER: SHIP BADLY DAMAGED!\n"	string
            //[16]	"\nWARNING: NO SKIFFS ON BOARD!\n"	string
            //[17]	"What?\n"	string
            //[18]	"What?\n"	string
            //[19]	"Fire broadsides only!\n"	string
            //[20]	"What?\n"	string
            //[21]	"What?\n"	string
            //[22]	"BOOOM!\n"	string
            //[23]	"Door destroyed!\n"	string
            //[24]	"None owned!\n"	string
            //[25]	"\n\nSwap "	string
            //[26]	"nobody!\n"	string
            //[27]	"\n\n"	string
            //        [28]	" must lead!\n"	string
            //[29]	"\nwith "	string
            //[30]	"nobody!\n"	string
            //[31]	"\n\n"	string
            //[32]	" must lead!\n"	string
            //[33]	"!\n"	string
            //[34]	"\nNot here!\n"	string
            //[35]	"what?\n"	string
            //[36]	"\nUnder sail!\n"	string
            //[37]	"carpet!\n"	string
            //[38]	"\nNo land nearby!\n"	string
            //[39]	"\nNot here!\n"	string
            //[40]	"horse!\n"	string
            //[41]	"\nNo land nearby!\n"	string
            //[42]	"\nNot here!\n"	string
            //[43]	"skiff!\n"	string
            //[44]	"ship!\n"	string
            //[45]	"\nNo skiffs on board!\n"	string
        }

        public enum SpecialItemNames2Strings
        {
            SPYGLASS,
            HMS_CAPE_PLAN,
            SEXTANT,
            POCKET_WATCH,
            BLACK_BADGE,
            WOODEN_BOX
        }

        public enum SpecialItemNamesStrings
        {
            MAGIC_CRPT,
            SKULL_KEYS,
            AMULET,
            CROWN,
            SCEPTRE
        }

        public enum SpellStrings
        {
            IN_LOR,
            GRAV_POR,
            AN_ZU,
            AN_NOX,
            MANI,
            AN_YLEM,
            AN_SANCT,
            AN_XEN_CORP,
            REL_HUR,
            IN_WIS,
            KAL_XEN,
            IN_XEN_MANI,
            VAS_LOR,
            VAS_FLAM,
            IN_FLAM_GRAV,
            IN_NOX_GRAV,
            IN_ZU_GRAV,
            IN_POR,
            AN_GRAV,
            IN_SANCT,
            IN_SANCT_GRAV,
            UUS_POR,
            DES_POR,
            WIS_QUAS,
            IN_BET_XEN,
            AN_EX_POR,
            IN_EX_POR,
            VAS_MANI,
            IN_ZU,
            REL_TYM,
            IN_VAS_POR_YLEM,
            QUAS_AN_WIS,
            IN_AN,
            WIS_AN_YLEM,
            AN_XEN_EX,
            REL_XEN_BET,
            SANCT_LOR,
            XEN_CORP,
            IN_QUAS_XEN,
            IN_QUAS_WIS,
            IN_NOX_HUR,
            IN_QUAS_CORP,
            IN_MANI_CORP,
            KAL_XEN_CORP,
            IN_VAS_GRAV_CORP,
            IN_FLAM_HUR,
            VAS_REL_POR,
            AN_TYM
        }

        public enum ThingsIFindStrings
        {
            A_CHEST_BANG_N,
            A_SACK_OF_GOLD_BANG_N,
            A_POTION_BANG_N,
            A_SCROLL_BANG_N,
            A_WEAPON_BANG_N,
            A_SHIELD_BANG_N,
            A_RING_OF_KEYS_BANG_N,
            A_GEM_BANG_N,
            A_HELM_BANG_N,
            A_RING_BANG_N,
            SOME_ARMOUR_BANG_N,
            AN_AMULET_BANG_N,
            SOME_TORCHES_BANG_N,
            SOME_FOOD_BANG_N,
            A_STRANGE_ROCK_BANG_N,
            A_ROTTING_BODY_BANG_N,
            A_MOULDY_CORPSE_BANG_N,
            NOTHING_OF_NOTE_BANG_N,
            PLAGUE_BANG_N,
            NOTHING_BANG_N,
            WORMS_BANG_N,
            GUTS_BANG_N,
            A_BLOOD_PULP_BANG_N,
            FOOD_BANG_N,
            GOLD_BANG_N,
            NO_TRAP_BANG_N,
            A_SIMPLE_TRAP_BANG_N,
            A_COMPLEX_TRAP_BANG_N,
            A_TRAP_BANG_N,
            A_STRANGE_ROCK_BANG_N_2,
            MANDRAKE_ROOT_BANG_N,
            MANDRAKE_ROOT_BANG_N_2,
            NIGHTSHADE_BANG_N,
            SPACE_SPRIGS_OF_N,
            NEWLINE,
            NOTHING_OF_NOTE_BANG_N_2,
            N_YOU_FIND_COLON_N_DARKNESS_DOT_N,
            YOU_FIND_COLON_N,
            NOTHING_OF_NOTE_DOT_N,
            NOTHING_HIDDEN_ON_THE_LADDER_DOT_N,
            NOT_TRAP_N,
            A_SIMPLE_TRAP_N,
            A_COMPLEX_TRAP_N,
            A_TRAP_N,
            NOTHING_HIDDEN_ON_THE_FOUNTAIN_DOT_N,
            NOTHING_HIDDEN_N_IN_THE_PIT_DOT_N,
            A_PIT_BANG_N,
            A_BOMB_TRAP_BANG_N,
            NOTHING_OF_NOTE_DOT_N_2,
            TREASURE_BANG_N,
            A_SLEEP_FIELD_DOT_N,
            A_POISON_GAS_FIELD_DOT_N,
            A_WALL_OF_FIRE_DOT_N,
            AN_ELECTRIC_FIELD_DOT_N,
            AN_ENERGY_FIELD_DOT_N,
            THIS_TILE_IS_IMPOSSIBLE_DOT_N,
            NOTHING_HIDDEN_ON_THE_DOOR_DOT_N_2,
            NOTHING_HIDDEN_ON_THE_WALL_DOT_N,
            NOTHING_IN_THE_CAVED_IN_PASSAGE_DOT_N,
            NOTHING_OF_THE_STALACTITE_DOT_N,
            NOTHING_HIDDEN_ON_THE_SKELETON_DOT_N,
            IT_CRUMBLES_AWAY_DOT_N,
            A_HIDDEN_DOOR_BANG_N,
            NOTHING_HIDDEN_ON_THE_DOOR_DOT_N_3,
            N_THOU_DOST_FIND_N,

            N_THOU_DOST_FIND_N_2
            // [0] = {string} "a chest!\n"
            // [1] = {string} "a sack of gold!\n"
            // [2] = {string} "a potion!\n"
            // [3] = {string} "a scroll!\n"
            // [4] = {string} "a weapon!\n"
            // [5] = {string} "a shield!\n"
            // [6] = {string} "a ring of keys!\n"
            // [7] = {string} "a gem!\n"
            // [8] = {string} "a helm!\n"
            // [9] = {string} "a ring!\n"
            // [10] = {string} "some armour!\n"
            // [11] = {string} "an amulet!\n"
            // [12] = {string} "some torches!\n"
            // [13] = {string} "some food!\n"
            // [14] = {string} "a strange rock!\n"
            // [15] = {string} "a rotting body!\n"
            // [16] = {string} "a moldy corpse!\n"
            // [17] = {string} "Nothing of note.\n"
            // [18] = {string} "Plague!\n"
            // [19] = {string} "nothing!\n"
            // [20] = {string} "worms!\n"
            // [21] = {string} "guts!\n"
            // [22] = {string} "a bloody pulp!\n"
            // [23] = {string} "food!\n"
            // [24] = {string} "gold!\n"
            // [25] = {string} "no trap!\n"
            // [26] = {string} "a simple trap!\n"
            // [27] = {string} "a complex trap!\n"
            // [28] = {string} "a trap!\n"
            // [29] = {string} "a strange rock!\n"
            // [30] = {string} "mandrake root!"
            // [31] = {string} "mandrake root!"
            // [32] = {string} "nightshade!"
            // [33] = {string} " sprigs of\n"
            // [34] = {string} "\n"
            // [35] = {string} "nothing of note.\n"
            // [36] = {string} "\nYou find:\ndarkness.\n"
            // [37] = {string} "You find:\n"
            // [38] = {string} "Nothing of note.\n"
            // [39] = {string} "Nothing hidden on the ladder.\n"
            // [40] = {string} "No trap\n"
            // [41] = {string} "A simple trap\n"
            // [42] = {string} "A complex trap\n"
            // [43] = {string} "A trap\n"
            // [44] = {string} "Nothing hidden on the fountain.\n"
            // [45] = {string} "Nothing hidden\nin the pit.\n"
            // [46] = {string} "A pit!\n"
            // [47] = {string} "A bomb trap!\n"
            // [48] = {string} "Nothing of note.\n"
            // [49] = {string} "Treasure!\n"
            // [50] = {string} "A sleep field.\n"
            // [51] = {string} "A poison gas field.\n"
            // [52] = {string} "A wall of fire.\n"
            // [53] = {string} "An electric field.\n"
            // [54] = {string} "An energy field.\n"
            // [55] = {string} "This tile is impossible.\n"
            // [56] = {string} "Nothing hidden on the door.\n"
            // [57] = {string} "Nothing hidden on the wall.\n"
            // [58] = {string} "Nothing in the caved in passage.\n"
            // [59] = {string} "Nothing on the stalactite.\n"
            // [60] = {string} "Nothing hidden on the skeleton.\n"
            // [61] = {string} "It crumbles away.\n"
            // [62] = {string} "A hidden door!\n"
            // [63] = {string} "Nothing hidden on the door.\n"
            // [64] = {string} "\nThou dost find\n"
            // [65] = {string} "\nThou dost find\n"
        }

        public enum TravelStrings
        {
            UP = 0,
            DOWN,
            RIDE,
            FLY,
            ROW,
            NORTH,
            SOUTH,
            EAST,
            WEST,
            WISH_TO_LEAVE,
            EXIT_TO,
            UNDERWORLD,
            BRITANNIA,
            NO,
            BLOCKED,
            ATTACK,
            ON_FOOT,
            BROKEN,
            NOTHING_TO_ATTACK,
            MISSED,
            MURDERED,
            KLIMB,
            DASH_ON_FOOT,
            WHAT
        }

        public enum Vision2Strings
        {
            DEATH_VISION,
            STRANGE_VISION,
            THOU_DOST_SEE,
            NEWLINE,
            YOU_SEE_COLON_DARKNESS_DOT_N,
            YOU_SEE_COLON_N,
            A_SLEEP_FIELD_DOT_N,
            A_POISON_GAS_FIELD_DOT_N,
            A_WALL_OF_FIRE_DOT_N,
            AN_ELECTRIC_FIELD_DOT_N,
            AN_ENERGY_FIELD_DOT_N,
            A_DRIPPING_STALACTITE_DOT_N,
            A_CAVED_IN_PASSAGE_DOT_N,
            AN_UNFORTUNATE_SOFTWARE_PIRATE_DOT_N,
            A_LESS_FORTUNATE_ADVENTURER_DOT_N,
            A_PASSAGE_DOT_N,
            AN_UP_LADDER_DOT,
            A_DOWN_LADDER_DOT_N,
            A_LADDER_DOT_N,
            A_WOODEN_CHEST_DOT_N,
            A_FOUNTAIN_DOT_N,
            A_PIT_DOT_N,
            AN_OPEN_CHEST_DOT_N,
            AN_ENERGY_FIELD_DOT_N_2,
            NOTHING_OF_NOTE_DOT_N,
            A_HEAVY_DOOR_DOT_N,
            A_WALL_DOT_N,
            SPEC_WALL_ERR_DOT_N,
            A_WALL_DOT_N_2,
            A_HEAVY_DOOR_DOT_N_2,
            A_HEAVY_DOOR_DOT_N_3
        }

        public enum WearUseItemStrings
        {
            REMOVED,
            NO_USEABLE_ITEMS,
            ITEM_COLON,
            ITEMS_COLON,
            CARPET_BANG,
            BOARDED_BANG,
            XIT_SHIP_FIRST,
            ONLY_ON_FOOT,
            NOT_HERE_BANG,
            SKULL_KEY_BANG,
            NOT_HERE_BANG_2,
            AMULET_N_N,
            WEARING_AMULET,
            CROWN_N_N,
            DON_THE_CROWN,
            SCEPTRE_N_N,
            WIELD_SCEPTRE,
            FIELD_DISSOLVED,
            NO_EFFECT_BANG,
            SPYGLASS_N_N,
            LOOKING_DOT_DOT_DOT,
            NO_STARS,
            NOT_HERE_BANG_3,
            PLANS_N_N,
            SHIP_RIGGED_DOUBLE_SPEED,
            ONLY_USE_ON_SHIPBOARD,
            SEXTANT_N_N,
            ONLY_OUTDOORS_BANG,
            ONLY_AT_NIGHT_BANG,
            POSITION_COLON,
            WATCH_N_N_THE_POCKET_W_READS,
            SPACE_PM,
            SPACE_AM,
            BADGE_N_N,
            BADGE_WORN_BANG_N,
            BOX_N_HOW_N,
            FAILED_BANG_N,
            SPACE_OF_LORD_BRITISH_DOT_N
        }

        public enum WordsOfPower
        {
            FALLAX,
            VILIS,
            INOPIA,
            MALUM,
            AVIDUS,
            INFAMA,
            IGANVUS,

            VERAMOCOR
            // [0] = {string} "FALLAX"
            // [1] = {string} "VILIS"
            // [2] = {string} "INOPIA"
            // [3] = {string} "MALUM"
            // [4] = {string} "AVIDUS"
            // [5] = {string} "INFAMA"
            // [6] = {string} "IGNAVUS"
            // [7] = {string} "VERAMOCOR"
        }


        public enum WorldStrings
        {
            RIDE = 0,
            FLY,
            ROW,
            HEAD,
            NORTH,
            SOUTH,
            EAST,
            WEST,
            HULL_WEAK,
            ROWING,
            BREAKING_UP,
            COLISSION,
            DOCKED,
            BLOCKED,
            OUCH,
            SLOW_PROG,
            VERY_SLOW,
            NORTH_2,
            SOUTH_2,
            EAST_2,
            WEST_2,
            JUNK_1,
            ATTACK_DASH,
            ON_FOOT,
            NOTHING_TO_ATTACK,
            NEW_ON_FOOT_NEW,
            ATTACKED_ENTRANCE,
            TWO_NEWLINES,
            BRIT_DAT,
            DUNGEON_DAT,
            NEW_WHAT_DUNGEON_NEW,
            ENTER_SPACE,
            to_enter_THE_SHRINE_OF,
            to_enter_HUT,
            to_enter_SHRINE_CODEX,
            to_enter_KEEP,
            to_enter_VILLAGE,
            to_enter_TOWNE,
            to_enter_CASTLE,
            to_enter_CAVE,
            to_enter_MINE,
            to_enter_DUNGEON,
            to_enter_RUINS,
            to_enter_LIGHTHOUSE,
            to_enter_PALACE_B,
            to_enter_CASTLE_LB,
            WHAT,
            EARTHQUAKE,
            ZZZ,
            BRIT_DAT_2,
            EXIT_TO_DOS,
            N,
            V1_16,
            SOUND,
            OFF,
            ON,
            WHAT_2,
            NEW_DQUOTE,
            PASS_SEEKER,
            NOT_SACRED_QUEST,
            PASSAGE_DENIED,
            ROUGH_SEAS
        }

        public enum WorldStrings2
        {
            SHIP_SUNK_BANG_N,
            ABANDON_SHIP_BANG_N,
            DROWNING_BANGS_N,
            N_WHIRLPOOL_BANG_N,
            STAR_BOOM_BANG_STAR_N_N,
            CAUGHT_BANG_N_N_THE_TROLLS_DEMAND_A_SP,
            SP_GP_TOLL_BANG_N_N_DOST_THOU_PAY_Q,
            N_THOU_SPIETH_TROLLS_UNDER_THE_BRIDGE_BANG_N_N,
            SP_SNEAKS_ACROSS,
            N_N,
            TROLLS_EVADED_BANG_N,

            BOTTOMLESS_N_SP_PIT_SP
            // [0] = {string} "Ship sunk!\n"
            // [1] = {string} "Abandon ship!\n"
            // [2] = {string} "DROWNING!!!\n"
            // [3] = {string} "\nWHIRLPOOL!\n"
            // [4] = {string} "\nAttacked!\n"
            // [5] = {string} "* BOOOM! *\n\n"
            // [6] = {string} "Caught!\n\nThe trolls demand a "
            // [7] = {string} " gp toll!\n\nDost thou pay?"
            // [8] = {string} "\nThou spieth trolls under the bridge!\n\n"
            // [9] = {string} " sneaks across"
            // [10] = {string} "\n\n"
            // [11] = {string} "Trolls evaded!\n"
            // [12] = {string} "BOTTOMLESS\n   PIT    "
        }

        public enum YellingStrings
        {
            FURL_BANG_N,
            HOIST_BANG_N,
            WHAT_Q_N_COLON,

            NOTHING_N
            // FURL!\n"
            // [3] = {string} "HOIST!\n"
            // [4] = {string} "what?\n:"
            // [5] = {string} "Nothing\n"
        }


        public enum ZstatsStrings
        {
            DONE_DOT_N,
            PLAYER_COLON,
            NONE_BANG_N,
            AMBFDTPRS,
            GPDSC,
            SPACE_LV_DASH,
            STR_EQUALS,
            _SPACE2_HP_COLON,
            N_INT_EQUALS,
            SPACE2_HM_COLON,
            N_DEX_EQUALS,
            SPACE2_EX_COLON,
            N_N_SPACE4_MAGIC_COLON,
            ARMS_N_N,
            NON_READY,
            EQUIPMENT,
            N_SPACE_FOOD_COLON,
            N_SPACE_GOLD_COLON,
            N_N_SPACE_KEYS_DOTS,
            N_SPACE_GEMS_DOTS,
            N_TORCHES_DOTS,
            N_SPACE_GRAPPLE,
            DASH_DASH,
            CODE1,
            CODE2,
            MOONSTONE_SPACE,
            NONE_OWNED_BANG,
            N_STATUS_COLON,
            REAGENTS,
            SPELLS,
            ITEMS,
            ARMAMENTS,
            DONE_N,
            N_N

            //[0] "Done.\n"	string
            //[1]	"Player: "	string
            //[2]	"None!\n"	string
            //[3]	"AMBFDTPRS"	string
            //[4]	"GPDSC"	string
            //[5]	" Lv-"	string
            //[6]	"Str="	string
            //[7]	"  HP:"	string
            //[8]	"\nInt="	string
            //[9]	"  HM:"	string
            //[10]	"\nDex="	string
            //[11]	"  Ex:"	string
            //[12]	"\n\n    Magic:"	string
            //[13]	"Arms\n\n"	string
            //[14]	"(None ready)"	string
            //[15]	"Equipment"	string
            //[16]	"\n Food: "	string
            //[17]	"\n Gold: "	string
            //[18]	"\n\n Keys......."	string
            //[19]	"\n Gems......."	string
            //[20]	"\n Torches...."	string
            //[21]	"\n Grapple"	string
            //[22]	"--"	string
            //[23]	"\u001c + "	string
            //[24]	"\u001d + "	string
            //[25]	"Moonstone "	string
            //[26]	"(None owned!)"	string
            //[27]	"\nStatus: "	string
            //[28]	"Reagents"	string
            //[29]	"Spells"	string
            //[30]	"Items"	string
            //[31]	"Armaments"	string
            //[32]	"Done\n"	string
            //[33]	"\n\n"	string
        }

        /// <summary>
        ///     All the data chunks
        /// </summary>
        private readonly DataChunks<DataChunkName> _dataChunks;

        public string DataDirectory { get; }

        public U5StringRef StringReferences { get; }

        /// <summary>
        ///     Construct the DataOvlReference
        /// </summary>
        /// <param name="dataDirectory">directory of data.ovl file</param>
        public DataOvlReference(string dataDirectory)
        {
            DataDirectory = dataDirectory;

            _dataChunks = new DataChunks<DataChunkName>(dataDirectory, FileConstants.DATA_OVL, DataChunkName.Unused);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x00, 0x18);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.FixedString, "Licence for the MS-Runtime", 0x18, 0x38);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Long Armour strings (13 of them)", 0x52,
                0xA6, 0, DataChunkName.LONG_ARMOUR);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Weapon strings (10 of them)", 0xF8, 0x81);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Ring and amulet strings (5 of them)", 0x179,
                0x5a);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Enemy Names (mixed case)", 0x1d3, 0x158,
                0x00, DataChunkName.MONSTER_NAMES_MIXED);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Enemy Names (upper case)", 0x32b, 0x165,
                0x00, DataChunkName.MONSTER_NAMES_UPPER);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Character type, monster names in capital letters (44 of them)", 0x32b, 0x165);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Unknown", 0x490, 0x33);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Item names (5 of them)", 0x4c3, 0x2b, 0,
                DataChunkName.SPECIAL_ITEM_NAMES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "(x where x goes from 0 to 7", 0x4ee, 0x18);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shard names (3 of them)", 0x506, 0x29, 0,
                DataChunkName.SHARDS); // changed from 0x28 to 0x29
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Additional item names (6 of them)", 0x52f,
                0x43, 0, DataChunkName.SPECIAL_ITEM_NAMES2);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shortened names - Armour", 0x572, 0x77, 0,
                DataChunkName.SHORT_ARMOUR);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Potion colors (8 of them)", 0x68c, 0x30, 0,
                DataChunkName.POTIONS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Reagents (8 of them)", 0x6bc, 0x4d, 0,
                DataChunkName.REAGENTS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Spell names", 0x709, 0x1bb, 0,
                DataChunkName.SPELLS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Character type and names (11 of them)",
                0x8c4, 0x54);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Health text (5 of them)", 0x918, 0x29);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Spell runes (26 of them)", 0x941, 0x64);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Unknown", 0x9a5, 0xa8);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "City names (in caps) (26 of them)", 0xa4d,
                0x111 + 0x3a, 0, DataChunkName.LOCATION_NAMES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Virtue names (8 of them)", 0xb98, 0x48);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Virtue mantras (8 of them)", 0xbe0, 0x1e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Store names", 0xbfe, 0x2fc, 0,
                DataChunkName.STORE_NAMES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Merchant Character names", 0xefa, 0x152, 0,
                DataChunkName.SHOPPE_KEEPER_NAMES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Compressed words used in the conversation files", 0x104c, 0x24e, 0,
                DataChunkName.TALK_COMPRESSED_WORDS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Filenames", 0x129a, 0x11c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x13b6, 0x3a6);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy Stats", 0x13CC, 0x30 * 8, 0x00,
                DataChunkName.ENEMY_STATS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy Ability Flags", 0x154C, 0x30 * 2, 0x00,
                DataChunkName.ENEMY_FLAGS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy Attack Range (1-9)", 0x15AC, 0x30, 0x00,
                DataChunkName.ENEMY_ATTACK_RANGE);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy Range THING", 0x15DC, 0x30, 0x00,
                DataChunkName.ENEMY_RANGE_THING);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy Friends", 0x16E4, 0x30, 0x00,
                DataChunkName.ENEMY_FRIENDS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Enemy THING", 0x1714, 0x30, 0x00,
                DataChunkName.ENEMY_THING);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Attack values", 0x160C, 0x37, 0x00,
                DataChunkName.ATTACK_VALUES);
            // excludes extended items such as ankh and spells
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Defense values", 0x1644, 0x2F, 0x00,
                DataChunkName.DEFENSE_VALUES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Attack Range values", 0x1674, 0x37, 0x00,
                DataChunkName.ATTACK_RANGE_VALUES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Spell Attack Range values", 0x16ad, 0x37, 0x00,
                DataChunkName.SPELL_ATTACK_RANGE);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Additional Weapon/Armour strings", 0x175c,
                0xa9, 0x10);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List,
                "String indexes for all equipment (except scrolls) (add 0x10 to index)", 0x1806, 0x2F * 2 + 2, 0x10,
                DataChunkName.EQUIP_INDEXES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Required Strength for Equipment Values",
                0x1ABE, 0x2F, 0x00, DataChunkName.REQ_STRENGTH_EQUIP);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List, "Text index (add + 0x10)", 0x187a, 0x1ee,
                0x10);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Which Map index do we start in (for TOWNE.DAT)", 0x1e2a, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Which Map index do we start in (for DWELLING.DAT)", 0x1e32, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Which Map index do we start in (for CASTLE.DAT)", 0x1e3a, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Which Map index do we start in (for KEEP.DAT)",
                0x1e42, 0x8);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List,
                "Name of cities index (13+22 shorts, add 0x10)", 0x1e4a, 0x50, 0x10,
                DataChunkName.LOCATION_NAME_INDEXES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "X-coordinates to Towns, Dwellings, Castles, Keeps, Dungeons", 0x1e9a, 0x28, 0x00,
                DataChunkName.LOCATIONS_X);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Y-coordinates to Towns, Dwellings, Castles, Keeps, Dungeons", 0x1ec2, 0x28, 0x00,
                DataChunkName.LOCATIONS_Y);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "moon phases (28 byte pairs, one for each day of the month)", 0x1EEA, 0x1C * 2, 0,
                DataChunkName.MOON_PHASES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Virtue and mantra index (add + 0x10)", 0x1f5e,
                0x20, 0x10);

            // extended stuff "old list"
            //flags that define the special abilities of
            //             monsters during combat; 32 bits per monster
            //             0x0020 = undead (affected by An Xen Corp)
            //             - passes through walls (ghost, shadowlord)
            //             - can become invisible (wisp, ghost, shadowlord)
            //             - can teleport (wisp, shadowlord)
            //             - can't move (reaper, mimic)
            //             - able to camouflage itself
            //             - may divide when hit (slime, gargoyle)
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "flags that define the special abilities of monsters during combat", 0x154C, 0x30 * 2);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List,
                "moon phases (28 byte pairs, one for each day of the month)", 0x1EEA, 0x38);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "x coordinates of shrines", 0x1F7E, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "y coordinates of shrines", 0x1F86, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Arms seller's name index", 0x22da, 0x12);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x22ec, 0x20c);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that have a tavern", 0x23ea, 0x09, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_TAVERN);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that sell horses", 0x23fa, 0x04, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_HORSES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Shoppe Keeper - Towne indexes that sell ships",
                0x240a, 0x04, 0x00, DataChunkName.SHOPPE_KEEPER_TOWNES_SHIPS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that sell reagents", 0x241a, 0x05, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_REAGENTS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that sell provisions", 0x242a, 0x03, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_PROVISIONS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that sell healing", 0x243a, 0x07, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_HEALING);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Shoppe Keeper - Towne indexes that have an inn", 0x244a, 0x06, 0x00,
                DataChunkName.SHOPPE_KEEPER_TOWNES_INN);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Indexes to the dialog text (add + 0x10) (see .TLK)", 0x24f8, 0x13e, 0x10);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, ".DAT file names (4 files)", 0x2636, 0x2b);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x2661, 0x9);

            // the following are reindexed. The file has some gunk in the middle of the strings which is indescript.
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Travel Related Strings", 0x266a, 0xE1, 0x00,
                DataChunkName.TRAVEL); // tweaked
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x2750, 0x28); // tweaked
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Text strings(some unknown in the middle)",
                0x2778, 0x6F); // tweaked
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x27E7, 0x0C); // tweaked
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Text strings(some unknown in the middle)",
                0x27F3, 0xE0); // tweaked

            // the following are reindexed. The file has some gunk in the middle of the strings which is indescript.
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x28d3, 0x83);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Sailing, Interface and World related strings", 0x2956, 0x278, 0x00, DataChunkName.WORLD);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x2bce, 0x9a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Text strings (some unknown in the middle)",
                0x2c68, 0x3c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x2ca4, 0x9);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Text strings (some unknown in the middle)",
                0x2cad, 0x146);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x2df4, 0x14d);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Initial string", 0x2f41, 0x5b);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "STORY.DAT string", 0x2f9d, 0xa);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x3664, 0x322);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Chunking information for Britannia's map, 0xFF the chunk only consists of tile 0x1, otherwise see BRIT.DAT",
                0x3886, 0x100);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random filenames, texts and unknown", 0x3986,
                0xaf);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x3a35, 0xd);

            // Another big thank you to Markus Brenner (@minstrel_dragon) for providing the "actual" base prices that 
            // helped track down the meaning and location of the reagent data
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Reagent base prices", 0x3a42, 0x28, 0,
                DataChunkName.REAGENT_BASE_PRICES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Reagent quantities", 0x3a6a, 0x28, 0,
                DataChunkName.REAGENT_QUANTITES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List, "All equipment base prices", 0x3a92, 0x60, 0,
                DataChunkName.EQUIPMENT_BASE_PRICE);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "What weapons are sold by the merchant in cities: Britain, Jhelom, Yew, Minoc, Trinsic, British Castle, Buccaneer's Den, Border March, Serpent Hold - (9 groups of 8 bytes)",
                0x3af2, 0x48, 0, DataChunkName.WEAPONS_SOLD_BY_MERCHANTS);

            // Healer prices
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Heal Prices", 0x3d96, 0x08, 0,
                DataChunkName.HEALER_HEAL_PRICES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Cure Prices", 0x3d9e, 0x08, 0,
                DataChunkName.HEALER_CURE_PRICES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List, "Resurrect Prices", 0x3da6, 0x10, 0,
                DataChunkName.HEALER_RESURRECT_PRICES);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x3b3a, 0x38);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.UINT16List,
                "Innkeeper welcome text index into SHOPPE.DAT (+0x0, 2 bytes for each index)", 0x3b72, 0x8);
            // this section contains information about hidden, non-regenerating objects (e.g. the magic axe in the dead tree in Jhelom); there are
            // only 0x71 such objects; the last entry in each table is 0
            //3db0
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "object type (tile - 0x100) (item)", 0x3E88,
                0x72);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "object quality (e.g. potion type, number of gems) (item)", 0x3EFA, 0x72);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "location number (see \"Party _location\") (item)", 0x3F6C, 0x72);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "level (item)", 0x3FDE, 0x72);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "x coordinate (item)", 0x4050, 0x72);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "y coordinate (item)", 0x40C2, 0x72);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Words of Power", 0x44ad, 0x3A, 0,
                DataChunkName.WORDS_OF_POWER);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Yelling strings", 0x452A, 0x20, 0,
                DataChunkName.YELLING);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Sleeping, transportation stuff, others, ",
                0x41e4, 0x21a, 0, DataChunkName.SLEEP_TRANSPORT);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Exclamations!, ", 0x454b, 0x27A, 0,
                DataChunkName.EXCLAIMS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Word of power locations (PERHAPS?!?)", 0x4512,
                0x10);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "Sprite index of replacement tile for word of power", 0x4513, 0xF);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x4aa5, 0x259);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Using and wearing item text", 0x48A5, 0x204,
                0, DataChunkName.WEAR_USE_ITEM);

            // dock coordinates (where purchased ships/skiffs are placed)
            // 0 = Jhelom
            // 1 = Minoc
            // 2 = East Brittany
            // 3 = Buccaneer's Den
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "x coordinate (dock)", 0x4D86, 0x4, 0x00,
                DataChunkName.X_DOCKS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "y coordinate (dock)", 0x4D8A, 0x4, 0x00,
                DataChunkName.Y_DOCKS);

            // this section contains information about hidden, non-regenerating objects (e.g. the magic axe in the dead tree in Jhelom); there are
            // only 0x71 such objects; the last entry in each table is 0
            // 0x3E88      0x72        object type (tile - 0x100)
            // 0x3EFA      0x72        object quality (e.g. potion type, number of gems)
            // 0x3F6C      0x72        location number (see "Party Location")
            // 0x3FDE      0x72        level
            // 0x4050      0x72        x coordinate
            // 0x40C2      0x72        y coordinate
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "object type (tile - 0x100)", 0x3E88, 0x72,
                0x100, DataChunkName.SEARCH_OBJECT_ID);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList,
                "object quality (e.g. potion type, number of gems)", 0x3EFA, 0x72,
                0x00, DataChunkName.SEARCH_OBJECT_QUALITY);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "location number (see \"Party Location\")",
                0x3F6C, 0x72,
                0x00, DataChunkName.SEARCH_OBJECT_LOCATION);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "level", 0x3FDE, 0x72,
                0x00, DataChunkName.SEARCH_OBJECT_FLOOR);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "x coordinate", 0x4050, 0x72,
                0x00, DataChunkName.SEARCH_OBJECT_X);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "y coordinate", 0x40C2, 0x72,
                0x00, DataChunkName.SEARCH_OBJECT_Y);

            // scan code translation table:
            // when the player presses a key that produces one of the scan codes in
            // the first table, the game translates it to the corresponding code in
            // the second table
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "scancodes", 0x541E, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "internal codes", 0x5426, 0x8);

            // begin bajh manual review
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts", 0x4e96, 0x263);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x50F9, 0x377);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts", 0x5470, 0x71);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x54e1, 0x83);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts", 0x5564, 0x49);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Nil", 0x55Ac, 0x1470);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Unknown", 0x6a1c, 0xce);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts", 0x6aea, 0xec, 0,
                DataChunkName.WORLD2);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Stat lines (z-stats?)", 0x6d08, 0x43);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Ultima IV", 0x6d48, 0xb);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Stat lines (z-stats?)", 0x6d08, 0x43);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "NPC Files", 0x6d56, 0x2e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Responses to keystroke actions", 0x6d84,
                0x179, 0, DataChunkName.RESPONSE_TO_KEYSTROKE);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Battle messages", 0x6efe, 0x112, 0x00,
                DataChunkName.BATTLE2);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "MISC file names", 0x7010, 0x1a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Unknown String", 0x702A, 0Xa);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Things said in jail", 0x7034, 0xb4);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "BRIT.DAT", 0x70E8, 0xa);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts", 0x70f2, 0xe0);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "KARMA.DAT", 0x71d2, 0xa);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Random texts (maybe power word?)", 0x71dc,
                0x2c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Strings about wishing well", 0x721c, 0x36);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "wishing for one of these keywords at a wishing well gets you a horse", 0x7252,
                0x32); // in the original definition
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Response Strings after wishing in wishing well", 0x7284, 0x27);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Fountain strings", 0x72ac, 0x54);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Time of day strings", 0x7300, 0x26);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Keep flame names", 0x7326, 0x18);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Dungeon names", 0x733e, 0x46);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Look2.dat (x2)", 0x7384, 0x14);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Signs?", 0x7398, 0x15e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Signs.dat (x2)", 0x74f6, 0x14);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Things you see (dungeons I think)", 0x750A,
                0x205, 0x00, DataChunkName.VISION2);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Drinking Strings", 0x76ef, 0x71);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Level up apparition strings", 0x7760, 0x94);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Karma.dat (x2)", 0x77f4, 0x14);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Level up apparition strings", 0x7808, 0x2e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Time of day", 0x7836, 0x1a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Time of day", 0x7836, 0x1a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Shoppe.dat", 0x7850, 0xc);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Magic shop strings", 0x785c, 0x1be);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Shoppe.dat", 0x7a1a, 0xc);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - General 1", 0x7a26, 0xa0,
                0x00, DataChunkName.SHOPPE_KEEPER_GENERAL);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - General 2 ", 0x785C, 0x168,
                0x00, DataChunkName.SHOPPE_KEEPER_GENERAL_2);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - Reagents Long Name ", 0x79C4,
                0x55, 0x00, DataChunkName.SHOPPE_KEEPER_REAGENTS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - do you want to buy?", 0x7ac6,
                0x4E, 0, DataChunkName.SHOPPE_KEEPER_DO_YOU_WANT);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - not enough money", 0x7b14,
                0x59, 0, DataChunkName.SHOPPE_KEEPER_NOT_ENOUGH_MONEY);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - various selling strings",
                0x7d44, 0x7f, 0, DataChunkName.SHOPPE_KEEPER_SELLING);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - what have you got for sale",
                0x7dc4, 0x7F, 0, DataChunkName.SHOPPE_KEEPER_WHATS_FOR_SALE);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "Shoppe.dat", 0x7f0a, 0xc);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - Healer shop strings", 0x806C,
                0x1a2, 0x00, DataChunkName.SHOPPE_KEEPER_HEALER);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - Healer shop strings", 0x3D6C,
                0x29, 0x00, DataChunkName.SHOPPE_KEEPER_HEALER2);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - blacksmith hello strings",
                0x7F58, 0x6A, 0, DataChunkName.SHOPPE_KEEPER_BLACKSMITH_HELLO);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Shoppe Keeper - blacksmith positive exclamation strings", 0x7FC2, 0x36, 0,
                DataChunkName.SHOPPE_KEEPER_BLACKSMITH_POS_EXCLAIM);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Shoppe Keeper - blacksmith 'we have' strings", 0x7FF8, 0x30, 0,
                DataChunkName.SHOPPE_KEEPER_BLACKSMITH_WE_HAVE);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - various string", 0x4d97,
                0x363, 0, DataChunkName.SHOPPE_KEEPER_INNKEEPER);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - various string", 0x4e96,
                0x264, 0, DataChunkName.SHOPPE_KEEPER_INNKEEPER_2);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - bar keeper gossip words",
                0x9ce4, 0x9B, 0, DataChunkName.BAR_KEEP_GOSSIP_WORDS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - bar keeper gossip people",
                0x9d80, 0xE7, 0, DataChunkName.BAR_KEEP_GOSSIP_PEOPLE);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - bar keeper gossip places",
                0x9e68, 0xA3, 0, DataChunkName.BAR_KEEP_GOSSIP_PLACES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - bar keeper random stuff",
                0x9f0e, 0xDE, 0, DataChunkName.SHOPPE_KEEPER_BAR_KEEP);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Shoppe Keeper - bar keeper gossip location map", 0x4cec, 0x1A, 0, DataChunkName.BAR_KEEP_GOSSIP_MAP);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Shoppe Keeper - bar keeper random stuff",
                0x9ab8, 0x227, 0, DataChunkName.SHOPPE_KEEPER_BAR_KEEP_2);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Inn room description text index", 0x4e7e, 0xc,
                0, DataChunkName.INN_DESCRIPTION_INDEXES);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Inn bed X-coordinate", 0x4e8a, 0x6, 0,
                DataChunkName.INN_BED_X_COORDS);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.ByteList, "Inn bed Y-coordinate", 0x4e90, 0x6, 0,
                DataChunkName.INN_BED_Y_COORDS);

            //SHOPPE_KEEPER_BAR_KEEP
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "end.dat", 0x820e, 0x8);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Numbers as strings (ie. twelfth)", 0x8216,
                0x17c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "End of game strings", 0x8392, 0xfe);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "miscmaps.dat", 0x8490, 0xe);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.SimpleString, "endmsg.dat", 0x849e, 0xc);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "random texts", 0x84aa, 0x74);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "finding/searching for things strings ",
                0x851e, 0x442, 0, DataChunkName.THINGS_I_FIND);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "where you found something (ie. In the wall) ", 0x8960, 0xe4);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "strings about unlocking or finding doors",
                0x8a44, 0x1b3, 0x00, DataChunkName.OPENING_THINGS_STUFF);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "potion colours", 0x8bfa, 0x34);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "scroll shortforms", 0x8c2e, 0x20);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "more found things!", 0x8c4e, 0x236, 0x00,
                DataChunkName.GET_THINGS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "getting things string!", 0x8c4e, 0x238);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "movement strings", 0x8e86, 0xed);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Mixing spells", 0x8f74, 0xbe);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Klimbing strings", 0x9026, 0x3A, 0x00,
                DataChunkName.KLIMBING);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "pay fine/bribe, merchant chat", 0x9062,
                0x1b2, 0x00, DataChunkName.CHIT_CHAT);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, ".tlk file list", 0x9216, 0x2e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Talking strings for ALL npcs", 0x9244, 0x1a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Dirty words", 0x925e, 0xda);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Common talking responses", 0x9338, 0x1cc, 0,
                DataChunkName.PHRASES_CONVERSATION);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Directions", 0x9504, 0x3e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "random texts", 0x9542, 0x2c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "random texts", 0x9542, 0x2c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "4 character shrine names (ie. hone)", 0x956e,
                0x30);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "shrine strings", 0x959e, 0x6e);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "MISCMAP*.* filenames", 0x960c, 0x29);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "urn strings", 0x9635, 0x33);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "save game strings", 0x9668, 0x21);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "OOL and SAV files", 0x968a, 0x32);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Inventory and Stats strings", 0x96BC, 0x12B,
                0, DataChunkName.ZSTATS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Inventory warnings", 0x97EA, 0x1c5, 0,
                DataChunkName.EQUIPPING);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Battle Strings", 0x99B0, 0x108, 0,
                DataChunkName.BATTLE);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Buying wine dialog", 0x9ab8, 0x22c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "4 character short form NPC questions, all quest related ", 0x9ce4, 0x9c);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList,
                "Character names, perhaps resistance people (hints?)", 0x9d80, 0x220);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Strings related to intro", 0xa020, 0x5a);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Character creation related strings", 0xa07a,
                0xa6);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Responses to key presses", 0xa120, 0x2a0,
                0x00, DataChunkName.KEYPRESS_COMMANDS);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Anti piracy messages", 0xa3c0, 0x170);
            _dataChunks.AddDataChunk(DataChunk.DataFormatType.StringList, "Final random strings", 0xa416, 0x44, 0,
                DataChunkName.ADDITIONAL);

            _dataChunks.AddDataChunk(DataChunk.DataFormatType.Unknown, "Nil", 0xA530, 0x1820);

            // load the super simple string lookup 
            StringReferences = new U5StringRef(this);
        }

        /// <summary>
        ///     Extracts a data chunk from the raw bytes
        /// </summary>
        /// <param name="dataType">format is the data in</param>
        /// <param name="description">a brief description of the data</param>
        /// <param name="offset">which offset to begin reading at</param>
        /// <param name="length">the number of bytes to read</param>
        /// <returns></returns>
        public DataChunk GetDataChunk(DataChunk.DataFormatType dataType, string description, int offset, int length) =>
            new(dataType, description, _dataChunks.FileByteList, offset, length);

        /// <summary>
        ///     Retrieve a data chunk by the name alone
        /// </summary>
        /// <param name="dataChunkName">chunk name</param>
        /// <returns>the associated datachunk</returns>
        public DataChunk GetDataChunk(DataChunkName dataChunkName) => _dataChunks.GetDataChunk(dataChunkName);

        // [0] = {string} "two"
        // [1] = {string} "three"
        // [2] = {string} "four"
        // [3] = {string} "five"
        // [4] = {string} "six"
        // [5] = {string} "sir"
        // [6] = {string} "milady"
        // [7] = {string} "That will be "
        // [8] = {string} " gold for the "
        // [9] = {string} " of ye,\n"
        // [10] = {string} ""\n\n"CAN'T PAY?\nBeat it!"\nyells "
        // [11] = {string} "\nEnjoy!"\n\n"
        // [12] = {string} "\n\n"I beg thy\npardon, "
        // [13] = {string} ","\nsays "
        // [14] = {string} ".\n"But haven't\nye had enough\nto drink?" "
        // [15] = {string} "Yes\n\n"
        // [16] = {string} "No!"
        // [17] = {string} ""Our wine list,\n"
        // [18] = {string} ".\n\n"
        // [19] = {string} "a) Rose.......18\n"
        // [20] = {string} "b) Claret....192\n"
        // [21] = {string} "c) Sauterne...79\n"
        // [22] = {string} "d) Muscatel...30\n"
        // [23] = {string} "e) Moselle...275\n"
        // [24] = {string} "f) Chablis....98\n\n"
        // [25] = {string} "Thy choice?" "
        // [26] = {string} "\n\n"Ah, a fine\nchoice, "
        // [27] = {string} ""\n\n"CAN'T PAY?\nBeat it!"\nyells "
        // [28] = {string} "\nEnjoy!""
        // [29] = {string} "\n\nHow many wouldst\nthou like?" "
        // [30] = {string} "\n\n"Hrumph.""
        // [31] = {string} "\n\n"
        // [32] = {string} ""Thou hast\nneither gold nor\nneed! Out!"\n"
        // [33] = {string} "yells "
        // [34] = {string} ".\n"
        // [35] = {string} ""Thou canst\nafford only "
        // [36] = {string} "!"\n\n"
        // [37] = {string} "\n\n"

        /// <summary>
        ///     Quicker method to get a specific string from a string list stored in a data chunk
        /// </summary>
        /// <param name="chunkName">String list chunk name</param>
        /// <param name="strIndex">index of string</param>
        /// <returns>string at the index specified</returns>
        public string GetStringFromDataChunkList(DataChunkName chunkName, int strIndex) =>
            GetDataChunk(chunkName).GetChunkAsStringList().StringList[strIndex];
    }
}