using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MyFlix.Models
{
	public class SearchResultsModel
	{
		public List<MovieDetailsModel> Movies { get; set; }
		private MySqlConnection connection;

		public SearchResultsModel(List<int> movieIds)
		{
			Movies = new List<MovieDetailsModel>();
			using (connection = new MySqlConnection(ConfigurationManager.ConnectionStrings["MyFlixDB"].ConnectionString))
			{
				try
				{
					connection.Open();
					foreach(int id in movieIds)
					{
						Movies.Add(new MovieDetailsModel(id, connection));
					}
					connection.Close();
				}
				catch
				{
					// TODO: Handle errors
				}
			}
		}
	}
}