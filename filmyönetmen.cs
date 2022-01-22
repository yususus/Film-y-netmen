using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Filmyonetmen
{
    class DoubleLinkedList<Film>
    {
        Dugum ilk = null;
        Dugum son = null;
        int Boyut = 0;
        public DoubleLinkedList() { }
        public DoubleLinkedList(Film data)
        {
            Add(data);
        }

        public void Add(Film data)
        {
            FilmEkle(data, Boyut);
        }

        public void FilmEkle(Film data, int fiyönet)
        {
            if (Boyut < fiyönet)
            {
                throw new IndexOutOfRangeException("YER YOK!!");
            }
            Dugum node = new Dugum(data);
            PBul(fiyönet, out Dugum yus);
            if (yus != ilk)
            {
                if (yus is null)
                {
                    son.Nx = node;
                    node.pre = son;
                    son = node;
                }
                else
                {
                    node.pre = yus.pre;
                    node.Nx = yus;
                    yus.pre = node;
                    node.pre.Nx = node;
                }
            }
            else
            {
                if (yus is null)
                    ilk = son = node;
                else
                {
                    ilk.pre = node;
                    node.Nx = ilk;
                    ilk = node;
                }
            }
            Boyut++;
        }
        public void SonDelete()
        {
            Delete(Boyut - 1);
        }
        public void Delete(int fiyönet)
        {
            if (Boyut <= fiyönet)
                throw new IndexOutOfRangeException("BİTTİ!!!");
            PBul(fiyönet, out Dugum yus);
            if (fiyönet is 0)
            {
                ilk = yus.Nx;
                ilk.pre = null;
                yus.Nx = null;
            }
            else if (fiyönet == Boyut - 1)
            {
                son = yus.pre;
                son.Nx = null;
                yus.pre = null;
            }
            else
            {
                yus.pre.Nx = yus.Nx;
                yus.Nx.pre = yus.pre;
                yus.pre = null;
                yus.Nx = null;
            }
            Boyut--;
        }
        private void PBul(int fiyönet, out Dugum Yus)
        {
            Yus = ilk;
            for (int i = 0; i < fiyönet; i++)
                Yus = Yus.Nx;
        }
        public IEnumerable<Film> GetEnumerator()
        {
            for (Dugum Yus = ilk; Yus != null; Yus = Yus.Nx)
                yield return Yus.data;
        }
        public int Bul(Film data)
        {
            int Döngü = 0;
            bool buldu = false;
            for (Dugum traveler = ilk; traveler != null; traveler = traveler.Nx, Döngü++)
                if (traveler.data.Equals(data))
                {
                    buldu = true;
                    break;
                }
            return buldu ? Döngü : -1;
        }
        class Dugum
        {
            public readonly Film data;
            public Dugum Nx = null;
            public Dugum pre = null;
            public Dugum(Film data)
            {
                this.data = data;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DoubleLinkedList<string> Dli;
            if (File.Exists(@"veri.txt"))
            {
               Dli = JsonSerializer.Deserialize<DoubleLinkedList<string>>(File.ReadAllText(@"veri.txt"));
            }
            else
            {
                Dli = new DoubleLinkedList<string>();
            }
            bool Devam = true;
            Console.WriteLine("Film ve Yönetmen");
            Dli.Add("Esaretin Bedeli - Frank DARABONT");
            Dli.Add("Baba - Francis Ford COPPOLA ");
            Dli.Add("Kara Şövalye - Christopher NOLAN");
            Dli.Add("Yüzüklerin Efendisi: Kralın Dönüşü - Peter JACKSON");
            Dli.Add("Dövüş Külübü - David FINCHER");
            Dli.Add("Matrix - Lana WACHOWSKİ, Hugo WEAVING");
            Dli.Add("Sıkı Dostlar - Martin SCORSESE");
            Dli.Add("Yıldızlararası - Christopher NOLAN");
            Dli.Add("Piyanist - Roman POLANSKI");
            Dli.Add("Gladyatör - Ridley SCOTT");
            Dli.Add("Aslan Kral - Roger ALLERS , Rob MINKOFF");
            Dli.Add("Geleceğe Dönüş - Robert ZEMECKIS");
            Dli.Add("Soysuzlar Çetesi - Quentin TARANTINO");
            Dli.Add("Sil Baştan - Michel GONDRY");
            Dli.Add("Cesur Yürek - Mel GIBSON ");
            Dli.Add("Joker - Nadine LABASKI");
            while (Devam == true)
            {
                Console.WriteLine("1-Filmler listele \n 2-Film kaçıncı sırada" +
                    "\n 3-Film ekleme yapabilirsiniz " +
                    "\n 4-Film sil   ");
                Console.WriteLine("Ne Yapmak İstediğinizin Numarasını Giriniz");
                int BasılanTus = Convert.ToInt32(Console.ReadLine());
                if (BasılanTus == 1)
                {
                    foreach (var x in Dli.GetEnumerator())
                        Console.WriteLine(x);
                }
                if (BasılanTus == 2)
                {
                    Console.WriteLine("Hangi filmi bulamadınız?");
                    string Bul = Console.ReadLine();
                    int bul2 = Dli.Bul(Bul);
                    bul2 = bul2 + 1;
                    Console.WriteLine("Filmin hangi sırada olduğu : {0}", bul2);
                }
                if (BasılanTus == 3)
                {
                    Console.WriteLine("Film - Yönetmen şeklinde ekleme yapabilirsiniz");
                    string Ekle = Console.ReadLine();
                    Dli.Add(Ekle);
                    Console.WriteLine("Film ekleme başarılı");

                }
                if (BasılanTus == 4)
                {
                    Dli.SonDelete();
                    Console.WriteLine("En son film silindi!!");

                }
                Console.WriteLine("Başka işlem yapmak istiyor musunuz? Evet ya da hayır");
                string Cevap = Console.ReadLine();
                Cevap = Cevap.ToUpper();
                if (Cevap == "EVET")
                {
                    Devam = true;
                    Console.Clear();
                }
                else
                {
                    Devam = false;
                }
            }
            File.WriteAllText(@"veri.txt", JsonSerializer.Serialize(Dli));
            Console.WriteLine("SİSTEM KAPANDI!!!!!");
            Console.ReadLine();
        }
    }
}

