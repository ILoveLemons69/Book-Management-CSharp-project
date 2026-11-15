using System.Globalization;

namespace ConsoleApp1.Models
{
    internal static class BookManager
    {

        // BooksList-ი ინახავს ყველა Books-ის ობიექტს
        public static SortedList<int, Books> BooksList = new SortedList<int, Books>();
        public static string Path = "C:\\Users\\student\\Downloads\\Book-Management-CSharp-project-main\\ConsoleApp1\\ConsoleApp1\\Saved Files\\SavedBooks.txt";

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
        public async static Task DeleteBooks(int lineIndexToRemove)
        {
            try
            {
                var lines = File.ReadAllLines(Path).ToList();

                if (lines.Count > lineIndexToRemove - 1)
                {
                    lines.RemoveAt(lineIndexToRemove - 1);

                    File.WriteAllLines(Path, lines);

                    Console.WriteLine($"\n[{BooksList[lineIndexToRemove].Id}] {BooksList[lineIndexToRemove].Title} - {BooksList[lineIndexToRemove].AuthorName}, released in {BooksList[lineIndexToRemove].ReleaseDate.ToString("yyyy/MM/dd")}");
                    BooksList.Clear();
                    Books.IdCounter = 0;
                    await ReadFromFile();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nThe chosen BOOK was DELETED Successfully\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nThe file does not contain what you are trying to delete.\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            catch (IOException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred while handling the file: {ex.Message}");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        //file-ის წამკითხველი გამოიყენება ამ კლასში დამხმარე მეთოდად რომ გავასწოროდ თან მიმდევრობა და აიდი წიგნების წაშლისას.
        public async static Task ReadFromFile()
        {
            string[] lines = File.ReadAllLines(Path);

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
    }
}
