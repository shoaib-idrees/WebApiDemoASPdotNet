using System.Reflection.Metadata.Ecma335;

namespace WebAPIDemo1.Models.Repositories
{
    public static class ShirtRepository
    {
        private static List<Shirt> shirts = new List<Shirt>()
        {
            new Shirt {ShirtId=1, Brand="My Brand",Color="Blue",Gender="Men", Price=30, Size=10},
            new Shirt {ShirtId=2, Brand="My Brand",Color="Black",Gender="Men", Price=28, Size=8},
            new Shirt {ShirtId=3, Brand="Your Brand",Color="Orange",Gender="Women", Price=26, Size=7},
            new Shirt {ShirtId=4, Brand="Your Brand",Color="Red",Gender="Women", Price=27, Size=6}
        };
        public static List<Shirt> GetShirts()
        {
            return shirts;
        }
        public static bool ShirtExists(int id)
        {
            return shirts.Any(x => x.ShirtId == id);
        }

        public static Shirt? GetShirtById(int id)
        {
            return shirts.FirstOrDefault(x => x.ShirtId == id);
        }
        public static Shirt? GetShirtByProperties(string? brand, string? gender, string? color, int? size)
        {
            return shirts.FirstOrDefault(x =>
            !string.IsNullOrWhiteSpace(brand) &&
            !string.IsNullOrWhiteSpace(x.Brand) &&
            x.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(gender) &&
            !string.IsNullOrWhiteSpace(x.Gender) &&
            x.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase) &&
            !string.IsNullOrWhiteSpace(color) &&
            !string.IsNullOrWhiteSpace(x.Color) &&
            x.Color.Equals(color, StringComparison.OrdinalIgnoreCase) &&
            size.HasValue &&
            x.Size.HasValue &&
            size.Value == x.Size.Value);
        }
        public static void AddShirt(Shirt shirt)
        {
            int maxId = shirts.Max(x => x.ShirtId);
            shirt.ShirtId = maxId + 1;
            shirts.Add(shirt);

        }
        public static void UpdateShirt(Shirt shirt)
        {
            var shirtToUpdate = shirts.First(x => x.ShirtId == shirt.ShirtId);
            shirtToUpdate.Brand = shirt.Brand;
            shirtToUpdate.Gender = shirt.Gender;
            shirtToUpdate.Size = shirt.Size;
            shirtToUpdate.Color = shirt.Color;
            shirtToUpdate.Price = shirt.Price;
        }
        public static void DeleteShirt(int shirtId)
        {
            var shirt = GetShirtById(shirtId);
            if (shirt !=null)
            {
                shirts.Remove(shirt);
            }
        }
    }
            
       
}
