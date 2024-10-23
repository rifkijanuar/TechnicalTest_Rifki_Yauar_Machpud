using RifkiTestTechnicalSkill.Models;

namespace RifkiTestTechnicalSkill.Services.Interface
{
    public interface IGenreService
    {
        Task AddGenre(Genre genre);
        Task UpdateGenre(Genre genre);
        Task<Genre?> GetGenreById(int id);
        Task DeleteGenre(Genre genre);
        Task<IEnumerable<Genre>> GetGenres();
    }
}
