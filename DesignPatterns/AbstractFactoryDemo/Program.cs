using System;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                Console.WriteLine("***************** function3 *****************");
                User user = new User();
                IDBOperator dBOperator = DB.CreateDBOperator();
                dBOperator.AddUser(user);
            }

            {
                Console.WriteLine("***************** function2 *****************");
                User user = new User();
                IUser iu = DataAccess.CreateUser();
                iu.AddUser(user);
            }

            {
                Console.WriteLine("***************** function1 *****************");
                IDBFactory db = null;
                Console.WriteLine("plz choose the db type.");
                var dbType = Console.ReadLine();
                if (Convert.ToInt32(dbType) == (int)DBType.SQLServer)
                {
                    db = new SQLServerDB();
                }
                if (Convert.ToInt32(dbType) == (int)DBType.MySQL)
                {
                    db = new MySQLDB();
                }
                User user = new User { Id = "001", Name = "bo" };
                db.AddUser(user);
                db.DeleteUserById(user.Id);
                db.UpdateUserById(user.Id);
                db.QueryUserById(user.Id);
            }
        }
    }

    public enum DBType
    {
        SQLServer = 1,
        MySQL = 2
    }
}
