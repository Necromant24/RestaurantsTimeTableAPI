namespace RestaurantAPI.Models
{

    public class DateData
    {
        public string WeekDay { get; set; }
        public string[] Workers { get; set; }
    }
    
    
    public class RaspisanieColumn
    {
        public string Date { get; set; }
        public DateData Data { get; set; }
    }
}