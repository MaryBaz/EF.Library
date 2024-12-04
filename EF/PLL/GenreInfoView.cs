using EF.Library.Model.Entities;
using EF.Library.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.PLL
{
    public class GenreInfoView
    {
        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Выберете действие:");
                Console.WriteLine("1. Список жанров книг.");
                Console.WriteLine("2. Добавить жанр книги.");
                Console.WriteLine("0. Выйти из меню.");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Program.genreInfoView.Show();
                            break;
                        }
                    case "2":
                        {
                            Program.genreInfoView.AddBookGenre();
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
            Console.WriteLine("Жанры:");

            using (var db = new AppContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();

                foreach (var genre in genres)
                {
                    Console.Write($"ID: {genre.Id} ");
                    Console.Write($"Имя: {genre.Name}");
                    Console.WriteLine();
                }
            }
        }
        public void AddBookGenre()
        {
            Console.WriteLine("Добавление жанра");

            using (var db = new AppContext())
            {
                GenreRepository genreRepository = new GenreRepository(db);
                var genres = genreRepository.SelectAll();

                foreach (var genre in genres)
                {
                    Console.WriteLine($"ID: {genre.Id}");
                    Console.WriteLine($"Имя: {genre.Name}");
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
                var books = bookRepository.SelectAll();

                foreach (var book in books)
                {
                    Console.Write($"ID: {book.Id} ");
                    Console.Write($"Имя: {book.Name} ");
                    Console.Write($"PublishYear: {book.PublishYear}");
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
                    genreRepository.AddBookGenre(selectedBook, selectedGenre);
                    Console.WriteLine($"Для книги {selectedBook.Name} добавлен жанр {selectedGenre.Name}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
