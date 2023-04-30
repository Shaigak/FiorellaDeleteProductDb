using EntityFrameWork.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameWork.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Slider> Sliders { get; set; }


        public DbSet<SliderInfo> SliderInfos { get; set; }
       
        public DbSet<Blog> Blogs { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<About> Abouts { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Advantage> Advantages { get; set; }

        public DbSet<Flower> Flowers { get; set; }

        public DbSet<Workers>Workers { get; set; }

        public DbSet<Florist> Florists { get; set; }

        public DbSet<Instagram> Instagrams { get; set; }

        public DbSet<Subscribe> Subscribes { get; set; }

        public DbSet<Setting> Settings { get; set; }

        public DbSet<BlogHeader> BlogHeaders { get; set; }

        public DbSet<HomeSlider> HomeSliders { get; set; }

        public DbSet<Footer> Footers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(m => !m.SoftDelete);


            modelBuilder.Entity<Setting>()
             .HasData(
              new Setting
              {
               Id = 1,
               Key = "HeaderLogo",
               Value = "logo.png"
              },
             new Setting
             {
               Id = 2,
               Key = "Phone",
               Value = "21894141721"
             },

             new Setting
             {
                 Id = 3,
                 Key = "Email",
                 Value= "p135@code.edu.az"
             }
            );

            modelBuilder.Entity<BlogHeader>()
             .HasData(
              new BlogHeader
              {
                  Id = 1,
                  Title = "Hello P135",
                  Description = "How are you ?"
              },
             new BlogHeader
             {
                 Id = 2,
                 Title = "Hello P414",
                 Description = "How are you?"
             }

            
            );

            modelBuilder.Entity<Footer>()
             .HasData(
              new Footer
              {
                  Id = 1,
                  Owner= "Bakhtiyar Shamilzada",
                  Image= "footer-bottom-1.png"

              }
           
           );

        }

   


    }
}
