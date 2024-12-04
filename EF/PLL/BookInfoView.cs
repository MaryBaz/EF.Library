using EF.Library.Model.Entities;
using EF.Library.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.PLL
{
    public class BookInfoView
    {
        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Выберете действие:");
                Console.WriteLine("1. Все книги.");
                Console.WriteLine("2. Поиск книги по Id");
                Console.WriteLine("3. Список книг определенного жанра и вышедших между определенными годами.");
                Console.WriteLine("4. Количество книг определенного автора в библиотеке.");
                Console.WriteLine("5. Количество книг определенного жанра в библиотеке.");
                Console.WriteLine("6. Проверка наличия книги определенного автора и с определенным названием в библиотеке.");
                Console.WriteLine("7. Проверка наличия книги на руках у пользователя.");
                Console.WriteLine("8. Количество книг на руках у пользователя.");
                Console.WriteLine("9. Последняя вышедшая книга.");
                Console.WriteLine("10. Список всех книг, отсортированного в алфавитном порядке по названию.");
                Console.WriteLine("11. Список всех книг, отсортированного в порядке убывания года их выхода.");
                Console.WriteLine("12. Обновление года публикации книги.");
                Console.WriteLine("0. Выйти из меню.");

                switch (Console.ReadLine())
                {
                    case "1":
                        {   ///Правильно
                            Program.bookInfoView.Show();
                            break;
                        }
                    case "2":
                        {   ///Правильно
                            Program.bookInfoView.ShowBook();
                            break;
                        }
                    case "3":
                        {   ///Правильно
                            Program.bookInfoView.GetBooksByGenreYears();
                            break;
                        }
                    case "4":
                        {   ///Правильно
                            Program.bookInfoView.GetBooksByAuthor();
                            break;
                        }
                    case "5":
                        {   ///Правильно
                            Program.bookInfoView.GetBooksByGenre();
                            break;
                        }
                    case "6":
                        {   ///Правильно
                            Program.bookInfoView.BookAuthorIsInLibrary();
                            break;
                        }
                    case "7":
                        {   ///Правильно
                            Program.bookInfoView.BookIsOnUser();
                            break;
                        }
                    case "8":
                        {   ///Правильно
                            Program.bookInfoView.UserBooks();
                            break;
                        }
                    case "9":
                        {   ///Правильно
                            Program.bookInfoView.LastPublishedBook();
                            break;
                        }
                    case "10":
                        {   ///Правильно
                            Program.bookInfoView.SelectAllOrderedByName();
                            break;
                        }
                    case "11":
                        {   ///Правильно
                            Program.bookInfoView.SelectAllOrderedDescByPublishYear();
                            break;
                        }
                    case "12":
                        {   ///Правильно
                            Program.bookInfoView.UpdateYearByBookId();
                            break;
                        }
                    case "0":
                        {
                            exitMenu = true;
                            break;
                        }
                }
            }
        }

        public void Show()
        {
            Console.WriteLine("Список всех книг:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAll();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear} ");
                    Console.WriteLine();
                }
            }
        }
        public void ShowBook()
        {
            int bookId;
            Console.Write("Введите Id книги:");
            while (!int.TryParse(Console.ReadLine(), out bookId))
            {
                Console.WriteLine("Ошибка! Введите целое число!");
            }
            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);

                var book = bookRepository.SelectById(bookId);
                if (book is not null)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear} ");
                    Console.WriteLine();
                }
            }
        }

        public void SelectById()
        {
            Console.WriteLine("Введите Id книги:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAll();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear} ");
                    Console.WriteLine();
                }
            }

        }
        public void UpdateYearByBookId()
        {
            Console.WriteLine("Обновление года публикации книги.");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAll();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear.ToString()} ");
                    Console.WriteLine();
                }
                int bookId;
                Console.Write("Введите Id книги:");
                while (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                Console.WriteLine("Введите год издания:");
                int year;
                while (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }

                bookRepository.UpdateYearById(bookId, year);
            }
        }
        /// п.1
        public void GetBooksByGenreYears()
        {
            Console.WriteLine("Список всех книг определенного жанра и вышедших между определенными годами.");

            using (var db = new AppContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();
                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name} ");
                    Console.WriteLine();
                }
                int genreId;
                Console.Write("Введите Id жанра:");
                while (!int.TryParse(Console.ReadLine(), out genreId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedGenre = genreRepository.SelectById(genreId);
                if (selectedGenre == null)
                {
                    Console.WriteLine($"Жанр с Id {genreId} отсуствует!");
                    return;
                }
                Console.WriteLine("Введите год издания, начало:");
                int yearFrom;
                while (!int.TryParse(Console.ReadLine(), out yearFrom))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                Console.WriteLine("Введите год издания, коноец:");
                int yearTo;
                while (!int.TryParse(Console.ReadLine(), out yearTo))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }

                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    var books = bookRepository.GetBooksByGenreYears(selectedGenre, yearFrom, yearTo);
                    if (books != null)
                    {
                        foreach (var book in books)
                        {
                            Console.Write($"ID: {book.Id} ");
                            Console.Write($"Имя: {book.Name} ");
                            Console.Write($"PublishYear: {book.PublishYear} ");
                            Console.WriteLine();
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// п.2
        public void GetBooksByAuthor()
        {
            Console.WriteLine("Список всех книг определенного автора.");

            using (var db = new AppContext())
            {
                AuthorRepository authorRepository = new AuthorRepository(db);
                var authors = authorRepository.SelectAll();
                foreach (var author in authors)
                {
                    Console.Write($"ID: {author.Id} ");
                    Console.Write($"Имя: {author.Name} ");
                    Console.WriteLine();
                }
                int authorId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out authorId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedAuthor = authorRepository.SelectById(authorId);
                if (selectedAuthor == null)
                {
                    Console.WriteLine($"Автор с Id {authorId} отсуствует!");
                    return;
                }

                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    Console.WriteLine($"{bookRepository.GetBooksByAuthorInLibrary(selectedAuthor)} книг с автором {selectedAuthor.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// п.3
        public void GetBooksByGenre()
        {
            Console.WriteLine("Список всех книг определенного жанра.");

            using (var db = new AppContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();
                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name} ");
                    Console.WriteLine();
                }
                int genreId;
                Console.Write("Введите Id жанра:");
                while (!int.TryParse(Console.ReadLine(), out genreId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedGenre = genreRepository.SelectById(genreId);
                if (selectedGenre == null)
                {
                    Console.WriteLine($"Жанр с Id {genreId} отсуствует!");
                    return;
                }

                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    Console.WriteLine($"{bookRepository.GetBooksByGenreInLibrary(selectedGenre)} книг с жанром {selectedGenre.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// п.4
        public void BookAuthorIsInLibrary()
        {
            Console.WriteLine("Список книг для проверки:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                AuthorRepository authorRepository = new AuthorRepository(db);

                var books = bookRepository.SelectAll();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.WriteLine();
                }
                int bookId;
                Console.Write("Введите Id книги:");
                while (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedBook = bookRepository.SelectById(bookId);
                if (selectedBook == null)
                {
                    Console.WriteLine($"Книга с Id {bookId} отсуствует!");
                    return;
                }
                //выбираем автора
                var authors = authorRepository.SelectAll();
                foreach (var author in authors)
                {
                    Console.Write($"ID: {author.Id} ");
                    Console.Write($"Имя: {author.Name} ");
                    Console.WriteLine();
                }
                int authorId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out authorId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedAuthor = authorRepository.SelectById(authorId);
                if (selectedAuthor == null)
                {
                    Console.WriteLine($"Автор с Id {bookId} отсуствует!");
                    return;
                }

                try
                {
                    var exists = bookRepository.BookByAuthorIsInLibrary(selectedAuthor, selectedBook) switch
                    {
                        true => "на руках у пользователя",
                        false => "в библиотеке",
                        _ => $"книга {selectedBook.Name} автора {selectedAuthor.Name} не найдена!"
                    };
                    Console.WriteLine($"Книга {selectedBook.Name} {exists}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        /// п.5
        public void BookIsOnUser()
        {
            Console.WriteLine("Список книг для проверки:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);

                var books = bookRepository.SelectAll();
                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.WriteLine();
                }
                int bookId;
                Console.Write("Введите Id книги:");
                while (!int.TryParse(Console.ReadLine(), out bookId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedBook = bookRepository.SelectById(bookId);
                if (selectedBook == null)
                {
                    Console.WriteLine($"Книга с Id {bookId} отсуствует!");
                    return;
                }

                try
                {
                    var filteredBooks = bookRepository.BookIsOnUser(selectedBook);
                    string exists;
                    if (filteredBooks.Count() > 0)
                        exists = "на руках у пользователя";
                    else
                        exists = "в библиотеке";
                    Console.WriteLine($"Книга {selectedBook.Name} {exists}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        /// п.6
        public void UserBooks()
        {
            Console.WriteLine("Кол-во книг на руках у пользователя:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                UserRepository userRepository = new UserRepository(db);

                var users = userRepository.SelectAll();
                foreach (var user in users)
                {
                    Console.Write($"ID: {user.Id} ");
                    Console.Write($"Имя: {user.Name} ");
                    Console.WriteLine();
                }
                int userId;
                Console.Write("Введите Id автора:");
                while (!int.TryParse(Console.ReadLine(), out userId))
                {
                    Console.WriteLine("Ошибка! Введите целое число!");
                }
                var selectedUser = userRepository.SelectById(userId);
                if (selectedUser == null)
                {
                    Console.WriteLine($"Пользователь с Id {userId} отсуствует!");
                    return;
                }

                try
                {
                    Console.WriteLine($"{bookRepository.UserBooks(selectedUser)} книг на руках у {selectedUser.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// п.7
        public void LastPublishedBook()
        {
            Console.WriteLine("Последняя изданная книга:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    /// коллекция из 1 книги максимум 
                    var book = bookRepository.LastPublishedBook();
                    foreach (var _book in book)
                    {
                        Console.Write($"ID: {_book.Id} ");
                        Console.Write($"Имя: {_book.Name} ");
                        Console.Write($"PublishYear: {_book.PublishYear} ");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        /// п.8
        public void SelectAllOrderedByName()
        {
            Console.WriteLine("Список всех книг в алфавитном порядке:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                var books = bookRepository.SelectAllOrderedByName();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear} ");
                    Console.WriteLine();
                }
            }

        }
        /// п.9
        public void SelectAllOrderedDescByPublishYear()
        {
            Console.WriteLine("Список всех книг в обратном порядке по году выпуска:");

            using (var db = new AppContext())
            {
                BookRepository bookRepository = new BookRepository(db);
                try
                {
                    var books = bookRepository.SelectAllOrderedDescByPublishYear();

                    foreach (var book in books)
                    {
                        Console.Write($"ID: {book.Id} ");
                        Console.Write($"Имя: {book.Name} ");
                        Console.Write($"PublishYear: {book.PublishYear} ");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
    }
}
