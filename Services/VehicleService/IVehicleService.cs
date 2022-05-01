using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalRestApi.Dtos.Vehicles;
using CarRentalRestApi.Dtos.Vehicles.CaravanDtos;
using CarRentalRestApi.Dtos.Vehicles.CarDtos;
using CarRentalRestApi.Models;
using CarRentalRestApi.Models.Responses;

namespace CarRentalRestApi.Services.VehicleService
{
    public interface IVehicleService
    {
       Task<ServiceResponse<List<GetVehicleDto>>> GetAllVehicles();
        Task<ServiceResponse<GetVehicleDto>> GetVehicleById(int id);
        Task<ServiceResponse<List<GetVehicleDto>>> AddCar(AddCarDto newCar);
        Task<ServiceResponse<List<GetVehicleDto>>> AddCaravan(AddCaravanDto newCaravan);

        Task<ServiceResponse<List<GetVehicleDto>>> DeleteVehicle(int id);

        Task<ServiceResponse<GetVehicleDto>> UpdateVehicle(UpdateVehicleDto updatedVehicle);

    }
}