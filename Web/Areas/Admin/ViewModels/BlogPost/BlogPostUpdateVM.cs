namespace Web.Areas.Admin.ViewModels.BlogPost
{
    public class BlogPostUpdateVM
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoName { get; set; }
    }
}
