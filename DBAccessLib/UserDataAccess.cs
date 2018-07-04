using DaiKin.DBDataAccessLib.Helper;
using DBAccessLib.DataMapping;
using DBDataAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAccessLib
{
	public abstract class UserDataAccess
	{
		public static AccountMapping SelectUserNameAndPasswordCount(string userName, string password)
		{
			int function;
			AccountMapping account = null;
			DbDataReader reader = SqlHelper.ExecuteReader(
					SqlHelper.ConnectionStringLocalTransaction,
					CommandType.Text,
					SqlProvider.SelectUserInfo,
					new SqlParameter("@userName", userName),
					new SqlParameter("@password", password));
			while (reader.Read())
			{
				account = new AccountMapping();
				account.UserName = reader["USERNAME"].ToString();
				account.Password = reader["USERPASSWORD"].ToString();
				if (int.TryParse(reader["USERFUNCTION"].ToString(), out function)) account.Function = function;
				else account.Function = 0;
				break;
			}
			return account;
		}

		public static List<AccountMapping> GetAllAccounts()
		{
			List<AccountMapping> accounts = new List<AccountMapping>();
			DbDataReader reader = SqlHelper.ExecuteReader(
					SqlHelper.ConnectionStringLocalTransaction,
					CommandType.Text,
					SqlProvider.SelectAllUsers);
			while (reader.Read())
			{
				AccountMapping account = new AccountMapping();
				account.UserName = reader["USERNAME"].ToString();
				account.Password = reader["PASSWORD"].ToString();
				account.Function = 3;
				accounts.Add(account);
			}
			return accounts;
		}

		public static int UpdateUser(string userName, string password, int function)
		{
			int affectRows = SqlHelper.ExecuteNonQuery(
					SqlHelper.ConnectionStringLocalTransaction,
					CommandType.Text,
					SqlProvider.UpdateUser,
					new SqlParameter("@userName", userName),
					new SqlParameter("@password", password),
					new SqlParameter("@function", function)
					);
			return affectRows;
		}

		public static int AddUser(string userName, string password, string function)
		{
			int affectRows = SqlHelper.ExecuteNonQuery(
					SqlHelper.ConnectionStringLocalTransaction,
					CommandType.Text,
					SqlProvider.AddUser,
					new SqlParameter("@userName", userName),
					new SqlParameter("@password", password),
					new SqlParameter("@function", function)
					);
			return affectRows;
		}

		public static int DeleteUser(string userName)
		{
			int affectRows = SqlHelper.ExecuteNonQuery(
				SqlHelper.ConnectionStringLocalTransaction,
				CommandType.Text,
				SqlProvider.DeleteUser,
				new SqlParameter("@userName", userName)
				);
			return affectRows;
		}
	}
}
