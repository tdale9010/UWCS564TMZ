$(document).ready(function (e) {
	$("#addFavorites").click(function () {
		var id = $("#addFavorites").data().id;

		var model = {};
		model.ID = id;

		$.ajax({
			type: "POST",
			url: "AddFavorites",
			data: model,
			dataType: "json",
			success: function (data){
				$("#addFavorites").fadeOut();

			},
			error: function () {
				alert('error handing here');
			}
		});
	});

	$(".favoriteTags").click(function () {
		var tag = $(this).val();
		var model = {};
		model.Name = tag;

		$.ajax({
			type: "POST",
			url: "AddFavoriteTag",
			data: model,
			dataType: "json",
			success: function (data) {
				var selector = ".favoriteTags[value^='" + $.trim(model.Name) + "'";
				$(selector).fadeOut();
			},
			error: function () {
				alert('error handing here');
			}
		});
	});

	$(".favoriteGenres").click(function () {
		var genre = $(this).val();
		var model = {};
		model.Name = genre;

		$.ajax({
			type: "POST",
			url: "AddFavoriteGenre",
			data: model,
			dataType: "json",
			success: function (data) {
				var selector = ".favoriteGenres[value^='" + $.trim(model.Name) + "'";
				$(selector).fadeOut();
			},
			error: function () {
				alert('error handing here');
			}
		});

	});
});