using System.Linq;
using BulwarkStudios.Stanford.Common.Players;
using BulwarkStudios.Stanford.Common.Specialization;
using BulwarkStudios.Stanford.Torus.Buildings;
using BulwarkStudios.Stanford.Torus.Sectors;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using UnityEngine;

namespace SimpleSpecializationsCustomizer;

public class Patches
{
    private static SpecializationList sList;

    private static SpecializationData getSpecializationDataFromSpecializationType(SpecializationType type)
    {
        switch (type)
        {
            case SpecializationType.Space: return sList.space;
            case SpecializationType.Food: return sList.food;
            case SpecializationType.Industry: return sList.industry;
            case SpecializationType.Population: return sList.population;
            case SpecializationType.Recycling: return sList.recycling;
        }

        return null;
    }

    [HarmonyPatch(typeof(BuildingInstance), nameof(BuildingInstance.ApplyState))]
    public static class BuildingInstancePatcher
    {
        public static void Postfix(BuildingInstance __instance)
        {
            if (sList == null) sList = Resources.FindObjectsOfTypeAll<SpecializationList>().First();
            if (!__instance.Data.name.Contains("Supplies"))
                foreach (var building in Plugin.buildings)
                    if (__instance.Data.name.Equals(building.Name))
                    {
                        __instance.Data.specializationScore = building.SpecializationScore;
                        var buildingSpecializations = new HashSet<SpecializationData>();
                        foreach (var specialization in building.Specializations)
                            buildingSpecializations.Add(getSpecializationDataFromSpecializationType(specialization));
                        __instance.Data.specializationTags = buildingSpecializations;
                    }
        }
    }

    [HarmonyPatch(typeof(CommandPlayerConstructBuilding), nameof(CommandPlayerConstructBuilding.OnStart))]
    public static class PlayerConstructBuildingPatcher
    {
        public static void Postfix(CommandPlayerConstructBuilding __instance)
        {
            Plugin.Log.LogInfo(__instance.building.Data.name + " - " + __instance.building.Data.specializationScore +
                               ":");
            foreach (var s in __instance.building.Data.specializationTags) Plugin.Log.LogInfo("    " + s.name);
            foreach (var b in Plugin.buildings)
            {
                Plugin.Log.LogInfo(b.Name + " - " + b.SpecializationScore + ":");
                foreach (var s in b.Specializations) Plugin.Log.LogInfo("    " + s);
            }

            foreach (var sectorBehaviour in Resources.FindObjectsOfTypeAll<TorusSectorBehaviour>())
            {
                Plugin.Log.LogInfo(sectorBehaviour.name + ":");
                foreach (var specializations in sectorBehaviour.State.specializations.specializationStates)
                    Plugin.Log.LogInfo("    " + specializations.key.name + ": " + specializations.value.score + " - " +
                                       specializations.key.t1RequiredScore + " - " +
                                       specializations.key.t2RequiredScore);
            }
        }
    }
}