﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryDemo
{
    public interface IDBFactory
    {
        void AddUser(User user);
        void DeleteUserById(string id);
        void UpdateUserById(string id);
        User QueryUserById(string id);
    }
}
