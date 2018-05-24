using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using FitnessForGeeksWebApi.Controllers;
using FitnessForGeeksWebApi.Utility;

namespace FitnessForGeeksWebApi.Database
{
    public class AccountManager : IDatabaseManager<Account>
    {
        public List<Account> Get()
        {
            var accounts = new List<Account>();
            MySqlDatabase.ExecuteReader("select * from accounts", reader => {
                accounts.Add(NewAccountFromReader(reader));
            });
            return accounts;
        }

        private Account NewAccountFromReader(MySqlDataReader reader)
        {
            return new Account(
                MySqlDatabase.GetValueOrNull<int>(reader, "id"),
                MySqlDatabase.GetValue<string>(reader, "username"),
                MySqlDatabase.GetValue<string>(reader, "password"),
                MySqlDatabase.GetValueOrNull<DateTime>(reader, "birthdate"),
                MySqlDatabase.GetValueOrNull<double>(reader, "weight"),
                MySqlDatabase.GetValueOrNull<double>(reader, "height"),
                MySqlDatabase.GetValueOrNull<bool>(reader, "isVerified"),
                MySqlDatabase.GetValue<string>(reader, "authKey"),
                MySqlDatabase.GetValue<string>(reader, "firstName"),
                MySqlDatabase.GetValue<string>(reader, "lastName"),
                MySqlDatabase.GetValue<string>(reader, "email"),
                MySqlDatabase.GetValue<string>(reader, "description")
            );
        }

        public Account GetByUsername(String username)
        {
            Account acc = null;
            try
            {
                MySqlDatabase.ExecuteReader(
                    $"select * from accounts where username='{username}'",
                    reader => {
                        acc = NewAccountFromReader(reader);
                    },
                    true
                );
            } catch (ResultIsEmptyException ex) {}
            return acc;
        }

        public bool Create(CreateAccountPostData data)
        {
            bool success = false;
            MySqlDatabase.ExecuteNoneQuery(
                $"insert into accounts(username, password, email, authKey) values ('{data.Username}','{data.Password}','{data.Email}','{Hash.Sha256(data.Username + data.Password)}')",
                count => 
                {
                    if (count == 1)
                        success = true;
                }
                );
            return success;
        }
    }
}
