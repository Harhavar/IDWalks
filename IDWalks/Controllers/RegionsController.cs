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
        public async Task<IActionResult> GetAllRegions()
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]

        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionsRepo.GetRegion(id);

            if (region == null)
            {
                return NotFound();
            }

            var RegionDTO= mapper.Map<Models.DTO.Region>(region);

            return Ok(RegionDTO);
        }

        [HttpPost]

        public async Task<IActionResult> AddRegionAsync(Models.DTO.AddRegionRequest addRegionRequest)
        {
            //RequestDTO to domine model
            var region = new Models.Domines.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Lat = addRegionRequest.Lat,
                Lang = addRegionRequest.Lang,
                Area = addRegionRequest.Area,
                Population = addRegionRequest.Population
            };

            //pass details to repository 

            region = await regionsRepo.AddAsync(region);


            //convert back to dto 

            var regionDTO = new Models.DTO.Region
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Lang = region.Lang,
                Area = region.Area,
                Population = region.Population
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { Id = regionDTO.Id }, regionDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegionAsync(Guid id)
        {
            // get region from database 

            var region = await regionsRepo.DeleteAsync(id);

            // if null not found 
            if (region == null)
            {
                return NotFound();
            }
            //conver responce back to DTO

            var delete = new  Models.DTO.Region

            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Lang = region.Lang,
                Area = region.Area,
                Population = region.Population

            };

            //return ok responce 
            return Ok(delete);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id ,[FromBody] Models.DTO.UpdateRegionRequest updateRegionRequest)
        {
            //convert Dto to Domine model

            var region = new Models.Domines.Region()
            {
                Code = updateRegionRequest.Code,
                Lat = updateRegionRequest.Lat,
                Lang = updateRegionRequest.Lang,
                Area = updateRegionRequest.Area,
                Population = updateRegionRequest.Population,
                Name = updateRegionRequest.Name

            };

            //update region using repository 

            region = await regionsRepo.UpdateAsync(id, region);

            //if null null reference
            if(region == null)
            {
                return NotFound();
            }

            //convert Domine back to DTO
            var update = new Models.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Lat = region.Lat,
                Lang = region.Lang,
                Area = region.Area,
                Population = region.Population,

            };

            //return ok 
            return Ok(update);
        }
    }
}
