using IDWalks.Data;
using IDWalks.Models.Domines;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Repository
{
    public interface IWalkRepo
    {
        Task<IEnumerable<Walk>> GetAllWalks();
    }
    public class WalkRepo : IWalkRepo
    {
        private readonly IndiaWalkDbContext indiaWalkDbContext;

        public WalkRepo(IndiaWalkDbContext indiaWalkDbContext)
        {
            this.indiaWalkDbContext = indiaWalkDbContext;
        }
        public async Task<IEnumerable<Walk>> GetAllWalks()
        {
            return await  indiaWalkDbContext.walks.ToListAsync();
        }
    }
}
