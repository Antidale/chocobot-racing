
using System.Text.Json.Serialization;

namespace chocobot_racing.DTOs;

public class GenerateRequest
{
    public string flags { get; init; } = string.Empty;
    public string? seed { get; init; }
    public MetaConfiguration? metaconfig { get; init; } = null;

    /// <summary>
    /// Encapsulates the various metaconfiguration options for generating seeds
    /// </summary>
    public class MetaConfiguration
    {
        public bool hide_flags { get; set; } = true;
        public TestSetings? test { get; set; } = null;

        public class TestSetings
        {
            /// <summary>
            /// Skip opening cutscene
            /// </summary>
            public bool quickstart { get; init; } = true;

            /// <summary>
            /// Begin with open underworld, hook, hovercraft, and Big Whale
            /// </summary>
            public bool open { get; init; } = true;

            /// <summary>
            /// All characters available at Mysidia (forces Cbye flag off)
            /// </summary>
            public bool characters { get; init; } = true;

            /// <summary>
            /// Prefill inventory
            /// </summary>
            public bool items { get; init; } = true;

            /// <summary>
            /// Start with max GP
            /// </summary>
            public bool gp { get; init; } = true;

            /// <summary>
            /// Skip boss battles
            /// </summary>
            public bool noboss { get; init; } = false;

            /// <summary>
            /// Force -hobs spell. This option forces the -vanilla:hobs flag to be disabled.
            /// </summary>
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
            public string? hobbs { get; init; } = null;

            public string? boss
            {
                get =>
                string.IsNullOrWhiteSpace(boss_location) || string.IsNullOrWhiteSpace(boss_fight)
                ? null
                : $"{{\"{boss_location}_slot\": \"{boss_fight}\"}}";
            }

            /// <summary>
            /// In conjuction with boss_fight, forces a boss at a location.
            /// When enabled, speaking to the soldier outside the Baron Training Room will directly trigger this battle. This option will force the Bstandard flag to be enabled.
            /// </summary>
            [JsonIgnore]
            public string boss_location { get; init; } = string.Empty;

            /// <summary>
            /// In conjuction with boss_location, forces a boss at a location.
            /// When enabled, speaking to the soldier outside the Baron Training Room will directly trigger this battle. This option will force the Bstandard flag to be enabled.
            /// </summary>
            [JsonIgnore]
            public string boss_fight { get; init; } = string.Empty;

        }
    }
}
