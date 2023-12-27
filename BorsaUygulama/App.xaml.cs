using BorsaUygulama.Pages;

namespace BorsaUygulama
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new GirisYap();
        }
    }
}