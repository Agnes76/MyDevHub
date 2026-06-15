using CalcTaxApp.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using MyDevHub.Models;
using static System.Net.Mime.MediaTypeNames;

namespace MyDevHub.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly MyHubContext _context;
        public ImageService(IWebHostEnvironment environment, MyHubContext context)
        {
            _webHostEnvironment = environment;
            _context = context;
        }
        public async Task<bool> UploadImage(GalleryViewModel model)
        {
            if (model.File != null)
            {
                var uploadPath = Path.Combine(
                   _webHostEnvironment.WebRootPath,
                    "uploads");

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                var fileName =
                    Guid.NewGuid() +
                    Path.GetExtension(model.File.FileName);

                var filePath =
                    Path.Combine(uploadPath, fileName);

                using var stream =
                    new FileStream(filePath, FileMode.Create);

                await model.File.CopyToAsync(stream);

                var image = new ImageFile
                {
                    FileName = model.File.FileName,
                    FilePath = "/uploads/" + fileName,
                    UploadedDate = DateTime.UtcNow
                };

                _context.ImageFiles.Add(image);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<GalleryViewModel> GetImages()
        {
            var imageFileList = await _context.ImageFiles
           .OrderByDescending(x => x.UploadedDate)
            .ToListAsync();

            var uploadPath = "";
            //var uploadPath = Path.Combine(
            //       _webHostEnvironment.WebRootPath, "uploads");

            //   if (!Directory.Exists(uploadPath))
            //   {
            //       Directory.CreateDirectory(uploadPath);
            //   }

               //foreach ( var imageFile in imageFileList)
               //{
               //   uploadPath = imageFile.FilePath;
               //}

               //var images = Directory
               //   .GetFiles(uploadPath)
               //   //.Select(Path.GetFileName)
               //   .ToList();


           var model = new GalleryViewModel
            {
               //File = 
               Images = imageFileList
            };
            return model;
        }
    }
}
