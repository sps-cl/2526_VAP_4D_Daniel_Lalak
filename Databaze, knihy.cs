// See https://aka.ms/new-console-template for more information
using EF_DatabaseApp1.Models;
using Microsoft.EntityFrameworkCore;

using (KnihovnaDbContext context = new KnihovnaDbContext())
{

    Console.WriteLine("Autori");
    Console.WriteLine(" ");
    foreach (var z in context.Autoris)
    {
        Console.WriteLine($"{z.Id}" +
            $" {z.Jmeno}" +
            $" {z.Prijmeni}"
            );
    }

    Console.WriteLine(" ");
    Console.WriteLine(" ");
    Console.WriteLine("Knihy");
    Console.WriteLine(" ");
    foreach (var z in context.Knihies)
    {
        Console.WriteLine($"{z.Id}" +
            $" /Nazev: {z.Nazev}/" +
            $"  /Rok vydáni: {z.RokVydani}/" +
            $" /Zanr: {z.Zanr}/"
            );
    }

    Console.WriteLine(" ");
    Console.WriteLine(" ");
    Console.WriteLine("Recenzenti");
    Console.WriteLine(" ");
    foreach (var z in context.Recenzentis)
    {
        Console.WriteLine($"{z.Id}" +
            $" {z.UzivatelskeJmeno}" 
            );
    }
    Console.WriteLine(" ");
    Console.WriteLine(" ");
    Console.WriteLine("Hodnoceni");
    Console.WriteLine(" ");
    foreach (var z in context.Recenzes)
    {
        Console.WriteLine($"{z.Id}" +
            $" {z.Hodnoceni}"
            );
    }

    Console.WriteLine(" ");
    Console.Write("Zadej ID autora pro zobrazení jeho knih: ");
    int autorId = int.Parse(Console.ReadLine()!);

    var knihyAutora = context.Knihies
        .Where(k => k.Id == autorId);

    Console.WriteLine("Knihy vybraného autora:");
    foreach (var k in knihyAutora)
    {
        Console.WriteLine($"{k.Id} /Nazev: {k.Nazev}/  /Rok vydani: {k.RokVydani}/  /Zanr: {k.Zanr}/");
    }

    Console.WriteLine("Vložte jméno nového autora:");
    string noveJmeno = Console.ReadLine();

    Console.WriteLine("Vložte příjmení nového autora:");
    string novePrijmeni = Console.ReadLine();
    if (string.IsNullOrEmpty(noveJmeno) || string.IsNullOrEmpty(novePrijmeni))
    {
        return;
    }
    Autori novyAutor = new Autori { Jmeno = noveJmeno, Prijmeni = novePrijmeni };
    
    context.Autoris.Add(novyAutor);
    context.SaveChanges();
    Console.WriteLine("Nový autor byl přidán.");

    Console.WriteLine("Vložte název nové knihy:");
    string novyNazev = Console.ReadLine();

    Console.WriteLine("Vložte rok vydání nové knihy:");
    int novyRokVydani = int.Parse(Console.ReadLine());

    Console.WriteLine("Vložte žánr nové knihy:");
    string novyZanr = Console.ReadLine();

    if (string.IsNullOrEmpty(novyNazev) || string.IsNullOrEmpty(novyZanr))
    {
        return;
    }
    int autorId = novyAutor.Id;

    Knihy novaKniha = new Knihy
    {
        Nazev = novyNazev,
        RokVydani = novyRokVydani,
        Zanr = novyZanr,
        AutorId = autorId
    };
    context.Knihies.Add(novaKniha);
    context.SaveChanges();
    Console.WriteLine("Nová kniha byla přidána.");

    Console.WriteLine("Vložte uživatelské jméno nového recenzenta:");
    string noveUzivatelskeJmeno = Console.ReadLine();

    Recenzenti novyRecenzent = new Recenzenti { UzivatelskeJmeno = noveUzivatelskeJmeno };
    context.Recenzentis.Add(novyRecenzent);
    context.SaveChanges();
    Console.WriteLine("Nový recenzent byl přidán.");
    Console.WriteLine("Vložte hodnocení (číslo) pro novou recenzi:");
    int noveHodnoceni = int.Parse(Console.ReadLine());
    int recenzentId = novyRecenzent.Id;
    int knihaId = novaKniha.Id;

    Recenze novaRecenze = new Recenze
    {
        Hodnoceni = noveHodnoceni,
        KnihaId = knihaId,
        RecenzentId = recenzentId
    };
    context.Recenzes.Add(novaRecenze);
    context.SaveChanges();
    Console.WriteLine("Nová recenze byla přidána.");
}
}
