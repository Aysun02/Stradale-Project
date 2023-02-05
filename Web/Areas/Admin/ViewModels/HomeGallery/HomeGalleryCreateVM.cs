namespace Web.Areas.Admin.ViewModels.HomeGallery
{
    public class HomeGalleryCreateVM
    {
        public string Title { get; set; }
        public int Ordinal { get; set; }
        public IFormFile Photo { get; set; }

    }
}
