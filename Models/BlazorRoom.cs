namespace BlazorServerTestDynamicAccess.Models
{
    public class BlazorRoom
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public decimal Price { set; get; }

        public bool IsActive { set; get; }
        public List<BlazorRoomProp> RoomProps { set; get; }
    }
}
