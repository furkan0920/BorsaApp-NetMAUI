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
            uye_Ol_CheckBox_Kad�n.IsChecked = false;
            uye_Ol_CheckBox_Unknown.IsChecked = false;
        }

    }
    private void uye_Ol_CheckBox_Kad�n_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (uye_Ol_CheckBox_Kad�n.IsChecked)
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
            uye_Ol_CheckBox_Kad�n.IsChecked = false;
        }
    }

    #endregion

    private async void btnKay�t_Pressed(object sender, EventArgs e)
    {
        var fadeSetDown = btnKay�t.FadeTo(0.75, 50, Easing.Linear);
        var scaleSetDown = btnKay�t.ScaleTo(0.8, 50, Easing.Linear);

        await Task.WhenAll(fadeSetDown, scaleSetDown);
    }

    private async void btnKay�t_Released(object sender, EventArgs e)
    {
        var fadeSetUp = btnKay�t.FadeTo(1, 50, Easing.Linear);
        var scaleSetUp = btnKay�t.ScaleTo(1, 50, Easing.Linear);

        await Task.WhenAll(fadeSetUp, scaleSetUp);

        bool isimControl = true;
        bool sifreControl = true;

        string cinsiyetSonuc;
        if (uye_Ol_CheckBox_Erkek.IsChecked)
            cinsiyetSonuc = "Erkek";
        else if (uye_Ol_CheckBox_Kad�n.IsChecked)
            cinsiyetSonuc = "Kad�n";
        else
            cinsiyetSonuc = "Bo� B�rak�ld�";

        if (txtSifre != txtSifreTekrar)
        {
            sifreControl = false;
        }
        if(string.IsNullOrWhiteSpace(txtIsim.Text))
        {
            isimControl = false;
            txtIsim.BackgroundColor = Color.FromRgb(255,123,54);
        }


        //TODO her bir entry i�in kontrol olu�turulacak
        

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
            await DisplayAlert("Yanl��", "->", "Tamam");
        }

    }
}