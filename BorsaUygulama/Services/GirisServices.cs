using BorsaUygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorsaUygulama.Services
{
    internal interface IGirisServices
    {
        Task<List<Giris>> GetGirisBilgi();
    }

    public class GirisServices : IGirisServices
    {
        HttpClient httpClient;

        public GirisServices()
        {
            httpClient = new HttpClient();
        }

        public async Task<List<Giris>> GetGirisBilgi()
        {
            //var response = await httpClient.GetAsync("https://localhost:7215/hisseler");

            return new List<Giris>();
        }
    }
}
