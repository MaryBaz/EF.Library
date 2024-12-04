using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Entities
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// У книги может быть много авторов
        /// и наоборот книга может быть написана несколькимим авторами
        /// связь многие ко многим
        /// </summary>
        public List<Book> Books { get; set; }

    }
}
