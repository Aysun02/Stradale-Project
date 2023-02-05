namespace Web.Areas.Admin.ViewModels.HomeGallery
{
    public class HomeGalleryUpdateVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Ordinal { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }
    }
}
