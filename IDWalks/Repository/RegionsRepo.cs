using IDWalks.Data;
using IDWalks.Models.Domines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IDWalks.Repository
{
    public interface IRegionsRepo
    {
        public Task<IEnumerable<Region>> GetAllAsync();

        Task<Region> GetRegion(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> DeleteAsync(Guid Id);

        Task<Region> UpdateAsync(Guid Id , Region region);
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

        public async Task<Region> AddAsync(Region region)
        {
            region.Id = Guid.NewGuid();
            await indiaWalkDbContext.regions.AddAsync(region);
            await indiaWalkDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> GetRegion(Guid id)
        {
           return await indiaWalkDbContext.regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region> DeleteAsync(Guid Id)
        {
            var region = await indiaWalkDbContext.regions.FirstOrDefaultAsync(r => r.Id == Id);

            if (region == null)
            {
                return null;
            }

            //delete the region
            indiaWalkDbContext.regions.Remove(region);
            await indiaWalkDbContext.SaveChangesAsync();


            return region;
        }

        public async Task<Region> UpdateAsync(Guid Id, Region region)
        {
            var existingRegion = await indiaWalkDbContext.regions.FirstOrDefaultAsync(x => x.Id == Id);

            if (existingRegion == null)
            {
                return null;

            }

            existingRegion.Code= region.Code;
            existingRegion.Name= region.Name;
            existingRegion.Lat= region.Lat;
            existingRegion.Lang= region.Lang;
            existingRegion.Population   = region.Population;
            existingRegion.Area= region.Area;

            await indiaWalkDbContext.SaveChangesAsync();

            return existingRegion;
        }
    }
}
