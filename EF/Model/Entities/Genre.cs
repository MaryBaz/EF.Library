using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //public List<Book> books { get; set; }
        ///считаем что 1 книга - 1 жанр
        public int BookId { get; set; }
        public Book book { get; set; }
    }
}
