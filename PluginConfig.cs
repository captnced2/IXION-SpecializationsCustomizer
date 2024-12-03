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
    }
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