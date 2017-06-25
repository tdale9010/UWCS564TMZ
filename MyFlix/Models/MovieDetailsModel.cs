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
		public DirectorModel Director { get; set; }
		public List<ActorModel> Actors { get; set; }
		public string Country { get; set; }
		public List<string> Locations { get; set; }
		public List<string> Tags { get; set; }
		public string ImageURL { get; set; }
		public int Year { get; set; }
		public decimal Rating { get; set; }

		private MySqlConnection connection;

		public MovieDetailsModel(int id)
		{
			ID = id;
			using(connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					GetMovieDetails();
					Director = GetDirector(id, connection);
					Actors = GetActors(id, connection);
					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
		}

		private void GetMovieDetails()
		{
			MySqlCommand command = new MySqlCommand("select m.title, trim(both '\"' from m.imdbPictureURL), m.year, m.rtAllCriticsRating, mc.country from movies m join movie_countries mc on m.id = mc.movie_id where m.id = @id");
			command.Parameters.AddWithValue("id", ID);
			command.Connection = connection;
			MySqlDataReader dr = command.ExecuteReader();
			while (dr.Read())
			{
				Title = dr.GetValue(0).ToString();
				ImageURL = dr.GetValue(1).ToString();
				Year = (int)dr.GetValue(2);
				Rating = (decimal)dr.GetValue(3) * 10.0m;
				Country = dr.GetValue(4).ToString();
			}
			dr.Close();
		}

		public DirectorModel GetDirector(int movieID, MySqlConnection connection)
		{
			if (connection.State != System.Data.ConnectionState.Open)
				throw new Exception("Expected open connection");

			DirectorModel director = null;
			MySqlCommand command = new MySqlCommand("select directorName from movie_directors where movieID = @movieID");
			command.Parameters.AddWithValue("movieID", movieID);
			command.Connection = connection;
			MySqlDataReader dr = command.ExecuteReader();
			while(dr.Read())
			{
				director = new DirectorModel(dr.GetValue(0).ToString());
			}
			dr.Close();

			return director;
		}

		public List<ActorModel> GetActors(int movieID, MySqlConnection connection)
		{
			if (connection.State != System.Data.ConnectionState.Open)
				throw new Exception("Expected open connection");

			List<ActorModel> actors = new List<ActorModel>();
			MySqlCommand command = new MySqlCommand("select actorName from movie_actors where movieID = @movieID");
			command.Parameters.AddWithValue("movieID", movieID);
			command.Connection = connection;
			MySqlDataReader dr = command.ExecuteReader();
			while (dr.Read())
			{
				actors.Add(new ActorModel(dr.GetValue(0).ToString()));
			}
			dr.Close();

			return actors;
		}
	}

	public class DirectorModel
	{
		public string Name { get; set; }

		public DirectorModel()
		{

		}

		public DirectorModel(string name)
		{
			Name = name;
		}
	}

	public class ActorModel
	{
		public string Name { get; set; }

		public ActorModel()
		{

		}

		public ActorModel(string name)
		{
			Name = name;
		}
	}
}