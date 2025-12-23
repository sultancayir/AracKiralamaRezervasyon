using System;
using System.Collections.Generic;
using System.Linq;

namespace AracKiralamaRezervasyon
{
    class Program
    {
        // ================== MODELLER ==================
        class Arac
        {
            public string Plaka;
            public string Model;
            public decimal GunlukFiyat;
            public bool Musait = true;
        }

        class Rezervasyon
        {
            public string Plaka;
            public int GunSayisi;
            public decimal Tutar;
        }

        // ================== VERİLER ==================
        static List<Arac> araclar = new List<Arac>();
        static List<Rezervasyon> rezervasyonlar = new List<Rezervasyon>();

        // ================== MAIN ==================
        static void Main(string[] args)
        {
            OrnekAraclariYukle();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== ARAÇ KİRALAMA REZERVASYON SİSTEMİ ===");
                Console.WriteLine("1) Araçları Listele");
                Console.WriteLine("2) Rezervasyon Ekle");
                Console.WriteLine("3) Rezervasyon İptal");
                Console.WriteLine("4) Toplam Gelir");
                Console.WriteLine("0) Çıkış");
                Console.Write("Seçiminiz: ");

                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        AraclariListele();
                        Bekle();
                        break;

                    case "2":
                        RezervasyonEkle();
                        Bekle();
                        break;

                    case "3":
                        RezervasyonIptal();
                        Bekle();
                        break;

                    case "4":
                        ToplamGelir();
                        Bekle();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        Bekle();
                        break;
                }
            }
        }

        // ================== FONKSİYONLAR ==================

        static void OrnekAraclariYukle()
        {
            araclar.Add(new Arac { Plaka = "34ABC34", Model = "Renault Clio", GunlukFiyat = 1200 });
            araclar.Add(new Arac { Plaka = "34DEF34", Model = "Fiat Egea", GunlukFiyat = 1400 });
            araclar.Add(new Arac { Plaka = "06JKL06", Model = "Toyota Corolla", GunlukFiyat = 1700 });
        }

        static void AraclariListele()
        {
            Console.WriteLine("\nAraçlar:");
            foreach (var a in araclar)
            {
                string durum = a.Musait ? "Müsait" : "Dolu";
                Console.WriteLine($"{a.Plaka} - {a.Model} - {a.GunlukFiyat} TL/gün ({durum})");
            }
        }

        static void RezervasyonEkle()
        {
            Console.Write("\nPlaka girin: ");
            string plaka = Console.ReadLine().Trim().ToUpper();

            var arac = araclar.FirstOrDefault(a => a.Plaka == plaka && a.Musait);
            if (arac == null)
            {
                Console.WriteLine("Araç bulunamadı veya müsait değil.");
                return;
            }

            Console.Write("Kaç gün kiralanacak: ");
            int gun = int.Parse(Console.ReadLine());

            decimal tutar = gun * arac.GunlukFiyat;

            rezervasyonlar.Add(new Rezervasyon
            {
                Plaka = plaka,
                GunSayisi = gun,
                Tutar = tutar
            });

            arac.Musait = false;

            Console.WriteLine($"Rezervasyon eklendi. Tutar: {tutar} TL");
        }

        static void RezervasyonIptal()
        {
            Console.Write("\nİptal edilecek aracın plakası: ");
            string plaka = Console.ReadLine().Trim().ToUpper();

            var rez = rezervasyonlar.FirstOrDefault(r => r.Plaka == plaka);
            if (rez == null)
            {
                Console.WriteLine("Rezervasyon bulunamadı.");
                return;
            }

            rezervasyonlar.Remove(rez);

            var arac = araclar.First(a => a.Plaka == plaka);
            arac.Musait = true;

            Console.WriteLine("Rezervasyon iptal edildi.");
        }

        static void ToplamGelir()
        {
            decimal toplam = rezervasyonlar.Sum(r => r.Tutar);
            Console.WriteLine($"\nToplam Gelir: {toplam} TL");
        }

        static void Bekle()
        {
            Console.WriteLine("\nDevam etmek için bir tuşa bas...");
            Console.ReadKey();
        }
    }
}
