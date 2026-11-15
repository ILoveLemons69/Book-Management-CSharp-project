using ConsoleApp1.Models;
using System.Globalization;

string[] lines = File.ReadAllLines(BookManager.Path);
// ამით პროგრამის გაშვებისას SavedBooks.txt-იდან ვიღებთ წიგნებს და ვამატებთ BooksList-ში
foreach (string line in lines)
{
    int startIndex = line.IndexOf('"') + 1;
    int endIndex = line.IndexOf('"', startIndex);
    string title = line.Substring(startIndex, endIndex - startIndex);

    startIndex = line.IndexOf('"', endIndex + 1) + 1;
    endIndex = line.IndexOf('"', startIndex);
    string author = line.Substring(startIndex, endIndex - startIndex);

    startIndex = line.IndexOf('"', endIndex + 1) + 1;
    endIndex = line.IndexOf('"', startIndex);
    string dateString = line.Substring(startIndex, endIndex - startIndex);

    DateTime date = DateTime.ParseExact(dateString, "yyyy/MM/dd", CultureInfo.InvariantCulture);

    Books book = new Books(title, author, date);
}

// ამით მომხმარებელს შეუძლია შეწყვიტოს ან გააგრძელოს აპლიკაციის გამოყენება
bool RunAgain()
{
    Console.Write("\nDo you wish to run the Program again? Y/N \n\n");
    bool input = Console.ReadLine().Contains("y", StringComparison.OrdinalIgnoreCase);

    if (input)
    {
        return true;
    }
    else
    {
        return false;
    }

}

do
{
    // აქ მომხმარებელს ვაძლევთ არჩევანს
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine("\nWhat would you like to do?");
    Console.Write(
        "\n[1] Add a new BOOK to the LIST" +
        "\n[2] See all BOOKs on the LIST" +
        "\n[3] Search for a BOOK on the LIST" +
        "\n[4] Delete a BOOK from the List" +
        "\n[5] exit the PROGRAM " +
        "\n \n");
    Console.ForegroundColor = ConsoleColor.White;

    Int32.TryParse(Console.ReadLine(), out int value);

    if (value == 1)
    {
        // აქ მომხმარებელს შეუძლია შექმნას ობიექტი და შეინახოს იგი
        Console.WriteLine("\nenter the TITLE of the BOOK \n");
        string title = Console.ReadLine();

        Console.WriteLine("\nenter the AUTHOR of the BOOK \n");
        string author = Console.ReadLine();

        Console.WriteLine("\nenter the RELEASE DATE (YYYY/MM/DD) of the BOOK \n");
        string releasedate = Console.ReadLine();

        if (title != null && author != null && releasedate != null)
        {
            DateTime date = DateTime.ParseExact(releasedate, "yyyy/MM/dd", CultureInfo.InvariantCulture);

            Books book = new Books(title, author, date, true);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(book.ToString());
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    if (value == 2)
    {
        // აქ მომხმარებელს შეუძლია ნახოს ყველა წიგნი
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("the LIST of all BOOKS: \n");
        BookManager.ShowBooks();
        Console.ForegroundColor = ConsoleColor.White;
    }

    if (value == 3)
    {
        // აქ მომხმარებელს შეუძლია მოძებნოს წიგნი მისი სახელით
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nenter your BOOK's TITLE: ");
        string title = Console.ReadLine();
        Console.ForegroundColor = ConsoleColor.White;

        if (title != null && title.Trim() != "")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            BookManager.SearchBooksByName(title.Trim());
            Console.ForegroundColor = ConsoleColor.White;
        }
        else if (title.Trim() == "" || title == null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("BOOKs not found");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    if (value == 4)
    {
        // აქ მომხმარებელს შეუძლია მოძებნოს და წაშალოს ობიექტი ლისტიდან და შენახული ფაილიდან
        Console.WriteLine("the LIST of all BOOKS: \n");
        BookManager.ShowBooks();
        Console.WriteLine("\nWhich BOOK do you want to DELETE?\n");
        Int32.TryParse(Console.ReadLine(), out int linetoremove);

        BookManager.DeleteBooks(linetoremove);

    }

    if (value == 5)
    {
        // აქ მომხმარებელს შეუძლია გათიშოს აპლიკაცია
        Console.ForegroundColor = ConsoleColor.Red;
        System.Environment.Exit(0);

    }

    if (value != 1 && value != 2 && value != 3 && value != 4 && value != 5)
    {
        // აქ აპლიკაცია თავს ითიშავს თუ არასწორი input-ი გადაეცა მას
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\nChosen operation doesn't exist");
        System.Environment.Exit(0);
    }

} while (RunAgain());
// აქ თუ რამენაირად გავცდით do { } while ()-ს მაშინ აპლიკაცია გაითიშება
Console.ForegroundColor = ConsoleColor.Red;
System.Environment.Exit(0);