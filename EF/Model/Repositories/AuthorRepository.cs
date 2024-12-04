using EF.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Repositories
{
    public class AuthorRepository
    {
        private AppContext appContext;
        public AuthorRepository(AppContext db)
        {
            this.appContext = db;
        }

        public Author SelectById(int id)
        {
            return (from author in appContext.Authors
                    where author.Id == id
                    select author).FirstOrDefault();
        }
        public IQueryable<Author> SelectAll()
        {
            return from author in appContext.Authors
                   select author;
        }


    }
}
