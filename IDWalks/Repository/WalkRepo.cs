using IDWalks.Data;
using IDWalks.Models.Domines;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace IDWalks.Repository
{
    public interface IWalkRepo
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkAsync(Guid Id);

        Task<Walk> UpdateAsync(Walk walk);

        Task<Walk> UpdateWalkAsync(Guid id , Walk walk);

        Task<Walk> DeleteAsync(Guid id);
    }
    public class WalkRepo : IWalkRepo
    {
        private readonly IndiaWalkDbContext indiaWalkDbContext;

        public WalkRepo(IndiaWalkDbContext indiaWalkDbContext)
        {
            this.indiaWalkDbContext = indiaWalkDbContext;
        }

        public async Task<Walk> UpdateAsync(Walk walk)
        {
            walk.id = Guid.NewGuid();
            await indiaWalkDbContext.walks.AddAsync(walk);
            await indiaWalkDbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await  indiaWalkDbContext.walks.
                Include(x => x.region).
                Include( x => x.walkDeficulty)
                .ToListAsync();
        }

        public async Task<Walk> GetWalkAsync(Guid Id)
        {
            return await indiaWalkDbContext.walks
                .Include( x => x.region)
                .Include( x => x.walkDeficulty)
                .FirstOrDefaultAsync(x => x.id == Id);
        }

        public async Task<Walk> UpdateWalkAsync(Guid id, Walk walk)
        {
            var existingwalk = await indiaWalkDbContext.walks.FindAsync(id);

            if(existingwalk != null)
            {
                existingwalk.name = walk.name;
                    existingwalk.Length = walk.Length;
                    existingwalk.RegionId = walk.RegionId;
                    existingwalk.WalkDeficultyId = walk.WalkDeficultyId;
                await indiaWalkDbContext.SaveChangesAsync();


                return existingwalk;
            }
            return null;
        }

        public async Task<Walk> DeleteAsync(Guid id)
        {
            var existWalk = await indiaWalkDbContext.walks.FirstOrDefaultAsync(x =>x.id == id);

            if ( existWalk == null)
            {

                return null;
            }

            indiaWalkDbContext.walks.Remove(existWalk);
            await indiaWalkDbContext.SaveChangesAsync();

            return existWalk;
        }
       
    }
}
