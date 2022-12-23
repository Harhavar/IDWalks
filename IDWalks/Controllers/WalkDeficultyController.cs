using AutoMapper;
using IDWalks.Models.Domines;
using IDWalks.Models.DTO;
using IDWalks.Repository;
using Microsoft.AspNetCore.Mvc;

namespace IDWalks.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkDeficultyController : Controller
    {
        private readonly IWalkDeficultyRepo walkDeficultyRepo;
        private readonly IMapper mapper;

        public WalkDeficultyController(IWalkDeficultyRepo walkDeficultyRepo, IMapper mapper)
        {
            this.walkDeficultyRepo = walkDeficultyRepo;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllWalkDeficultyAsync()
        {
            var walkDeficulty = await walkDeficultyRepo.GetWalkDeficulty();

            var walkDeficultyDTO = mapper.Map<List<Models.DTO.WalkDeficulty>>(walkDeficulty);

            return Ok(walkDeficultyDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkDeficulty")]
        public async Task<IActionResult> GetWalkDeficulty(Guid id)
        {
           var walk = await walkDeficultyRepo.GetWalkDeficultyAsync(id);

           var WalkDTO = mapper.Map<Models.DTO.WalkDeficulty>(walk);

            return Ok(WalkDTO);
        }

        [HttpPost]

        public async Task<IActionResult> addWalkDificultyAsync(Models.DTO.AddWalkDificultyRequest walkDeficulty)
        {
            //validate 
            if (!await ValidateaddWalkDificultyAsync(walkDeficulty))
            {
                return BadRequest(ModelState);
            }
            //convert DTo to Domine Object 
            var walkdificulty = new Models.Domines.WalkDeficulty()
            {
                code = walkDeficulty.code
            };

            //pass domine object to persit it 

            walkdificulty = await walkDeficultyRepo.AddWalkdificulty(walkdificulty);

            //convert domine object to DTO

            //var WalkDTO = new Models.DTO.WalkDeficulty()
            //{

            //    code = walkDeficulty.code
            //};
            var WalkDTO = mapper.Map<Models.DTO.WalkDeficulty>(walkdificulty);

            //Send DTO response to the client 

            // return CreatedAtAction(nameof(GetWalkAsync), new { Id = WalkDTO.id }, WalkDTO);
            return CreatedAtAction(nameof(GetWalkDeficulty), new { Id = WalkDTO.Id }, WalkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWalkDificultyAsync([FromRoute] Guid id , [FromBody] Models.DTO.UpdateWalkDificulty UpdatewalkDeficulty)
        {
            //validate 
            if (!await ValidateUpdateWalkDificultyAsync(UpdatewalkDeficulty))
            {
                return BadRequest(ModelState);
            }

            //Convert DTO to Domine model
            var walkdificult = new Models.Domines.WalkDeficulty()
            {
                code = UpdatewalkDeficulty.code
            };


            //update domine to repository 

            walkdificult = await walkDeficultyRepo.UpdateWalkDificultyAsync(id, walkdificult);
            //if it is null null reference 
            if (walkdificult != null)
            {
                return NotFound();
            }

            //convert Domine Back to DTO

            //var WDDTO = new Models.DTO.WalkDeficulty()
            //{
            //    Id = walkdificult.Id

            //};
            var WalkDTO = mapper.Map<Models.DTO.WalkDeficulty>(walkdificult);

            //return response
            return Ok(WalkDTO);
        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteWalkDeficulty(Guid id)
        {
            var walk = await walkDeficultyRepo.DeleteWalkDificulty(id);

            if (walk == null)
            {
                return NotFound();

            }

            var Delete = new Models.DTO.WalkDeficulty()
            {
                Id = walk.Id,
                code = walk.code

            };

            return Ok(Delete);

        }

        #region 
        private async Task<bool> ValidateaddWalkDificultyAsync(Models.DTO.AddWalkDificultyRequest walkDeficulty)
        {
            if (walkDeficulty == null)
            {
                ModelState.AddModelError(nameof(walkDeficulty), $"{nameof(walkDeficulty)} canot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(walkDeficulty.code))
            {
                ModelState.AddModelError(nameof(walkDeficulty.code), $"{(walkDeficulty.code)} is Required");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;

            }
            return true;
        }

        private async Task<bool> ValidateUpdateWalkDificultyAsync(Models.DTO.UpdateWalkDificulty UpdatewalkDeficulty)
        {

            if (UpdatewalkDeficulty == null)
            {
                ModelState.AddModelError(nameof(UpdatewalkDeficulty), $"{nameof(UpdatewalkDeficulty)} canot be empty");
                return false;
            }
            if (string.IsNullOrWhiteSpace(UpdatewalkDeficulty.code))
            {
                ModelState.AddModelError(nameof(UpdatewalkDeficulty.code), $"{(UpdatewalkDeficulty.code)} is Required");
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
