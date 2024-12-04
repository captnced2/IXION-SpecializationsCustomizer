using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Il2CppSystem;
using Enum = System.Enum;

namespace SimpleSpecializationsCustomizer;

public class PluginConfig
{
    public static void init()
    {
        foreach (var b in Plugin.buildings)
        {
            String section = "Buildings." + b.Name;
            var subCategory = new HashSet<String> { "Stockpile", "Battery" };
            foreach (var sub in subCategory)
                if (((string)b.Name).Contains(sub))
                    section = "Buildings." + sub + "." + ((string)b.Name).Replace(sub, "");

            b.SpecializationScore = Plugin.config.Bind(section, "specializationScore", b.SpecializationScore).Value;
            var s = Plugin.config.Bind(section, "specializations", string.Join(", ", b.Specializations),
                    "Allowed Values: " + Enum.GetNames<SpecializationType>().Join())
                .Value.Replace(" ", "").Split(",").ToHashSet();
            var specializations = new HashSet<SpecializationType>();
            foreach (var specialization in s)
            {
                Enum.TryParse(specialization, true, out SpecializationType sp);
                if (sp != SpecializationType.None)
                    specializations.Add(sp);
            }

            b.Specializations = specializations;
        }

        foreach (var s in Plugin.specializationTiers)
        {
            String section = "Tiers." + s.Specialization;
            s.Tier1 = Plugin.config.Bind(section, "tier1", s.Tier1).Value;
            s.Tier2 = Plugin.config.Bind(section, "tier2", s.Tier2).Value;
        }
    }
}

public class SpecializationTier()
{
    public SpecializationTier(SpecializationType specialization, int tier1, int tier2) : this()
    {
        Specialization = specialization;
        Tier1 = tier1;
        Tier2 = tier2;
    }

    public SpecializationType Specialization { get; set; }

    public int Tier1 { get; set; }

    public int Tier2 { get; set; }
}

public class Building()
{
    public Building(String name, int specializationScore, HashSet<SpecializationType> specializations) : this()
    {
        Name = name;
        SpecializationScore = specializationScore;
        Specializations = specializations;
    }

    public String Name { get; set; }

    public int SpecializationScore { get; set; }

    public HashSet<SpecializationType> Specializations { get; set; }
}

public enum SpecializationType
{
    None,
    Space,
    Food,
    Industry,
    Population,
    Recycling
}