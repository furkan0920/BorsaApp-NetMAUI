using BorsaUygulama.Models;
using BorsaUygulama.Services;

namespace BorsaUygulama.Pages;

public partial class UyeOl : ContentPage
{
    public UyeOl()
    {
        InitializeComponent();
    }

    #region CheckBox Kontrol

    private void uye_Ol_CheckBox_Erkek_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (uye_Ol_CheckBox_Erkek.IsChecked)
        {
            uye_Ol_CheckBox_Kadýn.IsChecked = false;
            uye_Ol_CheckBox_Unknown.IsChecked = false;
        }

    }
    private void uye_Ol_CheckBox_Kadýn_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (uye_Ol_CheckBox_Kadýn.IsChecked)
        {
            uye_Ol_CheckBox_Erkek.IsChecked = false;
            uye_Ol_CheckBox_Unknown.IsChecked = false;
        }
    }

    private void uye_Ol_CheckBox_Unknown_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (uye_Ol_CheckBox_Unknown.IsChecked)
        {
            uye_Ol_CheckBox_Erkek.IsChecked = false;
            uye_Ol_CheckBox_Kadýn.IsChecked = false;
        }
    }

    #endregion

    private async void btnKayýt_Pressed(object sender, EventArgs e)
    {
        var fadeSetDown = btnKayýt.FadeTo(0.75, 50, Easing.Linear);
        var scaleSetDown = btnKayýt.ScaleTo(0.8, 50, Easing.Linear);

        await Task.WhenAll(fadeSetDown, scaleSetDown);
    }

    private async void btnKayýt_Released(object sender, EventArgs e)
    {
        var fadeSetUp = btnKayýt.FadeTo(1, 50, Easing.Linear);
        var scaleSetUp = btnKayýt.ScaleTo(1, 50, Easing.Linear);

        await Task.WhenAll(fadeSetUp, scaleSetUp);

        bool isimControl = true;
        bool sifreControl = true;

        string cinsiyetSonuc;
        if (uye_Ol_CheckBox_Erkek.IsChecked)
            cinsiyetSonuc = "Erkek";
        else if (uye_Ol_CheckBox_Kadýn.IsChecked)
            cinsiyetSonuc = "Kadýn";
        else
            cinsiyetSonuc = "Boþ Býrakýldý";

        if (txtSifre != txtSifreTekrar)
        {
            sifreControl = false;
        }
        if(string.IsNullOrWhiteSpace(txtIsim.Text))
        {
            isimControl = false;
            txtIsim.BackgroundColor = Color.FromRgb(255,123,54);
        }


        //TODO her bir entry için kontrol oluþturulacak
        

        if (isimControl && sifreControl)
        {
            var uyeOl = new Uyelik()
            {
                Id = Guid.NewGuid(),
                Ad = txtIsim.Text,
                Soyad = txtSoyisim.Text,
                KullaniciAdi = txtKullaniciAdi.Text,
                Sifre = txtSifre.Text,
                UyelikTarih = pckDogumtarih.Date,
                Cinsiyet = cinsiyetSonuc
            };

            IUyelikServices uyelikServices = new UyelikServices();

            await uyelikServices.UyelikEkle(uyeOl);

            await Navigation.PopModalAsync();
        }
        else
        {
            await DisplayAlert("Yanlýþ", "->", "Tamam");
        }

    }
}