using TryBets.Odds.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Globalization;

namespace TryBets.Odds.Repository
{
    public class OddRepository : IOddRepository
    {
        protected readonly ITryBetsContext _context;
        public OddRepository(ITryBetsContext context)
        {
            _context = context;
        }

        public Match Patch(int matchId, int teamId, string betValue)
        {
            Match foundMatch = _context.Matches.FirstOrDefault(m => m.MatchId == matchId)!;
            if (foundMatch == null) throw new Exception("Match not founded");
            Team foundTeam = _context.Teams.FirstOrDefault(t => t.TeamId == teamId)!;
            if (foundTeam == null) throw new Exception("Team not founded");
            string newBetValue = betValue.Replace(",", ".");
            if (foundMatch.MatchTeamAId != teamId && foundMatch.MatchTeamBId != teamId) throw new Exception("Team is not in this match");
            if (foundMatch.MatchTeamAId == teamId) foundMatch.MatchTeamAValue += decimal.Parse(newBetValue, CultureInfo.InvariantCulture);
            else foundMatch.MatchTeamBValue += decimal.Parse(newBetValue, CultureInfo.InvariantCulture);

            _context.Matches.Update(foundMatch);
            _context.SaveChanges();

            return new Match
            {
                MatchId = matchId,
                MatchDate = foundMatch.MatchDate,
                MatchTeamAId = foundMatch.MatchTeamAId,
                MatchTeamBId = foundMatch.MatchTeamBId,
                MatchTeamAValue = foundMatch.MatchTeamAValue,
                MatchTeamBValue = foundMatch.MatchTeamBValue,
                MatchFinished = foundMatch.MatchFinished,
                MatchWinnerId = foundMatch.MatchWinnerId,
                MatchTeamA = null,
                MatchTeamB = null,
                Bets = null,
            };
        }
    }
}
