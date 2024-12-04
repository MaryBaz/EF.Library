using EF.Library.Model.Entities;
using EF.Library.Model.Repositories;
using EF.Library.PLL;

namespace EF.Library
{
    internal class Program
    {
        public static MainView mainView;
        public static UserInfoView userInfoView;
        public static BookInfoView bookInfoView;
        public static GenreInfoView genreInfoView;
        public static AuthorInfoView authorInfoView;

        static void Main(string[] args)
        {
            mainView = new MainView();
            userInfoView = new UserInfoView();
            bookInfoView = new BookInfoView();
            genreInfoView = new GenreInfoView();
            authorInfoView = new AuthorInfoView();

            mainView.Show();

            ////////////////////////////////
            return;

            //UserRepository userRepository = new UserRepository();
            using (var db = new AppContext())
            {
                //var users = userRepository.SelectAll().ToList();
                var users = (from user in db.Users
                             select user);
                foreach (User user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
            //PrepareData();
        }
        public static void PrepareData()
        {
            using (var db = new AppContext())
            {
                User jane = new User() { Name = "Jane" };
                User neo = new User() { Name = "Neo" };

                db.Users.AddRange(jane, neo);
                db.SaveChanges();

                Author dilts = new Author() { Name = "Robert Dilts" };
                Author grinder = new Author() { Name = "John Grinder" };
                Author ginzburg = new Author() { Name = "Ginzburg M.R." };
                Author toelsen = new Author() { Name = "Toelsen" };
                Author shevchuk = new Author() { Name = "Alex Shevchuk" };
                Author bakirov = new Author() { Name = "Anvar Bakirov" };

                Genre hlp = new Genre() { Name = "nlp practic" };
                Genre hipnosys = new Genre() { Name = "hipnos" };
                Genre programming = new Genre() { Name = "programming" };

                Book hipnosysCourse = new Book() { Name = "Hipnosys Course", PublishYear = 2010 };
                Book nlpCourse = new Book() { Name = "Language Tricks", PublishYear = 1999 };
                Book cs_docs = new Book() { Name = ".Net 7.0", PublishYear = 2020 };
                Book manipulation = new Book() { Name = "Manipulation", PublishYear = 2008 };
                Book cs_patterns = new Book() { Name = "CS Patterns", PublishYear = 2015 };
                Book conversationHipnosys = new Book() { Name = "Conversational Hypnosys", PublishYear = 2007 };

                hipnosysCourse.Authors = new();
                hipnosysCourse.Authors.Add(ginzburg);
                nlpCourse.Authors = new List<Author>();
                nlpCourse.Authors.AddRange(new[] { dilts, grinder });
                cs_docs.Authors = new List<Author>();
                cs_docs.Authors.AddRange(new[] { toelsen, shevchuk });
                manipulation.Authors = new List<Author>();
                manipulation.Authors.Add(bakirov);

                //cs_patterns.Author.Add(shevchuk);
                shevchuk.Books = new List<Book>();
                shevchuk.Books.AddRange(new[] { cs_patterns, cs_docs });
                bakirov.Books = new List<Book>();
                bakirov.Books.AddRange(new[] { conversationHipnosys, manipulation });
                //conversationHipnosys.Authors.Add(bakirov);

                hipnosysCourse.Genre = hipnosys;
                nlpCourse.Genre = hlp;
                cs_docs.Genre = programming;

                db.Authors.AddRange(dilts, ginzburg, toelsen, bakirov, grinder, shevchuk);
                db.Genres.AddRange(hlp, hipnosys, programming);
                db.Books.AddRange(hipnosysCourse, nlpCourse, cs_docs, manipulation, cs_patterns, conversationHipnosys);

                db.SaveChanges();

                jane.books.Add(nlpCourse);
                jane.books.Add(hipnosysCourse);
                neo.books.Add(cs_docs);
                neo.books.Add(manipulation);

                db.SaveChanges();
            }
        }

    }
}
