using ConsoleApp1.Helpers;
using ConsoleApp1.Models;
using System.Globalization;

//"Crime and Punishment (For Your Cat)", "Fyodor Dostoevsky", "2023/07/05"
//"One Million Digits Of Pi", "Socrates Co.", "2020/04/30"
//"mein kampf", "Adolf Hitler", "1924/12/30"
//"Minecraft: The Complete Handbook Collection", "Mojang", "2014/01/01"
//"Berserk deluxe edition", "Kentaro Miura", "2023/11/21"

JsonHelper jsonHelper = new JsonHelper();

BookManager.BooksList = await jsonHelper.LoadBooks();

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
        "\n[5] Clear ALL BOOKS" +
        "\n[6] CHANGE a BOOK" +
        "\n[7] exit the PROGRAM " +
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

            Books book = new Books(title, author, date);

            await jsonHelper.SaveBooks(BookManager.BooksList);

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
        // აქ წიგნების წაშლა შეგვიძლია
        Console.WriteLine("The LIST of all BOOKS:\n");

        BookManager.ShowBooks();

        Console.WriteLine("\nWhich BOOK do you want to DELETE?\n");

        Int32.TryParse(Console.ReadLine(), out int id);

        await BookManager.DeleteBooks(id);
    }

    if (value == 5)
    {
        // აქ მთლიანი ლისტი სუფთავდება
        await BookManager.ClearBooks();
    }

    if (value == 6)
    {
        // აქ შეგვიძლია წიგნების განახლება
        BookManager.ShowBooks();

        Console.WriteLine("\nWhich BOOK do you want to UPDATE?");
        Int32.TryParse(Console.ReadLine(), out int id);

        if (!BookManager.BooksList.TryGetValue(id, out Books? book))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nBook not found!");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            Console.WriteLine($"\nCurrent title: {book.Title}");
            Console.WriteLine("Enter NEW TITLE (or 'default' to keep it):");
            string? titleInput = Console.ReadLine();

            string? title = null;

            if (!string.IsNullOrWhiteSpace(titleInput) &&
                !titleInput.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                title = titleInput;
            }

            Console.WriteLine($"\nCurrent author: {book.AuthorName}");
            Console.WriteLine("Enter NEW AUTHOR (or 'default' to keep it):");
            string? authorInput = Console.ReadLine();

            string? author = null;

            if (!string.IsNullOrWhiteSpace(authorInput) &&
                !authorInput.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                author = authorInput;
            }

            Console.WriteLine($"\nCurrent release date: {book.ReleaseDate:yyyy/MM/dd}");
            Console.WriteLine("Enter NEW RELEASE DATE (YYYY/MM/DD or 'default' to keep it):");
            string? dateInput = Console.ReadLine();

            DateTime? date = null;

            if (!string.IsNullOrWhiteSpace(dateInput) &&
                !dateInput.Equals("default", StringComparison.OrdinalIgnoreCase))
            {
                if (DateTime.TryParseExact(
                    dateInput,
                    "yyyy/MM/dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime parsedDate))
                {
                    if (parsedDate.Date <= DateTime.Today)
                    {
                        date = parsedDate;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Future dates are not allowed! Keeping the original date.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid date! Keeping the original date.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            await BookManager.UpdateBook(id, title, author, date);
        }
    }

    if (value == 7)
    {
        // აქ მომხმარებელს შეუძლია გათიშოს აპლიკაცია
        Console.ForegroundColor = ConsoleColor.Red;
        System.Environment.Exit(0);

    }

    if (value != 1 && value != 2 && value != 3 && value != 4 && value != 5 && value != 6 && value != 7)
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