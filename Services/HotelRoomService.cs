using AutoMapper;
using AutoMapper.QueryableExtensions;
using BlazorServerTestDynamicAccess.Infralayer;
using BlazorServerTestDynamicAccess.Models;
using BlazorServerTestDynamicAccess.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorServerTestDynamicAccess.Services
{

    public class HotelRoomService : IHotelRoomService
    {
        private bool _isDisposed;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly AutoMapper.IConfigurationProvider _mapperConfiguration;

        public HotelRoomService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _mapperConfiguration = mapper.ConfigurationProvider;
        }

        public async Task<HotelRoomDTO> CreateHotelRoomAsync(HotelRoomDTO hotelRoomDTO)
        {
            var hotelRoom = _mapper.Map<HotelRoom>(hotelRoomDTO);
            hotelRoom.CreatedDate = DateTime.Now;
            hotelRoom.CreatedBy = "";
            var addedHotelRoom = await _dbContext.HotelRooms.AddAsync(hotelRoom);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<HotelRoomDTO>(addedHotelRoom.Entity);
        }

        public async Task<int> DeleteHotelRoomAsync(int roomId)
        {
            var roomDetails = await _dbContext.HotelRooms.FindAsync(roomId);
            if (roomDetails == null)
            {
                return 0;
            }

            _dbContext.HotelRooms.Remove(roomDetails);
            return await _dbContext.SaveChangesAsync();
        }

        public IAsyncEnumerable<HotelRoomDTO> GetAllHotelRoomsAsync()
        {
            return _dbContext.HotelRooms
                        //.Include(x => x.HotelRoomImages)
                        .ProjectTo<HotelRoomDTO>(_mapperConfiguration)
                        .AsAsyncEnumerable();
        }

        public Task<HotelRoomDTO> GetHotelRoomAsync(int roomId)
        {
            return _dbContext.HotelRooms
                          //  .Include(x => x.HotelRoomImages)
                            .ProjectTo<HotelRoomDTO>(_mapperConfiguration)
                            .FirstOrDefaultAsync(x => x.Id == roomId);
        }

        public async Task<bool> IsRoomUniqueAsync(string name, int roomId)
        {
            if (roomId == 0)
            {
                // Create Mode
                return !await _dbContext.HotelRooms.AnyAsync(x => x.Name == name);
            }
            else
            {
                // Edit Mode
                return !await _dbContext.HotelRooms.AnyAsync(x => x.Name == name && x.Id != roomId);
            }
        }

        public async Task<HotelRoomDTO> UpdateHotelRoomAsync(int roomId, HotelRoomDTO hotelRoomDTO)
        {
            if (roomId != hotelRoomDTO.Id)
            {
                return null;
            }

            var actualDbRoom = await _dbContext.HotelRooms.FindAsync(roomId);
            var updatedDbRoom = _mapper.Map(source: hotelRoomDTO, destination: actualDbRoom);
            updatedDbRoom.UpdatedBy = "";
            updatedDbRoom.UpdatedDate = DateTime.Now;
            var updatedRoomEntityEntry = _dbContext.HotelRooms.Update(updatedDbRoom);
            await _dbContext.SaveChangesAsync();
            return _mapper.Map<HotelRoomDTO>(updatedRoomEntityEntry.Entity);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                try
                {
                    if (disposing)
                    {
                        _dbContext.Dispose();
                    }
                }
                finally
                {
                    _isDisposed = true;
                }
            }
        }
    }

}
