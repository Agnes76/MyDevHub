namespace MyDevHub.Models
{
    public class ImageFile
    {
        public int Id { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string FilePath { get; set; } = string.Empty;

        public DateTime UploadedDate { get; set; }
    }
}
