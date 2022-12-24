using AutoMapper;
using IDWalks.Models.Domines;
using IDWalks.Models.DTO;
using IDWalks.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Data;

namespace IDWalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkController : Controller
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;
        private readonly IRegionsRepo regionsRepo;
        private readonly IWalkDeficultyRepo walkDeficultyRepo;

        public WalkController(IWalkRepo walkRepo, IMapper mapper , IRegionsRepo regionsRepo , IWalkDeficultyRepo walkDeficultyRepo)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
            this.regionsRepo = regionsRepo;
            this.walkDeficultyRepo = walkDeficultyRepo;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalksAsync()
        {
            var walk = await walkRepo.GetAllWalksAsync();

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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        //[ActionName("GetRegionAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            //get walk domine object from database
            var walk = await walkRepo.GetWalkAsync(id);

            var WalkDTO = mapper.Map<Models.DTO.Walk>(walk);


            return Ok(WalkDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddWalkAsync([FromBody] Models.DTO.AddWalkRequest addWalkRequest)
        {
            //validate the incoming request 
            if (!(await ValidateGetWalkAsync(addWalkRequest)))
            {
                return BadRequest(ModelState);
            }

            //convert DTo to Domine Object 
            var addwalk = new Models.Domines.Walk
            {
                name = addWalkRequest.name,
                Length = addWalkRequest.Length,
                RegionId = addWalkRequest.RegionId,
                WalkDeficultyId = addWalkRequest.WalkDeficultyId

            };

            //pass domine object to persit it 
            addwalk = await walkRepo.UpdateAsync(addwalk);

            //convert domine object to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                id = addwalk.id,
                name = addwalk.name,
                Length = addwalk.Length,
                RegionId = addwalk.RegionId,
                WalkDeficultyId = addwalk.WalkDeficultyId
            };

            //Send DTO response to the client 
            return CreatedAtAction(nameof(GetWalkAsync), new { Id = WalkDTO.id }, WalkDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            //validate update request
            if(!await (ValidateUpdateWalkAsync(updateWalkRequest)))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domine model
            var walkDomine = new Models.Domines.Walk()
            {
                name = updateWalkRequest.name,
                Length = updateWalkRequest.Length,
                RegionId = updateWalkRequest.RegionId,
                WalkDeficultyId = updateWalkRequest.WalkDeficultyId
            };

            //update domine to repository 
            walkDomine = await walkRepo.UpdateWalkAsync(id, walkDomine);

            //if it is null null reference 
            if (walkDomine == null)
            {
                return NotFound("walk not found");
            }

            //convert Domine Back to DTO
            var Update = new Models.DTO.Walk()
            {
                id = walkDomine.id,
                name = walkDomine.name,
                Length = walkDomine.Length,
                RegionId = walkDomine.RegionId,
                WalkDeficultyId = walkDomine.WalkDeficultyId
            };

            //return response 
            return Ok(Update);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // get walk from database 
            var walk = await walkRepo.DeleteAsync(id);


            //if null return not found
            if(walk == null)
            {
                return NotFound();

            }


            //conver responce back to DTO

            //var WalkDTO = new Models.DTO.Walk()
            //{
            //    id= walk.id,
            //    name = walk.name,
            //    Length = walk.Length,
            //    RegionId = walk.RegionId,
            //    WalkDeficultyId= walk.WalkDeficultyId
            //};
            var walkDTO = mapper.Map<Models.DTO.Walk>(walk);

            //return responce
            return Ok(walkDTO);
        }


        #region validation
        private async Task<bool> ValidateGetWalkAsync(Models.DTO.AddWalkRequest addWalkRequest)
        {
            if(addWalkRequest == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest), $"{nameof(addWalkRequest)} canot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(addWalkRequest.name))
            {
                ModelState.AddModelError(nameof(addWalkRequest.name), $"{(addWalkRequest.name)} is Required");
            }
            if (addWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkRequest.Length), $"{(addWalkRequest.Length)} invalid");
            }

            var region =await regionsRepo.GetRegion(addWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.RegionId), $"{(addWalkRequest.RegionId)} invalid");
            }

            var walkDeficulty = await walkDeficultyRepo.GetWalkDeficultyAsync(addWalkRequest.WalkDeficultyId);
            if (walkDeficulty == null)
            {
                ModelState.AddModelError(nameof(addWalkRequest.WalkDeficultyId), $"{(addWalkRequest.WalkDeficultyId)} invalid");
            }

            if(ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }

        private async Task<bool> ValidateUpdateWalkAsync(Models.DTO.UpdateWalkRequest updateWalkRequest)
        {
            if (updateWalkRequest == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest), $"{nameof(updateWalkRequest)} canot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(updateWalkRequest.name))
            {
                ModelState.AddModelError(nameof(updateWalkRequest.name), $"{(updateWalkRequest.name)} is Required");
            }
            if (updateWalkRequest.Length <= 0)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.Length), $"{(updateWalkRequest.Length)} invalid");
            }

            var region = await regionsRepo.GetRegion(updateWalkRequest.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.RegionId), $"{(updateWalkRequest.RegionId)} invalid");
            }

            var walkDeficulty = await walkDeficultyRepo.GetWalkDeficultyAsync(updateWalkRequest.WalkDeficultyId);
            if (walkDeficulty == null)
            {
                ModelState.AddModelError(nameof(updateWalkRequest.WalkDeficultyId), $"{(updateWalkRequest.WalkDeficultyId)} invalid");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        #endregion
    }
}
