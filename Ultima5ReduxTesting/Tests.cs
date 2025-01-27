﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Ultima5Redux;
using Ultima5Redux.DayNightMoon;
using Ultima5Redux.Dialogue;
using Ultima5Redux.Maps;
using Ultima5Redux.MapUnits;
using Ultima5Redux.MapUnits.CombatMapUnits;
using Ultima5Redux.MapUnits.NonPlayerCharacters;
using Ultima5Redux.MapUnits.NonPlayerCharacters.ExtendedNpc;
using Ultima5Redux.MapUnits.NonPlayerCharacters.ShoppeKeepers;
using Ultima5Redux.MapUnits.SeaFaringVessels;
using Ultima5Redux.MapUnits.TurnResults;
using Ultima5Redux.MapUnits.TurnResults.SpecificTurnResults;
using Ultima5Redux.PlayerCharacters;
using Ultima5Redux.PlayerCharacters.CombatItems;
using Ultima5Redux.PlayerCharacters.Inventory;
using Ultima5Redux.References;
using Ultima5Redux.References.Dialogue;
using Ultima5Redux.References.Maps;
using Ultima5Redux.References.MapUnits.NonPlayerCharacters;
using Ultima5Redux.References.PlayerCharacters.Inventory;
using Ultima5Redux.References.PlayerCharacters.Inventory.SpellSubTypes;

// ReSharper disable UnusedVariable
// ReSharper disable RedundantAssignment
// ReSharper disable NotAccessedVariable
// ReSharper disable RedundantArgumentDefaultValue

namespace Ultima5ReduxTesting
{
    [TestFixture]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class Tests
    {
        [SetUp] public void Setup()
        {
            // TestContext.Out.WriteLine("CWD: " + Directory.GetCurrentDirectory());
            // TestContext.Out.WriteLine("CWD Dump: " + Directory.EnumerateDirectories(Directory.GetCurrentDirectory()));
            // OutputDirectories(Directory.GetCurrentDirectory());
        }

        [TearDown] public void TearDown()
        {
        }

        public enum SaveFiles
        {
            Britain, Britain2, Britain3, BucDen1, BucDen3, b_carpet, b_frigat, b_horse, b_skiff, quicksave, fresh,
            blackt, BucDenEntrance, brandnew
        }

        // ReSharper disable once UnusedMember.Local
        private void OutputDirectories(string dir)
        {
            foreach (string subDir in Directory.EnumerateDirectories(dir))
            {
                TestContext.Out.WriteLine(subDir);
            }
        }

        private string GetSaveDirectory(SaveFiles saveFiles)
        {
            if (!Enum.IsDefined(typeof(SaveFiles), saveFiles))
                throw new InvalidEnumArgumentException(nameof(saveFiles), (int)saveFiles, typeof(SaveFiles));
            return Path.Combine(SaveRootDirectory, saveFiles.ToString());
        }

        private string DataDirectory => TestContext.Parameters.Get("DataDirectory",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? @"/Users/bradhannah/GitHub/Ultima5ReduxTestDependancies/Saves/Britain2"
                //@"/Users/bradhannah/games/u5tests/Britain2"
                : @"C:\games\ultima5tests\Britain2");

        private string SaveRootDirectory =>
            TestContext.Parameters.Get("SaveRootDirectory",
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                    ? @"/Users/bradhannah/GitHub/Ultima5ReduxTestDependancies/Saves"
                    //@"/Users/bradhannah/games/u5tests"
                    : @"C:\games\ultima5tests");

        private string NewSaveRootDirectory => TestContext.Parameters.Get("NewSaveRootDirectory",
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)
                ? @"/Users/bradhannah/GitHub/Ultima5ReduxTestDependancies/NewSaves"
                //@"/Users/bradhannah/games/u5tests"
                : @"C:\games\ultima5tests");
        //Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "UltimaVRedux"));

        private string GetNewSaveDirectory(SaveFiles saveFiles) =>
            Path.Combine(NewSaveRootDirectory, saveFiles.ToString());

        private World CreateWorldFromLegacy(SaveFiles saveFiles, bool bUseExtendedSprites = true,
            bool bLoadInitGam = false) =>
            new(true, GetSaveDirectory(saveFiles), DataDirectory,
                bUseExtendedSprites, bLoadInitGam);

        private World CreateWorldFromNewSave(SaveFiles saveFiles, bool bUseExtendedSprites = true,
            bool bLoadInitGam = false) =>
            new(false, Path.Combine(NewSaveRootDirectory, saveFiles.ToString()), DataDirectory,
                bUseExtendedSprites, bLoadInitGam);

