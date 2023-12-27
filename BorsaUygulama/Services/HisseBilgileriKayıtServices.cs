using BorsaUygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BorsaUygulama.Services
{
    internal interface IHisseBilgileriKayıtServices
    {
        Task<List<HisseBilgileriKayit>> GetHisseBilgileriKayıt();
        Task HisseEkle(HisseBilgileriKayit hisseEkle);
        Task HisseSil(int hisseSil);
    }

    public class HisseBilgileriKayıtServices : IHisseBilgileriKayıtServices
    {
        HttpClient httpClient;

        public HisseBilgileriKayıtServices()
        {

#if (DEBUG && ANDROID)
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            httpClient = new HttpClient();
#endif

        }

        public async Task<List<HisseBilgileriKayit>> GetHisseBilgileriKayıt()
        {
            var gidenHisseBilgileri = await httpClient.GetFromJsonAsync<List<HisseBilgileriKayit>>(UrlHelper.HisseBilgileriUrl);
            return gidenHisseBilgileri;
        }

        public async Task HisseEkle(HisseBilgileriKayit hisseEkle)
        {
            JsonContent jsonContent = JsonContent.Create(hisseEkle);
            await httpClient.PostAsync(UrlHelper.HisseEkleUrl, jsonContent);

            //var response =
            //if (response.IsSuccessStatusCode)
            //{

            //}
        }

        public async Task HisseSil(int hisseSil)
        {
            var url = UrlHelper.HisseEkleUrl + $"/{hisseSil}";
            await httpClient.DeleteAsync(url);
        }
    }
}
