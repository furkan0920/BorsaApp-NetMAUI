using BorsaUygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BorsaUygulama.Services
{
    internal interface IHisseAdlariServices
    {
        Task<List<HisseAdlari>> GetHisseAdlari();
    }

    public class HisseAdlariServices : IHisseAdlariServices
    {
        HttpClient httpClient;
        #region json veri çekme yöntem 1
        //JsonSerializerOptions jsonSerializerOptions; 
        #endregion

        public HisseAdlariServices()
        {
            #region json veri çekme yöntem 1
            //jsonSerializerOptions = new JsonSerializerOptions()
            //{
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //}; 
            #endregion

#if (DEBUG && ANDROID)
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            httpClient = new HttpClient();
#endif


        }

        public async Task<List<HisseAdlari>> GetHisseAdlari()
        {
            #region json veri çekme yöntem 2 (kısa)
            var gidenHisseAdlari = await httpClient.GetFromJsonAsync<List<HisseAdlari>>(UrlHelper.HisselerUrl);
            return gidenHisseAdlari;

            #endregion
            #region json veri çekme yöntem 1
            //var response = await httpClient.GetAsync("https://localhost:7006/hisseler");
            //if (response.IsSuccessStatusCode)
            //{
            //    string jsonContent = await response.Content.ReadAsStringAsync();
            //    var sonuc = JsonSerializer.Deserialize<List<HisseAdlari>>(jsonContent, jsonSerializerOptions);

            //    return sonuc;
            //}
            //return new List<HisseAdlari>(); 
            #endregion
        }
    }
}
