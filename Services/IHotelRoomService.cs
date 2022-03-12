using BlazorServerTestDynamicAccess.Models.DTOs;

namespace BlazorServerTestDynamicAccess.Services
{
    public interface IHotelRoomService : IDisposable
    {
        Task<HotelRoomDTO> CreateHotelRoomAsync(HotelRoomDTO hotelRoomDTO);

        Task<int> DeleteHotelRoomAsync(int roomId);

        IAsyncEnumerable<HotelRoomDTO> GetAllHotelRoomsAsync();

        Task<HotelRoomDTO> GetHotelRoomAsync(int roomId);

        Task<bool> IsRoomUniqueAsync(string name, int roomId);

        Task<HotelRoomDTO> UpdateHotelRoomAsync(int roomId, HotelRoomDTO hotelRoomDTO);
    }
}
