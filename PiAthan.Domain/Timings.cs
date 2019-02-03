namespace PiAthan.Domain
{
    public class AlAdhanResponse
    {
        public Data Data { get; set; }
    }
    
    public class Data
    {
        public Timings Timings { get; set; }
    }
    
    public class Timings
    {
        public string Fajr { get; set; }
        public string Dhuhr { get; set; }
        public string Asr { get; set; }
        public string Maghrib { get; set; }
        public string Isha { get; set; }
    }
}