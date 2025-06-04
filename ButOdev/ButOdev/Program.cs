using System;
using System.Collections.Generic;

public class Karakter
{
    public string Ad { get; set; }
    private int can;
    private int guc;
    private int mana;

    public static int ToplamSaldiri = 0;

    public int Can
    {
        get { return can; }
        set
        {
            if (value < 0)
            {
                can = 0;
            }
            else
            {
                can = value;
            }
        }
    }

    public int Guc
    {
        get { return guc; }
        set { guc = value; }
    }

    public int Mana
    {
        get { return mana; }
        set { mana = value; }
    }

    public Karakter(string ad, int can, int guc, int mana)
    {
        Ad = ad;
        Can = can;
        Guc = guc;
        Mana = mana;
    }

    public virtual void Saldir(Karakter hedef)
    {
        hedef.Can -= Guc;
        ToplamSaldiri++;
        Console.WriteLine($"{Ad}, {hedef.Ad}'a {Guc} hasar verdi!");
    }

    public virtual void Ulti(Karakter hedef)
    {
        if (Mana >= 100)
        {
            int hasar = Guc + 25;
            hedef.Can -= hasar;
            Mana -= 100;
            Console.WriteLine($"{Ad}, Ulti attı {hedef.Ad}'a {hasar} vurdu.");
        }
        else
        {
            Console.WriteLine($"{Ad}, yeterli manan yok, Mananı yenile.");
        }
    }

    public void ManaYenile()
    {
        Mana += 150;
        Console.WriteLine($"{Ad}, 150 mana yeniledi.");
    }
}

public class Oyuncu : Karakter
{
    public Oyuncu(string ad) : base(ad, 150, 30, 150) { }
}

public class Dusman : Karakter
{
    public Dusman(string ad, int can, int guc, int mana) : base(ad, can, guc, mana) { }
}

public class Zombi : Dusman
{
    public Zombi() : base("Zombi", 100, 15, 50) { }
}

public class Goblin : Dusman
{
    public Goblin() : base("Goblin", 120, 20, 60) { }
}

public class Ejderha : Dusman
{
    public Ejderha() : base("Ejderha", 150, 40, 100) { }

    public override void Ulti(Karakter hedef)
    {
        if (Mana >= 100)
        {
            int hasar = Guc;
            hedef.Can -= hasar;
            Mana -= 100;
            Console.WriteLine($"{Ad}, Ejderha {hedef.Ad}'a {hasar} vurdu.");
        }
        else
        {
            Console.WriteLine($"{Ad}, yeterli manası yok.");
        }
    }
}
    public class Program
    {
        static void Main()
        {
            Console.Write("Oyuncu Adı: ");
            string oyuncuAdi = Console.ReadLine();
            Oyuncu oyuncu = new Oyuncu(oyuncuAdi);
            List<Dusman> dusmanListesi = new List<Dusman> { new Zombi(), new Goblin(), new Ejderha() };
            int dusmanIndex = 0;
            int skor = 0; 

            Dusman mevcutDusman = dusmanListesi[dusmanIndex];

            while (oyuncu.Can > 0)
            {
                Console.WriteLine($"\n{oyuncu.Ad} - Can: {oyuncu.Can}, Mana: {oyuncu.Mana}");
                Console.WriteLine($"{mevcutDusman.Ad} - Can: {mevcutDusman.Can}, Mana: {mevcutDusman.Mana}");
                Console.WriteLine("1- Saldır\n2- Ulti\n3- Mana Yenile");
                Console.Write("Seçim: ");
                string secim = Console.ReadLine();

                if (secim == "1")
                {
                    oyuncu.Saldir(mevcutDusman);
                }
                else if (secim == "2")
                {
                    oyuncu.Ulti(mevcutDusman);
                }
                else if (secim == "3")
                {
                    oyuncu.ManaYenile();
                }

                if (mevcutDusman.Can > 0)
                {
                    if (mevcutDusman.Mana >= 100)
                    {
                        mevcutDusman.Ulti(oyuncu);
                    }
                    else
                    {
                        mevcutDusman.Saldir(oyuncu);
                    }
                }
                else
                {
                    Console.WriteLine($"{mevcutDusman.Ad} öldü!");
                    skor += 100; 
                    dusmanIndex++;
                    if (dusmanIndex < dusmanListesi.Count)
                    {
                        mevcutDusman = dusmanListesi[dusmanIndex];
                        Console.WriteLine($"{mevcutDusman.Ad} geldi!");
                    }
                    else
                    {
                        Console.WriteLine("Tüm düşmanlar öldü, Kazandınız!");
                        break;
                    }
                }

                if (oyuncu.Can <= 0)
                {
                    Console.WriteLine($"{oyuncu.Ad} öldü! Oyun bitti.");
                }
            }

            Console.WriteLine($"\nToplam Saldırı Sayısı: {Karakter.ToplamSaldiri}");
            Console.WriteLine($"Toplam Skor: {skor}");
            Console.WriteLine("Oyun sona erdi.");
            Console.WriteLine(@"
               ____    _    __  __ _____    _____     _______ ____  
              / ___|  / \  |  \/  | ____|  / _ \ \   / / ____|  _ \ 
             | |  _  / _ \ | |\/| |  _|   | | | \ \ / /|  _| | |_) |
             | |_| |/ ___ \| |  | | |___  | |_| |\ V / | |___|  _ < 
              \____/_/   \_\_|  |_|_____|  \___/  \_/  |_____|_| \_\
");
    }
    }

