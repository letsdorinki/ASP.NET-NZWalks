using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.models.Domain;
using NZWalks.API.models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    //https:localhost:portnumber/api/regions
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class RegionsController(
    NZWalksDbContext dbContext,
    IRegionRepository regionRepository,
    IMapper mapper,ILogger<RegionsController> logger) : ControllerBase
    {
        private readonly NZWalksDbContext dbContext = dbContext;
        private readonly IRegionRepository regionRepository = regionRepository;
        private readonly IMapper mapper = mapper;
        private readonly ILogger<RegionsController> logger = logger;

        [HttpGet]
       // [Authorize(Roles ="Readre")]
        public async Task<IActionResult> GetAll()
        {

            //try
            //{
            //    throw new Exception("This is a custom exception");
            var regionsDomain = await regionRepository.GetAllAsync();

            //logger.LogInformation($"Finished GetAllRegions Request with data:{JsonSerializer.Serialize(regionsDomain)}");
            //var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
 
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            //}
            //catch (Exception ex)
            //{
            //    logger.LogError(ex, ex.Message);
            //    throw;
            }
          
            //map Domain Models to DTOs
        
        //get single region (get region by 100)
        // get:https://localhost:portnumber/api/region/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Readre")]
        public  async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region =  dbContext.Regions.Find(id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if(regionDomain == null)
            {
                return NotFound();
            }
            //var regionsDto = new RegionDto
            //{

            //    Id = regionDomain.Id,
            //    Code = regionDomain.Code,
            //    Name = regionDomain.Name,
            //    RegionImageUrl = regionDomain.RegionImageUrl

            //};
            // return DTO back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        //Post To create New region
        //Post:https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public  async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                //Map or Covert DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                //use Domain model to create region
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

                //map Domainmodel back to Dto
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);

           
           
        }
        //update region
        //put :https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        //[Authorize(Roles = "Writer")]
        public  async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {    //map DTO to domain Model
            
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

                //check if region exists
                regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                //map dto to domain model 
                regionDomainModel.Code = updateRegionRequestDto.Code;
                regionDomainModel.Name = updateRegionRequestDto.Name;
                regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;
                await dbContext.SaveChangesAsync();
                //covert Domain Model to Dto
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);

           
        }

        //Delete region
        //DELETE :https://localhost:portnumber/api/regions
        [HttpDelete]
        [Route("{id:guid}")]
        //[Authorize(Roles = "Writer ,Readre")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
              var regionDomainModel = await regionRepository.DeleteAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete region
       
            //return deleted region back
            //map domain Model to Dto
            
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }



    }
}
