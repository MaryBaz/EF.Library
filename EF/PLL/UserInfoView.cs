using EF.Library.Model.Entities;
using EF.Library.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.PLL
{
    public class UserInfoView
    {
        public void ShowMenu()
        {
            bool exitMenu = false;
            while (!exitMenu)
            {
                Console.WriteLine("Выберете действие:");
                Console.WriteLine("1. Список книг.");
                Console.WriteLine("2. Поиск пользователя по Id");
                Console.WriteLine("3. Kоличество книг на руках у пользователя.");
                Console.WriteLine("4. Обновить Email пользователя по Id");
                Console.WriteLine("5. Взять книгу в библиотеке.");
                Console.WriteLine("6. Вернуть книгу в библиотеку.");
                Console.WriteLine("7. Добавить пользователя.");
                Console.WriteLine("8. Удалить пользователя.");
                Console.WriteLine("0. Выйти из меню.");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Program.userInfoView.Show();
                            break;
                        }
                    case "2":
                        {
                            Program.userInfoView.ShowUser();
                            break;
                        }
                    case "3":
                        {
                            //Program.authorInfoView.Show();
                            break;
                        }
                    case "4":
                        {
                            //Program.genreInfoView.Show();
                            break;
                        }
                    case "5":
                        {
                            //Program.genreInfoView.Show();
                            break;
                        }
                    case "6":
                        {
                            //Program.genreInfoView.Show();
                            break;
                        }
                    case "7":
                        {
                            //Program.genreInfoView.Show();
                            break;
                        }
                    case "8":
                        {
                            //Program.genreInfoView.Show();
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
            //bool exitMenu = false;
            Console.WriteLine("Пользователи:");

            //UserRepository userRepository = new UserRepository();
            using (var db = new AppContext())
            {
                UserRepository userRepository = new UserRepository(db);
                //var users = from user in db.Users
                //        select user;
                var users = userRepository.SelectAll();
                //var users = userRepository.SelectAll();
                foreach (var user in users)
                {
                    Console.Write($"ID: {user.Id} ");
                    Console.Write($"Имя: {user.Name} ");
                    Console.Write($"eMail: {user.Email} ");
                    Console.WriteLine();
                }
            }
        }
        public void ShowUser()
        {
            int userId;
            Console.Write("Введите Id пользователя:");
            while (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.WriteLine("Ошибка! Введите целое число!");
            }
            using (var db = new AppContext())
            {
                UserRepository userRepository = new UserRepository(db);

                var user = userRepository.SelectById(userId);
                if (user is not null)
                {
                    Console.Write($"ID: {user.Id} ");
                    Console.Write($"Имя: {user.Name} ");
                    Console.Write($"eMail: {user.Email} ");
                    Console.WriteLine();
                }
            }
        }
    }
}
