using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TicTacToe
{
    class oyunTahtasi
    {
        char[,] tahta;
        private static int n;
        public int x,y;
        public static int boyut { get { return n; } set {
                if (value == 3 || value == 5 || value == 7){
                    n = value;
                 }else{
                    n = 3;
                }
            } }
        public oyunTahtasi()
        {


        }

        public oyunTahtasi(int b)
        {
            boyut = b;
            tahta = new char[n,n];
        }

        public oyunTahtasi(char[,] gelenTahta)
        {
            boyut = Convert.ToInt32(Math.Sqrt(gelenTahta.Length));
            
            tahta = new char[n, n];
                for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    tahta[i, j] = gelenTahta[i, j];
                }
            }
              

        }
        public char[,] oyunTahtasiniAl()
        {
            return tahta;
        }
        public void oyunTahtasiniYazdir(oyuncu kullanici)
        {
            System.Threading.Thread.Sleep(300);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine("\n\tİsim: " + kullanici.isim + "\t\t\t Oynadığı Harf: " + kullanici.harf);
            Console.ForegroundColor = ConsoleColor.White;

            char[] d = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };
            int i, j;
            Console.WriteLine("\t\t");
            for (i = 0; i < n; i++)
            {
                Console.Write("\t\t" + d[i]);
            }
            Console.WriteLine();
            this.cizgi();
            Console.WriteLine();
            for (i = 0; i < n; i++)
            {
                Console.Write("   " + (i+1) + "\t|");
                for (j = 0; j < n; j++)
                {   
                    Console.Write("\t"+ tahta[i, j] + "\t|");
                }
                Console.WriteLine();
                this.cizgi();
                Console.WriteLine();
            }
        }

        public bool hamleyiYaz(string koordinat, char oyuncu)
        {
            koordinatAyir(koordinat);
            if(x >= n || y >= n)
            {
                return false;
            }
            else if (tahta[x,y] == 'X' || tahta[x,y] == 'O')
            {
                return false;
                
            }
            else
            {
                tahta[x, y] = oyuncu;

                return true;
            }
           
            
        }
        public bool kazanan(char oyuncu)
        {
            int i, j;
            bool durum = false;

                for(i = 0; i < n; i++)
            {
                durum = false;
                for(j = 0; j < n; j++)
                {
                    if (tahta[i, j] == oyuncu) durum = true; else durum = false;

                    if (!(durum)) break;
                }
                if (durum) return durum; 
            }

            for (i = 0; i < n; i++)
            {
                durum = false;
                for (j = 0; j < n; j++)
                {
                    if (tahta[j, i] == oyuncu) durum = true; else durum = false;

                    if (!durum) break;
                }
                if (durum) return durum;
            }

            durum = false;
            for(i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (tahta[i, j] == oyuncu && i == j)
                    {
                        durum = true;
                    }
                    else if (i == j && tahta[i, j] != oyuncu)
                    {
                        durum = false; break;
                    }
                }
              if (!durum) break;
            }
            if (durum) return durum;
            
            
            durum = false;
            for (i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    if (tahta[i, j] == oyuncu && i + j == n-1)
                    {
                      durum = true;
                    }
                    else if (i + j == n-1 && tahta[i, j] != oyuncu)
                    {
                      durum = false; break;
                    }
                }
                if (!durum) break;
            }
            if (durum) return durum;
            

            return durum;
        }
        public bool beraberlikKontrol()
        {
            if (kazanan('X') == true || kazanan('O') == true) return false; else return true;
            
        }

        public bool bosYer()
        {
            bool durum = false;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (tahta[i, j] == 'X') durum = false;

                    if (tahta[i, j] == 'O') durum = false;

                    if (tahta[i, j] != 'X' && tahta[i, j] != 'O') return true;
                }
            }
            return durum;
        }

        public void koordinatAyir(string k)
        {
            x = int.Parse(k[1].ToString()) -1;
            
            if (k[0] == 'A' || k[0] == 'a') y = 0;
            if (k[0] == 'B' || k[0] == 'b') y = 1;
            if (k[0] == 'C' || k[0] == 'c') y = 2;
            if (k[0] == 'D' || k[0] == 'd') y = 3;
            if (k[0] == 'E' || k[0] == 'e') y = 4;
            if (k[0] == 'F' || k[0] == 'f') y = 5;
            if (k[0] == 'G' || k[0] == 'g') y = 6;
            
        }

        public void textYaz(oyuncu kullanici)
        {
            StreamWriter yaz = new StreamWriter("oyunVerileri.txt");
            yaz.WriteLine("İsim: " + kullanici.isim);
            yaz.WriteLine("Harf: " + kullanici.harf);
            yaz.WriteLine("Boyut: " + oyunTahtasi.boyut);
            char[,] dizi = this.oyunTahtasiniAl();
            for (int i = 0; i < oyunTahtasi.boyut; i++)
            {
                for (int j = 0; j < oyunTahtasi.boyut; j++)
                {
                    if (dizi[i, j] == 'X' || dizi[i, j] == 'O')
                    {
                        yaz.Write(dizi[i, j] + " ");
                    }
                    else
                    {
                        yaz.Write("- ");
                    }
                }
                yaz.WriteLine();
            }

            yaz.Close();

            Console.WriteLine("Veriler kayıt edildi.");
        }

        public void cizgi()
        {
            if(n == 3)
            {
                Console.WriteLine("_________________________________________________________");
            }else if(n == 5)
            {
                Console.WriteLine("_________________________________________________________________________________________");
            }else
            {
                Console.WriteLine("_________________________________________________________________________________________________________________________");
            }
        }
    }
}
