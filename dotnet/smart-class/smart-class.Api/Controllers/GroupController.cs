using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.DTOs;
using smart_class.Core.Entities;
using smart_class.Core.Services;

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IMapper _mapper;

        public GroupController(IGroupService groupService, IMapper mapper)
        {
            _groupService = groupService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> Get()
        {
            IEnumerable<Group> groups = await _groupService.GetGroupsAsync();
            return Ok(_mapper.Map<IEnumerable<GroupDto>>(groups));
        }

        [Authorize(Roles = "Admin, Teacher")]
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> Get(int id)
        {
            Group? group = await _groupService.GetGroupByIdAsync(id);
            if (group == null)
                return NotFound();
            return Ok(_mapper.Map<GroupDto>(group));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Group>> Post([FromBody] GroupPostPut groupPost)
        {
            if (groupPost == null)
                return BadRequest("Group cannot be null.");
            Group addedGroup = await _groupService.AddGroupAsync(new Group { Name = groupPost.Name });
            return CreatedAtAction(nameof(Get), new { id = addedGroup.Id }, addedGroup);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Group>> Put(int id, [FromBody] GroupPostPut groupPut)
        {
            if (groupPut == null)
                return BadRequest("Group cannot be null.");
            Group? updatedGroup = await _groupService.UpdateGroupAsync(id, new Group { Name = groupPut.Name });
            if (updatedGroup == null)
                return NotFound();
            return Ok(updatedGroup);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            Group? deletedGroup = await _groupService.DeleteAsync(id);
            if (deletedGroup == null)
                return NotFound();
            return Ok(deletedGroup);
        }
    }
}