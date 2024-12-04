using EF.Library.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Repositories
{
    public class BookRepository
    {
        private AppContext appContext;
        public BookRepository(AppContext db)
        {
            this.appContext = db;
        }
        public Book SelectById(int id)
        {
            return (from book in appContext.Books
                    where book.Id == id
                    select book).FirstOrDefault();
        }
        public IQueryable<Book> SelectAll()
        {
            return from book in appContext.Books
                   select book;
        }
        public void Remove(Book book)
        {
            appContext.Remove(book);
            appContext.SaveChanges();
        }
        public void Add(Book book)
        {
            appContext.Add(book);
            appContext.SaveChanges();
        }
        public void UpdateYearById(int id, int year)
        {
            Book book = SelectById(id);
            book.PublishYear = year;
            appContext.SaveChanges();
        }
        /// <summary>
        /// Получать список книг определенного жанра и вышедших между определенными годами.
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <returns></returns>
        public IQueryable<Book> GetBooksByGenreYears(Genre genre, int yearFrom, int yearTo)
        {
            return appContext.Books.
                        Where(b => b.PublishYear >= yearFrom &&
                              b.PublishYear <= yearTo)
                        .Join(appContext.Genres
                                .Where(g => g.Id == genre.Id), b => b.Id, g => g.BookId,
                              (b, g) => b);
        }
        /// <summary>
        /// Получать количество книг определенного жанра в библиотеке.
        /// Нужно учесть что книга в библиотеке == книга ни у кого на руках
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <returns></returns>
        public int GetBooksByGenreInLibrary(Genre genre)
        {
            return appContext.Books.Where(b => b.Genre == genre && b.UserId == null).Count();
        }
        /// <summary>
        /// Получать количество книг определенного автора в библиотеке.
        /// </summary>
        /// <param name="genre"></param>
        /// <param name="yearFrom"></param>
        /// <param name="yearTo"></param>
        /// <returns></returns>
        public int GetBooksByAuthorInLibrary(Author author)
        {
            return appContext.Books.Where(b => b.Authors.Contains(author) && b.UserId == null).Count();
        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли книга определенного автора и с определенным названием в библиотеке.
        /// </summary>
        /// <param name="author"></param>
        /// <param name="bookName"></param>
        /// <returns></returns>
        public bool? BookByAuthorIsInLibrary(Author author, Book book)
        {
            if (appContext.Books.Where(b => b.Name == book.Name
                                            && b.Authors.Any(a => a == author)
                                            ).Count() == 0)
                return null;

            if (appContext.Books.Where(b => b.Name == book.Name
                                        && b.Authors.Any(a => a == author)
                                        && b.UserId == null
                                        ).Count() > 0)
                return false;
            else
                return true;
        }
        /// <summary>
        /// Получать булевый флаг о том, есть ли определенная книга на руках у пользователя.
        /// </summary>
        /// <param name="bookName"></param>
        /// <returns></returns>
        //public bool BookIsOnUser(Book book) 
        //{
        //    if (appContext.Books.Where(b => b == book && b.UserId != null).Count() > 0)
        //        return true; // на руках у пользователя
        //    else
        //        return true;
        //}
        public IQueryable<Book> BookIsOnUser(Book book)
        {
            return appContext.Books.Where(b => b == book && b.UserId != null);
        }

        /// <summary>
        /// Кол-во книг на руках у пользователя.
        /// </summary>
        /// <returns></returns>
        public int UserBooks(User user)
        {
            return appContext.Books.Where(b => b.User == user).Count();
        }
        /// <summary>
        /// Получение последней вышедшей книги.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Book> LastPublishedBook()
        {
            return appContext.Books.OrderByDescending(b => b.PublishYear).Take(1);
        }
        /// <summary>
        /// Получение списка всех книг, отсортированного в алфавитном порядке по названию.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Book> SelectAllOrderedByName()
        {
            return appContext.Books.OrderBy(n => n.Name);
        }
        /// <summary>
        /// Получение списка всех книг, отсортированного в порядке убывания года их выхода.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Book> SelectAllOrderedDescByPublishYear()
        {
            //Console.WriteLine($"{n.PublishYear ?? DateTime.Now.Year.ToString()}");
            return appContext.Books.OrderByDescending(n => n.PublishYear);
            //return appContext.Books.OrderByDescending(n => String.IsNullOrEmpty(n.PublishYear) ? DateTime.Now.Year : Int32.Parse(n.PublishYear)) ;
        }


    }
}
