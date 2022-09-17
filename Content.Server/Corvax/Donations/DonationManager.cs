﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Robust.Shared;
using Robust.Shared.Configuration;
using Robust.Shared.Network;

namespace Content.Server.Corvax.Donations;

public sealed class DonationManager : IDonationManager, IPostInjectInit
{
    [Dependency] private readonly IConfigurationManager _cfg = default!;

    private readonly HttpClient _httpClient = new();

    private ISawmill _sawmill = default!;
    private string _apiUrl = string.Empty;

    private readonly Dictionary<NetUserId, string> _cachedOOCColors = new();

    public void PostInject()
    {
        _sawmill = Logger.GetSawmill("donations");
        _cfg.OnValueChanged(CVars.DonationApiUrl, s => _apiUrl = s, true);
    }

    public async Task<string?> GetDonatorOOCColor(NetUserId userId)
    {
        if (_cachedOOCColors.TryGetValue(userId, out var cachedColor))
            return cachedColor;

        var info = await GetDonatorInfo(userId);
        var color = info?.OOCColor;
        if (color == null)
            return null;

        _cachedOOCColors[userId] = color;
        return color;
    }

    private async Task<DonatorInfoResponse?> GetDonatorInfo(NetUserId userId)
    {
        if (string.IsNullOrEmpty(_apiUrl))
            return null;

        var url = $"{_apiUrl}/donators/${userId.ToString()}";
        var response = await _httpClient.GetAsync(url);
        if (!response.IsSuccessStatusCode)
        {
            var errorText = await response.Content.ReadAsStringAsync();
            _sawmill.Error(
                "Failed to get player donator OOC color from API: [{StatusCode}] {Response}",
                response.StatusCode,
                errorText);
            return null;
        }

        return await response.Content.ReadFromJsonAsync<DonatorInfoResponse>();
    }
    
    private sealed record DonatorInfoResponse(string UserId, string OOCColor);
}