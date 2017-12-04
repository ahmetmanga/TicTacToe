using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    class oyuncu
    {
        private char h;
        public char harf
        {
            get { return h; }
            set
            {
                if(value == 'X' || value == 'O')
                {
                    h = value;
                }else
                {
                    h = 'X';
                }
            }
        }
        public string isim;
        bool insan;


        public oyuncu()
        {
            insan = true;
            harf = 'X';
        }
        public oyuncu(bool insanmiKontrolu,string ad)
        {
            insan = insanmiKontrolu;
            harf = 'X';
            isim = ad;
        }
        public oyuncu(bool insanmiKontrolu,char kr,string ad)
        {
            insan = insanmiKontrolu;
            harf = kr;
            isim = ad;
        }

        public char karakteriAl()
        {
            return h;
        }

        public bool oyuncuTurunuAl()
        {
            return insan;
        }

        public string oyuncununHamlesiniAl()
        {
            return "a";
        }
        public string insanOyuncuHamlesiniKontrol()
        {
            Console.WriteLine();
            Console.Write("Hücre Giriniz (Çıkış için 1 'e basın): ");
            Console.WriteLine();
            return Console.ReadLine();
            
            
        }

        public string bilgisayarHamlesiUret()
        {
            Random r = new Random();
            int x = r.Next(0, oyunTahtasi.boyut);
            int y = r.Next(0, oyunTahtasi.boyut) + 1;

            if (x == 0) return "A" + y.ToString();
            if (x == 1) return "B" + y.ToString();
            if (x == 2) return "C" + y.ToString();
            if (x == 3) return "D" + y.ToString();
            if (x == 4) return "E" + y.ToString();
            if (x == 5) return "F" + y.ToString();
            if (x == 6) return "G" + y.ToString();
            return "A1";
         }
    }
}
