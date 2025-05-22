using System.Linq;
using BulwarkStudios.Stanford.Common.Specialization;
using BulwarkStudios.Stanford.Torus.Buildings;
using BulwarkStudios.Stanford.Torus.UI;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            if (sList == null)
            {
                sList = Resources.FindObjectsOfTypeAll<SpecializationList>().First();
                foreach (var tier in Plugin.specializationTiers)
                    switch (tier.Specialization)
                    {
                        case SpecializationType.Space:
                        {
                            sList.space.t1RequiredScore = tier.Tier1;
                            sList.space.t2RequiredScore = tier.Tier2;
                            break;
                        }
                        case SpecializationType.Food:
                        {
                            sList.food.t1RequiredScore = tier.Tier1;
                            sList.food.t2RequiredScore = tier.Tier2;
                            break;
                        }
                        case SpecializationType.Industry:
                        {
                            sList.industry.t1RequiredScore = tier.Tier1;
                            sList.industry.t2RequiredScore = tier.Tier2;
                            break;
                        }
                        case SpecializationType.Population:
                        {
                            sList.population.t1RequiredScore = tier.Tier1;
                            sList.population.t2RequiredScore = tier.Tier2;
                            break;
                        }
                        case SpecializationType.Recycling:
                        {
                            sList.recycling.t1RequiredScore = tier.Tier1;
                            sList.recycling.t2RequiredScore = tier.Tier2;
                            break;
                        }
                    }
            }

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

    [HarmonyPatch(typeof(UIWindowBuilding), nameof(UIWindowBuilding.BeforeOpen))]
    public static class UIWindowBuildingPatcher
    {
        public static void Postfix(UIWindowBuilding __instance)
        {
            if (__instance.effectBuilding is null)
            {
                var effect = GameObject.Find("UI Window Building Effect");
                var newEffect = Object.Instantiate(effect,
                    __instance.transform.FindChild("ContentBuilding").FindChild("Viewport").FindChild("Content")
                        .FindChild("Built").transform);
                newEffect.transform.SetAsFirstSibling();
                __instance.effectBuilding = newEffect.GetComponent<UIWindowBuildingActionEffect>();
                newEffect.GetComponent<VerticalLayoutGroup>().enabled = true;
            }

            __instance.effectBuilding.txtEffectDesc.text = "Specialization score: " +
                                                           __instance.building.Data.specializationScore + "\n\n" +
                                                           __instance.effectBuilding.txtEffectDesc.text;
            if (__instance.building.Data.name.Equals("DataListeningCenter"))
            {
                __instance.effectBuilding.txtEffectDesc.text += "\n\nSector specialization scores:\n";
                foreach (var pair in __instance.building.GetSector().state.specializations.specializationStates)
                    __instance.effectBuilding.txtEffectDesc.text +=
                        pair.key.name + ": " + pair.value.score + " - T1: " + pair.key.t1RequiredScore + ", T2: " +
                        pair.key.t2RequiredScore + "\n";
            }

            if (__instance.transform.name.Equals("UI Window Building Docking Bay"))
                __instance.effectBuilding.txtEffectDesc.text =
                    "Specialization score: " + __instance.building.Data.specializationScore;
        }
    }
}