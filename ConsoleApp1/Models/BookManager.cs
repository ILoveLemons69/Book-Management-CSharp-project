using ConsoleApp1.Helpers;
using System.Globalization;

namespace ConsoleApp1.Models
{
    internal static class BookManager
    {

        // BooksList-ი ინახავს ყველა Books-ის ობიექტს
        public static SortedList<int, Books> BooksList = new SortedList<int, Books>();
        public static JsonHelper jsonHelper = new JsonHelper();
        public static string ProjectPath = System.IO.Path.Combine(
            AppContext.BaseDirectory,
            "..", "..", "..",
            "Saved Files",
            "SavedBooks.json"
        );

        // კონსოლში გამოაქვს ყველა Books.ToString-ი
        public static void ShowBooks()
        {
            foreach (var book in BooksList.Values)
            {
                if (book != null)
                {
                    Console.WriteLine($"[{book.Id}] {book.Title} - {book.AuthorName}, released in {book.ReleaseDate.ToString("yyyy/MM/dd")}");
                }
                else
                {
                    Console.WriteLine("There are no books to display!");
                }
            }
        }



        // ამით მომხმარებელი პოულობს წიგნების ობიექტებს მათი სახელებით
        public static void SearchBooksByName(string value)
        {
            var matchedBooks = BooksList.Values
                .Where(book => book.Title.Contains(value, StringComparison.OrdinalIgnoreCase));
            if (matchedBooks != null)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLIST of the found BOOKs");
                Console.ForegroundColor = ConsoleColor.White;
                int i = 0;
                foreach (var book in matchedBooks)
                {
                    Console.WriteLine(book.Title);
                    i++;
                }
                if (i == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("No Books were found!");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.WriteLine("No Books were found!");
            }
        }


        // ამით მომხმარებელი შლის ობიექტებს BooksList-იდან და SavedBooks.txt-იდან

        public static async Task DeleteBooks(int id)
        {
            bool deleted = await jsonHelper.DeleteBook(BooksList, id);

            if (deleted)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nBook deleted successfully!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBook with this ID was not found!\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

        //file-ის წამკითხველი გამოიყენება ამ კლასში დამხმარე მეთოდად რომ გავასწოროდ თან მიმდევრობა და აიდი წიგნების წაშლისას.
        public async static Task ReadFromFile()
        {
            string[] lines = File.ReadAllLines(ProjectPath);

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
        }

        public static async Task ClearBooks()
        {
            await jsonHelper.ClearFile();

            BooksList.Clear();

            Books.IdCounter = 0;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nAll books have been deleted successfully!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static async Task UpdateBook(int id, string? title,
            string? authorName, DateTime? releaseDate)
        {
            bool updated = await jsonHelper.UpdateBook(BooksList, id, title,
                authorName, releaseDate);

            if (updated)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nBook updated successfully!\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBook with this ID was not found!\n");
            }

            Console.ForegroundColor = ConsoleColor.White;
        }

    }
}
