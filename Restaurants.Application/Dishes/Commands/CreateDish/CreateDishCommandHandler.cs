using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IDishesRepository dishesRepository,
    IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new dish: {@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);

        if (restaurant is null)
        {
            logger.LogWarning("Restaurant with id: {RestaurantId} not found", request.RestaurantId);

            throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());
        }

        var dish = mapper.Map<Dish>(request);

        int id = await dishesRepository.CreateAsync(dish);

        return id;
    }
}