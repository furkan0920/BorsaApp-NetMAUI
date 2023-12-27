using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BorsaUygulamaApi.Migrations
{
    /// <inheritdoc />
    public partial class songuncelhal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Giris",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giris", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HisseAd",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HisseAd = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HisseAd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HisseBilgiKayit",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HisseSahibiId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AlimSatim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HisseAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HisseAdedi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HisseFiyat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HisseAlimSatimDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HisseBilgiKayit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Uyelik",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sifre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UyelikTarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Cinsiyet = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uyelik", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Giris");

            migrationBuilder.DropTable(
                name: "HisseAd");

            migrationBuilder.DropTable(
                name: "HisseBilgiKayit");

            migrationBuilder.DropTable(
                name: "Uyelik");
        }
    }
}
