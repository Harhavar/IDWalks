using IDWalks.Data;
using IDWalks.Models.Domines;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Repository
{
    public interface IRegionsRepo
    {
        public Task<IEnumerable<Region>> GetAllAsync();
    }
    public class RegionsRepo : IRegionsRepo
    {
        private readonly IndiaWalkDbContext indiaWalkDbContext;

        public RegionsRepo(IndiaWalkDbContext indiaWalkDbContext)
        {
            this.indiaWalkDbContext = indiaWalkDbContext;
        }
        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await indiaWalkDbContext.regions.ToListAsync(); 
        }
    }
}
