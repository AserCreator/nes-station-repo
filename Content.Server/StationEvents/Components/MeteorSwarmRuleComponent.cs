using Content.Server.StationEvents.Events;

namespace Content.Server.StationEvents.Components;

[RegisterComponent, Access(typeof(MeteorSwarmRule))]
public sealed partial class MeteorSwarmRuleComponent : Component
{
    [DataField("cooldown")]
    public float Cooldown;

    /// <summary>
    /// We'll send a specific amount of waves of meteors towards the station per ending rather than using a timer.
    /// </summary>
    [DataField("waveCounter")]
    public int WaveCounter;

    [DataField("minimumWaves")]
    public int MinimumWaves = 30;

    [DataField("maximumWaves")]
    public int MaximumWaves = 80;

    [DataField("minimumCooldown")]
    public float MinimumCooldown = 6f;

    [DataField("maximumCooldown")]
    public float MaximumCooldown = 20f;

    [DataField("meteorsPerWave")]
    public int MeteorsPerWave = 10;

    [DataField("meteorVelocity")]
    public float MeteorVelocity = 20f;

    [DataField("maxAngularVelocity")]
    public float MaxAngularVelocity = 0.25f;

    [DataField("minAngularVelocity")]
    public float MinAngularVelocity = -0.25f;
}
