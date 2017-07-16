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
	public class HomeController : Controller
	{
		public ActionResult Home()
		{
			return View();
		}

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

		public ActionResult Search()
		{
			SearchModel model = new SearchModel();
			return View(model);
		}
	}
}