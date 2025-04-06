using AutoMapper;
using Restaurants.Domain.Entities;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantsProfile : Profile
{
    public RestaurantsProfile()
    {
        CreateMap<UpdateRestaurantCommand, Restaurant>();

        CreateMap<CreateRestaurantCommand, Restaurant>()
            .ForMember(restaurant => restaurant.Address,
                configExpression => configExpression
                    .MapFrom(createRestaurantDto => new Address
                    {
                        City = createRestaurantDto.City,
                        Street = createRestaurantDto.Street,
                        PostalCode = createRestaurantDto.PostalCode
                    }));

        CreateMap<Restaurant, RestaurantDto>()
            .ForMember(restaurantDto => restaurantDto.City,
                configExpression => configExpression
                    .MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.City))
            .ForMember(restaurantDto => restaurantDto.PostalCode,
                configExpression => configExpression
                    .MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.PostalCode))
            .ForMember(restaurantDto => restaurantDto.Street,
                configExpression => configExpression
                    .MapFrom(restaurant => restaurant.Address == null ? null : restaurant.Address.Street))
            .ForMember(restaurantDto => restaurantDto.Dishes,
                configExpression => configExpression
                   .MapFrom(restaurant => restaurant.Dishes));
    }
}