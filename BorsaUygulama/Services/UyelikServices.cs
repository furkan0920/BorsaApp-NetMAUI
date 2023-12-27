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
    internal interface IUyelikServices
    {
        Task UyelikEkle(Uyelik ekle);
        Task SifreDegistir(Uyelik sifre);
        Task<List<Uyelik>> GetUyelikBilgileri();
    }

    public class UrlHelper
    {
        public static string BaseAddress = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:7006" : "https://localhost:7006";  //local de değilsen bu gereksiz

        public static string UyelerUrl = $"{BaseAddress}/Uyeler";
        public static string UyelikOlusturUrl = $"{BaseAddress}/UyelikOlustur";
        public static string HisselerUrl = $"{BaseAddress}/Hisseler";
        public static string HisseEkleUrl = $"{BaseAddress}/HisseEkle";
        public static string HisseBilgileriUrl = $"{BaseAddress}/HisseBilgileri";
        public static string SifreDegistir = $"{BaseAddress}/SifreDegistir";
    }

    public class UyelikServices : IUyelikServices
    {
        HttpClient httpClient;

        public UyelikServices()
        {
#if (DEBUG && ANDROID)
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            httpClient = new HttpClient(handler.GetPlatformMessageHandler());
#else
            httpClient = new HttpClient();
#endif
        }

        public async Task<List<Uyelik>> GetUyelikBilgileri()
        {
            var gidenUyelikBilgileri = await httpClient.GetFromJsonAsync<List<Uyelik>>(UrlHelper.UyelerUrl);
            return gidenUyelikBilgileri;
        }


        public async Task UyelikEkle(Uyelik ekle)
        {
            JsonContent jsonContent = JsonContent.Create(ekle); //string veriyi json yapıp geri döndürme
            var response = await httpClient.PostAsync(UrlHelper.UyelikOlusturUrl, jsonContent);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task SifreDegistir(Uyelik sifre)
        {
            JsonContent jsonContent = JsonContent.Create(sifre);
            var response = await httpClient.PostAsync(UrlHelper.SifreDegistir, jsonContent);
        }
    }

    public class HttpsClientHandlerService
    {
        public HttpMessageHandler GetPlatformMessageHandler()
        {
#if ANDROID
        var handler = new Xamarin.Android.Net.AndroidMessageHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if(cert != null && cert.Issuer.Equals("CN=localhost"))
                return true;
            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            TrustOverrideForUrl = IsHttpsLocalHost
        };
        return handler;
#else
        throw new PlatformNotSupportedException("Sadece android ve ios");
#endif
        }
#if IOS
        public bool IsHttpsLocalHost(NSUrlSessionHandler senderi, string url, Security.SecTrust trust)
        {
            if (url.StartsWith("https://localhost"))
                return true;
            return false;
        }
#endif
    }
}
