using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.models.Domain;
using NZWalks.API.models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{    
    //  /api/walks
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController(IMapper mapper, IWalkRepository walkRepository) : ControllerBase
    {
        private readonly IMapper mapper = mapper;
        private readonly IWalkRepository walkRepository = walkRepository;
        // Create walks
        //Post: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            //map DTO to Domain Model
            
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);
                //map domain model to DTO

                return Ok(mapper.Map<WalkDto>(walkDomainModel));

            
          

        }

        //GET Walks
        //GET: /api/walks?filterOn=Name&filterQuery=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery, [FromQuery] string? sortBy, [FromQuery] bool ? isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery]int pageSize =1000)
        {
            

           var walksDomainModel = await walkRepository.GetAllAsync(filterOn,filterQuery, sortBy,isAscending ?? true,pageNumber,pageSize);
            //Create an exception
            throw new Exception("This is a new exception");
            
            
            //map Domain Model to DTO

            return Ok(mapper.Map<List<WalkDto>>(walksDomainModel));
            
        }

        //Get Walk By Id
        //Get :/api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }
        // update walk by Id 
        //put : /api/walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id,UpdateWalkRequestDto updateWalkRequestDto)
        {
            
                // map DTO to DomainModel 
                var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

                if (walkDomainModel == null)
                {
                    return NotFound();
                }

                //map DomainModel to DTO
                return Ok(mapper.Map<WalkDto>(walkDomainModel));

            

        }

        //Delete a walk By id
        //Delete : /api/walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var deleteWalkDomainModel = await walkRepository.DeleteAsync(id);
            if (deleteWalkDomainModel == null)
            {
                return NotFound();
            }
            //map domain model to DTO
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }

    }
}
