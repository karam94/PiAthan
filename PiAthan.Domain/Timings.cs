using System;
using System.Collections.Generic;
using System.Globalization;

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

        public List<Salah> GetSalahTimes()
        {
            return new List<Salah>
            {
                new Salah
                {
                    Name = "Fajr",
                    Datetime = DateTime.ParseExact(Fajr, "HH:mm", CultureInfo.InvariantCulture)
                },

                new Salah
                {
                    Name = "Dhuhr",
                    Datetime = DateTime.ParseExact(Dhuhr, "HH:mm", CultureInfo.InvariantCulture)
                },

                new Salah
                {
                    Name = "Asr",
                    Datetime = DateTime.ParseExact(Asr, "HH:mm", CultureInfo.InvariantCulture)
                },

                new Salah
                {
                    Name = "Maghrib",
                    Datetime = DateTime.ParseExact(Maghrib, "HH:mm", CultureInfo.InvariantCulture)
                },

                new Salah
                {
                    Name = "Isha",
                    Datetime = DateTime.ParseExact(Isha, "HH:mm", CultureInfo.InvariantCulture)
                }
            };
        }
    }

    public class Salah
    {
        public string Name { get; set; }
        public DateTime Datetime { get; set; }
    }
}