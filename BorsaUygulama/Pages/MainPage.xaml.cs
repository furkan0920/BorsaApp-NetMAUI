using Microsoft.Maui.Controls;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using BorsaUygulama.Pages;
using BorsaUygulama.Services;
using BorsaUygulama.Models;
using System.Linq;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace BorsaUygulama
{

    public partial class MainPage : TabbedPage
    {
        IHisseAdlariServices hisseAdlariServices;
        IHisseBilgileriKayıtServices hisseBilgileriKayıtServices;

        ObservableCollection<HisseBilgileriKayit> hisseBilgileriKayit;

        public MainPage()
        {
            InitializeComponent();

            Hesabim_Ad.Text = KullaniciBilgiler.Instance.Ad;
            Hesabim_Soyad.Text = KullaniciBilgiler.Instance.Soyad;

            hisseAdlariServices = new HisseAdlariServices();
            hisseBilgileriKayıtServices = new HisseBilgileriKayıtServices();
            hisseBilgileriKayit = new ObservableCollection<HisseBilgileriKayit>();

            HisseAdlariGetir();

            HisseleriListele.ItemsSource = hisseBilgileriKayit;


            //this.Resources["defaultText"] = "Hisseler";
        }


        private async void HisseAdlariGetir()
        {
            var gelenHisseAdlari = await hisseAdlariServices.GetHisseAdlari();

            HisseEkle_HisseSec_Picker.ItemsSource = gelenHisseAdlari
                .OrderBy(x => x.HisseAd)
                .Select(x => x.HisseAd)
                .ToList();

        }


        private async void HisseEkle_Temizle_Button_Pressed(object sender, EventArgs e)
        {
            var fadeSetDown = HisseEkle_Temizle_Button.FadeTo(0.75, 50, Easing.Linear);
            var scaleSetDown = HisseEkle_Temizle_Button.ScaleTo(0.8, 50, Easing.Linear);

            await Task.WhenAll(fadeSetDown, scaleSetDown);
        }
        private async void HisseEkle_Temizle_Button_Released(object sender, EventArgs e)
        {
            var fadeSetUp = HisseEkle_Temizle_Button.FadeTo(1, 50, Easing.Linear);
            var scaleSetUp = HisseEkle_Temizle_Button.ScaleTo(1, 50, Easing.Linear);

            await Task.WhenAll(fadeSetUp, scaleSetUp);
        }


        private async void HisseEkle_Kaydet_Button_Pressed(object sender, EventArgs e)
        {
            var fadeSetDown = HisseEkle_Kaydet_Button.FadeTo(0.75, 50, Easing.Linear);
            var scaleSetDown = HisseEkle_Kaydet_Button.ScaleTo(0.8, 50, Easing.Linear);

            await Task.WhenAll(fadeSetDown, scaleSetDown);
        }
        private async void HisseEkle_Kaydet_Button_Released(object sender, EventArgs e)
        {
            var fadeSetUp = HisseEkle_Kaydet_Button.FadeTo(1, 50, Easing.Linear);
            var scaleSetUp = HisseEkle_Kaydet_Button.ScaleTo(1, 50, Easing.Linear);

            await Task.WhenAll(fadeSetUp, scaleSetUp);




            var hisseEkle = new HisseBilgileriKayit()
            {
                HisseSahibiId = KullaniciBilgiler.Instance.Id,
                AlimSatim = HisseEkle_HisseAlimSatim_Picker.SelectedItem.ToString(),
                HisseAdi = HisseEkle_HisseSec_Picker.SelectedItem.ToString(),
                HisseAdedi = HisseEkle_HisseAdet_Entry.Text,
                HisseFiyat = HisseEkle_HisseFiyat_Entry.Text,
                HisseAlimSatimDate = DateTime.Now
            };

            IHisseBilgileriKayıtServices hisseBilgileriKayıtServices = new HisseBilgileriKayıtServices();

            await hisseBilgileriKayıtServices.HisseEkle(hisseEkle);

            await DisplayAlert("Hisse Eklendi", "", "Tamam");
        }


        private async void Alinan_Hisseleri_Getir_Button_Clicked(object sender, EventArgs e)
        {
            await GetHisseBilgileri();
        }
        private async Task GetHisseBilgileri()
        {
            var sonuc = await hisseBilgileriKayıtServices.GetHisseBilgileriKayıt();
            hisseBilgileriKayit.Clear();

            foreach (var item in sonuc.Where(x => x.AlimSatim == "Alınan"))
            {
                if (item.HisseSahibiId == KullaniciBilgiler.Instance.Id)
                {
                    hisseBilgileriKayit.Add(item);
                }
            }
        }


        private async void Satilan_Hisseleri_Getir_Button_Clicked(object sender, EventArgs e)
        {
            await GetHisseBilgileriSatilan();
        }
        private async Task GetHisseBilgileriSatilan()
        {
            var sonuc = await hisseBilgileriKayıtServices.GetHisseBilgileriKayıt();
            hisseBilgileriKayit.Clear();

            foreach (var item in sonuc.Where(x => x.AlimSatim == "Satılan"))
            {
                if (item.HisseSahibiId == KullaniciBilgiler.Instance.Id)
                {
                    hisseBilgileriKayit.Add(item);
                }
            }
        }


        private async void Secili_Kaydi_Sil_Button_Clicked(object sender, EventArgs e)
        {
            if ((HisseBilgileriKayit)HisseleriListele.SelectedItem is not null)
            {
                var seciliHisseKayit = (HisseBilgileriKayit)HisseleriListele.SelectedItem;
                var seciliHisse = seciliHisseKayit.Id;

                bool cevap = await DisplayAlert("Seçmiş olduğunuz kayıt silinecek!","Emin misiniz?", "Sil", "İptal Et");
                if (cevap)
                {
                    await hisseBilgileriKayıtServices.HisseSil(seciliHisse);
                }
            }
            else
            {
                await DisplayAlert("Herhangi bir seçim yapmadınız!", "Lütfen bir kayıt seçiniz ->", "Tamam");
            }
        }


        private void Hesabim_Cikis_Yap_Btn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
        private async void Hesabim_Sifre_Degistir_Btn_Clicked(object sender, EventArgs e)
        {
            bool passControl = true;

            if (txtSifre_update.Text != txtSifre_tekrar_update.Text)
            {
                passControl = false;
                Yanlis_sifre_bilgileri.IsVisible = true;
                txtSifre_update.BackgroundColor = Color.FromRgb(255, 0,  0);
                txtSifre_tekrar_update.BackgroundColor = Color.FromRgb(255, 0,  0);
            }
            else
            {
                var uyeBilgileri = new Uyelik()
                {
                    Id = KullaniciBilgiler.Instance.Id,
                    Ad = KullaniciBilgiler.Instance.Ad,
                    Soyad = KullaniciBilgiler.Instance.Soyad,
                    KullaniciAdi = KullaniciBilgiler.Instance.KullaniciAdi,
                    Sifre = txtSifre_update.Text,
                    UyelikTarih = KullaniciBilgiler.Instance.UyelikTarih,
                    Cinsiyet = KullaniciBilgiler.Instance.Cinsiyet
                };

                IUyelikServices uyelikServices = new UyelikServices();
                await uyelikServices.SifreDegistir(uyeBilgileri);
                //update yapılacak
                Yanlis_sifre_bilgileri.IsVisible = false;
                txtSifre_update.BackgroundColor = Color.FromRgb(0, 255, 0);
                txtSifre_tekrar_update.BackgroundColor = Color.FromRgb(0, 255, 0);
                txtSifre_update.Text = "";
                txtSifre_tekrar_update.Text = "";
                await DisplayAlert("Şifreniz Güncelleme Başarılı", "", "Tamam");

            }
        }

    }
}