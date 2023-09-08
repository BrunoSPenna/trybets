using TryBets.Matches.DTO;

namespace TryBets.Matches.Repository;

public class TeamRepository : ITeamRepository
{
    protected readonly ITryBetsContext _context;
    public TeamRepository(ITryBetsContext context)
    {
        _context = context;
    }

    public IEnumerable<TeamDTOResponse> Get()
    {
        var teams = _context.Teams.ToList();

        // Mapeia as equipes para objetos TeamDTOResponse
        var teamDTOs = teams.Select(team => new TeamDTOResponse
        {
            TeamId = team.TeamId,
            TeamName = team.TeamName
        });

        return teamDTOs;
    }
}