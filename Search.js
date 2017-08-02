$(document).ready(function (e)
{
	$("#submit").click(function ()
	{
		var searchParams = $("#searchForm").serialize();
		$.ajax({
			type: "GET",
			url: "SearchResults",
			data: searchParams,
			dataType: "json",
			success: function (data)
			{
				$("#results").empty();

                var table = $("<table></table>");
                table.addClass("table table-hover");
				var header = $("<tr><th>Title</th><th>Director</th><th>Rating</th><th>Image</th></tr>");
				table.append(header);

				for (var i = 0; i < data.Movies.length; i++){
					var row = $("<tr></tr>");
					row.append($("<td><a href='/Home/MovieDetails?id=" + data.Movies[i].ID + "'>" + data.Movies[i].Title + "</a></td>"));
					row.append($("<td>" + data.Movies[i].Director + "</td>"));
					row.append($("<td>" + data.Movies[i].Rating + "</td>"));
                    row.append($("<td><img src='" + data.Movies[i].ImageURL + "' /></td>"));
					table.append(row);
				}


				$("#results").append(table);
				
			},
			error: function ()
			{
				alert('error handing here');
			}
		});
	});
});