using PiAthan.Domain;
using RestSharp;

namespace PiAthan.Services
{
    public class SalahTimeService
    {
        public Timings GetSalahTimes()
        {
            var client = new RestClient("http://api.aladhan.com/");
            var request = new RestRequest("v1/timingsByCity?city=Blackburn&country=United%20Kingdom&method=3&school=1", Method.GET);
            var response = client.Execute<AlAdhanResponse>(request);

            return response.Data.Data.Timings;
        }
    }
}