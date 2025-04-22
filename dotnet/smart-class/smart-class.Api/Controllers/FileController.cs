//using AutoMapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using smart_class.Api.Entities;
//using smart_class.Core.DTOs;
//using smart_class.Core.Services;
//using File = smart_class.Core.Entities.File;


//namespace smart_class.Api.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class FileController : ControllerBase
//    {
//        private readonly IFileService _fileService;
//        private readonly IMapper _mapper;

//        public FileController(IFileService fileService, IMapper mapper)
//        {
//            _fileService = fileService;
//            _mapper = mapper;
//        }

//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<FileDto>>> Get()
//        {
//            IEnumerable<File> files = await _fileService.GetFilesAsync();
//            return Ok(_mapper.Map<IEnumerable<FileDto>>(files));
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<FileDto>> Get(int id)
//        {
//            File? file = await _fileService.GetFileByIdAsync(id);
//            if (file == null)
//                return NotFound();
//            return Ok(_mapper.Map<FileDto>(file));
//        }

//        [Authorize(Roles = "Teacher")]
//        [HttpPost]
//        public async Task<ActionResult<File>> Post([FromBody] FilePostPut filePost)
//        {
//            if (filePost == null)
//                return BadRequest("File cannot be null.");
//            File addedFile = await _fileService.AddFileAsync(new File { FilePath = filePost.FilePath });
//            return CreatedAtAction(nameof(Get), new { id = addedFile.Id }, addedFile);
//        }

//        [Authorize(Roles = "Teacher")]
//        [HttpPut("{id}")]
//        public async Task<ActionResult<File>> Put(int id, [FromBody] FilePostPut filePut)
//        {
//            if (filePut == null)
//                return BadRequest("File cannot be null.");
//            File? updatedFile = await _fileService.UpdateFileAsync(id, new File { FilePath = filePut.FilePath });
//            if (updatedFile == null)
//                return NotFound();
//            return Ok(updatedFile);
//        }

//        [Authorize(Roles = "Teacher")]
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<File>> Delete(int id)
//        {
//            File? deletedFile = await _fileService.DeleteAsync(id);
//            if (deletedFile == null)
//                return NotFound();
//            return Ok(deletedFile);
//        }
//    }
//}
using Amazon.S3;
using Amazon.S3.Transfer;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using smart_class.Api.Entities;
using smart_class.Core.DTOs;
using smart_class.Core.Entities;
using smart_class.Core.Services;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using File = smart_class.Core.Entities.File;

namespace smart_class.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public FileController(IFileService fileService, IMapper mapper, IAmazonS3 s3Client, IConfiguration configuration)
        {
            _fileService = fileService;
            _mapper = mapper;
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"];
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

        //[Authorize(Roles = "Teacher")]
        [HttpPost]
        public async Task<ActionResult<FileDto>> Post([FromBody] FilePostPut filePost)
        {
            // בדוק אם המידע שנשלח הוא null או אם נתיב הקובץ ריק
            if (filePost == null || string.IsNullOrEmpty(filePost.FilePath))
                return BadRequest("FilePath cannot be null or empty.");

            var filePath = filePost.FilePath; // הנחה שהנתיב לקובץ נמסר בבקשה

            // בדוק אם הקובץ קיים לפני ההעלאה
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound("The specified file does not exist.");
            }

            // Upload to S3
            var fileTransferUtility = new TransferUtility(_s3Client);

            try
            {
                await fileTransferUtility.UploadAsync(filePath, _bucketName);
            }
            catch (AmazonS3Exception ex)
            {
                return StatusCode((int)ex.StatusCode, $"Error uploading file: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            var fileUrl = $"https://{_bucketName}.s3.amazonaws.com/{Path.GetFileName(filePath)}";

            // שמור את המידע בבסיס הנתונים
            File addedFile = await _fileService.AddFileAsync(new File { FilePath = fileUrl, UploadedAt = DateTime.Now });

            // Return the file URL in the response
            //var fileDto = new FileDto { FilePath = fileUrl };
            return CreatedAtAction(nameof(Get), new { id = addedFile.Id }, addedFile); // עדכן את ה-ID לפי הצורך
        }

        //[Authorize(Roles = "Teacher")]
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

        //[Authorize(Roles = "Teacher")]
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