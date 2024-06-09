using firstAsp.Data;
using firstAsp.Models.Domain;

namespace firstAsp.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly FirstDbContext firstDbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,
            FirstDbContext firstDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.firstDbContext = firstDbContext;
        }
        
        
        
        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images",
              $"{image.FileName}{image.FileExtension}"   );
            //Upload Image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);
            //https://localhost:1234/Images/image.jpeg
              var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath= urlFilePath;
            //add image to the image table
            await firstDbContext.Images.AddAsync(image);
            await firstDbContext.SaveChangesAsync();
            return image;

        }
    }
}
