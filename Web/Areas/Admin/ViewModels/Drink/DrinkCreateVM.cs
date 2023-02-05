using Core.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Web.Areas.Admin.ViewModels.Drink
{
    public class DrinkCreateVM
    {
        public string DrinkName { get; set; }
        public double MediumPrice { get; set; }
        public double LargePrice { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public List<SelectListItem>? Categories { get; set; }
        public ProductStatus Status { get; set; }

    }
}
