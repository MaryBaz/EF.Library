using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.Model.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        /// <summary>
        /// Отношение 1 ко многим:
        /// 1 пользователь может иметь на руках любое кол-во книг или не иметь
        /// </summary>
        public List<Book> books { get; set; } = new List<Book>();
    }
}
