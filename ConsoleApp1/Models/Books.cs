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
        // კონსტრუქტორი რომელიც ქმნის და ამატებს ობიექტს შესანახ ფაილში
        public Books(string title, string authorName, DateTime releaseDate, bool saving)
        {
            IdCounter++;
            Id = IdCounter;
            Title = title;
            AuthorName = authorName;
            ReleaseDate = releaseDate;

            BookManager.BooksList.Add(Id, this);

            if (saving)
            {
                using (StreamWriter writer = new StreamWriter("C:\\Users\\PC\\Desktop\\ConsoleApp1\\ConsoleApp1\\Saved Files\\SavedBooks.txt", true))
                {
                    writer.WriteLine($"{'"' + title + '"'}, {'"' + authorName + '"'}, {'"' + releaseDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) + '"'}");
                }
            }
        }

        // ობიექტის ინფორმაციის სტრინგი
        public override string ToString()
        {
            return $"\n[{Id}] {Title} - {AuthorName}, released in {ReleaseDate.ToString("yyyy/MM/dd")}\n";
        }
    }
}
