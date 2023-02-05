namespace Web.Areas.Admin.ViewModels.Information
{
    public class InformationCreateVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
