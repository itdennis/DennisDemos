﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public class SQLServerDBOperator : IDBOperator
    {
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserById(string id)
        {
            throw new NotImplementedException();
        }

        public User QueryUserById(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUserById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
