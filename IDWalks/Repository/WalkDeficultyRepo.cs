using IDWalks.Data;
using IDWalks.Models.Domines;
//using IDWalks.Models.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.VisualBasic;

namespace IDWalks.Repository
{
    public interface IWalkDeficultyRepo
    {
        Task<IEnumerable<WalkDeficulty>> GetWalkDeficulty();

        Task<WalkDeficulty> GetWalkDeficultyAsync(Guid id);

         Task<WalkDeficulty> AddWalkdificulty(WalkDeficulty walkDef);

        Task<WalkDeficulty> UpdateWalkDificultyAsync(Guid id, WalkDeficulty walkDeficulty);

        Task<WalkDeficulty> DeleteWalkDificulty(Guid id);
    }
    public class WalkDeficultyRepo : IWalkDeficultyRepo
    {
        private readonly IndiaWalkDbContext indiaWalkDbContext;

        public WalkDeficultyRepo(IndiaWalkDbContext indiaWalkDbContext)
        {
            this.indiaWalkDbContext = indiaWalkDbContext;
        }

        public async Task<WalkDeficulty> AddWalkdificulty(WalkDeficulty walkDef)
        {
             walkDef.Id = Guid.NewGuid();
            await indiaWalkDbContext.walkDeficulties.AddAsync(walkDef);

            await indiaWalkDbContext.SaveChangesAsync();

            return walkDef;
        }

        public async Task<WalkDeficulty> DeleteWalkDificulty(Guid id)
        {
            var walkD = await indiaWalkDbContext.walkDeficulties.FirstOrDefaultAsync(x => x.Id == id);

            if (walkD != null)
            {
                indiaWalkDbContext.walkDeficulties.Remove(walkD);
                await indiaWalkDbContext.SaveChangesAsync();

                return walkD;
            }

            return null;
        }

        public async Task<IEnumerable<WalkDeficulty>> GetWalkDeficulty()
        {
            return await indiaWalkDbContext.walkDeficulties.ToListAsync();
        }

        public async Task<WalkDeficulty> GetWalkDeficultyAsync(Guid id)
        {
            return await indiaWalkDbContext.walkDeficulties.FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<WalkDeficulty> UpdateWalkDificultyAsync(Guid id, WalkDeficulty walkDeficulty)
        {
            var existingDificulty = await indiaWalkDbContext.walkDeficulties.FindAsync(id);

            if (existingDificulty != null)
            {
                ///walkDeficulty.Id = existingDificulty.Id;
                existingDificulty.code = walkDeficulty.code;
              // await indiaWalkDbContext.walkDeficulties.AddAsync(walkDeficulty);
               await indiaWalkDbContext.SaveChangesAsync();

                return walkDeficulty;
            }

            return null;
        }
    }
}
