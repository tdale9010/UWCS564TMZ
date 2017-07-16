using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MyFlix.Models
{
	public class MovieDetailsModel
	{
		public int ID { get; set; }
		public string Title { get; set; }
		public string Director { get; set; }
		public List<string> Actors { get; set; }
		public string Country { get; set; }
		public List<string> Locations { get; set; }
		public List<string> Tags { get; set; }
		public List<string> Genres { get; set; }
		public string ImageURL { get; set; }
		public int Year { get; set; }
		public decimal Rating { get; set; }


		public MovieDetailsModel(int id)
		{
			ID = id;
			using(MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					GetMovieDetails(connection);
					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
		}

		public MovieDetailsModel(int id, MySqlConnection connection)
		{
			ID = id;
			if(connection.State == System.Data.ConnectionState.Open)
			{
				GetMovieDetails(connection);
			}
		}

		private void GetMovieDetails(MySqlConnection connection)
		{
			MySqlCommand command = new MySqlCommand("Movie_Info");
			command.Parameters.AddWithValue("MovieId", ID);
			command.Connection = connection;
			command.CommandType = System.Data.CommandType.StoredProcedure;
			MySqlDataReader dr = command.ExecuteReader();
			while (dr.Read())
			{
				Title = dr.GetValue(0).ToString();
				Rating = (decimal)dr.GetValue(1) * 10.0m;
				Director = dr.GetValue(2).ToString();
				Genres = dr.GetValue(3).ToString().Split(',').ToList();
				Actors = dr.GetValue(4).ToString().Split(',').ToList();
				Locations = dr.GetValue(5).ToString().Split(',').ToList();
				Country = dr.GetValue(6).ToString();
				Tags = dr.GetValue(7).ToString().Split(',').ToList();
				ImageURL = dr.GetValue(8).ToString().Trim('\"');
			}
			dr.Close();
		}		
	}
}