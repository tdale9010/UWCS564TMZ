//window.onload(function ()
//{
    var panelDiv = document.getElementById("home dashboard");

    for (var i = 0; i < 3; i++)
    {
        var row = document.createElement("div");
        row.className += "row";

        for (var j = 0; j < 3; j++)
        {
            var panel = document.createElement("div");
            panel.className += "panel panel-default col-md-4";
            var panelBody = document.createElement("div");
            panelBody.className += "panel-body";
            var text = document.createElement("p");
            text.innerHTML = "Test " + i;
            /*
                Populate with actual movie information
            */

            panelBody.appendChild(text);
            panel.appendChild(panelBody);
            row.appendChild(panel);
        }
        
        panelDiv.appendChild(row);
    }
//});

function showMovie(movie)
{
    $(function (movie) {
        $.ajax({
            type: 'POST',
            url: 'SqlController.cs/getMovie',
            data: JSON.stringify({ id: 1, retVal: [] }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: showMovie(retVal)
        });
    });
}