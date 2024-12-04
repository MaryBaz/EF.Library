using EF.Library.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Repositories
{
    public class UserRepository
    {
        private AppContext appContext;
        public UserRepository(AppContext db)
        {
            appContext = db;
        }
        public User SelectById(int id)
        {
            return (from user in appContext.Users
                    where user.Id == id
                    select user).FirstOrDefault();
        }
        public IQueryable<User> SelectAll()
        {
            return from user in appContext.Users
                   select user;
        }
        public void Remove(User user)
        {
            appContext.Remove(user);
            appContext.SaveChanges();
        }
        public void Add(User user)
        {
            appContext.Add(user);
            appContext.SaveChanges();
        }
        public void UpdateEmailById(int id, string email)
        {
            User user = SelectById(id);
            user.Email = email;
        }
        public int AcceptBook(User user, Book book)
        {
            // если книгу кто-то взял до нас
            if (book.User is not null)
                return 0;

            user.books.Add(book);
            return 1;
        }
        public int ReturnBook(User user, Book book)
        {
            // если книга ни кем не взята
            if (book.User is null)
                return 0;

            user.books.Remove(book);
            return 1;
        }
    }
}
