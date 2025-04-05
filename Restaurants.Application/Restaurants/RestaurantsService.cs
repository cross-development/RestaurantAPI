using Microsoft.Extensions.Logging;
using AutoMapper;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
    ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");

        var restaurants = await restaurantsRepository.GetAllAsync();

        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantsDtos!;
    }

    public async Task<RestaurantDto?> GetRestaurantById(int id)
    {
        logger.LogInformation($"Getting one restaurant by id: {id}");

        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);

        return restaurantDto;
    }

    public async Task<int> CreateRestaurant(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Creating a new restaurant");

        var restaurant = mapper.Map<Restaurant>(createRestaurantDto);

        int id = await restaurantsRepository.CreateAsync(restaurant);

        return id;
    }
}