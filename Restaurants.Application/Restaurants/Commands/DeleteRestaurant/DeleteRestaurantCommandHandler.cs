﻿using Microsoft.Extensions.Logging;
using MediatR;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,
    IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
{
    public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting restaurant with id: {request.Id}");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with id: {request.Id} not found");

            return false;
        }

        var isDeleted = await restaurantsRepository.DeleteAsync(restaurant);

        return isDeleted;
    }
}