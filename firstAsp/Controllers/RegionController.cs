using AutoMapper;
using firstAsp.CustomActionFilters;
using firstAsp.Data;
using firstAsp.Models.Domain;
using firstAsp.Models.DTO;
using firstAsp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace firstAsp.Controllers
{
    //https://localhost:1234/api/regions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionController : ControllerBase

    {
        private readonly FirstDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        private readonly ILogger<RegionController> logger;

        public RegionController(FirstDbContext dbContext,IRegionRepository regionRepository,
            IMapper mapper,ILogger<RegionController> logger)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        // [Authorize(Roles ="Reader")]
        public async Task <IActionResult> GetAll()
        {

            try 
                {
                //throw new Exception("this is a custom exception");
                var regionsDomain = await regionRepository.GetAllAsync();
                logger.LogInformation($"Finished GetAllRegions request with data: {JsonSerializer.Serialize(regionsDomain)}");
                var regionDto = mapper.Map<List<RegionDto>>(regionsDomain);
                return Ok(regionDto);
            }
            catch (Exception e ) 
                      {
                logger.LogError(e, e.Message);
                throw;
                      }
            
           
          
        }
        [HttpGet]
        [Route("{id}:Guid")]
        [Authorize(Roles = "Reader")]
        public async Task <IActionResult> GetById([FromRoute] Guid id)
        {
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task <IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
                var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
                regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);

            
        }

        //Update region
        //PUT:https://localost:portNumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
       {
                var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
                regionDomainModel = await regionRepository.UpdateAsync(id,regionDomainModel);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
               // await dbContext.SaveChangesAsync();
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);
                return Ok(regionDto);
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles = "Writer")]
        public async Task <IActionResult> Delete([FromRoute] Guid id) 
        {
            //var regionDomainModel =await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel==null)
            {
                return NotFound();
            }
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            
            return Ok(regionDto);

        }
  
    }
}

