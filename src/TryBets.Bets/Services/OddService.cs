using System.Net.Http;
using TryBets.Bets.Models;
namespace TryBets.Bets.Services;

//testando123.

public class OddService : IOddService
{
    private readonly HttpClient _client;
    public OddService(HttpClient client)
    {
        _client = client;
    }

    public async Task<object> UpdateOdd(int MatchId, int TeamId, decimal BetValue)
    {
        throw new NotImplementedException();
        var response = await _client.GetAsync($"http://localhost:5504/{MatchId}/{TeamId}/{BetValue}");
        var updatedMatch = await response.Content.ReadAsStringAsync();
        return updatedMatch;
    }
}