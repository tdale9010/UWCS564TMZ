$(document).ready(function (e)
{
	$(".removeFilm").click(function ()
	{
		var movie = $(this).val();
		var model = {};
		model.ID = movie;

		$.ajax({
			type: "POST",
			url: "RemoveFavoriteMovie",
			data: model,
			dataType: "json",
			success: function (data)
			{
				var selector = "[value^='" + $.trim(model.ID) + "'";
				$(selector).fadeOut();
			},
			error: function ()
			{
				alert('error handing here');
			}
		});
	});

	$(".removeGenre").click(function ()
	{
		var genre = $(this).val();
		var model = {};
		model.Name = genre;

		$.ajax({
			type: "POST",
			url: "RemoveFavoriteGenre",
			data: model,
			dataType: "json",
			success: function (data)
			{
				var selector = "[value^='" + $.trim(model.Name) + "'";
				$(selector).fadeOut();
			},
			error: function ()
			{
				alert('error handing here');
			}
		});
	});

	$(".removeTag").click(function ()
	{
		var tag = $(this).val();
		var model = {};
		model.Name = tag;

		$.ajax({
			type: "POST",
			url: "RemoveFavoriteTag",
			data: model,
			dataType: "json",
			success: function (data)
			{
				var selector = "[value^='" + $.trim(model.Name) + "'";
				$(selector).fadeOut();
			},
			error: function ()
			{
				alert('error handing here');
			}
		});
	});

});