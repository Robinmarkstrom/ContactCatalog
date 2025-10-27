# ContactCatalog

En enkel kontaktkatalog som hanterar Id, namn, e-postadress och taggar med hjälp av en konsolmeny.

# Körning

1. Startar projektet i visual Studio
2. Menyval 1 ger dig tillgång att lägga in en kontakt i katalogen genom att skriva in Id, namn, e-postadress och taggar.
3. Menyval 2 listar alla kontakar i kontaktkatalogen.
4. Menyval 3 ger dig tillgång att söka efter en kontakt i katalogen utifrån ett namn (delsträng matching).
5. Menyval 4 ger till tillgång att filtrera kontaker i katalogen efter taggar.
6. Menyval 5 exporterar alla kontakter till CSV.
7. För att importera fil från CSV använd detta kommand i terminalen. dotnet run -- --import path/to/contacts.csv.

# Designval
Dictionary<int, Contact> används för att en snabb uppslagning av kontakter då den använder unika id.
HashSet<string> säkerställer att inga det inte finns några dubbletter av e-postadresser.
LINQ Where och OrderBy används för en effektiv och läsbar kod för filtrering och sökning, det gör den även väldigt lättunderhållen.
Finns egna untantag mot ogiltiga e-postadresser samt dubbletter.
