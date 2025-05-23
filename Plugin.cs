﻿using System.Collections.Generic;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using IMHelper;

namespace SimpleSpecializationsCustomizer;

[BepInPlugin(Guid, Name, Version)]
[BepInProcess("IXION.exe")]
[BepInDependency("captnced.IMHelper", BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BasePlugin
{
    private const string Guid = "captnced.SimpleSpecializationsCustomizer";
    private const string Name = "SimpleSpecializationsCustomizer";
    private const string Version = "1.2.0";
    private new static ManualLogSource Log;
    internal static ConfigFile config;
    internal static HashSet<Building> buildings;
    internal static HashSet<SpecializationTier> specializationTiers;
    private static bool enabled = true;
    private static Harmony harmony;

    public override void Load()
    {
        Log = base.Log;
        config = Config;
        defineBuildings();
        defineTiers();
        PluginConfig.init();
        harmony = new Harmony(Guid);
        if (IL2CPPChainloader.Instance.Plugins.ContainsKey("captnced.IMHelper")) setEnabled();
        if (!enabled)
            Log.LogInfo("Disabled by IMHelper!");
        else
            init();
    }

    private static void setEnabled()
    {
        enabled = ModsMenu.isSelfEnabled();
    }

    private static void init()
    {
        harmony.PatchAll();
        foreach (var patch in harmony.GetPatchedMethods())
            Log.LogInfo("Patched " + patch.DeclaringType + ":" + patch.Name);
        Log.LogInfo("Loaded \"" + Name + "\" version " + Version + "!");
    }

    private static void disable()
    {
        harmony.UnpatchSelf();
        Log.LogInfo("Unloaded \"" + Name + "\" version " + Version + "!");
    }

    public static void enable(bool value)
    {
        enabled = value;
        if (enabled) init();
        else disable();
    }

    private void defineTiers()
    {
        specializationTiers = new HashSet<SpecializationTier>();
        specializationTiers.Add(new SpecializationTier(SpecializationType.Industry, 300, 800));
        specializationTiers.Add(new SpecializationTier(SpecializationType.Population, 400, 1000));
        specializationTiers.Add(new SpecializationTier(SpecializationType.Space, 450, 700));
        specializationTiers.Add(new SpecializationTier(SpecializationType.Food, 400, 800));
        specializationTiers.Add(new SpecializationTier(SpecializationType.Recycling, 300, 800));
    }

    private void defineBuildings()
    {
        buildings = new HashSet<Building>();
        buildings.Add(new Building("Workshop", 16, new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("StockpileSmall", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("StockpileMedium", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("StockpileLarge", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("BatteryT1", 9, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("BatteryT2", 25, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("BatteryT3", 49, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("FireStation", 36, new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("DroneLandingBay", 64,
            new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("DockingBay", 108, new HashSet<SpecializationType> { SpecializationType.Space }));
        buildings.Add(new Building("EVAAirlock", 108, new HashSet<SpecializationType> { SpecializationType.Space }));
        buildings.Add(new Building("ProbeLauncher", 108, new HashSet<SpecializationType> { SpecializationType.Space }));
        buildings.Add(new Building("ColonyTrainingCenter", 0,
            new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("TechLab", 81, new HashSet<SpecializationType> { SpecializationType.Space }));
        buildings.Add(new Building("SteelMill", 108, new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("ElectronicsFactory", 90,
            new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("PolymerRefinery", 54,
            new HashSet<SpecializationType> { SpecializationType.Industry }));
        buildings.Add(new Building("FusionStation", 72,
            new HashSet<SpecializationType> { SpecializationType.Food, SpecializationType.Industry }));
        buildings.Add(new Building("WaterTreatment", 35,
            new HashSet<SpecializationType> { SpecializationType.Recycling, SpecializationType.Industry }));
        buildings.Add(new Building("WasteTreatment", 81,
            new HashSet<SpecializationType> { SpecializationType.Recycling, SpecializationType.Industry }));
        buildings.Add(new Building("NuclearPowerPlant", 0,
            new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("BaseQuarter", 9,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("OptimizedQuarter", 9,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("DomoticQuarter", 18,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(
            new Building("CellHousing", 16, new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("CryonicCenter", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("Infirmary", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("HealthCenter", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("Refectory", 24,
            new HashSet<SpecializationType> { SpecializationType.Population, SpecializationType.Food }));
        buildings.Add(new Building("InsectFarm", 32, new HashSet<SpecializationType> { SpecializationType.Food }));
        buildings.Add(new Building("CropsFarm", 18, new HashSet<SpecializationType> { SpecializationType.Food }));
        buildings.Add(new Building("CropsFarmField", 16, new HashSet<SpecializationType> { SpecializationType.Food }));
        buildings.Add(new Building("AlgaeFarm", 24, new HashSet<SpecializationType> { SpecializationType.Food }));
        buildings.Add(new Building("AlgaeFarmPlantation", 24,
            new HashSet<SpecializationType> { SpecializationType.Food }));
        buildings.Add(new Building("MushroomWall", 81,
            new HashSet<SpecializationType> { SpecializationType.Recycling, SpecializationType.Food }));
        buildings.Add(new Building("DataListeningCenter", 0,
            new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("AlternativeLifeCenter", 28,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("GeneticConatusMemorial", 49,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("LunaclysmMemorial", 49,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("MardukMemorial", 49,
            new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("LawEnforcement", 28, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(new Building("HullTemple", 0, new HashSet<SpecializationType> { SpecializationType.None }));
        buildings.Add(
            new Building("Observatory", 38, new HashSet<SpecializationType> { SpecializationType.Population }));
        buildings.Add(new Building("ExofightingDome", 100,
            new HashSet<SpecializationType> { SpecializationType.Population }));
    }
}