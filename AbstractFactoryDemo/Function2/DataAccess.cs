using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public class DataAccess
    {
        public static readonly string AssemblyName = "AbstractFactoryDemo";
        public static readonly string db = "SQLServer";

        public static IUser CreateUser()
        {
            string className = $"{AssemblyName}.{db}User";
            return (IUser)Assembly.Load(AssemblyName).CreateInstance(className);
        }
    }
}
