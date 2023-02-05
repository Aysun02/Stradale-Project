namespace Web.Areas.Admin.ViewModels.BlogPost
{
    public class BlogPostCreateVM
    {
        public string Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
    }
}
