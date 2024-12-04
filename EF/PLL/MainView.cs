using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Library.PLL
{
    public class MainView
    {
        bool exitMenu = false;
        public void Show()
        {
            while (!exitMenu)
            {
                Console.WriteLine("Выберете с какими данными будете работать:");
                Console.WriteLine("1. Пользователи библиотеки");
                Console.WriteLine("2. Книги");
                Console.WriteLine("3. Авторы");
                Console.WriteLine("4. Жанры");
                Console.WriteLine("0. Выйти.");

                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            Program.userInfoView.ShowMenu();
                            break;
                        }
                    case "2":
                        {
                            Program.bookInfoView.ShowMenu();
                            break;
                        }
                    case "3":
                        {
                            //Program.authorInfoView.Show();
                            break;
                        }
                    case "4":
                        {
                            Program.genreInfoView.ShowMenu();
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
    }
}
