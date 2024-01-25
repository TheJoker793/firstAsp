using AutoMapper;
using firstAsp.Data;
using firstAsp.Models.Domain;
using firstAsp.Models.DTO;
using firstAsp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

        public RegionController(FirstDbContext dbContext,IRegionRepository regionRepository,IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //Get All Regions
        //https://localhost:portNumber/api/regions
        [HttpGet]
        public async Task <IActionResult> GetAll()
        {
            //get data from database-domain
            var regionsDomain = await regionRepository.GetAllAsync();
            //map models to DTOs
            /* var regionDto = new List<RegionDto>();
             foreach (var regionDomain in regionsDomain)
             {
                 regionDto.Add(new RegionDto()
                 {
                     Id = regionDomain.Id,
                     Code = regionDomain.Code,
                     Name = regionDomain.Name,
                     RegionImageUrl = regionDomain.RegionImageUrl,
                 });

             }*/
            //map domain model to dto
            var regionDto=mapper.Map<List<RegionDto>>(regionsDomain);
            return Ok(regionDto);
        }
        [HttpGet]
        [Route("{id}:Guid")]
        public async Task <IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data from Database - Domain Model
            // var region = dbContext.Regions.Find(id);
            //var regionDomain =await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models to DTOs
           /* var regionDto = new RegionDto

            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };*/

            //return DTOs back to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }
        [HttpPost]
        //post to create new region
        //POST:https://localhost:portNuber/api/regions
        public async Task <IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map or convert DTO to Domain Model
         /*   var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl,
            };*/

            //Use Domain to create Region 
            //await dbContext.regions.AddAsync(regionDomainModel);
            //regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);   
          var  regionDomainModel = mapper.Map<Region>(addRegionRequestDto);
            //await dbContext.SaveChangesAsync();
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);
            //map domain model back to dto

            /*var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        }

        //Update region
        //PUT:https://localost:portNumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task <IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //map dto to domain model
            var regionDomainModel = new Region
            { 
                Code = updateRegionRequestDto.Code,
                Name=updateRegionRequestDto.Name,
                RegionImageUrl=updateRegionRequestDto.RegionImageUrl
                
            };

            // check if region exists
            //var regionDomainModel =await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            //await regionRepository.UpdateAsync(id,updateRegionRequestDto);
            //regionDomainModel = await regionRepository.UpdateAsync(id,regionDomainModel);
            regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            // map tdo to domain model
          /*  regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;*/
            await dbContext.SaveChangesAsync();
            //convert domain model to dto
            /*var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };*/
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task <IActionResult> Delete([FromRoute] Guid id) 
        {
            //var regionDomainModel =await dbContext.regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel==null)
            {
                return NotFound();
            }
            //delete region
            /*dbContext.regions.Remove(regionDomainModel);
            await dbContext.SaveChangesAsync();*/

            //return deleted region back
            //map domain model to Dto
            /* var regionDto = new RegionDto
             {
                 Id=regionDomainModel.Id,
                 Code=regionDomainModel.Code,
                 Name=regionDomainModel.Name,
                 RegionImageUrl=regionDomainModel.RegionImageUrl
             };*/
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);
            
            return Ok(regionDto);

        }
  
    }
}

