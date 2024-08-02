using AutoMapper;
using Compass.Dto;
using Compass.Models;

namespace Compass.Helper
{
	public class MappingProfiles:Profile
	{
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();
			CreateMap<UserDto, User>();
			CreateMap<TourCategory, TourCategoryDto>();
			CreateMap<TourCategoryDto, TourCategory>();
			CreateMap<City, CityDto>();
			CreateMap<CityDto, City>();
			CreateMap<AirFlight, AirflightDto>();
			CreateMap<AirflightDto, AirFlight>();
			CreateMap<Airline, AirlineDto>();
			CreateMap<AirlineDto, Airline>();
			CreateMap<Airport, AirportDto>();
			CreateMap<AirportDto, Airport>();
			CreateMap<AppRate, AppRateDto>();
			CreateMap<AppRateDto, AppRate>();
			CreateMap<Car, CarDto>();
			CreateMap<CarDto, Car>();
			CreateMap<FlightClass, FlightClassDto>();
			CreateMap<FlightClassDto, FlightClass>();
			CreateMap<Hotel, HotelDto>();
			CreateMap<HotelDto, Hotel>();
			CreateMap<HotelRoom, HotelRoomDto>();
			CreateMap<HotelRoomDto, HotelRoom>();
			CreateMap<Package, PackageDto>();
			CreateMap<PackageDto, Package>();
			CreateMap<Reservation, ReservationDto>();
			CreateMap<ReservationDto, Reservation>();
			CreateMap<Resturant, ResturantDto>();
			CreateMap<ResturantDto, Resturant>();
			CreateMap<Tour, TourDto>();
			CreateMap<TourDto, Tour>();

		}
    }
}
