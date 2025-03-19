using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.DTOs;
using smart_class.Core.Entities;
using smart_class.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstitutionController : ControllerBase
    {
        private readonly IInstitutionService _institutionService;
        private readonly IMapper _mapper;

        public InstitutionController(IInstitutionService institutionService, IMapper mapper)
        {
            _institutionService = institutionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InstitutionDto>>> Get()
        {
            IEnumerable<Institution> institutions = await _institutionService.GetInstitutionsAsync();
            return Ok(_mapper.Map<IEnumerable<InstitutionDto>>(institutions));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InstitutionDto>> Get(int id)
        {
            Institution? institution = await _institutionService.GetInstitutionByIdAsync(id);
            if (institution == null)
                return NotFound();
            return Ok(_mapper.Map<InstitutionDto>(institution));
        }

        [HttpPost]
        public async Task<ActionResult<Institution>> Post([FromBody] InstitutionPostPut institutionPost)
        {
            if (institutionPost == null)
                return BadRequest("Institution cannot be null.");
            Institution addedInstitution = await _institutionService.AddInstitutionAsync(new Institution { Name = institutionPost.Name });
            return CreatedAtAction(nameof(Get), new { id = addedInstitution.Id }, addedInstitution);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Institution>> Put(int id, [FromBody] InstitutionPostPut institutionPut)
        {
            if (institutionPut == null)
                return BadRequest("Institution cannot be null.");
            Institution? updatedInstitution = await _institutionService.UpdateInstitutionAsync(id, new Institution { Name = institutionPut.Name });
            if (updatedInstitution == null)
                return NotFound();
            return Ok(updatedInstitution);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Institution>> Delete(int id)
        {
            Institution? deletedInstitution = await _institutionService.DeleteAsync(id);
            if (deletedInstitution == null)
                return NotFound();
            return Ok(deletedInstitution);
        }
    }
}