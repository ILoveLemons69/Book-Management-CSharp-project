using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    internal abstract class Storage
    {
        public abstract Task SaveBooks(
            SortedList<int, Books> books);

        public abstract Task<SortedList<int, Books>> LoadBooks();

        public abstract Task<bool> DeleteBook(
            SortedList<int, Books> books, int id);

        public abstract Task ClearFile();

        public abstract Task<bool> UpdateBook(
            SortedList<int, Books> books,
            int id,
            string? title,
            string? authorName,
            DateTime? releaseDate);
    }
}
