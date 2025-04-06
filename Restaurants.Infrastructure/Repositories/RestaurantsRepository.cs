using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

internal class RestaurantsRepository(RestaurantsDbContext dbContext) : IRestaurantsRepository
{
    public async Task<IEnumerable<Restaurant>> GetAllAsync()
    {
        var restaurants = await dbContext.Restaurants.ToListAsync();

        return restaurants;
    }

    public async Task<Restaurant?> GetByIdAsync(int id)
    {
        var restaurant = await dbContext.Restaurants
            .Include(restaurant => restaurant.Dishes)
            .FirstOrDefaultAsync(restaurant => restaurant.Id == id);

        return restaurant;
    }

    public async Task<int> CreateAsync(Restaurant entity)
    {
        dbContext.Restaurants.Add(entity);

        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Restaurant restaurant)
    {
        dbContext.Update(restaurant);

        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Restaurant restaurant)
    {
        dbContext.Remove(restaurant);

        var result = await dbContext.SaveChangesAsync();

        return result > 0;
    }
}