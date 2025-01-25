using System.Reflection;

namespace MindSpace.Application.Commons.Constants
{
    public static class AppCts
    {

        /// <summary>
        /// All file path
        /// </summary>
        public static class Locations
        {
            public static readonly string AbsoluteProjectPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;

            public static class RelativeFilePath
            {
                public static string RestaurantSeeder = Path.Combine("Seeders", "FakeData", "RestaurantSeedData.json");
                public static string DishSeeder = Path.Combine("Seeders", "FakeData", "DishSeedData.json");
            }
        }
    }
}
