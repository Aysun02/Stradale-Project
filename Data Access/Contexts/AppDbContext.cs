using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Contexts
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Addition> Additions { get; set; }
        public DbSet<WiseExpression> WiseExpression { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPhoto> BlogPhotos { get; set; }
        public DbSet<GalleryElement> GalleryElements { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<HomeMainSlider> homeMainSliders { get; set; }
        public DbSet<HomeMainSliderPhoto> homeMainSliderPhotos { get; set; }
        public DbSet<HomeGallery> homeGalleries { get; set; }
        public DbSet<HomeGalleryPhoto> homeGalleriesPhotos { get;set; }
        public DbSet<Information> Information { get; set; }
        public DbSet<InfoPhoto> InfoPhoto { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferPhoto> OffersPhoto { get; set; }

    }
}
