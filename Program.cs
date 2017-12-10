using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TicTacToe
{
    class Program
    {
        oyuncu kullanici, bilgisayar;
        oyunTahtasi tictac;
        static string text;
        static void Main()
        {
            Console.Title = "TicTacToe | Proje 4";
          
            Program p = new Program();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Yeni Oyun Açmak İçin - 1");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Kayıttan Oyun Yüklemek İçin - 2");
            Console.ForegroundColor = ConsoleColor.White;


            text = Console.ReadLine();
            int secim = p.sayiKontrol(text) ? Convert.ToInt32(text) : 1;

            if (secim == 2) p.kayittanYukle(); else p.normalYukle();

            p.oyunuOyna();

            Console.ReadLine();
            
        }

        public void normalYukle()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int boyut;
            string isim;
            char harf;
            Console.Write("Tablo boyutunu girin : ");
            
            text = Console.ReadLine();
            while (!sayiKontrol(text))
            {
                Console.Write("Tablo boyutunu girin : ");
                text = Console.ReadLine();
            }
            boyut = Convert.ToInt32(text);
            Console.Write("İsminizi Girin : ");
            isim = Console.ReadLine();
            Console.Write("Harf Seçin. (X yada O) - Direk geçmek isterseniz başka tuşa basın : ");
            harf = Convert.ToChar(Console.ReadLine());

            tictac = new oyunTahtasi(boyut);

            nesneOlustur(harf, isim);
        }

        public void kayittanYukle()
        {
            int boyut;
            string isim = " ";
            string[] dizi;
            char[,] t;
            char harf;
            StreamReader sr = new StreamReader("oyunVerileri.txt");
            dizi = sr.ReadLine().Split(' ');

            for (int i = 1; i < dizi.Length; i++)   isim = String.Concat(isim, dizi[i] + " ");


            dizi = sr.ReadLine().Split(' ');
            harf = Convert.ToChar(dizi[1]);
            dizi = sr.ReadLine().Split(' ');
            boyut = int.Parse(dizi[1]);
           
            Console.WriteLine("Text Dosyası Okundu. Veriler Alındı.");
            Console.ForegroundColor = ConsoleColor.White;

            t = new char[boyut, boyut];
            for (int i = 0; i < boyut; i++)
            {
                dizi = sr.ReadLine().Split(' ');
                for (int j = 0; j < dizi.Length-1; j++)
                {
                   if(dizi[j] == "X" || dizi[j] == "O")
                    {
                        t[i, j] = Convert.ToChar(dizi[j]);
                    }
                   if(dizi[j] != "X" && dizi[j] != "O")
                    {

                        t[i, j] = Convert.ToChar(" ");
                    }

                }
            }
           
            tictac = new oyunTahtasi(t);

            nesneOlustur(harf, isim);
            sr.Close();
        }

        public void oyunuOyna()
        {
            string hucre;
            bool kayit = false;
            while (tictac.bosYer() && !(tictac.kazanan(kullanici.harf) || tictac.kazanan(bilgisayar.harf)))
            {   tekrar:
                tictac.oyunTahtasiniYazdir(kullanici);
                hucre = kullanici.insanOyuncuHamlesiniKontrol();
                if (hucre == "1") { kayit = true; break; }else if(hucre.Length <= 1) { goto tekrar;  }
                while (tictac.hamleyiYaz(hucre, kullanici.harf) == false)
                {
                    hucre = kullanici.insanOyuncuHamlesiniKontrol();
                }
                if (!(tictac.bosYer()) || tictac.kazanan(kullanici.harf) || tictac.kazanan(bilgisayar.harf)) break;
                hucre = bilgisayar.bilgisayarHamlesiUret();
                while (tictac.hamleyiYaz(hucre, bilgisayar.harf) == false)
                {
                    hucre = bilgisayar.bilgisayarHamlesiUret();
                }
                tictac.oyunTahtasiniYazdir(bilgisayar);
                Console.WriteLine("Bilgisayar hamle üretti.");

            }
            if (kayit) tictac.textYaz(kullanici); else kazananiBelirle();
        }

        public void kazananiBelirle()
        {

            tictac.oyunTahtasiniYazdir(kullanici);

            Console.WriteLine();

            if (tictac.beraberlikKontrol())
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Berabere Kaldınız.");
            }
            else if (tictac.kazanan(kullanici.harf))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Kazandınız");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Bilgisayar Kazandı");
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Tekrar Oynamak İstermisiniz? (Evet : 1)");
            text = Console.ReadLine();
            int secim = (sayiKontrol(text)) ? Convert.ToInt32(text) : 2;
            if(secim == 1) Main(); else Environment.Exit(2);
            

        }

        public void nesneOlustur(char harf, string isim)
        {
            if (harf == 'X' || harf == 'O' || harf == 'x' || harf == 'o')
                kullanici = new oyuncu(true, harf, isim);
            else
                kullanici = new oyuncu(true, isim);

            if (harf == 'O' || harf == 'o') harf = 'X'; else harf = 'O';

            bilgisayar = new oyuncu(false, harf, "Bilgisayar");
        }

        public bool sayiKontrol(string text)
        {
            foreach(char c in text)
            {
                if (!Char.IsNumber(c)) return false;
            }
            return true;
        }

       
    }
}
