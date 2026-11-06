namespace Zombihaboru
{
    internal class Program
    {
        static Random r = new Random();
        static string[] NevekLetrehozasa(int db)
        {
            string[] nevekOsszes = { "Jávorszky", "Ádám", "Bence", "Csaba", "Dávid", "Erik", "Ferenc", "Gábor", "Hunor", "István", "János", "Károly", "Levente", "Márk", "Norbert", "Péter", "Richárd", "Sándor", "Tamás", "Viktor", "Zoltán", "Anna", "Barbara", "Csilla", "Dóra", "Eszter", "Fanni", "Hanna", "Kata", "Lilla", "Zsófia" };
            string[] nevek = new string[db];
            for (int i = 0; i < db; i++)
            {
                int index = r.Next(0, nevekOsszes.Length);
                nevek[i] = nevekOsszes[index];
            }
            return nevek;
        }
        static void Kiir(string[] nevek)
        {
            foreach (var item in nevek)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
        static int[] EleteroBeallitasa(int lakossagSzama)
        {
            int[] eleterok = new int[lakossagSzama];
            for (int i = 0; i < lakossagSzama; i++)
            {
                eleterok[i] = 100;
            }
            return eleterok;
        }
        static int[] TamadoEroBeallitasa(int zombikSzama)
        {
            int[] tamadoErok = new int[zombikSzama];
            for (int i = 0; i < zombikSzama; i++)
            {
                tamadoErok[i] = r.Next(10, 25);
            }
            return tamadoErok;
        }
        static void KiirSzamok(int[] tomb)
        {
            foreach (var item in tomb)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        static bool VanEEloLakos(int[] eleterok)
        {
            foreach (var item in eleterok)
            {
                if (item > 0)
                {
                    return true;
                }
            }
            return false;
        }
        static bool VanEEloZombi(int[] tamadoErok)
        {
            foreach (var item in tamadoErok)
            {
                if (item > 0)
                {
                    return true;
                }
            }
            return false;
        }
        static void Jatek(int[] lakossagEletereje, int[] zombikTamadoEreje, string[] lakossag, string[] zombik)
        {
            int korSzama = 1;
            while (VanEEloLakos(lakossagEletereje) && VanEEloZombi(zombikTamadoEreje))
            {
                Console.WriteLine($"\n{korSzama}. Támadás\n");
                int i = 0;
                int aktualisEro;
                int zombiEleteroVeszteseg = r.Next(1, 6);
                foreach (var item in zombik)
                {
                    int tamadoEro = zombikTamadoEreje[i];
                    while (tamadoEro > 0 && VanEEloLakos(lakossagEletereje))
                    {

                        int lakosIndex = r.Next(0, lakossagEletereje.Length);
                        aktualisEro = tamadoEro;
                        if (lakossagEletereje[lakosIndex] > 0)
                        {
                            lakossagEletereje[lakosIndex] -= tamadoEro;
                            tamadoEro = 0;
                            if (lakossagEletereje[lakosIndex] < 0)
                            {
                                tamadoEro -= lakossagEletereje[lakosIndex];
                                lakossagEletereje[lakosIndex] = 0;
                            }
                            Console.WriteLine($"\n{item} megtámadta ({aktualisEro}) {lakossag[lakosIndex]}-t, aki most {lakossagEletereje[lakosIndex]} életerővel rendelkezik.");
                            if (lakossagEletereje[lakosIndex] == 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine($"\n{lakossag[lakosIndex]} meghalt.");
                                Console.ResetColor();
                            }

                        }
                    }
                    i++;

                }

                while (zombiEleteroVeszteseg > 0)
                {
                    int randomZombiIndex = r.Next(0, zombikTamadoEreje.Length);
                    if (zombikTamadoEreje[randomZombiIndex] > 0)
                    {
                        zombikTamadoEreje[randomZombiIndex] -= zombiEleteroVeszteseg;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"\n{zombik[randomZombiIndex]} (zombi) elvesztett {zombiEleteroVeszteseg} életerőt.");
                        if (zombikTamadoEreje[randomZombiIndex] <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine($"\n{zombik[randomZombiIndex]} (zombi) meghalt.");

                        }
                        zombiEleteroVeszteseg = 0;
                    }
                }
                korSzama++;
                Console.ResetColor();
            }
            if (VanEEloLakos(lakossagEletereje))
            {
                Console.WriteLine("\nA lakosság győzött!");
            }
            else
            {
                Console.WriteLine("\nA zombik győztek!");
            }
        }

        static void Main(string[] args)
        {
            int lakossagSzama = 5;
            int zombikSzama = 3;
            string[] lakossag = NevekLetrehozasa(lakossagSzama);
            string[] zombik = NevekLetrehozasa(zombikSzama);
            Console.Write("Lakosság: ");
            Kiir(lakossag);
            Console.Write("Zombik: ");
            Kiir(zombik);
            int[] lakossagEletereje = EleteroBeallitasa(lakossagSzama);
            int[] zombikTamadoEreje = TamadoEroBeallitasa(zombikSzama);
            Console.Write("Lakosság eletereje: ");
            KiirSzamok(lakossagEletereje);
            Console.Write("Zombik tamadoereje: ");
            KiirSzamok(zombikTamadoEreje);
            Jatek(lakossagEletereje, zombikTamadoEreje, lakossag, zombik);
        }


    }
}
