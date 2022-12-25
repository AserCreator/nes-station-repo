﻿using Robust.Shared.Prototypes;

namespace Content.Shared.Corvax.TTS;

/// <summary>
/// Prototype represent available TTS voices
/// </summary>
[Prototype("ttsVoice")]
// ReSharper disable once InconsistentNaming
public sealed class TTSVoicePrototype : IPrototype
{
    [IdDataFieldAttribute]
    public string ID { get; } = default!;
    
    [DataField("name")]
    public string Name { get; } = string.Empty;

    [ViewVariables(VVAccess.ReadWrite)]
    [DataField("speaker", required: true)]
    public string Speaker { get; } = string.Empty;
    
    /// <summary>
    /// Whether the species is available "at round start" (In the character editor)
    /// </summary>
    [DataField("roundStart")]
    public bool RoundStart { get; } = true;
    
    [DataField("sponsorOnly")]
    public bool SponsorOnly { get; } = false;
}
