namespace ReviewSamples.Modules.Variants;

public enum RoomType
{
    Deluxe,
    Standard,
    Economy,
    Apartment
}

public class Room
{
    public int RoomNumber { get; set; }
    public RoomType Type { get; set; }
    public decimal PricePerNight { get; set; }

    public Room(int roomNumber, RoomType type, decimal pricePerNight)
    {
        RoomNumber = roomNumber;
        Type = type;
        PricePerNight = pricePerNight;
    }
}

public class Booking
{
    public string GuestName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int RoomNumber { get; set; }
}

public class HotelFixed
{
    private List<Booking> _bookings = new List<Booking>();

    public string BookRoom(Room room, string guestName, DateTime checkInDate, DateTime checkOutDate)
    {
        if (room == null)
            return "error: room is null";

        if (string.IsNullOrWhiteSpace(guestName))
            return "error: guest name is empty";

        if (checkInDate >= checkOutDate)
            return "error: check-in date must be before check-out date";

        if (checkInDate < DateTime.Today)
            return "error: check-in date cannot be in the past";

        if (!IsRoomAvailable(room.RoomNumber, checkInDate, checkOutDate))
            return "bad";

        var booking = new Booking
        {
            GuestName = guestName,
            CheckInDate = checkInDate,
            CheckOutDate = checkOutDate,
            RoomNumber = room.RoomNumber
        };

        _bookings.Add(booking);
        return "ok";
    }

    private bool IsRoomAvailable(int roomNumber, DateTime checkInDate, DateTime checkOutDate)
    {
        foreach (var booking in _bookings)
        {
            if (booking.RoomNumber == roomNumber)
            {
                if (checkInDate < booking.CheckOutDate && checkOutDate > booking.CheckInDate)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public decimal CalculateBookingPrice(Booking booking, Room room)
    {
        if (booking == null || room == null)
            return 0;

        if (booking.RoomNumber != room.RoomNumber)
            return 0;

        int nights = (booking.CheckOutDate - booking.CheckInDate).Days;

        if (nights <= 0)
            return 0;

        return nights * room.PricePerNight;
    }

    public IReadOnlyList<Booking> GetAllBookings()
    {
        return _bookings.AsReadOnly();
    }
}