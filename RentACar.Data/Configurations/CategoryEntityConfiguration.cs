namespace RentACar.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using RentACar.Data.Models;

    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(this.GenerateCategories());
        }

        private Category[] GenerateCategories()
        {
            ICollection<Category> categories = new HashSet<Category>();

            Category category;

            category = new Category()
            {
                Id = 1,
                Name = "Sedan"
            };
            categories.Add(category); 

            category = new Category()
            {
                Id = 2,
                Name = "Station Wagon"
            };
            categories.Add(category);
                      
            category = new Category()
            {
                Id = 3,
                Name = "SUV"
            };
            categories.Add(category);

            category = new Category()
            {
                Id = 4,
                Name = "Hatchback"
            };
            categories.Add(category);

            return categories.ToArray();
        }
    }
}
