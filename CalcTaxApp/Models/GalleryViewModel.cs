using System.ComponentModel.DataAnnotations;

namespace MyDevHub.Models
{
    public class GalleryViewModel
    {
        [Required]
        public IFormFile? File { get; set; }

        public List<ImageFile> Images { get; set; }
    }
}