        [Test] [TestCase(SaveFiles.Britain)] public void AllSmallMapsLoadTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";
            foreach (SmallMapReferences.SingleMapReference smr in GameReferences.Instance.SmallMapRef.MapReferenceList)
            {
                SmallMapReferences.SingleMapReference singleMap =
                    GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(smr.MapLocation, smr.Floor);
                // we don't test dungeon maps here
                if (singleMap.MapType == Map.Maps.Dungeon) continue;
                world.State.TheVirtualMap.LoadSmallMap(singleMap);
            }

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_LoadTrinsicSmallMapsLoadTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Trinsic, 0));

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_LoadMinocBuyFrigateAndCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Minoc,
                    0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            world.State.TheVirtualMap.CreateFrigateAtDock(
                SmallMapReferences.SingleMapReference.Location.Minoc);
            Point2D dockLocation =
                LargeMapLocationReferences.GetLocationOfDock(SmallMapReferences.SingleMapReference.Location.Minoc);
            List<MapUnit> mapUnits =
                world.State.TheVirtualMap.TheMapHolder.OverworldMap.GetMapUnitsByPosition(dockLocation, 0);

            var frigate2 =
                world.State.TheVirtualMap.TheMapHolder.OverworldMap.GetSpecificMapUnitByLocation<Frigate>(dockLocation,
                    0);
            Assert.True(frigate2 != null);

            Assert.True(
                world.State.TheVirtualMap.IsShipOccupyingDock(SmallMapReferences.SingleMapReference.Location.Minoc));

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
            {
                Debug.Assert(false, "after large map load it did not become a large map");
                throw new Exception();
            }

            largeMap.MoveAvatar(new Point2D(frigate2.MapUnitPosition.X, frigate2.MapUnitPosition.Y));
            var turnResults = new TurnResults();
            world.TryToBoard(out bool bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);

            Assert.True(frigate2 != null);
            Assert.True(mapUnits[0] is Frigate);
            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.BucDen3)] public void test_LoadMinocBuySkiffAndCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Minoc,
                    0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            world.State.TheVirtualMap.CreateSkiffAtDock(
                SmallMapReferences.SingleMapReference.Location.Minoc);
            Point2D dockLocation =
                LargeMapLocationReferences.GetLocationOfDock(SmallMapReferences.SingleMapReference.Location.Minoc);
            List<MapUnit> mapUnits = world.State.TheVirtualMap.CurrentMap.GetMapUnitsByPosition(dockLocation, 0);

            var skiff =
                world.State.TheVirtualMap.TheMapHolder.OverworldMap
                    .GetSpecificMapUnitByLocation<Skiff>(dockLocation, 0);
            Assert.IsNotNull(skiff);
            Assert.True(
                world.State.TheVirtualMap.IsShipOccupyingDock(SmallMapReferences.SingleMapReference.Location.Minoc));

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            largeMap.MoveAvatar(new Point2D(skiff.MapUnitPosition.X,
                skiff.MapUnitPosition.Y)); //-V3095
            var turnResults = new TurnResults();

            world.TryToBoard(out bool bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);

            Assert.True(skiff != null);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_LoadSkaraBraeSmallMapsLoadTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Skara_Brae, 0));

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void LoadBritishBasement(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 0));
            int i = 24 * (60 / 2);
            while (i > 0)
            {
                var turnResults = new TurnResults();
                world.AdvanceTime(2, turnResults);
                i--;
            }

            TestContext.Out.Write("Ending ");

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void AllSmallMapsLoadWithOneDayTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            foreach (SmallMapReferences.SingleMapReference smr in GameReferences.Instance.SmallMapRef.MapReferenceList)
            {
                Debug.WriteLine("***** Loading " + smr.MapLocation + " on floor " + smr.Floor);
                SmallMapReferences.SingleMapReference singleMapReference =
                    GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(smr.MapLocation, smr.Floor);
                if (smr.MapLocation == SmallMapReferences.SingleMapReference.Location.Britannia_Underworld)
                    throw new Ultima5ReduxException("OOOF");
                if (singleMapReference.MapType == Map.Maps.Dungeon) continue;
                world.State.TheVirtualMap.LoadSmallMap(
                    GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(smr.MapLocation, smr.Floor));

                int i = 24 * (60 / 2);
                //i = 1;
                while (i > 0)
                {
                    var turnResults = new TurnResults();
                    world.AdvanceTime(2, turnResults);
                    i--;
                }

                Debug.WriteLine("***** Ending " + smr.MapLocation + " on floor " + smr.Floor);
            }
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void CarpetOverworldDayWandering(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            world.MonsterAi = false;
            int i = 24 * (60 / 2) / 4;
            while (i > 0)
            {
                var turnResults = new TurnResults();
                world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                i--;
            }

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void SingleSmallMapWithDayWandering(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 0));
            int i = 24 * (60 / 2) / 4;
            while (i > 0)
            {
                var turnResults = new TurnResults();

                world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults,
                    true);
                world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                    .CurrentPosition
                    .XY);
                i--;
            }

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TileOverrides(SaveFiles saveFiles)
        {
            var to = new TileOverrideReferences();

            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lycaeum, 1));

            world.State.TheVirtualMap.CurrentMap.GuessTile(new Point2D(14, 7));
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_LoadOverworld(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_LoadOverworldOverrideTile(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            world.State.TheVirtualMap.CurrentMap.GuessTile(new Point2D(81, 106));
        }

        [Test] public void Test_InventoryReferences()
        {
            var invRefs = new InventoryReferences();
            List<InventoryReference> invList =
                invRefs.GetInventoryReferenceList(InventoryReferences.InventoryReferenceType.Armament);
            foreach (InventoryReference invRef in invList)
            {
                string str = invRef.GetRichTextDescription();
                str = invRefs.HighlightKeywords(str);
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_PushPull_WontBudge(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Britain, 0));
            var turnResults = new TurnResults();
            world.TryToPushAThing(new Point2D(5, 7), Point2D.Direction.Down, out bool bWasPushed, turnResults);
            Assert.False(bWasPushed);

            world.TryToPushAThing(new Point2D(22, 2), Point2D.Direction.Left, out bWasPushed, turnResults);
            Assert.True(bWasPushed);
            string derp = world.State.Serialize();

            world.TryToPushAThing(new Point2D(2, 8), Point2D.Direction.Right, out bWasPushed, turnResults);
            Assert.True(bWasPushed);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_FreeMoveAcrossWorld(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            world.MonsterAi = false;
            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            Point2D startLocation = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY.Copy();

            for (int i = 0; i < 256; i++)
            {
                var turnResults = new TurnResults();

                world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, true, turnResults);
            }

            Point2D finalLocation = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY.Copy();

            Assert.True(finalLocation == startLocation);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_CheckAlLTilesForMoongates(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            Point2D startLocation = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY.Copy();
            for (int x = 0; x < 256; x++)
            {
                for (int y = 0; y < 256; y++)
                {
                    TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(x, y);
                }
            }

            Point2D finalLocation = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY.Copy();

            Assert.True(finalLocation == startLocation);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_LookupMoonstoneInInventory(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            foreach (MoonPhaseReferences.MoonPhases phase in Enum.GetValues(typeof(MoonPhaseReferences.MoonPhases)))
            {
                if (phase == MoonPhaseReferences.MoonPhases.NoMoon) continue;
                bool bBuried = world.State.TheMoongates.IsMoonstoneBuried((int)phase);
                int nMoonstonesInInv = world.State.PlayerInventory.TheMoonstones.Items[phase].Quantity;
                string desc = GameReferences.Instance.InvRef.GetInventoryReference(
                    InventoryReferences.InventoryReferenceType.Item,
                    phase.ToString()).ItemDescription;
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_GetAndUseMoonstone(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            world.State.TheTimeOfDay.Hour = 12;

            var moongatePosition = new Point2D(166, 19);
            world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = moongatePosition;
            // first search should find moonstone
            var turnResults = new TurnResults();

            world.TryToSearch(moongatePosition, out bool bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);

            world.TryToGetAThing(moongatePosition, out bWasSuccessful, out InventoryItem item, turnResults,
                Point2D.Direction.Down);
            Assert.True(bWasSuccessful);
            Assert.True(item != null);
            Assert.True(item.GetType() == typeof(Moonstone));

            world.TryToUseMoonstone((Moonstone)item, out bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_SearchForMoonstoneAndGet(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            world.State.TheTimeOfDay.Hour = 12;

            var moongatePosition = new Point2D(166, 19);
            // first search should find moonstone
            var turnResults = new TurnResults();

            world.TryToSearch(moongatePosition, out bool bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);
            // second search should be empty
            world.TryToSearch(moongatePosition, out bWasSuccessful, turnResults);
            Assert.True(!bWasSuccessful);
            string derp = world.State.Serialize();

            TileReference tileRef = world.State.TheVirtualMap.CurrentMap.GetTileReference(moongatePosition);
            MapUnit moonstoneMapUnit =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(moongatePosition, true);
            Assert.True(moonstoneMapUnit is MoonstoneNonAttackingUnit);

            int nSprite = world.State.TheVirtualMap.CurrentMap.GuessTile(moongatePosition);

            // can't get it twice!
            world.TryToGetAThing(moongatePosition, out bWasSuccessful, out InventoryItem item, turnResults,
                Point2D.Direction.Down);
            Assert.True(bWasSuccessful);
            Assert.True(item != null);
            world.TryToGetAThing(moongatePosition, out bWasSuccessful, out item, turnResults, Point2D.Direction.Down);
            Assert.True(!bWasSuccessful);
            Assert.True(item == null);

            world.TryToSearch(moongatePosition, out bWasSuccessful, turnResults);
            Assert.True(!bWasSuccessful);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_MoongateHunting(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = new Point2D(167, 22);
            bool bOnMoongate = world.IsAvatarOnActiveMoongate();
            world.State.TheTimeOfDay.Hour = 23;
            world.State.TheTimeOfDay.Day = 1;
            for (int i = 1; i <= 28; i++)
            {
                world.State.TheTimeOfDay.Day = (byte)i;
                world.State.TheTimeOfDay.Hour = 23;
                Point3D p3d = world.GetMoongateTeleportLocation();
                world.State.TheTimeOfDay.Hour = 4;
                p3d = world.GetMoongateTeleportLocation();
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TestCorrectMoons(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            var moonPhaseReferences = new MoonPhaseReferences(GameReferences.Instance.DataOvlRef);

            world.State.TheTimeOfDay.Day = 25;
            world.State.TheTimeOfDay.Hour = 4;

            MoonPhaseReferences.MoonPhases trammelPhase =
                moonPhaseReferences.GetMoonPhasesByTimeOfDay(world.State.TheTimeOfDay,
                    MoonPhaseReferences.MoonsAndSun.Trammel);
            MoonPhaseReferences.MoonPhases feluccaPhase =
                moonPhaseReferences.GetMoonPhasesByTimeOfDay(world.State.TheTimeOfDay,
                    MoonPhaseReferences.MoonsAndSun.Felucca);
            Assert.True(trammelPhase == MoonPhaseReferences.MoonPhases.GibbousWaning);
            Assert.True(feluccaPhase == MoonPhaseReferences.MoonPhases.LastQuarter);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_MoonPhaseReference(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            var moonPhaseReferences = new MoonPhaseReferences(GameReferences.Instance.DataOvlRef);

            for (byte nDay = 1; nDay <= 28; nDay++)
            {
                for (byte nHour = 0; nHour < 24; nHour++)
                {
                    world.State.TheTimeOfDay.Day = nDay;
                    world.State.TheTimeOfDay.Hour = nHour;

                    MoonPhaseReferences.MoonPhases moonPhase =
                        moonPhaseReferences.GetMoonGateMoonPhase(world.State.TheTimeOfDay);

                    float fMoonAngle = MoonPhaseReferences.GetMoonAngle(world.State.TheTimeOfDay);
                    Assert.True(fMoonAngle >= 0 && fMoonAngle < 360);
                }
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TalkToSmith(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var location =
                SmallMapReferences.SingleMapReference.Location.Iolos_Hut;
            NonPlayerCharacterState npcState =
                world.State.TheNonPlayerCharacterStates.GetStateByLocationAndIndex(location, 4);

            world.CreateConversationAndBegin(npcState, OnUpdateOfEnqueuedScriptItem);
        }

        private static void OnUpdateOfEnqueuedScriptItem(Conversation conversation)
        {
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_KlimbMountain(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = new Point2D(166, 21);
            var turnResults = new TurnResults();
            world.TryToKlimb(out World.KlimbResult klimbResult, turnResults);
            Assert.True(klimbResult == World.KlimbResult.RequiresDirection);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_MoveALittle(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = new Point2D(166, 21);
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_MoveALittleOverworld(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = new Point2D(166, 21);
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults);
            world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults);
            world.TryToMoveNonCombatMap(Point2D.Direction.Right, false, false, turnResults);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TalkToDelwyn(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            var location =
                SmallMapReferences.SingleMapReference.Location.Minoc;

            NonPlayerCharacterState npcState =
                world.State.TheNonPlayerCharacterStates.GetStateByLocationAndIndex(location, 9);

            Conversation convo = world.CreateConversationAndBegin(npcState,
                OnUpdateOfEnqueuedScriptItemHandleDelwyn);
            convo.BeginConversation();
            convo.AddUserResponse("yes");
            convo.AddUserResponse("yes");
            convo.AddUserResponse("yes");
            convo.AddUserResponse("bye");

            Conversation convo2 = world.CreateConversationAndBegin(npcState,
                OnUpdateOfEnqueuedScriptItemHandleDelwyn);
            convo2.BeginConversation();
            convo2.AddUserResponse("yes");
            convo2.AddUserResponse("yes");
            convo2.AddUserResponse("yes");
            convo2.AddUserResponse("bye");
        }

        private void OnUpdateOfEnqueuedScriptItemHandleDelwyn(Conversation conversation)
        {
            TalkScript.ScriptItem item = conversation.DequeueFromOutputBuffer();
            switch (item.Command)
            {
                case TalkScript.TalkCommand.PlainString:
                    Debug.WriteLine(item.StringData);
                    break;
                case TalkScript.TalkCommand.AvatarsName:
                    break;
                case TalkScript.TalkCommand.EndConversation:
                    break;
                case TalkScript.TalkCommand.Pause:
                    break;
                case TalkScript.TalkCommand.JoinParty:
                    break;
                case TalkScript.TalkCommand.Gold:
                    break;
                case TalkScript.TalkCommand.Change:
                    break;
                case TalkScript.TalkCommand.Or:
                    break;
                case TalkScript.TalkCommand.AskName:
                    break;
                case TalkScript.TalkCommand.KarmaPlusOne:
                    break;
                case TalkScript.TalkCommand.KarmaMinusOne:
                    break;
                case TalkScript.TalkCommand.CallGuards:
                    break;
                case TalkScript.TalkCommand.IfElseKnowsName:
                    break;
                case TalkScript.TalkCommand.NewLine:
                    break;
                case TalkScript.TalkCommand.Rune:
                    break;
                case TalkScript.TalkCommand.KeyWait:
                    break;
                case TalkScript.TalkCommand.StartLabelDefinition:
                    break;
                case TalkScript.TalkCommand.StartNewSection:
                    break;
                case TalkScript.TalkCommand.EndScript:
                    break;
                case TalkScript.TalkCommand.GotoLabel:
                    break;
                case TalkScript.TalkCommand.DefineLabel:
                    break;
                case TalkScript.TalkCommand.DoNothingSection:
                    break;
                case TalkScript.TalkCommand.PromptUserForInput_NPCQuestion:
                    // userResponse = "yes";
                    // conversation.AddUserResponse(userResponse);
                    break;
                case TalkScript.TalkCommand.PromptUserForInput_UserInterest:
                    break;
                case TalkScript.TalkCommand.UserInputNotRecognized:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Test] [TestCase(SaveFiles.Britain2, false)] [TestCase(SaveFiles.Britain2, true)]
        public void Test_BasicBlackSmithDialogue(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            if (bReloadJson) world.ReLoadFromJson();

            var blacksmith = GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Minoc,
                NonPlayerCharacterReference.SpecificNpcDialogType.Blacksmith, null,
                world.State.PlayerInventory) as BlackSmith;

            Assert.True(blacksmith != null, nameof(blacksmith) + " != null");
            string purchaseStr2 = blacksmith.GetEquipmentBuyingOutput(DataOvlReference.Equipment.LeatherHelm, 100);
            string purchaseStr = blacksmith.GetEquipmentBuyingOutput(DataOvlReference.Equipment.AmuletOfTurning, 100);

            for (int i = 0; i < 10; i++)
            {
                string pissedOff = blacksmith.GetPissedOffShoppeKeeperGoodbyeResponse();
                string happy = blacksmith.GetHappyShoppeKeeperGoodbyeResponse();
                string selling = blacksmith.GetEquipmentSellingOutput(100, "Big THING");
                string buying =
                    blacksmith.GetEquipmentBuyingOutput(DataOvlReference.Equipment.Arrows, 100);
            }

            string hello = blacksmith.GetHelloResponse(world.State.TheTimeOfDay);
            blacksmith.GetForSaleList();
            _ = blacksmith.GetDoneResponse();
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_BasicHealerDialogue(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var healer = (Healer)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Cove,
                NonPlayerCharacterReference.SpecificNpcDialogType.Healer, null,
                world.State.PlayerInventory);

            _ = healer.NoNeedForMyArt();

            int price = healer.GetPrice(Healer.RemedyTypes.Heal);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_BasicTavernDialogue(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var barKeeper = (BarKeeper)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Paws,
                NonPlayerCharacterReference.SpecificNpcDialogType.Barkeeper, null, world.State.PlayerInventory);

            string myOppressionTest = barKeeper.GetGossipResponse("oppr", true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_BasicMagicSellerDialogue(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var magicSeller = (MagicSeller)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Cove,
                NonPlayerCharacterReference.SpecificNpcDialogType.MagicSeller, null, world.State.PlayerInventory);
            List<Reagent> reagents = magicSeller.GetReagentsForSale();

            int price1 = reagents[0].GetAdjustedBuyPrice(world.State.CharacterRecords,
                SmallMapReferences.SingleMapReference.Location.Cove);
            int price2 = reagents[1].GetAdjustedBuyPrice(world.State.CharacterRecords,
                SmallMapReferences.SingleMapReference.Location.Cove);

            string hello = magicSeller.GetHelloResponse(world.State.TheTimeOfDay);
            string buyThing = magicSeller.GetReagentBuyingOutput(reagents[0]);
            buyThing = magicSeller.GetReagentBuyingOutput(reagents[1]);
            buyThing = magicSeller.GetReagentBuyingOutput(reagents[2]);
            buyThing = magicSeller.GetReagentBuyingOutput(reagents[3]);
            buyThing = magicSeller.GetReagentBuyingOutput(reagents[4]);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_AdjustedMerchantPrices(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            int nCrossbowBuy = world.State.PlayerInventory.TheWeapons.Items[WeaponReference.SpecificWeaponType.Crossbow]
                .GetAdjustedBuyPrice(world.State.CharacterRecords,
                    ((RegularMap)world.State.TheVirtualMap.CurrentMap).CurrentSingleMapReference.MapLocation);
            int nCrossbowSell = world.State.PlayerInventory.TheWeapons
                .Items[WeaponReference.SpecificWeaponType.Crossbow]
                .GetAdjustedSellPrice(world.State.CharacterRecords,
                    ((RegularMap)world.State.TheVirtualMap.CurrentMap).CurrentSingleMapReference.MapLocation);
            Assert.True(nCrossbowBuy > 0);
            Assert.True(nCrossbowSell > 0);

            int nKeysPrice = world.State.PlayerInventory.TheProvisions
                .Items[ProvisionReferences.SpecificProvisionType.Keys]
                .GetAdjustedBuyPrice(world.State.CharacterRecords,
                    SmallMapReferences.SingleMapReference.Location.Buccaneers_Den);
            var guildMaster = (GuildMaster)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Buccaneers_Den,
                NonPlayerCharacterReference.SpecificNpcDialogType.GuildMaster, null, world.State.PlayerInventory);
            string buyKeys = guildMaster.GetProvisionBuyOutput(ProvisionReferences.SpecificProvisionType.Keys, 240);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_SimpleStringTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            string str =
                GameReferences.Instance.DataOvlRef.StringReferences.GetString(DataOvlReference.Battle2Strings
                    .N_VICTORY_BANG_N);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_ShipwrightDialogue(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var shipwright = (Shipwright)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Buccaneers_Den,
                NonPlayerCharacterReference.SpecificNpcDialogType.Shipwright, null, world.State.PlayerInventory);

            string hi = shipwright.GetHelloResponse(world.State.TheTimeOfDay);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void Test_EnterBuilding(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";
            var turnResults = new TurnResults();

            world.TryToEnterBuilding(new Point2D(159, 20), out bool bWasSuccessful, turnResults);

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void Test_EnterYewAndLookAtMonster(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            // yew
            var turnResults = new TurnResults();
            world.TryToEnterBuilding(new Point2D(58, 43), out bool bWasSuccessful, turnResults);

            foreach (MapUnit mapUnit in world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.AllMapUnits)
            {
                if (mapUnit is DiscoverableLoot) continue;
                _ = mapUnit.GetNonBoardedTileReference();
            }

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.BucDen1)] public void Test_GetInnStuffAtBucDen(SaveFiles saveFiles)
        {
            // World world = new World(SaveDirectory);
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            var turnResults = new TurnResults();
            world.TryToEnterBuilding(new Point2D(159, 20), out bool bWasSuccessful, turnResults);

            var innKeeper = (Innkeeper)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Buccaneers_Den,
                NonPlayerCharacterReference.SpecificNpcDialogType.InnKeeper, null, world.State.PlayerInventory);

            Point2D bedPosition = innKeeper.InnKeeperServices.SleepingPosition;

            IEnumerable<PlayerCharacterRecord> records =
                world.State.CharacterRecords.GetPlayersAtInn(SmallMapReferences.SingleMapReference.Location
                    .Buccaneers_Den);

            string noRoom = innKeeper.GetNoRoomAtTheInn(world.State.CharacterRecords);
            foreach (PlayerCharacterRecord record in records)
            {
                world.State.CharacterRecords.JoinPlayerCharacter(record);
            }

            List<PlayerCharacterRecord> activeRecords = world.State.CharacterRecords.GetActiveCharacterRecords();

            string goodbye = innKeeper.GetPissedOffShoppeKeeperGoodbyeResponse();
            string pissed = innKeeper.GetPissedOffNotEnoughMoney();
            string howMuch = innKeeper.GetThatWillBeGold(activeRecords[1]);
            string shoppeName = innKeeper.TheShoppeKeeperReference.ShoppeName;

            _ = world.State.CharacterRecords.GetCharacterFromParty(4);
            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain3)] public void Test_GetInnStuffAtBritain(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            var turnResults = new TurnResults();
            world.TryToEnterBuilding(new Point2D(159, 20), out bool bWasSuccessful, turnResults);

            var innKeeper = (Innkeeper)GameReferences.Instance.ShoppeKeeperDialogueReference.GetShoppeKeeper(
                SmallMapReferences.SingleMapReference.Location.Britain,
                NonPlayerCharacterReference.SpecificNpcDialogType.InnKeeper, null, world.State.PlayerInventory);

            Point2D bedPosition = innKeeper.InnKeeperServices.SleepingPosition;

            IEnumerable<PlayerCharacterRecord> records =
                world.State.CharacterRecords.GetPlayersAtInn(SmallMapReferences.SingleMapReference.Location.Britain);

            string noRoom = innKeeper.GetNoRoomAtTheInn(world.State.CharacterRecords);
            foreach (PlayerCharacterRecord record in records)
            {
                world.State.CharacterRecords.JoinPlayerCharacter(record);
            }

            List<PlayerCharacterRecord> activeRecords = world.State.CharacterRecords.GetActiveCharacterRecords();

            string goodbye = innKeeper.GetPissedOffShoppeKeeperGoodbyeResponse();
            string pissed = innKeeper.GetPissedOffNotEnoughMoney();
            string howmuch = innKeeper.GetThatWillBeGold(activeRecords[1]);
            string shoppename = innKeeper.TheShoppeKeeperReference.ShoppeName;
            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void Test_MakeAHorse(SaveFiles saveFiles)
        {
            // World world = new World(SaveDirectory);
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            var turnResults = new TurnResults();
            world.TryToEnterBuilding(new Point2D(159, 20), out bool bWasSuccessful, turnResults);

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            Horse horse = smallMap.CreateHorseAroundAvatar(turnResults);
            Assert.True(horse != null);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_MoveWithExtendedSprites(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            world.MonsterAi = false;

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            Avatar avatar = largeMap.GetAvatarMapUnit();
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            // make sure it is using the extended sprite
            //GetCurrentTileReference

            Assert.True(largeMap.GetAvatarMapUnit().CurrentBoardedMapUnit
                .GetBoardedTileReference()
                .Index == 515);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_CheckedBoardedTileCarpet(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            Avatar avatar = largeMap.GetAvatarMapUnit();
            Assert.True(avatar.IsAvatarOnBoardedThing);
            Assert.True(avatar.CurrentBoardedMapUnit != null);

            int nCarpets = world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet]
                .Quantity;
            var turnResults = new TurnResults();
            world.TryToXit(out bool bWasSuccessful, turnResults);
            Assert.True(nCarpets == world.State.PlayerInventory.SpecializedItems
                .Items[SpecialItem.SpecificItemType.Carpet].Quantity);
            Assert.True(bWasSuccessful);
            world.TryToBoard(out bool bWasSuccessfulBoard, turnResults);
            Assert.True(bWasSuccessfulBoard);
            world.TryToXit(out bWasSuccessful, turnResults);

            nCarpets = world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet]
                .Quantity;
            Point2D curPos = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY;
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults);
            world.TryToGetAThing(curPos, out bool bGotACarpet, out InventoryItem carpet, turnResults,
                Point2D.Direction.Down);
            Assert.True(bGotACarpet);
            //Assert.True(carpet!=null);
            Assert.True(nCarpets + 1 == world.State.PlayerInventory.SpecializedItems
                .Items[SpecialItem.SpecificItemType.Carpet].Quantity);
            world.TryToGetAThing(curPos, out bGotACarpet, out carpet, turnResults, Point2D.Direction.Down);
            Assert.True(!bGotACarpet);

            world.TryToUseSpecialItem(
                world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet],
                out bool bAbleToUseItem, turnResults);
            Assert.True(bAbleToUseItem);
            world.TryToXit(out bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults);
            curPos = world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY;
            world.TryToGetAThing(curPos, out bGotACarpet, out carpet, turnResults, Point2D.Direction.Down);
            Assert.True(bGotACarpet);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.BucDen3)] public void Test_CheckUseCarpet(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            int nCarpets = world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet]
                .Quantity;
            var turnResults = new TurnResults();

            world.TryToUseSpecialItem(
                world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet],
                out bool bAbleToUseItem, turnResults);
            Assert.True(bAbleToUseItem);
            Assert.True(world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet]
                .Quantity == nCarpets - 1);
        }

        [Test] [TestCase(SaveFiles.b_horse)] public void Test_CheckedBoardedTileHorse(SaveFiles saveFiles)
        {
            Utils.Ran = new Random(4);
            World world = CreateWorldFromLegacy(saveFiles);

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be large map");

            Avatar avatar = smallMap.GetAvatarMapUnit();
            Assert.True(avatar.IsAvatarOnBoardedThing);
            Assert.True(avatar.CurrentBoardedMapUnit != null);

            var turnResults = new TurnResults();
            world.TryToXit(out bool bWasSuccessful, turnResults);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_skiff)] public void Test_CheckedBoardedTileSkiff(SaveFiles saveFiles)
        {
            // World world = new World(SaveDirectory);
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            Avatar avatar = largeMap.GetAvatarMapUnit();
            Assert.True(avatar.IsAvatarOnBoardedThing);
            Assert.True(avatar.CurrentBoardedMapUnit != null);

            var turnResults = new TurnResults();
            world.TryToXit(out bool bWasSuccessful, turnResults);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_skiff)] public void Test_CheckedBoardedTileSkiffMoveOntoSkiff(SaveFiles saveFiles)
        {
            // World world = new World(SaveDirectory);
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            Avatar avatar = largeMap.GetAvatarMapUnit();
            Assert.True(avatar.IsAvatarOnBoardedThing);
            Assert.True(avatar.CurrentBoardedMapUnit != null);
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, true, turnResults);
            //Assert.True(moveResult == World.TryToMoveResult.Blocked);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_frigat)] public void Test_CheckedBoardedTileFrigate(SaveFiles saveFiles)
        {
            // World world = new World(SaveDirectory);
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            Avatar avatar = largeMap.GetAvatarMapUnit();
            Assert.True(avatar.IsAvatarOnBoardedThing);
            Assert.True(avatar.CurrentBoardedMapUnit != null);

            var turnResults = new TurnResults();

            world.TryToXit(out bool bWasSuccessful, turnResults);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.BucDen1)] public void Test_ForceVisibleRecalculationInBucDen(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var startSpot = new Point2D(159, 20);
            var turnResults = new TurnResults();
            world.TryToEnterBuilding(startSpot, out bool bWasSuccessful, turnResults);

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                .CurrentPosition.XY);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_ForceVisibleRecalculationInLargeMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            largeMap.MoveAvatar(new Point2D(128, 0));
            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                .CurrentPosition.XY);
        }

        [Test] [TestCase(SaveFiles.b_carpet)]
        public void Test_LookupPrimaryAndSecondaryEnemyReferences(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            EnemyReference enemyReference =
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448));

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    0),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords, enemyReference);

            EnemyReference secondEnemyReference = GameReferences.Instance.EnemyRefs.GetFriendReference(enemyReference);
            //GetEnemyReference(enemyReference.FriendIndex);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadCampFireCombatMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    0),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords,
                // orcs
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448)),
                1,
                // troll
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(484)),
                1);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;

            Assert.True(player.Record.Class == PlayerCharacterRecord.CharacterClass.Avatar);
            var turnResults = new TurnResults();
            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Left, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);
            // Assert.True(tryToMoveResult == World.TryToMoveResult.Blocked);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon0WithDivide(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon, 0),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);

            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;

            Assert.NotNull(combatMap);
            combatMap.DivideEnemy(combatMap.AllEnemies.ToList()[2]);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_HammerCombatMapInitiativeTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // right sided hammer
            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon, 4),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            var turnResults = new TurnResults();
            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Left, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_EscapeCombatMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    4),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            var turnResults = new TurnResults();

            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Left, turnResults, false);
            combatMap.AdvanceToNextCombatMapUnit();
            world.TryToMoveCombatMap(Point2D.Direction.Up, turnResults, false);

            do
            {
                combatMap.NextCharacterEscape(out CombatPlayer combatPlayer);
                CombatPlayer newCombatPlayer = combatMap.CurrentCombatPlayer;

                if (combatPlayer == null) break;
            } while (true);

            world.State.TheVirtualMap.ReturnToPreviousMapAfterCombat();
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadFirstCombatMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    11),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords,
                // orcs
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448)),
                5,
                // troll
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(484)),
                1);

            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            {
                Assert.True(player.Record.Class == PlayerCharacterRecord.CharacterClass.Avatar);
                var turnResults = new TurnResults();

                world.TryToMoveCombatMap(player, Point2D.Direction.Up, turnResults, false);
            }

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadCombatMapThenBack(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            var turnResults = new TurnResults();

            world.TryToEnterBuilding(new Point2D(159, 20), out bool bWasSuccessful, turnResults);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    2),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords,
                // orcs
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448)),
                5,
                // troll
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(484)),
                1);

            IEnumerable<SingleCombatMapReference.EntryDirection> thing = GameReferences.Instance.CombatMapRefs
                .GetSingleCombatMapReference(SingleCombatMapReference.Territory.Britannia, 2)
                .GetEntryDirections();

            world.State.TheVirtualMap.ReturnToPreviousMapAfterCombat();

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadAllCombatMapsWithMonsters(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            //List<CombatMap> worldMaps = new List<CombatMap>();
            for (int i = 0; i < 16; i++)
            {
                SingleCombatMapReference singleCombatMapReference =
                    GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                        SingleCombatMapReference.Territory.Britannia, i);
                foreach (SingleCombatMapReference.EntryDirection worldEntryDirection in singleCombatMapReference
                             .GetEntryDirections())
                {
                    world.State.TheVirtualMap.LoadCombatMap(
                        GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                            SingleCombatMapReference.Territory.Britannia,
                            i),
                        worldEntryDirection, world.State.CharacterRecords);
                    TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);
                }
            }

            EnemyReference enemy1 =
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(320));

            // for (int i = 0; i < 112; i++)
            // temporarily not checking the very last 111 level of dungeon because it 
            // uses LB mirror sprites and breaks everything
            for (int i = 0; i < 111; i++)
            {
                SingleCombatMapReference singleCombatMapReference =
                    GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                        SingleCombatMapReference.Territory.Dungeon,
                        i);

                if (!singleCombatMapReference.GetEntryDirections().Any())
                {
                    IEnumerable<SingleCombatMapReference.EntryDirection> dirs =
                        singleCombatMapReference.GetEntryDirections();
                }

                foreach (SingleCombatMapReference.EntryDirection dungeonEntryDirection in singleCombatMapReference
                             .GetEntryDirections())
                {
                    world.State.TheVirtualMap.LoadCombatMap(
                        GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                            SingleCombatMapReference.Territory.Dungeon,
                            i),
                        dungeonEntryDirection, world.State.CharacterRecords, enemy1, 4);
                    TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);
                }
            }

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_GetEscapablePoints(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    15),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords,
                // orcs
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448)),
                5,
                // troll
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(484)),
                1);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            List<Point2D> points =
                combatMap.GetEscapablePoints(new Point2D(12, 12),
                    Map.WalkableType.CombatLand);
            _ = "";
        }

        [Test] public void Test_ExtentCheck()
        {
            var point = new Point2D(15, 15);
            List<Point2D> constrainedPoints = point.GetConstrainedSurroundingPoints(1, 15, 15);
            Assert.True(constrainedPoints.Count == 2);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TalkToNoOne(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Expected SmallMap");
            NonPlayerCharacter npc =
                smallMap.GetNpcToTalkTo(MapUnitMovement.MovementCommandDirection.North);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadInitialSaveGame(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, true);

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Expected SmallMap");
            NonPlayerCharacter npc =
                smallMap.GetNpcToTalkTo(MapUnitMovement.MovementCommandDirection.North);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadAndReloadInitialSave(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, true);
            world.ReLoadFromJson();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadAndReloadCarpetOverworld(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            world.ReLoadFromJson();
        }

        [Test]
        [TestCase(SaveFiles.b_carpet)]
        [TestCase(SaveFiles.BucDen1)]
        [TestCase(SaveFiles.Britain2)]
        [TestCase(SaveFiles.b_frigat)]
        public void test_ReloadStressTest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            world.ReLoadFromJson();

            string loadedJson = world.State.Serialize();
            world.ReLoadFromJson();
            string newLoadedJson = world.State.Serialize();

            Assert.AreEqual(loadedJson, newLoadedJson);

            _ = world.State.TheVirtualMap.CurrentMap.CurrentPosition;
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_TestInventoryAfterReload(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);
            world.ReLoadFromJson();
            string loadedJson = world.State.Serialize();
            Assert.NotNull(world.State);
            world.ReLoadFromJson();
            string newLoadedJson = world.State.Serialize();
            Assert.NotNull(world.State);

            Assert.AreEqual(loadedJson, newLoadedJson);

            Reagent reagent = world.State.PlayerInventory.SpellReagents.Items[Reagent.SpecificReagentType.Garlic];
            Assert.NotNull(reagent);
            string reagentName = reagent.LongName;

            CombatItemReference weaponRef =
                world.State.PlayerInventory.TheWeapons.AllCombatItems.ToList()[0].TheCombatItemReference;
            Assert.NotNull(weaponRef);
            string weaponRefStr = weaponRef.EquipmentName;

            Weapon weapon =
                world.State.PlayerInventory.TheWeapons.GetWeaponFromEquipment(DataOvlReference.Equipment.GlassSword);
            Assert.NotNull(weapon);
            string weaponStr = weapon.LongName;

            Armour armourHelm =
                world.State.PlayerInventory.ProtectiveArmour.GetArmourFromEquipment(DataOvlReference.Equipment
                    .IronHelm);
            Assert.NotNull(armourHelm);
            string helmStr = armourHelm.LongName;

            // spells
            // references

            _ = world.State.TheVirtualMap.CurrentMap.CurrentPosition;
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_ReloadAndQuantityCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            int nTorches = world.State.PlayerInventory.TheProvisions.Torches;
            int nFood = world.State.PlayerInventory.TheProvisions.Food;
            int nGold = world.State.PlayerInventory.TheProvisions.Gold;
            Assert.NotZero(nTorches);
            Assert.NotZero(nFood);
            Assert.NotZero(nGold);
            world.ReLoadFromJson();
            int nTorchesNew = world.State.PlayerInventory.TheProvisions.Torches;
            int nFoodNew = world.State.PlayerInventory.TheProvisions.Food;
            int nGoldNew = world.State.PlayerInventory.TheProvisions.Gold;
            Assert.AreEqual(nTorches, nTorchesNew);
            Assert.AreEqual(nFood, nFoodNew);
            Assert.AreEqual(nGold, nGoldNew);

            string loadedJson = world.State.Serialize();
            Assert.NotNull(world.State);
            world.ReLoadFromJson();
            string newLoadedJson = world.State.Serialize();
            Assert.NotNull(world.State);

            nTorchesNew = world.State.PlayerInventory.TheProvisions.Torches;
            nFoodNew = world.State.PlayerInventory.TheProvisions.Food;
            nGoldNew = world.State.PlayerInventory.TheProvisions.Gold;

            Assert.AreEqual(nTorches, nTorchesNew);
            Assert.AreEqual(nFood, nFoodNew);
            Assert.AreEqual(nGold, nGoldNew);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_CheckSpellReagents(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);

            List<Reagent.SpecificReagentType> reagents = new()
            {
                Reagent.SpecificReagentType.SulfurAsh
            };

            Assert.True(GameReferences.Instance.MagicRefs.GetMagicReference(MagicReference.SpellWords.In_Lor)
                .IsCorrectReagents(reagents));
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_ReloadAndCheckNPCs(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            world.ReLoadFromJson();

            string loadedJson = world.State.Serialize();
            Assert.NotNull(world.State);
            world.ReLoadFromJson();
            string newLoadedJson = world.State.Serialize();
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_ReloadNewSave(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_ReloadNewSaveToCombatAndBack(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            EnemyReference enemyReference =
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448));

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Britannia,
                    0),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords, enemyReference);

            world.State.TheVirtualMap.ReturnToPreviousMapAfterCombat();

            TileReference tileRef = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_SerializeDeserializeGameSummary(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            string saveDirectory = GetNewSaveDirectory(saveFiles);
            string summaryFileAndPath = Path.Combine(saveDirectory, FileConstants.NEW_SAVE_SUMMARY_FILE);

            GameSummary gameSummary = world.State.CreateGameSummary(saveDirectory);
            string serializedGameSummary = gameSummary.SerializeGameSummary();
            var reserializedGameSummary = GameSummary.DeserializeGameSummary(serializedGameSummary);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_CheckSpriteReferences(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);
            CombatItemReference itemRef =
                GameReferences.Instance.CombatItemRefs.GetCombatItemReferenceFromEquipment(DataOvlReference.Equipment
                    .LongSword);
            Assert.True(itemRef.Sprite == 261);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_CastInLor(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            var details = new SpellCastingDetails();
            Spell spell = world.State.PlayerInventory.MagicSpells.Items[MagicReference.SpellWords.In_Lor];
            SpellResult result = spell.CastSpell(world.State, details);

            Assert.True(result.Status == SpellResult.SpellResultStatus.Success);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_GetMagicReferenceByString(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            MagicReference magReg = GameReferences.Instance.MagicRefs.GetMagicReference("In_Lor");
            // world.State.PlayerInventory.Mag 

            Assert.NotNull(magReg);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_CarpetEnemiesExist(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            ///// HEY BRAD
            /// we are at that part where the loading new JSON save files becomes super broken!
            world.ReLoadFromJson();

            Assert.True(
                world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.Enemies.Count(m => m.IsActive) > 0);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_WorldVisibilityLargeMapAtEdge(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);
            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            var newAvatarPos = new Point2D(146, 254);
            newAvatarPos = new Point2D(146, 255);
            largeMap.MoveAvatar(newAvatarPos);
            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(newAvatarPos);

            Assert.True(world.State.TheVirtualMap.CurrentMap.VisibleOnMap[146][0]);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_SmallMapVisibilityAtEdge(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Trinsic, 0));

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                .CurrentPosition.XY);

            //Assert.True(world.State.TheVirtualMap.CurrentMap.TouchedOuterBorder);
        }

        [Test] [TestCase(SaveFiles.fresh)] public void test_LoadLegacyFreshAvatarExists(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            ////GameReferences.Instance.Initialize(DataDirectory);

            Assert.True(world.State.TheVirtualMap.CurrentMap.IsMapUnitOccupiedTile(new Point2D(15, 15)));
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_EnterDungeon(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            ////GameReferences.Instance.Initialize(DataDirectory);

            var minocCovetousDungeon = new Point2D(156, 27);
            largeMap.MoveAvatar(minocCovetousDungeon);
            var turnResults = new TurnResults();
            world.TryToEnterBuilding(minocCovetousDungeon, out bool bWasSuccessful, turnResults);
            Assert.IsNotNull(world);
        }

        [Test] [TestCase(SaveFiles.fresh)] public void test_DefaultMoonstoneCheckMinoc(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            Assert.True(world.State.TheMoongates.IsMoonstoneBuried(new Point3D(224, 133, 0)));
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_AvatarInitiatedCombatChecks(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            Assert.True(world.State.TheMoongates.IsMoonstoneBuried(new Point3D(224, 133, 0)));

            // VirtualMap.AggressiveMapUnitInfo aggressiveMapUnitInfo = world.State.TheVirtualMap.GetAggressiveMapUnitInfo(
            //     world.State.TheVirtualMap.TheMapUnits.GetAvatarMapUnit().MapUnitPosition.XY,
            //     new Point2D(146, 238), SingleCombatMapReference.Territory.Britannia,
            //     world.State.TheVirtualMap.TheMapUnits.GetAvatarMapUnit());
            SingleCombatMapReference singleCombatMapReference =
                largeMap.GetCombatMapReferenceForAvatarAttacking(
                    largeMap.GetAvatarMapUnit().MapUnitPosition.XY,
                    new Point2D(146, 238), SingleCombatMapReference.Territory.Britannia);

            // attack insects
            Assert.True(singleCombatMapReference.CombatMapNum == 7);
            //Assert.True(missileType == CombatItemReference.MissileType.None);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_AvatarPassSomeTurnsInCarpet(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            Assert.True(world.State.TheMoongates.IsMoonstoneBuried(new Point3D(224, 133, 0)));

            world.State.TheVirtualMap.OneInXOddsOfNewMonster = 2;
            Utils.Ran = new Random(1);
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Left, false, false, turnResults,
                true);

            world.TryToPassTime(turnResults);
            world.TryToPassTime(turnResults);
            world.TryToPassTime(turnResults);
            world.TryToPassTime(turnResults);
            world.TryToPassTime(turnResults);
            world.TryToPassTime(turnResults);
        }

        [Test] [TestCase(SaveFiles.b_frigat)] public void test_AvatarMoveIntoOcean(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            Assert.True(world.State.TheMoongates.IsMoonstoneBuried(new Point3D(224, 133, 0)));

            world.State.TheVirtualMap.OneInXOddsOfNewMonster = 2;
            Utils.Ran = new Random(1);
            var turnResults = new TurnResults();

            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
            world.TryToMoveNonCombatMap(Point2D.Direction.Down, false, false, turnResults,
                true);
        }

        private static T GetEnumByName<T>(InventoryReference inventoryReference) where T : Enum
        {
            var enumResult = (T)Enum.Parse(typeof(T), inventoryReference.ItemName);
            return enumResult;
        }

        [Test] [TestCase(SaveFiles.b_frigat)] public void test_EnumCalcsWithInventory(SaveFiles saveFiles)
        {
            //GameReferences.Instance.Initialize(DataDirectory);

            var artifact = GetEnumByName<LordBritishArtifact.ArtifactType>(
                GameReferences.Instance.InvRef.GetInventoryReference(
                    InventoryReferences.InventoryReferenceType.Item, "Crown"));
            Assert.AreEqual(artifact, LordBritishArtifact.ArtifactType.Crown);
        }

        [Test] [TestCase(SaveFiles.b_frigat)] public void test_CheckAllNonAttackingUnits(SaveFiles saveFiles)
        {
            //GameReferences.Instance.Initialize(DataDirectory);

            foreach (KeyValuePair<string, List<InventoryReference>> kvp in GameReferences.Instance.InvRef
                         .InvRefsDictionary)
            {
                if (kvp.Value[0].InvRefType == InventoryReferences.InventoryReferenceType.Reagent) continue;
                foreach (InventoryReference invRef in kvp.Value)
                {
                    NonAttackingUnit nau =
                        NonAttackingUnitFactory.Create(invRef.ItemSpriteExposed,
                            SmallMapReferences.SingleMapReference.Location.Britannia_Underworld, new MapUnitPosition());
                    InventoryItemFactory.Create(invRef);
                }
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_LBChestCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 0));

            // MapUnit mapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(new Point2D(0, 0), true);
            // Assert.IsNotNull(mapUnit);
            //
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, -1));

            var chestPosition = new Point2D(16, 21);
            MapUnit mapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(chestPosition, true);
            Assert.IsNotNull(mapUnit);
            Assert.IsTrue(mapUnit is Chest);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToOpenAThing(chestPosition, out bool bWasChestOpened, turnResults);
            Assert.IsTrue(bWasChestOpened);
            MapUnit shouldBeAStack = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(chestPosition, true);
            Assert.IsNotNull(shouldBeAStack);

            TestContext.Out.Write("Ending ");

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.fresh)] public void test_BlackthorneThingCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            Trace.Write("Starting ");
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Palace_of_Blackthorn, 3));

            var thingPosition = new Point2D(15, 13);
            bool bIsMapUnit = world.State.TheVirtualMap.CurrentMap.IsMapUnitOccupiedTile(thingPosition);
            Assert.IsTrue(bIsMapUnit);
            MapUnit mapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(thingPosition, true);
            Assert.IsNotNull(mapUnit);

            TestContext.Out.Write("Ending ");

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_CheckAndGetAllMapUnitsSmallMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";
            foreach (SmallMapReferences.SingleMapReference smr in GameReferences.Instance.SmallMapRef.MapReferenceList)
            {
                SmallMapReferences.SingleMapReference singleMap =
                    GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(smr.MapLocation, smr.Floor);
                // we don't test dungeon maps here
                if (singleMap.MapType == Map.Maps.Dungeon) continue;
                world.State.TheVirtualMap.LoadSmallMap(singleMap);

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        var thingPosition = new Point2D(i, j);

                        bool bIsMapUnit = world.State.TheVirtualMap.CurrentMap.IsMapUnitOccupiedTile(thingPosition);
                        if (bIsMapUnit)
                        {
                            MapUnit mapUnit =
                                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(thingPosition, false);
                            // this is an exception for DiscoverableLoot since it is not technically visible, but does exist
                            if (mapUnit != null || !world.State.TheVirtualMap.ContainsSearchableMapUnits(thingPosition))
                            {
                                Assert.IsNotNull(mapUnit);
                            }

                            if (mapUnit is ItemStack itemStack)
                            {
                                GameReferences.Instance.SpriteTileReferences.GetTileReference(itemStack.KeyTileReference
                                    .Index);
                            }
                        }
                    }
                }
            }

            TestContext.Out.Write("Ending ");

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_JimmyDoorLBBasement(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, -1));

            var avatarPos = new Point2D(15, 25);
            var turnResults = new TurnResults();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos = world.TryToJimmyDoor(avatarPos,
                world.State.CharacterRecords.AvatarRecord, out bool bWasSuccessful,
                turnResults);

            TestContext.Out.Write("Ending ");

            Assert.True(true);
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_SearchLBChest(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, -1));

            var avatarPos = new Point2D(14, 23);
            var chestPos = new Point2D(13, 23);
            var turnResults = new TurnResults();

            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToSearch(chestPos, out bool bWasSuccessful, turnResults);
            Assert.IsFalse(bWasSuccessful);

            aggressiveMapUnitInfos = world.TryToSearch(chestPos, out bWasSuccessful, turnResults);
            Assert.IsFalse(bWasSuccessful);

            aggressiveMapUnitInfos =
                world.TryToGetAThing(chestPos, out bool bGotAThing, out InventoryItem thingIGot, turnResults,
                    Point2D.Direction.Down);
            Assert.IsFalse(bWasSuccessful);

            aggressiveMapUnitInfos = world.TryToOpenAThing(chestPos, out bWasSuccessful, turnResults);
            Assert.IsTrue(bWasSuccessful);
            aggressiveMapUnitInfos = world.TryToOpenAThing(chestPos, out bWasSuccessful, turnResults);
            Assert.IsFalse(bWasSuccessful);
            aggressiveMapUnitInfos =
                world.TryToLook(chestPos, out World.SpecialLookCommand specialLookCommand, turnResults);
            Assert.IsTrue(turnResults.HasTurnResult);
        }

        // search all the things
        // open all the things
        // look at all the things

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon71WithOpenDoor(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    71),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            //TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;

            var turnResults = new TurnResults();
            // locked door
            world.TryToOpenAThing(new Point2D(5, 4), out bool bWasSuccessful, turnResults);
            Assert.IsFalse(bWasSuccessful);

            world.TryToJimmyDoor(new Point2D(5, 4), world.State.CharacterRecords.AvatarRecord, out bWasSuccessful,
                turnResults);
            Assert.IsTrue(bWasSuccessful);

            world.TryToOpenAThing(new Point2D(5, 4), out bWasSuccessful, turnResults);
            Assert.IsTrue(bWasSuccessful);

            Assert.NotNull(combatMap);
            combatMap.DivideEnemy(combatMap.AllEnemies.ToList()[2]);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon93(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    93),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            //TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;

            var turnResults = new TurnResults();

            Assert.NotNull(combatMap);
            combatMap.DivideEnemy(combatMap.AllEnemies.ToList()[2]);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon93GetStuffAndMove(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    93),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            //TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            Assert.IsNotNull(player);

            var turnResults = new TurnResults();

            Assert.NotNull(combatMap);

            var chestPosition = new Point2D(8, 9);
            world.TryToOpenAThing(chestPosition, out bool bWasSuccessful, turnResults);
            Assert.IsTrue(bWasSuccessful);

            var itemStack =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(chestPosition, true) as ItemStack;

            while (itemStack.HasStackableItems)
            {
                StackableItem item = itemStack.PopStackableItem();
            }

            // put the avatar just up from the chest
            player.MapUnitPosition.XY = new Point2D(8, 8);
            world.TryToMoveCombatMap(player, Point2D.Direction.Down, turnResults, false);
            Assert.IsTrue(turnResults.PeekLastTurnResult.TheTurnResultType !=
                          TurnResult.TurnResultType.ActionMoveBlocked);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon24LotsOfStuff(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    24),
                SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            //TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            Assert.IsNotNull(player);

            var turnResults = new TurnResults();

            Assert.NotNull(combatMap);

            var gemPosition = new Point2D(7, 2);
            MapUnit gemMapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(gemPosition, true);
            Assert.IsInstanceOf<DeadBody>(gemMapUnit);

            var itemStackPosition = new Point2D(8, 2);
            MapUnit itemStackMapUnit =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(itemStackPosition, true);
            Assert.IsInstanceOf<ItemStack>(itemStackMapUnit);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon0StartLocation(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon, 0),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);

            //TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(0, 0);

            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            Assert.IsNotNull(player);

            //Assert.AreEqual(player.MapUnitPosition.X, 3);

            var turnResults = new TurnResults();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon2HasFields(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon, 2),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            Assert.IsNotNull(player);

            MapUnit shouldBeFieldMapUnit =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(new Point2D(5, 5), true);
            Assert.IsInstanceOf<CombatMapUnit>(shouldBeFieldMapUnit);

            var turnResults = new TurnResults();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_Dungeon38HasWhirlpools(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    38),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            CombatPlayer player = combatMap.CurrentCombatPlayer;
            Assert.IsNotNull(player);

            //MapUnit shouldBeFieldMapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(new Point2D(5, 5), true);
            //Assert.IsInstanceOf<CombatMapUnit>(shouldBeFieldMapUnit);

            var turnResults = new TurnResults();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadAllDungeonCombatMaps(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            for (int i = 0; i <= 111; i++)
            {
                world.State.TheVirtualMap.LoadCombatMap(
                    GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                        SingleCombatMapReference.Territory.Dungeon,
                        i),
                    SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);
            }

            var turnResults = new TurnResults();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadDungeon42CheckBatMovement(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 1;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    42),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            var turnResults = new TurnResults();

            combatMap.ProcessEnemyTurn(turnResults,
                out CombatMapUnit activeCombatMapUnit,
                out CombatMapUnit targetedCombatMapUnit,
                //out string preAttackOutputStr, out string postAttackOutputStr,
                out Point2D missedPoint);
            Assert.IsInstanceOf<Enemy>(activeCombatMapUnit);

            combatMap.ProcessEnemyTurn(turnResults,
                out activeCombatMapUnit, out targetedCombatMapUnit,
                //out preAttackOutputStr, out postAttackOutputStr,
                out missedPoint);
            Assert.IsInstanceOf<Enemy>(activeCombatMapUnit);

            combatMap.ProcessEnemyTurn(turnResults,
                out activeCombatMapUnit, out targetedCombatMapUnit,
                //out preAttackOutputStr, out postAttackOutputStr,
                out missedPoint);
            Assert.IsInstanceOf<Enemy>(activeCombatMapUnit);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadDungeon58EnemyOnNonAttackingUnit(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            // force avatar to go first
            world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 1;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    58), SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords);

            var turnResults = new TurnResults();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LoadDungeon110BasicAttack(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            // force avatar to go first
            //world.State.CharacterRecords.AvatarRecord.Stats.Dexterity = 100;

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    110), SingleCombatMapReference.EntryDirection.South, world.State.CharacterRecords);

            var turnResults = new TurnResults();
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            Assert.IsInstanceOf<Enemy>(combatMap.GetAndRefreshCurrentCombatMapUnit());
        }

        [Test] public void Test_SurroundingPoints()
        {
            // force avatar to go first
            var xy = new Point2D(8, 8);
            //xy.GetRandomSurroundingPointThatIsnt
            List<Point2D> surroundingCombatPlayerPoints =
                xy.GetConstrainedSurroundingPoints(1, 16, 16);
            Assert.IsTrue(surroundingCombatPlayerPoints.Count == 8);
            foreach (Point2D p in surroundingCombatPlayerPoints)
            {
                Assert.AreNotEqual(xy, p);
            }

            var p1 = new Point2D(5, 5);
            List<Point2D> points = p1.GetConstrainedSurroundingPoints(1, 10, 10);
            List<Point2D> points2 = p1.GetConstrainedSurroundingPoints(4, 6, 6);
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_ClosestTileReferenceAround(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Expected LargeMap");

            int nClosest = largeMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(5), new Point2D(0, 0), 16);
            Assert.AreEqual(nClosest, 255);

            var aroundBritain = new Point2D(82, 106);
            nClosest = largeMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(5), aroundBritain, 16);
            Assert.Less(nClosest, 255);

            nClosest = largeMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(6), aroundBritain, 2);
            Assert.AreEqual(nClosest, 255);

            nClosest = largeMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(6), aroundBritain, 8);
            Assert.Less(nClosest, 255);

            var minoc = new Point2D(157, 20);
            nClosest = largeMap.ClosestTileReferenceAround(
                minoc, 8, GameReferences.Instance.SpriteTileReferences.IsFrigate);
            Assert.Less(nClosest, 255);

            nClosest = largeMap.ClosestTileReferenceAround(
                minoc, 2, GameReferences.Instance.SpriteTileReferences.IsFrigate);
            Assert.AreEqual(nClosest, 255);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("should have been a small map");

            // fountain
            var courtYard = new Point2D(15, 19);
            nClosest = smallMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(TileReference.SpriteIndex
                    .Fountain_KeyIndex), courtYard, 8);
            Assert.Less(nClosest, 255);

            nClosest = smallMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(TileReference.SpriteIndex
                    .Fountain_KeyIndex), courtYard, 2);
            Assert.AreEqual(nClosest, 255);

            // brick in the corner
            nClosest = smallMap.ClosestTileReferenceAround(
                GameReferences.Instance.SpriteTileReferences.GetTileReference(68), Point2D.Zero, 8);
            Assert.Less(nClosest, 255);
        }

        [Test] [TestCase(SaveFiles.BucDen3)] public void Test_LoadLoadMoveSmallMap(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world = CreateWorldFromLegacy(saveFiles);

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            //world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            //world.State.TheVirtualMap.CurrentMap.CurrentPosition.XY = new Point2D(166, 21);
            var turnResults = new TurnResults();
            // // we don't test dungeon maps here
            // if (singleMap.MapType == Map.Maps.Dungeon) continue;
            // world.State.TheVirtualMap.LoadSmallMap(singleMap);

            //
            world.TryToMoveNonCombatMap(Point2D.Direction.Up, false, false, turnResults);
            Assert.AreEqual(smallMap.CurrentPosition.XY,
                smallMap.GetAvatarMapUnit().MapUnitPosition.XY,
                $"CurrPos = {world.State.TheVirtualMap.CurrentMap.CurrentPosition.X}, {world.State.TheVirtualMap.CurrentMap.CurrentPosition.Y} but AvatarUnit = {smallMap.GetAvatarMapUnit().MapUnitPosition.XY.X}, {smallMap.GetAvatarMapUnit().MapUnitPosition.XY.Y}");
        }

        [Test] [TestCase(SaveFiles.blackt)] public void Test_BlacktLargeMapCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            _ = "";
        }

        [Test] [TestCase(SaveFiles.blackt)] public void Test_AnimationFramesSane(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            TileReference water1 = GameReferences.Instance.SpriteTileReferences.GetTileReference(1);
            TileReference water2 = GameReferences.Instance.SpriteTileReferences.GetTileReference(2);

            Debug.Assert(water1.KeyTileTileReferenceIndex == 1);
            Debug.Assert(water2.KeyTileTileReferenceIndex == 1);

            for (int i = 0; i < 50; i++)
            {
                // Debug.Assert(water1.GetRandomAnimationFrameIndex(out bool _) is 1 or 2);
                // Debug.Assert(water2.GetRandomAnimationFrameIndex(out bool _) is 1 or 2);
            }
        }

        [Test] [TestCase(SaveFiles.Britain3)] public void Test_FoodSignInBritain(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            var foodSignPos = new Point2D(13, 8);
            TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(foodSignPos);
            Assert.True(tileReference.Index == 240);
            int nGuessIndex = world.State.TheVirtualMap.CurrentMap.GuessTile(foodSignPos);
            Assert.True(nGuessIndex == 49);
            int nNextGuess = world.State.TheVirtualMap.CurrentMap.GuessTile(new Point2D(17, 19));
            Assert.True(nNextGuess == 48);

            nNextGuess = world.State.TheVirtualMap.CurrentMap.GetAlternateFlatSprite(new Point2D(17, 19));
            Assert.True(nNextGuess == 48);

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(new Point2D(16, 9));

            TileStack ts = world.State.TheVirtualMap.GetTileStack(new Point2D(17, 19), true);
            _ = "";
        }


        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LBCastleTileOverride(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 3));

            var flagPolePos = new Point2D(13, 15);
            TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(flagPolePos);
            Assert.True(tileReference.Index == 5);

            //int nGuessIndex = world.State.TheVirtualMap.GuessTile(foodSignPos);
            //Assert.True(nGuessIndex == 49);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_AraratTileOverrides(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Ararat, 1));

            var cornerPosition = new Point2D(6, 19);
            // TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(flagPolePos);
            // Assert.True(tileReference.Index == 5);

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(cornerPosition);
            TileStack tileStack = world.State.TheVirtualMap.GetTileStack(cornerPosition, true);
            Assert.True(tileStack.TileReferencesDictionary.ContainsKey(5));

            int nNextGuess = world.State.TheVirtualMap.CurrentMap.GetAlternateFlatSprite(cornerPosition);
            Assert.True(nNextGuess == 5);
            //int nGuessIndex = world.State.TheVirtualMap.GuessTile(foodSignPos);
            //Assert.True(nGuessIndex == 49);
            _ = "";
        }


        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_LycaeumOverrides(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lycaeum, 0));

            var cornerPosition = new Point2D(23, 15);
            // TileReference tileReference = world.State.TheVirtualMap.CurrentMap.GetTileReference(flagPolePos);
            // Assert.True(tileReference.Index == 5);

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(cornerPosition);
            TileStack tileStack = world.State.TheVirtualMap.GetTileStack(cornerPosition, true);
            Assert.True(tileStack.TileReferencesDictionary.ContainsKey(68));

            int nNextGuess = world.State.TheVirtualMap.CurrentMap.GetAlternateFlatSprite(cornerPosition);
            Assert.True(nNextGuess == 68);
            //int nGuessIndex = world.State.TheVirtualMap.GuessTile(foodSignPos);
            //Assert.True(nGuessIndex == 49);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_AttackManacles(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Palace_of_Blackthorn, 1));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            var avatarPosition = new Point2D(10, 1);
            smallMap.MoveAvatar(avatarPosition);
            //world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(avatarPosition);
            var turnResults = new TurnResults();
            IEnumerable<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToAttackNonCombatMap(new Point2D(10, 10),
                    out MapUnit mapUnit, out SingleCombatMapReference singleCombatMapReference,
                    out World.TryToAttackResult tryToAttackResult, turnResults);
            Assert.IsFalse(tryToAttackResult == World.TryToAttackResult.NpcMurder);
            _ = "";
        }


        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_HorizTombstoneCheck(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            EnemyReference enemyReference =
                GameReferences.Instance.EnemyRefs.GetEnemyReference(GameReferences.Instance.SpriteTileReferences
                    .GetTileReference(448));

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    58),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords, enemyReference);

            bool bIsHoriz = world.State.TheVirtualMap.CurrentMap.IsHorizTombstone(new Point2D(5, 3));
            Assert.True(bIsHoriz);

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon,
                    53),
                SingleCombatMapReference.EntryDirection.East, world.State.CharacterRecords, enemyReference);

            bIsHoriz = world.State.TheVirtualMap.CurrentMap.IsHorizTombstone(new Point2D(4, 5));
            Assert.False(bIsHoriz);
        }


        [Test] [TestCase(SaveFiles.b_carpet)] public void Test_AttackSinVraal(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            SmallMapReferences.SingleMapReference sinVraalHut =
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.SinVraals_Hut, 0);
            world.State.TheVirtualMap.LoadSmallMap(sinVraalHut);

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            smallMap.MoveAvatar(new Point2D(15, 15));

            MapUnit sinVraal =
                smallMap.CurrentMapUnits.NonPlayerCharacters.FirstOrDefault(npc =>
                    npc.NpcRef.NPCKeySprite is >= 472 and <= 475);

            Assert.NotNull(sinVraal);

            var turnResults = new TurnResults();
            IEnumerable<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToAttackNonCombatMap(sinVraal.MapUnitPosition.XY,
                    //new Point2D(10, 10),
                    out MapUnit mapUnit, out SingleCombatMapReference singleCombatMapReference,
                    out World.TryToAttackResult tryToAttackResult, turnResults);

            Assert.True(tryToAttackResult == World.TryToAttackResult.CombatMapNpc,
                $"Expected CombatMapEnemy but got: {tryToAttackResult}");
            //Assert.True(tryToAttackResult == World.TryToAttackResult.NpcMurder);
            _ = "";
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_LoadTrinsicHorseAI(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Trinsic, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            smallMap.MoveAvatar(new Point2D(15, 15));

            Assert.True(true);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> thing = world.TryToPassTime(turnResults);

            var tod = new TimeOfDay(0, 0, 0, 0, 0);
            foreach (Horse horse in smallMap.CurrentMapUnits.Horses)
            {
                //horse.CompleteNextMove(world.State.TheVirtualMap, world.State.TheTimeOfDay, );
            }
        }

        [Test] [TestCase(SaveFiles.BucDen1)] public void test_BucdenKillInnkeeperStillDead(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(SaveFiles.BucDen1, true, false);
            //World world = CreateWorldFromNewSave(SaveFiles.BucDenEntrance, true, false);
            //CreateWorldFromLegacy(saveFiles);
            world.ReLoadFromJson();
            _ = "";

            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentSingleMapReference.MapLocation ==
                        SmallMapReferences.SingleMapReference.Location.Buccaneers_Den);

            // world.State.TheVirtualMap.LoadSmallMap(
            //     GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
            //         SmallMapReferences.SingleMapReference.Location.Buccaneers_Den, 0));
            // world.State.TheVirtualMap.MoveAvatar(new(15, 15));

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> thing = world.TryToPassTime(turnResults);

            var tod = new TimeOfDay(0, 0, 0, 0, 0);

            //ReturnToPreviousMapAfterCombat

            bool bFoundInnkeeper = false;
            NonPlayerCharacter preNpc = null;
            //MapUnits preMapUnits = world.State.TheVirtualMap.CurrentMap.Cur;
            // MapUnitCollection preMapUnitCollection = preMapUnits.CurrentMapUnits;
            foreach (NonPlayerCharacter npc in world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                if (npc.FriendlyName == "InnKeeper")
                {
                    Assert.False(npc.NpcState.IsDead);
                    npc.NpcState.IsDead = true;
                    Assert.True(npc.NpcState.IsDead);
                    bFoundInnkeeper = true;
                    preNpc = npc;
                    break;
                }
            }

            Assert.True(bFoundInnkeeper);
            Assert.NotNull(preNpc);

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Buccaneers_Den, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            smallMap.MoveAvatar(new Point2D(15, 15));
            bFoundInnkeeper = false;

            // MapUnits postMapUnits = world.State.TheVirtualMap.CurrentMap.CurrentMapUnits;
            // MapUnitCollection postMapUnitCollection = postMapUnits.CurrentMapUnits;

            // Assert.AreSame(preMapUnits, postMapUnits);
            // Assert.AreSame(preMapUnitCollection, postMapUnitCollection);

            foreach (NonPlayerCharacter npc in
                     world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                if (npc.FriendlyName == "InnKeeper")
                {
                    Assert.True(preNpc.NpcRef == npc.NpcRef);
                    Assert.True(preNpc.NpcState == npc.NpcState);
                    //Assert.True(preNpc == npc);
                    Assert.True(npc.NpcState.IsDead);
                    bFoundInnkeeper = true;
                }
            }

            Assert.True(bFoundInnkeeper);
        }


        [Test] [TestCase(SaveFiles.Britain)] public void test_LoadTrinsicGuardAngry(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            // this should put the guard at position 15,22
            world.State.TheTimeOfDay.Hour = 16;
            world.State.TheTimeOfDay.Minute = 16;

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Trinsic, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            // directly in front of the guard
            smallMap.MoveAvatar(new Point2D(15, 23));

            smallMap.IsWantedManByThePoPo = true;

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> thing = world.TryToPassTime(turnResults);

            TurnResult turnResult;
            bool bWasAttemptedArrest = false;
            while (turnResults.HasTurnResult)
            {
                turnResult = turnResults.PopTurnResult();
                if (turnResult is AttemptToArrest attemptToArrest) bWasAttemptedArrest = true;
            }

            Assert.True(bWasAttemptedArrest, "The guard did not attempt arrest");
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_YellForSails(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Minoc,
                    0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            LargeMap overworldMap = world.State.TheVirtualMap.TheMapHolder.OverworldMap;

            world.State.TheVirtualMap.CreateFrigateAtDock(
                SmallMapReferences.SingleMapReference.Location.Minoc);
            Point2D dockLocation =
                LargeMapLocationReferences.GetLocationOfDock(SmallMapReferences.SingleMapReference.Location.Minoc);
            List<MapUnit> mapUnits = overworldMap.GetMapUnitsByPosition(dockLocation, 0);

            var frigate2 = overworldMap.GetSpecificMapUnitByLocation<Frigate>(dockLocation, 0);
            Assert.True(frigate2 != null);

            Assert.True(
                world.State.TheVirtualMap.IsShipOccupyingDock(SmallMapReferences.SingleMapReference.Location.Minoc));

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);

            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            largeMap.MoveAvatar(new Point2D(frigate2.MapUnitPosition.X, frigate2.MapUnitPosition.Y));
            var turnResults = new TurnResults();
            world.TryToBoard(out bool bWasSuccessful, turnResults);
            Assert.True(bWasSuccessful);

            Assert.True(frigate2 != null);
            Assert.True(mapUnits[0] is Frigate);
            Assert.True(true);

            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToYellForSails(out bool bSailsHoisted, turnResults);
            Assert.True(bSailsHoisted);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_TryToIgniteTorch(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Minoc,
                    0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            smallMap.MoveAvatar(new Point2D(15, 23));

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos = world.TryToIgniteTorch(turnResults);
            bool bHasBasicType = turnResults.ContainsResultType(typeof(BasicResult));
            Assert.True(bHasBasicType);
            bool bHasTurnResult = turnResults.ContainsTurnResultType(TurnResult.TurnResultType.ActionIgniteTorch);
            Assert.True(bHasTurnResult);
        }

        [Test] [TestCase(SaveFiles.Britain)] public void test_TryToUsePotion(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Minoc,
                    0));
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            smallMap.MoveAvatar(new Point2D(15, 23));

            foreach (Potion potion in world.State.PlayerInventory.MagicPotions.Items.Values) TestPotion(world, potion);
        }

        private void TestPotion(World world, Potion potion)
        {
            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos = world.TryToUsePotion(potion,
                world.State.CharacterRecords.AvatarRecord, out bool bSucceeded, out MagicReference.SpellWords spell,
                turnResults);

            bool bHasBasicType = turnResults.ContainsResultType(typeof(DrankPotion));
            Assert.True(bHasBasicType);
            bool bHasTurnResult = turnResults.ContainsTurnResultType(TurnResult.TurnResultType.ActionUseDrankPotion);
            Assert.True(bHasTurnResult);
            var drankPotion = turnResults.GetFirstTurnResult<DrankPotion>();
            Assert.NotNull(drankPotion);
            Assert.AreEqual(drankPotion.PotionColor, potion.Color);
        }


        [Test] [TestCase(SaveFiles.Britain)] public void test_TryToUseScrolls(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);
            _ = "";

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Skara_Brae,
                    0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            smallMap.MoveAvatar(new Point2D(15, 23));

            foreach (Scroll scroll in world.State.PlayerInventory.MagicScrolls.Items.Values)
            {
                scroll.Quantity++;
                TestScroll(world, scroll);
            }
        }

        private void TestScroll(World world, Scroll scroll)
        {
            TurnResults turnResults = new();

            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                world.TryToUseScroll(scroll, world.State.CharacterRecords.AvatarRecord, turnResults);

            bool bHasBasicType = turnResults.ContainsResultType(typeof(ReadScroll));
            Assert.True(bHasBasicType);
            bool bHasTurnResult = turnResults.ContainsTurnResultType(TurnResult.TurnResultType.ActionUseReadScroll);
            Assert.True(bHasTurnResult);
            var readScroll = turnResults.GetFirstTurnResult<ReadScroll>();
            Assert.NotNull(readScroll);
            Assert.AreEqual(readScroll.ReadByWho, world.State.CharacterRecords.AvatarRecord);
        }

        [Test] [TestCase(SaveFiles.quicksave)] public void test_YewGuardsAreMad(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Yew, 0));

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(world.State.TheVirtualMap.CurrentMap
                .CurrentPosition.XY);

            //Assert.True(world.State.TheVirtualMap.CurrentMap.TouchedOuterBorder);
        }

        [Test] [TestCase(SaveFiles.BucDenEntrance)]
        public void Test_BoardCarpetAndGoFromLargeToSmall(SaveFiles saveFiles)
        {
            World world = CreateWorldFromNewSave(saveFiles, true, false);
            // World world = CreateWorldFromLegacy(saveFiles);
            _ = "";
            var turnResults = new TurnResults();

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Overworld);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            var grendelsHutPosition = new Point2D(153, 91);

            largeMap.MoveAvatar(grendelsHutPosition);

            int nCarpets = world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet]
                .Quantity;
            Assert.True(nCarpets == world.State.PlayerInventory.SpecializedItems
                .Items[SpecialItem.SpecificItemType.Carpet].Quantity);

            Assert.IsFalse(largeMap.IsAvatarRidingCarpet);

            world.TryToUseSpecialItem(
                world.State.PlayerInventory.SpecializedItems.Items[SpecialItem.SpecificItemType.Carpet],
                out bool bAbleToUseItem, turnResults);
            Assert.True(bAbleToUseItem);
            Assert.IsTrue(largeMap.IsAvatarRidingCarpet);

            world.TryToEnterBuilding(grendelsHutPosition, out bool bWasSuccessful, turnResults);

            Assert.IsTrue(largeMap.IsAvatarRidingCarpet);
        }

        private void OnUpdateOfEnqueuedScriptItemHandleJeremy(Conversation conversation)
        {
            TalkScript.ScriptItem item = conversation.DequeueFromOutputBuffer();
            switch (item.Command)
            {
                case TalkScript.TalkCommand.PlainString:
                    Debug.WriteLine(item.StringData);
                    break;
                case TalkScript.TalkCommand.Gold:
                    _ = "";
                    break;
                case TalkScript.TalkCommand.Change:
                    _ = "";
                    break;
                case TalkScript.TalkCommand.KarmaPlusOne:
                    break;
                case TalkScript.TalkCommand.KarmaMinusOne:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void Test_TalkToJeremyKeysAndGold(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            var location =
                SmallMapReferences.SingleMapReference.Location.Yew;

            NonPlayerCharacterState npcState =
                world.State.TheNonPlayerCharacterStates.GetStateByLocationAndIndex(location, 8);

            Conversation convo = world.CreateConversationAndBegin(npcState, OnUpdateOfEnqueuedScriptItemHandleJeremy);
            convo.BeginConversation();
            convo.AddUserResponse("keys");
            convo.AddUserResponse("yes");
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_HiddenAndFoundSearchItems(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            Assert.True(
                world.State.TheVirtualMap.TheMapHolder.OverworldMap.CurrentMapUnits.Enemies.Count(m => m.IsActive) > 0);

            bool bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Britannia_Underworld,
                -1, new Point2D(233, 233));
            Assert.True(bIsStuff);

            List<SearchItem> stuff = world.State.TheVirtualMap.TheSearchItems.GetUnDiscoveredSearchItemsByLocation(
                SmallMapReferences.SingleMapReference.Location.Britannia_Underworld,
                -1, new Point2D(233, 233));

            Assert.True(stuff.Count > 0);
            Assert.True(stuff[0].TheSearchItemReference.CalcTileReference.Index == 267); // ItemArmour
            Assert.True(stuff[0].TheSearchItemReference.Quality == 15); // ItemWeapon

            bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Britannia_Underworld,
                -1, new Point2D(0, 0));
            Assert.False(bIsStuff);

            bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Combat_resting_shrine,
                0, new Point2D(0, 0));
            Assert.False(bIsStuff);

            List<SearchItem> yewStuff = world.State.TheVirtualMap.TheSearchItems.GetUnDiscoveredSearchItemsByLocation(
                SmallMapReferences.SingleMapReference.Location.Yew);

            Assert.NotNull(yewStuff);
            Assert.True(yewStuff.Count > 0);
        }


        [Test] [TestCase(SaveFiles.b_carpet)] public void test_SearchWhileOnLavaOverworld(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadLargeMap(LargeMapLocationReferences.LargeMapType.Underworld);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");

            var avatarPost = new Point2D(232, 233);
            var thingPos = new Point2D(233, 233);
            largeMap.MoveAvatar(avatarPost);

            Assert.True(
                world.State.TheVirtualMap.TheMapHolder.OverworldMap.CurrentMapUnits.Enemies.Count(m => m.IsActive) > 0);

            MapUnit mapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(thingPos, false);
            Assert.IsNull(mapUnit);

            bool bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Britannia_Underworld,
                -1, thingPos);
            Assert.True(bIsStuff);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> things =
                world.TryToSearch(thingPos, out bool bWasSuccessful, turnResults);

            Assert.True(bWasSuccessful);

            mapUnit = world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(thingPos, false);
            Assert.IsNotNull(mapUnit);
            //ItemStack itemStack = null;
            Assert.True(mapUnit is ItemStack);
            Assert.NotNull(mapUnit);
            var itemStack = (ItemStack)mapUnit;
            Assert.True(itemStack.HasStackableItems);

            int nTotalItems = itemStack.TotalItems;
            for (int i = 0; i < nTotalItems; i++)
            {
                List<VirtualMap.AggressiveMapUnitInfo> derp = world.TryToGetAThing(thingPos, out bool bGotAThing,
                    out InventoryItem inventoryItem, turnResults, Point2D.Direction.Right);
                Assert.True(bGotAThing);
            }

            GameReferences.Instance.SearchLocationReferences.PrintCsvOutput();
        }

        [Test] [TestCase(SaveFiles.b_carpet)] public void test_SearchLbBeforeAndAfterReload(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, 2));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            smallMap.MoveAvatar(new Point2D(11, 12));

            var bookcasePosition = new Point2D(12, 12);

            bool bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                2, bookcasePosition);
            Assert.True(bIsStuff);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> things =
                world.TryToSearch(bookcasePosition, out bool bWasSuccessful, turnResults);

            Assert.True(bWasSuccessful);

            world.ReLoadFromJson();

            bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                2, bookcasePosition);
            Assert.True(bIsStuff);

            turnResults = new TurnResults();
            things = world.TryToSearch(bookcasePosition, out bWasSuccessful, turnResults);

            Assert.True(bWasSuccessful);
        }

        [Test] [TestCase(SaveFiles.fresh)] public void test_SearchLDeadBodiesInWestBritanny(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            //GameReferences.Instance.Initialize(DataDirectory);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.West_Britanny, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            smallMap.MoveAvatar(new Point2D(2, 2));

            var deadBodyPosition = new Point2D(3, 2);

            bool bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.West_Britanny,
                0, deadBodyPosition);
            Assert.True(bIsStuff);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> things =
                world.TryToSearch(deadBodyPosition, out bool bWasSuccessful, turnResults);

            Assert.True(bWasSuccessful);

            MapUnit topVisibleMapUnit =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(deadBodyPosition, true);

            world.ReLoadFromJson();

            MapUnit reloadedTopVisibleMapUnit =
                world.State.TheVirtualMap.CurrentMap.GetTopVisibleMapUnit(deadBodyPosition, true);

            Assert.AreEqual(topVisibleMapUnit.GetType(), reloadedTopVisibleMapUnit.GetType());

            bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.West_Britanny,
                0, deadBodyPosition);
            Assert.True(bIsStuff);

            turnResults = new TurnResults();
            things = world.TryToSearch(deadBodyPosition, out bWasSuccessful, turnResults);

            Assert.True(bWasSuccessful);
        }

        [Test] [TestCase(SaveFiles.b_carpet, false)] [TestCase(SaveFiles.b_carpet, true)]
        public void test_SearchGlassSwordGetMultiple(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);
            if (world.State.TheVirtualMap.CurrentMap is not LargeMap largeMap)
                throw new Ultima5ReduxException("Should be large map");
            var glassSwordPosition = new Point2D(64, 80);
            largeMap.MoveAvatar(new Point2D(63, 80));

            foreach (PlayerCharacterRecord record in world.State.CharacterRecords.Records)
            {
                Assert.IsTrue(record.Equipped.LeftHand != DataOvlReference.Equipment.GlassSword &&
                              record.Equipped.RightHand != DataOvlReference.Equipment.GlassSword);
            }

            Weapon glassSwordWeapon =
                world.State.PlayerInventory.TheWeapons.GetWeaponFromEquipment(DataOvlReference.Equipment.GlassSword);
            world.State.PlayerInventory.TheWeapons.GetWeaponFromEquipment(DataOvlReference.Equipment.GlassSword)
                .Quantity = 0;
            Assert.IsTrue(glassSwordWeapon.Quantity == 0);

            bool bIsStuff = world.State.TheVirtualMap.TheSearchItems.IsAvailableSearchItemByLocation(
                SmallMapReferences.SingleMapReference.Location.Britannia_Underworld,
                0, glassSwordPosition);
            Assert.True(bIsStuff);

            var turnResults = new TurnResults();
            List<VirtualMap.AggressiveMapUnitInfo> things =
                world.TryToSearch(glassSwordPosition, out bool bWasSuccessful, turnResults);

            Assert.IsTrue(bWasSuccessful);

            world.TryToGetAThing(glassSwordPosition, out bWasSuccessful, out InventoryItem item, turnResults,
                Point2D.Direction.Right);
            Assert.IsTrue(bWasSuccessful);
            Assert.IsNotNull(item);
            if (item is not Weapon weapon)
            {
                Assert.IsTrue(false, "item returned is not weapon");
                return;
            }

            Assert.IsNotNull(weapon);
            Assert.IsTrue(weapon.SpecificEquipment == DataOvlReference.Equipment.GlassSword);

            // we are searching with stuff in our inventory - it should come up empty
            turnResults = new TurnResults();
            things = world.TryToSearch(glassSwordPosition, out bWasSuccessful, turnResults);
            Assert.IsFalse(bWasSuccessful);

            PlayerCharacterRecord avatar = world.State.CharacterRecords.AvatarRecord;
            PlayerCharacterRecord.EquipResult equip =
                avatar.EquipEquipment(world.State.PlayerInventory, DataOvlReference.Equipment.GlassSword);
            Assert.IsTrue(equip == PlayerCharacterRecord.EquipResult.Success);

            things = world.TryToSearch(glassSwordPosition, out bWasSuccessful, turnResults);
            Assert.IsTrue(bWasSuccessful);
        }

        [Test] [TestCase(SaveFiles.b_carpet, false)] [TestCase(SaveFiles.b_carpet, true)]
        public void test_EquipRing(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);
            CombatItem ring =
                world.State.PlayerInventory.GetItemFromEquipment(DataOvlReference.Equipment.RingProtection);
            Assert.IsTrue(ring.Quantity == 35);

            // ring of protection, avatar
            PlayerCharacterRecord avatar = world.State.CharacterRecords.AvatarRecord;
            PlayerCharacterRecord.EquipResult equipResult = avatar.EquipEquipment(
                world.State.PlayerInventory, ring.SpecificEquipment);

            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.Success);
            Assert.IsTrue(ring.Quantity == 34);
        }

        [Test] [TestCase(SaveFiles.b_carpet, false)] [TestCase(SaveFiles.b_carpet, true)]
        public void test_EquipLotsOfStuff(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);
            CombatItem ring =
                world.State.PlayerInventory.GetItemFromEquipment(DataOvlReference.Equipment.RingProtection);
            Assert.IsTrue(ring.Quantity == 35);

            // ring of protection, avatar
            PlayerCharacterRecord avatar = world.State.CharacterRecords.AvatarRecord;
            PlayerCharacterRecord.EquipResult equipResult = avatar.EquipEquipment(
                world.State.PlayerInventory, ring.SpecificEquipment);

            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.Success);
            Assert.IsTrue(ring.Quantity == 34);

            equipResult = avatar.EquipEquipment(world.State.PlayerInventory, DataOvlReference.Equipment.TwoHHammer);
            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.SuccessUnequipRight);
            equipResult = avatar.EquipEquipment(world.State.PlayerInventory, DataOvlReference.Equipment.JeweledSword);
            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.Success);
            equipResult = avatar.EquipEquipment(world.State.PlayerInventory, DataOvlReference.Equipment.JewelShield);
            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.Success);
            equipResult = avatar.EquipEquipment(world.State.PlayerInventory, DataOvlReference.Equipment.IronHelm);
            Assert.IsTrue(equipResult == PlayerCharacterRecord.EquipResult.Success);
        }

        [Test] [TestCase(SaveFiles.b_carpet, false)] [TestCase(SaveFiles.b_carpet, true)]
        public void test_TalkToGuard(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);
            world.State.TheTimeOfDay.Hour = 12;
            world.State.TheTimeOfDay.Minute = 2;

            Point2D avatarPos = new(15, 23);
            Point2D guardPos = new(15, 22);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Jhelom, 0));
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Expected SmallMap");
            smallMap.MoveAvatar(avatarPos);

            TurnResults turnResults = new();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveStuff = world.TryToTalk(
                MapUnitMovement.MovementCommandDirection.North, turnResults);

            Assert.IsTrue(aggressiveStuff.Count > 0);
        }

        [Test] public void Test_SomeJimmiesFail()
        {
            for (int i = 0; i < 100; i++)
            {
                if (OddsAndLogic.IsJimmySuccessful(20)) continue;

                Debug.WriteLine("Jimmy didn't work on iteration " + i);
                return;
            }

            Assert.IsTrue(false);
            throw new Ultima5ReduxException("Jimmy never fails...");
        }

        [Test] [TestCase(SaveFiles.b_carpet, false)] [TestCase(SaveFiles.b_carpet, true)]
        public void test_LetDrudgeWorthAttackMe(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle, -1));
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be large map");
            Point2D getAttackedPosition = new(12, 11);
            smallMap.MoveAvatar(getAttackedPosition);

            var turnResults = new TurnResults();
            world.AdvanceTime(2, turnResults);
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_FreeSomeoneYewStocksPassTurns(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            world.State.TheGameOverrides.DebugTheLockPickingOverrides =
                GameOverrides.LockPickingOverrides.AlwaysSucceed;

            if (bReloadJson) world.ReLoadFromJson();

            //GameReferences.Instance.Initialize(DataDirectory);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Yew, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            Point2D avatarPosition = new(20, 17);
            smallMap.MoveAvatar(avatarPosition);

            Point2D manInStocksPosition = new(20, 16);
            var turnResults = new TurnResults();
            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos = world.TryToJimmyDoor(manInStocksPosition,
                world.State.CharacterRecords.AvatarRecord, out bool bWasSuccessful,
                turnResults);

            //Assert.IsTrue(bWasSuccessful);

            world.AdvanceTime(2, turnResults);
            world.AdvanceTime(2, turnResults);
            world.AdvanceTime(2, turnResults);
            world.AdvanceTime(2, turnResults);
            world.AdvanceTime(2, turnResults);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Yew, 0));
        }

        /// <summary>
        ///     Make sure that all Shoppe Keepers have real AI assignments
        /// </summary>
        /// <param name="saveFiles"></param>
        /// <param name="bReloadJson"></param>
        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_ShoppeKeeperAiMakesSense(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");
            // world.State.TheVirtualMap.LoadSmallMap(
            //     GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
            //         SmallMapReferences.SingleMapReference.Location.Trinsic, 0));
            //
            foreach (SmallMapReferences.SingleMapReference smr in GameReferences.Instance.SmallMapRef.MapReferenceList)
            {
                SmallMapReferences.SingleMapReference singleMap =
                    GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(smr.MapLocation, smr.Floor);
                // we don't test dungeon maps here
                if (singleMap.MapType == Map.Maps.Dungeon) continue;
                world.State.TheVirtualMap.LoadSmallMap(singleMap);
                foreach (NonPlayerCharacter npc in world.State.TheVirtualMap.CurrentMap.CurrentMapUnits
                             .NonPlayerCharacters)
                {
                    // if (mapUnit is NonPlayerCharacter npc)
                    // {
                    if (!npc.NpcRef.IsShoppeKeeper) continue;
                    bool bFoundNonZero = false;
                    foreach (byte a in npc.NpcRef.Schedule.AiTypeList)
                    {
                        if (a > 0) bFoundNonZero = true;
                    }

                    Assert.IsTrue(bFoundNonZero, $"ShoppeKeeper didn't have a non zero AI: {npc.FriendlyName}");
                }
            }
        }


        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_ShoppeKeeperYewFood(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Yew, 0));
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            foreach (NonPlayerCharacter npc in
                     world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                // if (mapUnit is NonPlayerCharacter npc)
                // {
                if (!npc.NpcRef.IsShoppeKeeper) continue;
                bool bFoundNonZero = false;

                smallMap.MoveAvatar(new Point2D(npc.MapUnitPosition.X - 1, npc.MapUnitPosition.Y));
                TurnResults turnResults = new();

                foreach (byte a in npc.NpcRef.Schedule.AiTypeList)
                {
                    if (a > 0) bFoundNonZero = true;
                }

                Assert.IsTrue(bFoundNonZero, $"ShoppeKeeper didn't have a non zero AI: {npc.FriendlyName}");

                List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnitInfos =
                    world.TryToTalk(MapUnitMovement.MovementCommandDirection.East, turnResults);
                // }
            }
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_WindmereDaemonGuards(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Yew, 0));

            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            TurnResults turnResults = new();
            world.AdvanceTime(2, turnResults);

            foreach (NonPlayerCharacter npc in
                     world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                _ = "";
            }
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_BuildConversationAndTest(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Palace_of_Blackthorn, 0));

            TurnResults turnResults = new();
            world.AdvanceTime(2, turnResults);

            foreach (NonPlayerCharacter npc in
                     world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                _ = "";
            }

            string talkScripts = GameReferences.Instance.TalkScriptsRef.Serialize();

            TalkScript bguardTalkScript = GameReferences.Instance.TalkScriptsRef.GetCustomTalkScript("BlackthornGuard");
            Assert.IsNotNull(bguardTalkScript);
            //Assert.IsTrue(bguardTalkScript.NumberOfScriptLines > 5);

            TalkScript genericGuard =
                GameReferences.Instance.TalkScriptsRef.GetCustomTalkScript("GenericGuardExtortion");

            foreach (NonPlayerCharacter npc in
                     world.State.TheVirtualMap.CurrentMap.CurrentMapUnits.NonPlayerCharacters)
            {
                _ = "";
            }
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_MakeAWell(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Empath_Abbey, 0));

            TurnResults turnResults = new();
            Point2D wellPosition = new(15, 15);
            var wishingWell =
                WishingWell.Create(world.State.TheVirtualMap.CurrentMap.CurrentSingleMapReference.MapLocation,
                    wellPosition, world.State.TheVirtualMap.CurrentMap.CurrentSingleMapReference.Floor);

            List<VirtualMap.AggressiveMapUnitInfo> aggressiveMapUnits =
                world.TryToLook(wellPosition, out World.SpecialLookCommand specialLookCommand, turnResults);

            Conversation convo = world.CreateConversationAndBegin(wishingWell.NpcState,
                OnUpdateOfEnqueuedScriptItemHandleDelwyn, "WishingWell");
            convo.BeginConversation();
            string npcName = wishingWell.FriendlyName;
            Assert.False(npcName == "None");
            convo.AddUserResponse("yes");
        }


        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_StairsGoRightWay(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Jhelom, 1));
            if (world.State.TheVirtualMap.CurrentMap is not SmallMap smallMap)
                throw new Ultima5ReduxException("Should be small map");

            var eastAndDownPosition = new Point2D(7, 28);
            TileReference eastAndDown = world.State.TheVirtualMap.CurrentMap.GetTileReference(eastAndDownPosition);
            Assert.IsTrue(TileReferences.IsStaircase(eastAndDown.Index));
            Assert.IsTrue(smallMap.IsStairGoingDown(eastAndDownPosition,
                out TileReference eastAndDownCalcReference));
            Assert.IsTrue(TileReferences.IsStaircase(eastAndDownCalcReference.Index));
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_DoorsInRightDirection(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles);

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Palace_of_Blackthorn, -1));

            var doorPos = new Point2D(8, 18);

            Assert.False(world.State.TheVirtualMap.CurrentMap.IsHorizDoor(doorPos));
        }

        [Test] [TestCase(SaveFiles.Britain2)] public void test_DungeonReferences(SaveFiles saveFiles)
        {
            GameReferences.Initialize(DataDirectory);
            var dungeonReferences = new DungeonReferences(DataDirectory);
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_LoadDeceitDungeonInWorld(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            SingleDungeonMapFloorReference deceitFirstFloor = GameReferences.Instance.DungeonReferences
                .GetDungeon(SmallMapReferences.SingleMapReference.Location.Deceit)
                .GetSingleDungeonMapFloorReferenceByFloor(3);
            world.State.TheVirtualMap.LoadDungeonMap(deceitFirstFloor, new Point2D(2, 1));
            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentSingleMapReference.Floor == 3);
        }

        [Test] [TestCase(SaveFiles.fresh)] [TestCase(SaveFiles.Britain2)]
        public void test_LoadDeceitDungeonFromLegacyAndReloadJson(SaveFiles saveFiles)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            SingleDungeonMapFloorReference deceitFirstFloor = GameReferences.Instance.DungeonReferences
                .GetDungeon(SmallMapReferences.SingleMapReference.Location.Deceit)
                .GetSingleDungeonMapFloorReferenceByFloor(3);
            world.State.TheVirtualMap.LoadDungeonMap(deceitFirstFloor, new Point2D(2, 1));

            world.ReLoadFromJson();

            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentSingleMapReference.Floor == 3);
        }

        [Test] [TestCase(SaveFiles.fresh, false)]
        public void test_CheckAllDungeonRoomLocations(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            Console.WriteLine("Dungeon,X,Y,Floor,RoomNumber");

            foreach (DungeonMapReference dungeon in GameReferences.Instance.DungeonReferences.DungeonMapReferences)
            {
                Point2D dungeonLocation = GameReferences.Instance.LargeMapRef.LocationXy[dungeon.DungeonLocation];
                Console.WriteLine($@"{dungeon.DungeonLocation},{dungeonLocation.X},{dungeonLocation.Y}");
            }

            foreach (DungeonMapReference dungeon in GameReferences.Instance.DungeonReferences.DungeonMapReferences)
            {
                for (int i = 0; i < 8; i++)
                {
                    SingleDungeonMapFloorReference singleFloor = dungeon.GetSingleDungeonMapFloorReferenceByFloor(i);

                    world.State.TheVirtualMap.LoadDungeonMap(singleFloor, Point2D.Zero);
                    Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentPosition.Floor == i);

                    for (int nCol = 0; nCol < 8; nCol++)
                    {
                        for (int nRow = 0; nRow < 8; nRow++)
                        {
                            var position = new Point2D(nCol, nRow);
                            DungeonTile dungeonTile = singleFloor.GetDungeonTile(position);
                            if (dungeonTile.TheTileType == DungeonTile.TileType.Room)
                            {
                                Console.WriteLine(
                                    $"{singleFloor.DungeonLocation},{position.X},{position.Y},{singleFloor.DungeonFloor},{dungeonTile.RoomNumber}");
                            }
                        }
                    }
                }
            }
        }

        [Test] [TestCase(SaveFiles.fresh, false)] [TestCase(SaveFiles.fresh, true)]
        public void test_MimicShouldNotAttack(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadCombatMap(
                GameReferences.Instance.CombatMapRefs.GetSingleCombatMapReference(
                    SingleCombatMapReference.Territory.Dungeon, 15),
                SingleCombatMapReference.EntryDirection.North, world.State.CharacterRecords);
            if (world.State.TheVirtualMap.CurrentMap is not CombatMap combatMap)
                throw new Ultima5ReduxException("Should be combat map");

            TurnResults turnResults = new();
            // let's process a turn
            //CombatMap.CombatTurnResult combatTurnResult = 

            combatMap.ProcessEnemyTurn(turnResults,
                out CombatMapUnit activeCombatMapUnit,
                out CombatMapUnit targetedCombatMapUnit,
                out Point2D missedPoint);

            if (activeCombatMapUnit is not Enemy enemy)
            {
                throw new Ultima5ReduxException("Trying to process the turn of an enemy, but the active unit is: " +
                                                activeCombatMapUnit.GetType());
            }

            Assert.True(turnResults.HasTurnResult);
        }

        [Test] public void test_DiagonalAttackSim()
        {
            var from = new Point2D(3, 5);
            var to = new Point2D(4, 4);
            List<Point2D> result = from.Raytrace(to);
            Assert.IsTrue(result.Count == 2);

            var from2 = new Point2D(5, 3);
            var to2 = new Point2D(4, 4);
            List<Point2D> result2 = from2.Raytrace(to2);
            Assert.IsTrue(result2.Count == 2);
        }

        [Test] [TestCase(SaveFiles.brandnew, false)] [TestCase(SaveFiles.brandnew, true)]
        public void test_BasicNewFileLoad(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.CurrentMap.RecalculateVisibleTiles(new Point2D(15, 15));
        }

        [Test] [TestCase(SaveFiles.brandnew, false)] [TestCase(SaveFiles.brandnew, true)]
        public void test_CheckSmallMapsLoaded(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                    0));
            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentPosition.Floor == 0);
            if (world.State.TheVirtualMap.CurrentMap is SmallMap smallMap)
            {
                Assert.IsNotNull(smallMap);
                if (smallMap == null) throw new NullReferenceException();
                Assert.IsNotNull(smallMap.GetSmallMaps());
            }
            else
            {
                throw new Ultima5ReduxException("Supposed to be small map");
            }
        }

        public void test_BasicLoadDifferentFloors(SaveFiles saveFiles, bool bReloadJson)
        {
            World world = CreateWorldFromLegacy(saveFiles, true, false);
            Assert.NotNull(world);
            Assert.NotNull(world.State);

            if (bReloadJson) world.ReLoadFromJson();

            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                    0));
            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentPosition.Floor == 0);
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                    -1));
            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentPosition.Floor == -1);
            world.State.TheVirtualMap.LoadSmallMap(
                GameReferences.Instance.SmallMapRef.GetSingleMapByLocation(
                    SmallMapReferences.SingleMapReference.Location.Lord_Britishs_Castle,
                    1));
            Assert.True(world.State.TheVirtualMap.CurrentMap.CurrentPosition.Floor == 1);
        }
    }
}