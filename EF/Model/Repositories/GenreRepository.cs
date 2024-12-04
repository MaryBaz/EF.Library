using EF.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Repositories
{
    public class GenreRepository
    {
        private AppContext appContext;
        public GenreRepository(AppContext db)
        {
            this.appContext = db;
        }

        public Genre SelectById(int id)
        {
            return (from genre in appContext.Genres
                    where genre.Id == id
                    select genre).FirstOrDefault();
        }
        public IQueryable<Genre> SelectAll()
        {
            return from genre in appContext.Genres
                   select genre;
        }
        public void AddBookGenre(Book book, Genre genre)
        {
            book.Genre = genre;
            appContext.SaveChanges();
        }

    }
}
