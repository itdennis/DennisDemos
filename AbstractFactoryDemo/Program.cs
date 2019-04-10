using System;

namespace AbstractFactoryDemo
{
    class Program
    {
        static void Main(string[] args)
        {
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

    public enum DBType
    {
        SQLServer = 1,
        MySQL = 2
    }
}
