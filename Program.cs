using System;
using System.IO;
using System.Data.SQLite;

class Program{
    static void Main(string[] args)
    {
        getFilterData();
    }

    static void getFilterData()
    {
        string stringConnect = "Data Source=/home/ant/Ekreative course/basic_for_veteran/Data_Base_For_Veterans/Homework_1/Data_Base/car_showroom.db;Version=3;";
        Console.WriteLine("Зробіть вибір:");
        Console.WriteLine("1 - Клієнт у яких є буква 'В'");
        Console.WriteLine("2 - Клієнти, які народилися між 2000 і 2010  роками");
        Console.WriteLine("3 - Вибрати усі машини виробника Audi");
        Console.WriteLine("4 - Вибрати усі машини клієнта з id = 1");
        Console.WriteLine("5 - Всі машини, власники яких народились до 2000 року");
        Console.WriteLine("6 - Вибрати імʼя найстаршого клієнта");
        Console.WriteLine("7 - Показати кількість авто у клієнта з id = 1");
        Console.WriteLine("8 - Дані про клієнта з найновішою машиною");
        int menuNumber = Convert.ToInt32(Console.ReadLine());
        switch(menuNumber)
        {
            case 1:
                getFilteredByLetter(stringConnect);
                break;
            case 2:
                getRangeYear(stringConnect);
                break;
            case 3:
                getProducerCar(stringConnect);
                break;
            case 4:
                getAllClientCars(stringConnect);
                break;
            case 5:
                getAllClientsBornBefore(stringConnect);
                break;
            case 6:
                getOldestClients(stringConnect);
                break;
            case 7:
                getNumberOfCars(stringConnect);
                break;
            case 8:
                getDataWithNewestCar(stringConnect);
                break;
            default:
                break;
        }
        
    }

    private static void getDataWithNewestCar(string stringConnect)
    {
        using(SQLiteConnection connection = new SQLiteConnection(stringConnect))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select clients.* from clients join cars on clients.id = cars.users_id where year_prod in(select max(year_prod) from cars)";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getNumberOfCars(string stringConnect)
    {
        Console.WriteLine("Введіть id");
        int clientId  = Convert.ToInt32(Console.ReadLine());
        using(SQLiteConnection connection = new SQLiteConnection(stringConnect))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select count(*) from cars where users_id in ({clientId})";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getOldestClients(string stringConnect)
    {
        using(SQLiteConnection connection = new SQLiteConnection(stringConnect))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from clients where birth_year in (select min(birth_year) from clients)";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[1]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getAllClientsBornBefore(string stringConnect)
    {
        Console.WriteLine("Введіть рік");
        int bornYear = Convert.ToInt32(Console.ReadLine());
        using(SQLiteConnection connection = new SQLiteConnection(stringConnect))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from cars where users_id in (select id from clients where birth_year < {bornYear})";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}\t{reader[4]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getAllClientCars(string stringConnect)
    {
        Console.WriteLine("Введіть id клієнта");
        int clientId = Convert.ToInt32(Console.ReadLine());
        using(SQLiteConnection connection = new SQLiteConnection(stringConnect))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from cars where users_id in ({clientId})";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}\t{reader[4]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getProducerCar(string path)
    {
        Console.WriteLine("Введіть назву виробника");
        string carProducer = Console.ReadLine();
        using(SQLiteConnection connection = new SQLiteConnection(path))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from cars where producer in({carProducer})";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getRangeYear(string path)
    {
        Console.WriteLine("Введіть перший рік");
        string firstYear = Console.ReadLine();
        Console.WriteLine("Введіть другий рік");
        string secondYear = Console.ReadLine();
        using(SQLiteConnection connection = new SQLiteConnection(path))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from clients where birth_year > {firstYear} and birth_year < {secondYear}";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }

    private static void getFilteredByLetter(string path)
    {
        Console.WriteLine("Введіть букву");
        string letters = Console.ReadLine();
        using(SQLiteConnection connection = new SQLiteConnection(path))
        {
            connection.Open();
            Console.WriteLine("connection open");
            string sqlQuery = $"select * from clients where name like '%{letters}%'";
            using(SQLiteCommand command = new SQLiteCommand(sqlQuery, connection))
            {
                using(SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader[0]}\t{reader[1]}\t{reader[2]}\t{reader[3]}");
                    }
                }
            }
            connection.Close();
            Console.WriteLine("connection close");
        }
    }
}

