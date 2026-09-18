using System.Threading.Channels;

namespace Hydac_Project
{
    class Bruger
    {
        public string Brugernavn { get; }
        public string Kode { get; }
        public int Brugerid { get; }
        public Bruger(string brugernavn, string kode, int brugerid)
        {
            Brugernavn = brugernavn;
            Kode = kode;
            Brugerid = brugerid;
        }
    }
    class Gæster
    {
        public string Gæstenavn { get; }
        public string Gæstefirma { get; }
        public DateTime Gankomst { get; }
        public DateTime Gafgang { get; }
        public string SU { get; }
        public string Ansvarlige { get; }
        public int Adgangsnøgle { get; }
        public string Status { get; set; }
        public DateTime StatusTid { get; set; }
        public Gæster(string gæstenavn, string gæstefirma, DateTime gankomst, DateTime gafgang, string su, string ansvarlige, int adgangsnøgle, string status, DateTime statustid)
        {
            Gæstenavn = gæstenavn;
            Gæstefirma = gæstefirma;
            Gankomst = gankomst;
            Gafgang = gafgang;
            Ansvarlige = ansvarlige;
            SU = su;
            Adgangsnøgle = adgangsnøgle;
            Status = status;
            StatusTid = statustid;
        }
    }
    internal class Program
    {
        static List<Bruger> brugere = new List<Bruger>();
        static List<Gæster> gæstere = new List<Gæster>();
        static Bruger? loggedInd = null;
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("Velkommen til Hydac");
                while (true)
                {
                    Console.WriteLine("1 = Opret Bruger, 2 = Bruger List, 3 = Opret Gæst, 4 = Gæste List, 5 = Login, 6 = Tjek ind/ud");
                    if (loggedInd == null)
                        Console.Write("> ");
                    else
                        Console.Write($"{loggedInd.Brugernavn} > ");
                    string? valg = Console.ReadLine();
                    switch (valg?.ToUpper())
                    {
                        case "1":
                            brugere.Add(Opretbruger());
                            Console.Clear();
                            break;
                        case "2":
                            Brugerliste();
                            Console.WriteLine("Skriv Menu for at gå tilbage");
                            while (true)
                            {
                                if (Console.ReadLine() == "Menu")
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Denne commando virker ikke. Prøv igen.");
                                }
                            }
                            break;
                        case "3":
                            if (loggedInd == null)
                            {
                                Console.Clear();
                                Console.WriteLine("Du er ikke logged in.");
                            }
                            else
                            {

                                gæstere.Add(Opretgæst());
                                Console.Clear();
                            }
                            break;
                        case "4":
                            Gæsteliste();
                            Console.WriteLine("Skriv Menu for at gå tilbage");
                            while (true)
                            {
                                if (Console.ReadLine() == "Menu")
                                {
                                    Console.Clear();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Denne commando virker ikke. Prøv igen.");
                                }
                            }
                            break;
                        case "5":
                            Login();
                            break;
                        case "6":
                            Tjekstatus();
                            break;
                        default:
                            Console.Clear();
                            break;

                    }
                    break;
                }

            }
            static Bruger Opretbruger()
            {
                Console.Clear();
                Console.Write("Opret brugernavn: ");
                string brugernavn = Console.ReadLine() ?? "";

                Console.Write("Opret kode: ");
                string kode = Console.ReadLine() ?? "";
                int brugerid = 1;
                foreach (Bruger i in brugere)
                    brugerid++;
                Console.WriteLine("Din bruger er blevet oprettet.");
                return new Bruger(brugernavn, kode, brugerid);

            }
            static void Brugerliste()
            {
                Console.Clear();
                if (brugere.Count == 0)
                {
                    Console.WriteLine("Ingen brugere er oprettet");
                }
                else
                {
                    Console.WriteLine("Oprettet Brugere:");
                    foreach (Bruger i in brugere)
                        Console.WriteLine($"{i.Brugernavn} | {i.Brugerid}");
                }
            }
            static void Login()
            {
                Console.Clear();
                if (brugere.Count == 0)
                {
                    Console.WriteLine("Opret en bruger først");
                }
                else
                {
                    Console.Write("Brugernavn: ");
                    string brugernavn = Console.ReadLine() ?? "";

                    Console.Write("Kode: ");
                    string kode = Console.ReadLine() ?? "";

                    foreach (Bruger i in brugere)
                    {
                        if (i.Brugernavn == brugernavn && i.Kode == kode)
                        {
                            loggedInd = i;
                            Console.WriteLine($"Du er nu logget ind som {i.Brugernavn}");
                            Console.Clear();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"Denne bruger er ikke oprettet. Prøv igen.");
                            return;
                        }
                    }
                }
            }
            static Gæster Opretgæst()
            {
                    Console.Clear();
                    Console.Write("Gæste Navn: ");
                    string gæstenavn = Console.ReadLine() ?? "";

                    Console.Write("Gæste Firma: ");
                    string gæstefirma = Console.ReadLine() ?? "";

                    Console.Write("Ankomst Tid: ");
                    DateTime gankomst = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Afgangs Tid: ");
                    DateTime gafgang = Convert.ToDateTime(Console.ReadLine());

                    Console.Write("Sikkerhedsudstyr udleveret? Ja/Nej: ");
                    string su = Console.ReadLine() ?? "";

                    string ansvarlige = loggedInd.Brugernavn;
                    Random rnd = new Random();
                    int adgangsnøgle = rnd.Next(10_000_000, 100_000_000);
                    string status = "Ikke Ankommet";
                    DateTime statustid = Convert.ToDateTime(null);
                    

                    Console.WriteLine("Din Gæst er oprettet.");
                    return new Gæster(gæstenavn, gæstefirma, gankomst, gafgang, su, ansvarlige,adgangsnøgle, status, statustid);

                }
            }
            
            static void Tjekstatus()
        {
            Console.Clear();
            if(gæstere.Count == 0)
            {
                Console.WriteLine("Ingen gæster er oprettet for planlagt besøg.");
            }
            else
            {
                Console.Write("Indsæt adgangsnøglen for at registrer ankomst: ");
                if (!int.TryParse(Console.ReadLine(), out int adgangsnøgle))
                {
                    Console.Clear();
                    Console.WriteLine("Det var ikke et gyldigt tal.");
                    return;
                }
                foreach (Gæster g in gæstere)
                {
                    if(g.Adgangsnøgle == adgangsnøgle && g.Status == "Ikke Ankommet")
                    {
                        Console.Clear();
                        g.Status = "Ankommet";
                        g.StatusTid = DateTime.Now;
                        Console.WriteLine($"{g.Gæstenavn} er tjekket ind.");

                    }
                    else if (g.Adgangsnøgle == adgangsnøgle && g.Status == "Ankommet")
                    {
                        Console.Write("Er du sikker på at du vil tjekke ud? ja/nej: ");
                        string svar = Console.ReadLine().ToLower();
                        if(svar == "ja")
                        {
                            Console.Clear();
                            g.Status = "Tjekket ud";
                            g.StatusTid = DateTime.Now;
                            Console.WriteLine($"{g.Gæstenavn} er tjekket ud.");
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Denne adgangsnøgle var ugyldig.");
                    }
                }
            }
        }

            static void Gæsteliste()
            {
                Console.Clear();
                if (gæstere.Count == 0)
                {
                    Console.WriteLine("Ingen gæstere er oprettet");
                }
                else
                {
                    Console.WriteLine("----- Gæste Liste -----");
                    foreach (Gæster i in gæstere)
                        Console.WriteLine($"{i.Gæstenavn} | {i.Gæstefirma} | {i.Gankomst:HH:mm} | {i.Gafgang:HH:mm} | {i.Gankomst:dd-MM-yyyy} | {i.SU} | {i.Ansvarlige} | {i.Adgangsnøgle} | {i.Status} - {i.StatusTid:HH:mm}");
                }
            }
        }
    }

