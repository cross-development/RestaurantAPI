using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

public class UpdateRestaurantCommandHandler(ILogger<UpdateRestaurantCommandHandler> logger,
    IMapper mapper, IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
{
    public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating restaurant with id: {request.Id}");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        if (restaurant is null)
        {
            logger.LogWarning($"Restaurant with id: {request.Id} not found");

            return false;
        }

        mapper.Map(request, restaurant);

        var isDeleted = await restaurantsRepository.UpdateAsync(restaurant);

        return isDeleted;
    }
}