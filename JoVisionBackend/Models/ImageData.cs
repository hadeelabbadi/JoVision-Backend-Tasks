namespace JoVisionBackend.Models
{
    public class ImageData
    {
        public string FileName { get; set; } = "";
        public string Owner { get; set; } = "";

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }
    }
}