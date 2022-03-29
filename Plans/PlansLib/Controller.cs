﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlansLib
{
	/// <summary>
	/// Controller Class to control the logic of the program
	/// </summary>
    public class Controller
    {
		/// <summary>
		/// Create User Method for creating and saving a user into the db
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static bool CreateUser(User user)
		{
			string passwordHash = SecurityOps.HashString(user.PasswordHash);

			if (DatabaseOps.OpenConnection())
			{
				if (DatabaseOps.GetPasswordHash(user).Equals(string.Empty))
				{
					
					bool result = DatabaseOps.CreateUser(user);
					DatabaseOps.CloseConnection();
					return result;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Method to load user from userdb
		/// </summary>
		/// <param name="email"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		public static User LoadUser(User user)
		{
			bool results = false;

			if (DatabaseOps.OpenConnection())
			{
				string passwordHash = DatabaseOps.GetPasswordHash(user);
				results = SecurityOps.VerifyHash(passwordHash, user.PasswordHash);
				if (results == false)
				{
					user = null;
				}
				else
				{
					user = DatabaseOps.GetUser(user);
				}
				
				DatabaseOps.CloseConnection();

			}
			else
			{
				user = null;
				results = false;
			}
			return user;
		}



	}
}
