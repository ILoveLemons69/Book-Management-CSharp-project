using ConsoleApp1.Models;
using System.Text.Json;

namespace ConsoleApp1.Helpers
{
    internal class JsonHelper : Storage
    {
        private static readonly string Path = BookManager.ProjectPath;

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };
        // აქ ხდება ფაილში წიგნების შენახვა
        public override async Task SaveBooks(SortedList<int, Books> books)
        {
            string json = JsonSerializer.Serialize(books, Options);

            await File.WriteAllTextAsync(BookManager.ProjectPath, json);
        }

        // აქ ხდება წიგნების გამოჩენა კონსოლში
        public override async Task<SortedList<int, Books>> LoadBooks()
        {
            if (!File.Exists(Path))
            {
                return new SortedList<int, Books>();
            }

            string json = await File.ReadAllTextAsync(Path);

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SortedList<int, Books>();
            }

            SortedList<int, Books> books = JsonSerializer.Deserialize<SortedList<int, Books>>(json, Options)
                ?? new SortedList<int, Books>();

            // ამ ხაზით ჩვენ შეგვიძლია დარწმუნებული ვიყოთ რომ ობიექტების Id მახასიათებლები არ გამეორდებიან
            Books.IdCounter = books.Count > 0 ? books.Keys.Max() : 0;

            return books;
        }

        // აქ ხდება ერთი არჩეული წიგნის წაშლა
        public override async Task<bool> DeleteBook(
            SortedList<int, Books> books, int id)
        {
            if (!books.Remove(id))
            {
                return false;
            }

            await SaveBooks(books);

            return true;
        }

        // აქ ხდება მთლიანი ფაილის გასუფთავება
        public override async Task<bool> ClearFile()
        {
            await File.WriteAllTextAsync(Path, string.Empty);
            return true;
        }

        // აქ ხდება ფაილში ერთი წიგნის ობიექტის განახლება
        public override async Task<bool> UpdateBook(SortedList<int, Books> books, int id, string? title,
            string? authorName, DateTime? releaseDate)
        {
            if (!books.TryGetValue(id, out Books? book))
            {
                return false;
            }

            if (title != null)
                book.Title = title;

            if (authorName != null)
                book.AuthorName = authorName;

            if (releaseDate.HasValue)
                book.ReleaseDate = releaseDate.Value;

            await SaveBooks(books);

            return true;
        }
    }
}
