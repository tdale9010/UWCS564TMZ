using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MyFlix.Models
{
	public class SearchModel
	{
		public string Title { get; set; }
		public decimal? Rating { get; set; }
		public string Director { get; set; }
		public string Genre { get; set; }
		public string Actor { get; set; }
		public string Location { get; set; }
		public string Country { get; set; }
		public string Tag { get; set; }
		private const int MaxResults = 100;

		public List<int> Search()
		{
			List<int> movieIds = new List<int>();
			using (MySqlConnection connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					MySqlCommand command = new MySqlCommand("Search_Results");
					command.Parameters.AddWithValue("TitleIn", Title);
					command.Parameters.AddWithValue("RatingIn", Rating);
					command.Parameters.AddWithValue("DirectorIn", Director);
					command.Parameters.AddWithValue("GenreIn", Genre);
					command.Parameters.AddWithValue("ActorIn", Actor);
					command.Parameters.AddWithValue("FilmLocationIn", Location);
					command.Parameters.AddWithValue("CountryIn", Country);
					command.Parameters.AddWithValue("TagIn", Tag);
					command.Connection = connection;
					command.CommandType = System.Data.CommandType.StoredProcedure;
					MySqlDataReader dr = command.ExecuteReader();
					int i = 0;
					while (dr.Read() && i < MaxResults)
					{
						movieIds.Add((int)dr.GetValue(0));
						i++;
					}
					dr.Close();
					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
			return movieIds;
		}
	}
}