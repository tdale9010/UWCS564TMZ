$(document).ready(function (e)
{
	$("#userNameHolder").hide();
	$("#saveUserName").prop('disabled', true);
	$("#ratingHolder").hide();

	$("#updateUserName").click(function () {
		$("#userNameHolder").show();
	});

	$("#updateRating").click(function () {
		$("#ratingHolder").show();
	});

	$("#newUserName").keydown(function ()
	{
		var username = $("#newUserName").val();
		if (validateEmail(username)) {
			$("#saveUserName").prop('disabled', false);
		} else {
			$("#saveUserName").prop('disabled', true);
		}
	});

	$("#saveUserName").click(function () {
		var username = $("#newUserName").val();
		var password = $("#password").val();

		$.ajax({
			type: "POST",
			url: "UpdateUserName?userName=" + username + "&password=" + password,   // This is terrible, but I don't have time to do it the right way
			success: function (data) {
				location.reload();
			}
		});
	});

	$("#saveRating").click(function () {
		var rating = $("#newRating").val()

		$.ajax({
			type: "POST",
			url: "UpdateRating?rating=" + rating,
			success: function (data) {
				location.reload();
			}
		});
	});

	$(".removeFilm").click(function () {
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

function validateEmail(email){
	var re = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
	var wtf = email.match(re);
	return email.match(re);
}