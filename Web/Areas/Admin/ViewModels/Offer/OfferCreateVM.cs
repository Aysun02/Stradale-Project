namespace Web.Areas.Admin.ViewModels.Offer
{
    public class OfferCreateVM
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public string CoffeeType1 { get; set; }
        public string Offer1 { get; set; }
        public IFormFile Photo1 { get; set; }
        public string CoffeeType2 { get; set; }
        public string Offer2 { get; set; }
        public IFormFile Photo2 { get; set; }
    }
}
