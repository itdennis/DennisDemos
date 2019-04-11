using System.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AbstractFactoryDemo
{
    public class DB
    {
        public static readonly string AssemblyName = "AbstractFactoryDemo";
        public static readonly string ClassAssemblyName = "AbstractFactoryDemo.SQLServerDBOperator";

        public static IDBOperator CreateDBOperator()
        {
            return (IDBOperator)Assembly.Load(AssemblyName).CreateInstance(ClassAssemblyName);
        }
    }
}
