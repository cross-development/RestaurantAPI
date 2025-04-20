using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> CreateAsync(Dish entity)
    {
        dbContext.Dishes.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> DeleteAsync(IEnumerable<Dish> entities)
    {
        dbContext.RemoveRange(entities);

        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }
}