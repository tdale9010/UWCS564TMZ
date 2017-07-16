using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Security.Cryptography;

namespace MyFlix.Models
{
	public class UserModel
	{
		private const decimal defaultRating = 75.0M;

		public int ID { get; set; }
		public string Username { get; set; }
		public decimal MinRating { get; set; }
		public List<string> FavoriteFilms { get; set; }
		public List<string> FavoriteGenres { get; set; }
		public List<string> FavoriteTags { get; set; }

		public UserModel(string username)
		{
			Username = username;
			FavoriteFilms = new List<string>();
			FavoriteGenres = new List<string>();
			FavoriteTags = new List<string>();
			using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					MySqlCommand idCommand = new MySqlCommand("select ID from users where name=@Username");
					idCommand.Connection = connection;
					idCommand.CommandType = System.Data.CommandType.Text;
					idCommand.Parameters.AddWithValue("Username", username);
					ID = (int)idCommand.ExecuteScalar();

					MySqlCommand userCommand = new MySqlCommand("User_Info");
					userCommand.Connection = connection;
					userCommand.CommandType = System.Data.CommandType.StoredProcedure;
					userCommand.Parameters.AddWithValue("UserIDIn", ID);
					MySqlDataReader userDr = userCommand.ExecuteReader();
					while(userDr.Read())
					{
						MinRating = (decimal)userDr.GetValue(3);
					}
					userDr.Close();

					MySqlCommand filmsCommand = new MySqlCommand(@"Select m.Title from favoritefilms fm
																	inner join movies m on fm.MovieID=m.movieid
																	where fm.userID=@UserIDIn;");
					filmsCommand.Connection = connection;
					filmsCommand.CommandType = System.Data.CommandType.Text;
					filmsCommand.Parameters.AddWithValue("UserIDIn", ID);
					MySqlDataReader filmsDr = filmsCommand.ExecuteReader();
					while(filmsDr.Read())
					{
						FavoriteFilms.Add(filmsDr.GetValue(0).ToString());
					}
					filmsDr.Close();

					MySqlCommand genreCommand = new MySqlCommand("Favorite_Genre");
					genreCommand.Connection = connection;
					genreCommand.CommandType = System.Data.CommandType.StoredProcedure;
					genreCommand.Parameters.AddWithValue("UserIDIn", ID);
					MySqlDataReader genreDr = genreCommand.ExecuteReader();
					while(genreDr.Read())
					{
						FavoriteGenres.Add(genreDr.GetValue(0).ToString());
					}
					genreDr.Close();

					MySqlCommand tagCommand = new MySqlCommand("Favorite_Tag");
					tagCommand.Connection = connection;
					tagCommand.CommandType = System.Data.CommandType.StoredProcedure;
					tagCommand.Parameters.AddWithValue("UserIDIn", ID);
					MySqlDataReader tagDr = tagCommand.ExecuteReader();
					while(tagDr.Read())
					{
						FavoriteTags.Add(tagDr.GetValue(0).ToString());
					}
					tagDr.Close();

					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
		}

		public static void CreateUser(string username, string password)
		{

			using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					MySqlCommand idCommand = new MySqlCommand("User_Info_Next_ID");
					idCommand.Connection = connection;
					idCommand.CommandType = System.Data.CommandType.StoredProcedure;
					int id;
					
					if(!int.TryParse(idCommand.ExecuteScalar().ToString(), out id))
					{
						id = 1;
					}

					MySqlCommand userCommand = new MySqlCommand("User_Info_Create_All");
					userCommand.Connection = connection;
					userCommand.CommandType = System.Data.CommandType.StoredProcedure;
					userCommand.Parameters.AddWithValue("UserIDIn", id);
					userCommand.Parameters.AddWithValue("NameIn", username);
					userCommand.Parameters.AddWithValue("PasswordIn", GetHash(password));
					userCommand.Parameters.AddWithValue("RatingIn", defaultRating);
					userCommand.ExecuteNonQuery();

					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
		}

		private static string GetHash(string password)
		{
			using (MD5 md5Hash = MD5.Create())
			{
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
				StringBuilder sBuilder = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}
				return sBuilder.ToString();
			}
		}
	}
}