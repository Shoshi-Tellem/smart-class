using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.DTOs;
using smart_class.Core.Services;
using File = smart_class.Core.Entities.File;


namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public FileController(IFileService fileService, IMapper mapper)
        {
            _fileService = fileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FileDto>>> Get()
        {
            IEnumerable<File> files = await _fileService.GetFilesAsync();
            return Ok(_mapper.Map<IEnumerable<FileDto>>(files));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FileDto>> Get(int id)
        {
            File? file = await _fileService.GetFileByIdAsync(id);
            if (file == null)
                return NotFound();
            return Ok(_mapper.Map<FileDto>(file));
        }

        [HttpPost]
        public async Task<ActionResult<File>> Post([FromBody] FilePostPut filePost)
        {
            if (filePost == null)
                return BadRequest("File cannot be null.");
            File addedFile = await _fileService.AddFileAsync(new File { FilePath = filePost.FilePath });
            return CreatedAtAction(nameof(Get), new { id = addedFile.Id }, addedFile);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<File>> Put(int id, [FromBody] FilePostPut filePut)
        {
            if (filePut == null)
                return BadRequest("File cannot be null.");
            File? updatedFile = await _fileService.UpdateFileAsync(id, new File { FilePath = filePut.FilePath });
            if (updatedFile == null)
                return NotFound();
            return Ok(updatedFile);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<File>> Delete(int id)
        {
            File? deletedFile = await _fileService.DeleteAsync(id);
            if (deletedFile == null)
                return NotFound();
            return Ok(deletedFile);
        }
    }
}