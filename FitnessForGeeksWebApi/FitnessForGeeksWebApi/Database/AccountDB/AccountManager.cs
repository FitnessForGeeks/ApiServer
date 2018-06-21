using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using FitnessForGeeksWebApi.Controllers;
using FitnessForGeeksWebApi.Utility;

namespace FitnessForGeeksWebApi.Database.AccountDB
{
    public class AccountManager : IDatabaseManager<Account>
    {
        public List<Account> GetAll()
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
                MySqlDatabase.GetValueOrNull<int>(reader, "height"),
                MySqlDatabase.GetValueOrNull<bool>(reader, "isVerified"),
                MySqlDatabase.GetValue<string>(reader, "authKey"),
                MySqlDatabase.GetValue<string>(reader, "firstName"),
                MySqlDatabase.GetValue<string>(reader, "lastName"),
                MySqlDatabase.GetValue<string>(reader, "email"),
                MySqlDatabase.GetValue<string>(reader, "description"),
                MySqlDatabase.GetValueOrNull<bool>(reader, "isMale")
            );
        }

		public void UpdateAccount(Account account)
		{
            var query = "update accounts set ";
            if (account.FirstName != null)
                query += $"firstName = '{account.FirstName}',";
            if (account.LastName != null)
                query += $"lastName = '{account.LastName}',";
            if (account.Email != null)
                query += $"email = '{account.Email}',";
            if (account.IsMale.HasValue)
                query += $"isMale = {account.IsMale},";
            if (account.Birthdate.HasValue)
                query += $"birthdate = str_to_date('{account.Birthdate.Value.ToString("dd-MM-yyyy")}','%d-%m-%Y'),";
            if (account.Weight.HasValue)
                query += $"weight = {account.Weight},";
            if (account.Height.HasValue)
                query += $"height = {account.Height},";
            if (account.Description != null)
                query += $"description = '{account.Description}',";
            query = query.Remove(query.Length - 1);
			MySqlDatabase.ExecuteNoneQuery(query + $" where accounts.id = {account.Id}");
		}

		private Account GetByParameter<T>(string columnName, T value)
        {
            Account acc = null;
            // sql requires '' around strings
            var query = typeof(T) == typeof(string) 
                ? $"'{value}'" 
                : value.ToString();

            MySqlDatabase.ExecuteReader(
                $"select * from accounts where {columnName}={query}",
                reader => {
                    acc = NewAccountFromReader(reader);
                }
            );
            return acc;
        }

		public Account GetById(int accountId)
		{
			return GetByParameter<int>("id", accountId);
		}

		public Account GetByUsername(string username)
        {
            return GetByParameter<string>("username", username);
        }

        public Account GetByAuthKey(string authKey)
        {
            return GetByParameter<string>("authKey", authKey);
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
