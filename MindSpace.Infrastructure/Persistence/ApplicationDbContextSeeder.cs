using MindSpace.Application.Commons.Utilities;

namespace MindSpace.Infrastructure.Persistence
{
    public class ApplicationDbContextSeeder
    {
        // =====================================
        // === Props & Fields
        // =====================================

        private readonly IFileReader _fileReader;
        private readonly IDataSeeder _dataSeeder;
        private readonly ApplicationDbContext _dbContext;

        // =====================================
        // === Constructors
        // =====================================

        public ApplicationDbContextSeeder(IFileReader fileReader, ApplicationDbContext dbContext, IDataSeeder dataSeeder)
        {
            _fileReader = fileReader;
            _dbContext = dbContext;
            _dataSeeder = dataSeeder;
        }

        // =====================================
        // === Methods
        // =====================================

        public async Task SeedAllAsync()
        {
            await _dataSeeder.SeedAsync();
            //await new JsonDataSeeder<Restaurant>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.RestaurantSeeder).SeedAsync();
            //await new JsonDataSeeder<Dish>(_fileReader, _dbContext).AddRelativeFilePath(AppCts.Locations.RelativeFilePath.DishSeeder).SeedAsync();
        }
    }
}