using MediatR;
using Restaurants.Application.Dishes.Dtos;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;

public class GetDishByIdForRestaurantQuery : IRequest<DishDto>
{
    public int Id { get; init; }
    public int RestaurantId { get; init; }
}