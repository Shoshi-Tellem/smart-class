using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.Classes;
using smart_class.Core.DTOs;
using smart_class.Core.Entities;
using smart_class.Core.Services;
using smart_class.Service;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAdminService adminService, IMapper mapper) : ControllerBase
    {
        private readonly IAdminService _adminService = adminService;
        private readonly IMapper _mapper = mapper;

        // GET: api/<AdminController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminDto>>> Get()
        {
            IEnumerable<Admin> admins = await _adminService.GetAdminsAsync();
            return Ok(_mapper.Map<IEnumerable<AdminDto>>(admins));
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminDto>> Get(int id)
        {
            Admin? admin = await _adminService.GetAdminByIdAsync(id);
            if (admin == null)
                return NotFound();
            return Ok(_mapper.Map<AdminDto>(admin));
        }

        // POST api/<AdminController>
        [HttpPost]
        public async Task<ActionResult<Admin>> Post([FromBody] AdminPostPut adminPost)
        {
            if (adminPost == null)
                return BadRequest("Admin cannot be null.");
            Admin addedAdmin = await _adminService.AddAdminAsync(new Admin { Name = adminPost.Name, Password = adminPost.Password, Email = adminPost.Email, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now });
            return CreatedAtAction(nameof(Get), new { id = addedAdmin.Id }, addedAdmin);
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Admin>> Put(int id, [FromBody] AdminPostPut adminPut)
        {
            if (adminPut == null)
                return BadRequest("Admin cannot be null.");
            Admin? updatedAdmin = await _adminService.UpdateAdminAsync(id, new Admin { Name = adminPut.Name, Password = adminPut.Password, Email = adminPut.Email, UpdatedAt = DateTime.Now });
            if (updatedAdmin == null)
                return NotFound();
            return Ok(updatedAdmin);
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Admin>> Delete(int id)
        {
            Admin? deletedAdmin = await _adminService.DeleteAsync(id);
            if (deletedAdmin == null)
                return NotFound();
            return Ok(deletedAdmin);
        }
    }
}