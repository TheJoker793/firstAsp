using AutoMapper;
using firstAsp.CustomActionFilters;
using firstAsp.Models.Domain;
using firstAsp.Models.DTO;
using firstAsp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace firstAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkssController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalkssController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //CREATE WALKS
        //POST:/api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            
                //Map dto to domain model
                var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);
                await walkRepository.CreateAsync(walkDomainModel);
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            
           
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery]string?sortBy,bool?isAscending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize=1000)
        {
           
                    
                var walksDomanModel = await walkRepository.GetAllAsync(filterOn, filterQuery,
                sortBy, isAscending,
                pageNumber, pageSize);
                return Ok(mapper.Map<List<WalkDto>>(walksDomanModel));
                 
           
            
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walkDomainModel=await walkRepository.GetByIdAsync(id);
            if (walkDomainModel==null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walkDomainModel));

        }
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalksRequestDto updateWalksRequestDto) 
        {
            if (ModelState.IsValid)
            {
                var walkDomainModel = mapper.Map<Walk>(updateWalksRequestDto);
                walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
                if (walkDomainModel == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDto>(walkDomainModel));
            }
            else { return BadRequest(ModelState);}
            


        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var deleteWalkDomainModel=await walkRepository.DeleteAsync(id);
            if (deleteWalkDomainModel==null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel)); 
        }
    }
}
