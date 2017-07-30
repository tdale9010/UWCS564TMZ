using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using MyFlix.Models;

namespace MyFlix.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{
		private int? UserID
		{
			get
			{
				return Session["UserID"] as int?;
			}
			set
			{
				Session["UserID"] = value;
			}
		}

		public ActionResult Home()
		{
			UserModel model = new UserModel(User.Identity.Name);
			UserID = model.ID;
			return View(model);
		}


		[AllowAnonymous]
		public ActionResult About()
		{
			ViewBag.Message = "MyFlix is an application etc....";

			return View();
		}

		public ActionResult MovieDetails(int id)
		{
			MovieDetailsModel model = new MovieDetailsModel(id);
			return View(model);
		}

		[HttpGet]
		public JsonResult SearchResults(SearchModel searchParams)
		{
			SearchResultsModel model = new SearchResultsModel(searchParams.Search());
			return Json(model, JsonRequestBehavior.AllowGet);
		}


		public ActionResult Search(string genre, string tag)
		{
			SearchModel model = new SearchModel();
			model.Genre = genre;
			model.Tag = tag;
			return View(model);
		}

		public ActionResult AddFavorites(MovieIDModel idModel)
		{
			if (UserID.HasValue)
			{
				UserFavoriteModel model = new UserFavoriteModel(UserID.Value, idModel.ID);
				model.Save();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}

		public ActionResult AddFavoriteTag(TagModel tagModel)
		{
			if(UserID.HasValue)
			{
				UserTagModel model = new UserTagModel(UserID.Value, tagModel.Name);
				model.Save();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}

		public ActionResult AddFavoriteGenre(GenreModel genreModel)
		{
			if(UserID.HasValue)
			{
				UserGenreModel model = new UserGenreModel(UserID.Value, genreModel.Name);
				model.Save();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}

		public ActionResult RemoveFavoriteMovie(MovieIDModel movieModel)
		{
			if(UserID.HasValue)
			{
				UserFavoriteModel model = new UserFavoriteModel(UserID.Value, movieModel.ID);
				model.Delete();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}

		public ActionResult RemoveFavoriteGenre(GenreModel genreModel)
		{
			if (UserID.HasValue)
			{
				UserGenreModel model = new UserGenreModel(UserID.Value, genreModel.Name);
				model.Delete();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}

		public ActionResult RemoveFavoriteTag(TagModel tagModel)
		{
			if(UserID.HasValue)
			{
				UserTagModel model = new UserTagModel(UserID.Value, tagModel.Name);
				model.Delete();
			}
			else
			{
				// Handle invalid state
				return new EmptyResult();
			}
			return Json(string.Empty);
		}
	}
}