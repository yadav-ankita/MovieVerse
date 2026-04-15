using MovieVerse.Models.GenericRepo;

namespace MovieVerse.Models.Repo
{
    public interface IRecordsRepo : IGenericRepo<Records>
    {
        Task<List<Records>> GetRecordsByUserIdAsync(string userId);
    }
}
