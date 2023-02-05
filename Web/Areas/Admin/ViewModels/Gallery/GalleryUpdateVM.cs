namespace Web.Areas.Admin.ViewModels.Gallery
{
    public class GalleryUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }
    }
}
