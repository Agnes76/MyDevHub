using MyDevHub.Models;

namespace MyDevHub.Services
{
    public interface IImageService
    {
        Task<bool> UploadImage(GalleryViewModel model);

        Task<GalleryViewModel> GetImages();
    }
}
