using System.Globalization;

namespace ConsoleApp1.Models
{
    internal class Books
    {
        // Books-ის მახასიათებლები

        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public string Title { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public static int IdCounter { get; set; } = 0;
        public bool Saving { get; set; } = true;

        public Books()
        {

        }
        // კონსტრუქტორი რომელიც მოხოლოდ ქმნის ობიექტს
        public Books(string title, string authorName, DateTime releaseDate)
        {
            IdCounter++;
            Id = IdCounter;

            Title = title;
            AuthorName = authorName;
            ReleaseDate = releaseDate;

            BookManager.BooksList.Add(Id, this);
        }

        // ობიექტის ინფორმაციის სტრინგი
        public override string ToString()
        {
            return $"\n[{Id}] {Title} - {AuthorName}, released in {ReleaseDate.ToString("yyyy/MM/dd")}\n";
        }
    }
}
