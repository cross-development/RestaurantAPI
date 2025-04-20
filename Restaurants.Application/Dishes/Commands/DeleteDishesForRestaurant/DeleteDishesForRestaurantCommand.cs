using MediatR;

namespace Restaurants.Application.Dishes.Commands.DeleteDishesForRestaurant;

public class DeleteDishesForRestaurantCommand : IRequest
{
    public int RestaurantId { get; init; }
}