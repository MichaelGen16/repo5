using ReviewSamples.Modules.Variants;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ГОСТИНИЦА. БРОНИРОВАНИЕ НОМЕРОВ ===\n");

        var hotel = new HotelFixed();

        var room1 = new Room(101, RoomType.Deluxe, 15000m);
        var room2 = new Room(102, RoomType.Standard, 8000m);

        Console.WriteLine("Номера:");
        Console.WriteLine($"  {room1.RoomNumber} - {room1.Type} - {room1.PricePerNight:C}");
        Console.WriteLine($"  {room2.RoomNumber} - {room2.Type} - {room2.PricePerNight:C}\n");

        string result1 = hotel.BookRoom(room1, "Иванов", new DateTime(2026, 7, 1), new DateTime(2026, 7, 5));
        Console.WriteLine("1. Бронирование номера 101 (Иванов, 01.07-05.07)");
        Console.WriteLine($"   Результат: {result1}\n");

        string result2 = hotel.BookRoom(room1, "Петров", new DateTime(2026, 7, 3), new DateTime(2026, 7, 7));
        Console.WriteLine("2. Бронирование номера 101 (Петров, 03.07-07.07)");
        Console.WriteLine($"   Результат: {result2}\n");

        string result3 = hotel.BookRoom(room2, "Сидорова", new DateTime(2026, 7, 10), new DateTime(2026, 7, 15));
        Console.WriteLine("3. Бронирование номера 102 (Сидорова, 10.07-15.07)");
        Console.WriteLine($"   Результат: {result3}\n");

        var booking = hotel.GetAllBookings().First();
        decimal price = hotel.CalculateBookingPrice(booking, room1);
        Console.WriteLine($"4. Расчет стоимости: {price:C}");

        Console.WriteLine("\n=== ПРОГРАММА ЗАВЕРШЕНА ===");
        Console.WriteLine("Нажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
}