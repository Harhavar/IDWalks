using AutoMapper;
using IDWalks.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace IDWalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;


        public WalkController(IWalkRepo walkRepo , IMapper mapper)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalks()
        {
            var walk = await walkRepo.GetAllWalks();

            //var walksDTO = new List<Models.DTO.Walk>();
            //walk.ToList().ForEach(walk =>
            //{
            //    var WalkDTO = new Models.DTO.Walk()
            //    {
            //        walkDeficulty = walk.walkDeficulty,
            //        Length = walk.Length,
            //        name = walk.name,
            //        RegionId = walk.RegionId,
            //        WalkDeficultyId= walk.WalkDeficultyId,
                    

            //    };
            //    walksDTO.Add(WalkDTO);

            //});

            //retur dto regions
            //var RegionDTO = new List<Models.DTO.Region>();
            //regions.ToList().ForEach(regions =>
            //{
            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id= regions.Id,
            //        Name= regions.Name,
            //        Code= regions.Code,
            //        Lang= regions.Lang, 
            //        Lat = regions.Lat,
            //        Area= regions.Area, 
            //        Population= regions.Population

            //    };
            //    RegionDTO.Add(regionDTO);

            //});
            var walksDTO = mapper.Map<List<Models.DTO.Walk>>(walk);
            return Ok(walksDTO);
        }
    }
}
