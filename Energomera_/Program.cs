using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace Energomera_
{
    class Program
    {
        static int[] mas = new int[8] { 1, 2, 3, 5, 6, 7, 8, 9 };
        
        static private string patch = "DBTest.db";

        static int Task1Sum(int[] M)
        {
            int sumM = 0;
            foreach (int i in M)
            {
                if (i % 2 != 0) sumM += i;
            }
            return sumM;
        }
        static bool Task2Check(string input)
        {
            int circle = 0;
            int figure = 0;
            int square = 0;
            int gg=input[0].CompareTo('(');
            for (int i=0; i<input.Length;i++)
            {
                if (input[i].CompareTo('(') == 0) circle++;
                if (input[i].CompareTo('{') == 0) figure++;
                if (input[i].CompareTo('[') == 0) square++;
                if (input[i].CompareTo(')') == 0) circle--;
                if (input[i].CompareTo('}') == 0) figure--;
                if (input[i].CompareTo(']') == 0) square--;
                if (circle < 0 || figure < 0 || square < 0) return false;
            }
            if (circle == 0 && figure == 0 && square == 0) return true;
            return false;
           

        }
        static void Task3Check()
        {
            createDB();
            command("INSERT INTO Items (Name) VALUES ('Иван')");
            Console.WriteLine("Добавлено имя: Иван");
            string NumberCommand = "";

            while (NumberCommand!="4")
            {
                Console.WriteLine("введите команду: 1- Добавить запись, 2- Изменить запись, 3- Удалить запись, 4- Выход.");
                NumberCommand=Console.ReadLine();
                if (NumberCommand == "1") 
                {
                    Console.WriteLine("Введите имя:");
                    string text = Console.ReadLine();
                    command("INSERT INTO Items (Name) VALUES ('"+ text + "')");
                }
                if (NumberCommand == "2")
                {
                    Console.WriteLine("Введите имя из таблицы:");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите новое имя:");
                    string text = Console.ReadLine();
                    command("UPDATE Items SET Name='"+text+"' WHERE Name ='"+name+"'");
                }
                if (NumberCommand == "3")
                {
                    Console.WriteLine("Введите имя:");
                    string text = Console.ReadLine();
                    command("DELETE FROM Items WHERE Name='" + text + "'");
                }

            }

            SQLiteCommand command(string text)
            {
                using (var sqlite = new SQLiteConnection(@"DataSource=" + patch))
                {
                    sqlite.Open();
                    SQLiteCommand comanda = new SQLiteCommand(text, sqlite);
                    comanda.ExecuteNonQuery();
                    var cmd = new SQLiteCommand("SELECT * FROM Items", sqlite);
                    SQLiteDataReader data = cmd.ExecuteReader();
                    Console.WriteLine("------------значения таблицы------------------");
                    while (data.Read())
                    {
                        Console.WriteLine(data.GetInt64(0) + " " + data.GetString(1));
                    }
                    Console.WriteLine("-----------------------------------------------");
                    return comanda;
                }
            }
            void createDB()
            {
                if (!System.IO.File.Exists(patch))
                {
                    
                    using (var sqlite = new SQLiteConnection(@"DataSource=" + patch))
                    {
                        sqlite.Open();
                        Console.WriteLine("БД создана");
                        command("create table Items(Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE, Name TEXT NOT NULL)");
                        Console.WriteLine("Таблица Items создана");
                    }
                }
            }
        }
        
        static void Main(string[] args)
        {

        Console.WriteLine("Задание 1");
        Console.Write("Значения массива: ");
        foreach (int i in mas) { Console.Write(i + " "); }
        Console.WriteLine();
        Console.WriteLine("Сумма всех нечётных чисел в массиве: " + Task1Sum(mas));
        Console.WriteLine();
        Console.WriteLine("Задание 2");
        Console.WriteLine("Исходные данные: (((){}[]]]])(");
        Console.WriteLine("Результат: " + Task2Check("(((){}[]]]])("));
        Console.WriteLine("Исходные данные: (){}[][][]()");
        Console.WriteLine("Результат: " + Task2Check("(){}[][][]()"));
        Console.WriteLine("Исходные данные: ({[]})()[]");
        Console.WriteLine("Результат: " + Task2Check("({[]})()[]"));
        Console.WriteLine();
        Console.WriteLine("Задание 3");
            Task3Check();
        }
    }
}
