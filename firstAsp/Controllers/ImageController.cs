using firstAsp.Models.Domain;
using firstAsp.Models.DTO;
using firstAsp.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace firstAsp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {

            ValidateFileUpload(request);
            if (ModelState.IsValid)
            {
                //convert dto to domain model
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInByte = request.File.Length,
                    FileName=request.FileName,
                    FileDescription= request.FileDescription,
                };
                //user repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
                


                //user repository to upload image
            }
            return BadRequest(ModelState);

        }

        private void ValidateFileUpload(ImageUploadRequestDto request) 
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file","insupported file extension");
            }
            if (request.File.Length>10485769)
            {
                ModelState.AddModelError("file", "file size more than 10mb please upload a smaller file");
            }

        }

    }
}
