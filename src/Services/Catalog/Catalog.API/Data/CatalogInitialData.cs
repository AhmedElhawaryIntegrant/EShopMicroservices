using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if(await session.Query<Product>().AnyAsync())
            {
                return;
            }
            else
            {
                var products = GetPreConfiguredProducts();
                foreach (var product in products)
                {
                    session.Store(product);
                }

                await session.SaveChangesAsync(cancellation);
            }

        }

        private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id= Guid.NewGuid(),
                Name = "Iphone 13",
                Description = "Ipone 13 Description",
                Price = 1000,
                ImageFile = "product-1.png",
                Category = new List<string>(){"Smart Phone","Electronics"}

            },
            new Product
            {
                Id= Guid.NewGuid(),
                Name = "Samsung S21",
                Description = "Samsung S21 Description",
                Price = 800,
                ImageFile = "product-2.png",
                Category = new List<string>(){"Smart Phone","Electronics"}

            },
              new Product
            {
                Id= Guid.NewGuid(),
                Name = "Samsung TV",
                Description = "Samsung TV Description",
                Price = 1400,
                ImageFile = "product-3.png",
                Category = new List<string>(){ "Smart TV", "Electronics"}

            },
            new Product
            {
                Id= Guid.NewGuid(),
                Name = "Lenovo laptop",
                Description = "Lenovo laptop Description",
                Price = 2500,
                ImageFile = "product-4.png",
                Category = new List<string>(){ "Laptop", "Electronics"}

            },
               new Product
            {
                Id= Guid.NewGuid(),
                Name = "Dell laptop",
                Description = "DELL laptop Description",
                Price = 3000,
                ImageFile = "product-5.png",
                Category = new List<string>(){"Laptop","Electronics"}

            },
             new Product
            {
                Id= Guid.NewGuid(),
                Name = "LG TV",
                Description = "LG TV Description",
                Price = 1500,
                ImageFile = "product-5.png",
                Category = new List<string>(){"Smart TV","Electronics"}

            }
        };
    }
}
