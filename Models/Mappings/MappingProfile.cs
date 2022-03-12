using AutoMapper;
using BlazorServerTestDynamicAccess.Data;
using BlazorServerTestDynamicAccess.Models.DTOs;

namespace BlazorServerTestDynamicAccess.Models.Mappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<HotelRoomDTO, HotelRoom>().ReverseMap(); // two-way mapping
            CreateMap<UserRegisterDTO,User>().ReverseMap();
        }
    }
}
