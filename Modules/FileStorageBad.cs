namespace ReviewSamples.Modules.Variants;

// Создан enum для типов номеров (критерий З)
public enum RoomType
{
    Deluxe,    // Д - Люкс
    Standard,  // З - Стандарт
    Economy,   // Е - Эконом
    Apartment  // А - Апартаменты
}

// Исправленный класс Room (критерий А, З)
public class Room
{
    public int RoomNumber { get; set; }
    public RoomType Type { get; set; }
    public decimal PricePerNight { get; set; }  // decimal вместо double

    public Room(int roomNumber, RoomType type, decimal pricePerNight)
    {
        RoomNumber = roomNumber;
        Type = type;
        PricePerNight = pricePerNight;
    }
}

// Исправленный класс Booking (критерий А)
public class Booking
{
    public string GuestName { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public int RoomNumber { get; set; }
}

// Исправленный класс Hotel (критерий А, Д, Е, Ж, З)
public class HotelFixed
{
    private List<Booking> _bookings = new List<Booking>();

    // Метод с понятным именем и проверками (критерий А, Д)
    public string BookRoom(Room room, string guestName, DateTime checkInDate, DateTime checkOutDate)
    {
        // Проверка входных данных (критерий Д)
        if (room == null)
            throw new ArgumentNullException(nameof(room), "Комната не может быть null");

        if (string.IsNullOrWhiteSpace(guestName))
            throw new ArgumentException("Имя гостя не может быть пустым", nameof(guestName));

        if (checkInDate >= checkOutDate)
            throw new ArgumentException("Дата заезда должна быть раньше даты выезда");

        if (checkInDate < DateTime.Today)
            throw new ArgumentException("Дата заезда не может быть в прошлом");

        // Проверка доступности номера (вынесено в отдельный метод)
        if (!IsRoomAvailable(room.RoomNumber, checkInDate, checkOutDate))
            return "bad";

        // Создание бронирования
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

    // Отдельный метод для проверки доступности (критерий Б, Ж)
    private bool IsRoomAvailable(int roomNumber, DateTime checkInDate, DateTime checkOutDate)
    {
        foreach (var booking in _bookings)
        {
            if (booking.RoomNumber == roomNumber)
            {
                // Проверка пересечения дат
                if (checkInDate < booking.CheckOutDate && checkOutDate > booking.CheckInDate)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Метод расчета стоимости с понятным именем и decimal (критерий А, З)
    public decimal CalculateBookingPrice(Booking booking, Room room)
    {
        // Проверка входных данных (критерий Д)
        if (booking == null)
            throw new ArgumentNullException(nameof(booking), "Бронирование не может быть null");

        if (room == null)
            throw new ArgumentNullException(nameof(room), "Комната не может быть null");

        if (booking.RoomNumber != room.RoomNumber)
            throw new ArgumentException("Номер комнаты в бронировании не соответствует комнате");

        // Расчет количества ночей
        int nights = (booking.CheckOutDate - booking.CheckInDate).Days;

        if (nights <= 0)
            throw new InvalidOperationException("Количество ночей должно быть больше 0");

        // Возврат decimal (критерий З)
        return nights * room.PricePerNight;
    }

    // Дополнительный метод для получения всех бронирований
    public IReadOnlyList<Booking> GetAllBookings()
    {
        return _bookings.AsReadOnly();
    }
}