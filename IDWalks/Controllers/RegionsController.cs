using AutoMapper;
using IDWalks.Models.Domines;
using IDWalks.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace IDWalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class RegionsController : Controller
    {
        private readonly IRegionsRepo regionsRepo;
        private readonly IMapper mapper;

        public RegionsController(IRegionsRepo RegionsRepo , IMapper mapper)
        {
            regionsRepo = RegionsRepo;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetRegions()
        {
            var regions = await regionsRepo.GetAllAsync();

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

            var RegionDTO = mapper.Map<List<Models.DTO.Region>>(regions);

            return Ok(RegionDTO);
        }
        
    }
}
