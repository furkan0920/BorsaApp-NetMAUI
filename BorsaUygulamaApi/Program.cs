using BorsaUygulamaApi.EF;
using BorsaUygulamaApi.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/Hisseler", () =>
{
    BorsaAppContext context = new BorsaAppContext();

    var sonuc = context.HisseAd
    .Select(x => x)
    .ToList();

    return sonuc;
});

app.MapGet("/HisseBilgileri", () =>
{
    BorsaAppContext context = new BorsaAppContext();

    var sonuc = context.HisseBilgiKayit
    .Select(x => x)
    .ToList();

    return sonuc;
});

app.MapGet("/Uyeler", () =>
{
    BorsaAppContext context = new BorsaAppContext();

    var sonuc = context.Uyelik
    .Select(x => x)
    .ToList();

    return sonuc;
});

app.MapPost("/UyelikOlustur", (Uyelik uyelik) =>
{
    BorsaAppContext context = new BorsaAppContext();
    context.Uyelik.Add(uyelik);
    context.SaveChanges();
});

app.MapPost("/HisseEkle", (HisseBilgileriKayit hisseBilgileriKayitEkle) =>
{
    BorsaAppContext context = new BorsaAppContext();
    context.HisseBilgiKayit.Add(hisseBilgileriKayitEkle);
    context.SaveChanges();
});

app.MapPost("/SifreDegistir", (Uyelik uyelik) =>
{
    BorsaAppContext context = new BorsaAppContext();
    var sifreyiDegistir = context.Uyelik.Find(uyelik.Id);
    if (sifreyiDegistir is not null)
    {
        sifreyiDegistir.Sifre = uyelik.Sifre;
        context.SaveChanges();
    }
});

app.MapDelete("/HisseEkle/{id}", (int id) =>
{
    BorsaAppContext context = new BorsaAppContext();
    var silinecekHisse = context.HisseBilgiKayit.FirstOrDefault(x => x.Id == id);
    if (silinecekHisse is not null)
    {
        context.HisseBilgiKayit.Remove(silinecekHisse);
        context.SaveChanges();
    }
});


app.Run();