using System;
using System.Collections.Generic;
using System.Text;

namespace DBDataAccessLib
{
    public abstract class SqlProvider
    {
		public static readonly string SelectUserInfo = "select * from Account where UserName=@userName and UserPassword=@password";

		public static readonly string SelectAllUsers = "select * from Account";
		public static readonly string UpdateUser = "update Account set UserPassword=@Password,UserFunction=@function where UserName=@userName";
		public static readonly string AddUser = "insert into Account (UserName,UserPassword,UserFunction) values (@userName,@password,@function)";
		public static readonly string DeleteUser = "delete from Account where UserName=@userName";

		public static readonly string SelectAllDBNames = "select * from sys.databases";

        public static readonly string GetDBTime = "select to_char(sysdate,'yyyy-MM-dd HH24:mi:ss') from dual";
		public static readonly string SelectAllRegions="select * from LightsRegions";
	}
}
