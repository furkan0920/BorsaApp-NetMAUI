using BorsaUygulama.Models;
using BorsaUygulama.Services;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace BorsaUygulama.Pages;

public partial class GirisYap : ContentPage
{
    IUyelikServices uyelikServices;

    public GirisYap()
    {
        InitializeComponent();
        uyelikServices = new UyelikServices();
    }


    public async void GirisBilgileriGetir()
    {
        bool girisKontrol = false;
        var gelenUyelikBilgileri = await uyelikServices.GetUyelikBilgileri();

        foreach (var uye in gelenUyelikBilgileri)
        {
            if (uye.KullaniciAdi == entryKullaniciAd.Text && uye.Sifre == entrySifre.Text)
            {
                girisKontrol = true;
                KullaniciBilgiler.Instance.Id = uye.Id;
                KullaniciBilgiler.Instance.Ad = uye.Ad;
                KullaniciBilgiler.Instance.Soyad = uye.Soyad;
            }
        }
        if (girisKontrol)
        {
            //var result = await DisplayAlert("Giriþ Baþarýlý!", "Hisse kayýt iþlemlerinizi yapmaya baþlayabilirsiniz", "Tamam", "Kapat");

            var toast = Toast.Make("Giriþ Baþarýlý! Hisse kayýt iþlemlerinizi yapmaya baþlayabilirsiniz", ToastDuration.Short);
            await toast.Show();


            await Navigation.PushModalAsync(new MainPage());
        }
        else
        {
            Yanlis_giris_bilgileri.IsVisible = true;
        }

        girisKontrol = false;
    }

    private async void btnGiris_Pressed(object sender, EventArgs e)
    {
        var fadeSetDown = btnGiris.FadeTo(0.75, 50, Easing.Linear);
        var scaleSetDown = btnGiris.ScaleTo(0.8, 50, Easing.Linear);

        await Task.WhenAll(fadeSetDown, scaleSetDown);
    }

    private async void btnGiris_Released(object sender, EventArgs e)
    {
        var fadeSetUp = btnGiris.FadeTo(1, 50, Easing.Linear);
        var scaleSetUp = btnGiris.ScaleTo(1, 50, Easing.Linear);

        await Task.WhenAll(fadeSetUp, scaleSetUp);

        GirisBilgileriGetir();
    }

    private async void btnUyeOl_Pressed(object sender, EventArgs e)
    {
        var fadeSetDown = btnUyeOl.FadeTo(0.75, 50, Easing.Linear);
        var scaleSetDown = btnUyeOl.ScaleTo(0.8, 50, Easing.Linear);

        await Task.WhenAll(fadeSetDown, scaleSetDown);
    }

    private async void btnUyeOl_Released(object sender, EventArgs e)
    {
        var fadeSetUp = btnUyeOl.FadeTo(1, 50, Easing.Linear);
        var scaleSetUp = btnUyeOl.ScaleTo(1, 50, Easing.Linear);

        await Task.WhenAll(fadeSetUp, scaleSetUp);

        await Navigation.PushModalAsync(new UyeOl());
    }
}