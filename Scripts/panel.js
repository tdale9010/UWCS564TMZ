window.onload = display;



function display() {

    var panelDiv = document.getElementById("home dashboard");


    // insert arbitrary number of movies to add
    for (var i = 0; i < 9; i++) {
        var row = createRow();

        for (var j = 0; j < 3; j++) {
            var panel, panelBody

            var panelObject = [panel, panelBody, row];

            createPanel(panelObject);

            /*
            var panel = document.createElement("div");
            panel.className += "panel panel-default col-md-4";
            var panelBody = document.createElement("div");
            panelBody.className += "panel-body";
            var text = document.createElement("p");
            text.innerHTML = "Test " + i;
            

            panelBody.appendChild(text);
            panel.appendChild(panelBody);
            row.appendChild(panel);
            */
            var content = [];

            movieQuery(1, panelObject, content);
        }

        panelDiv.appendChild(row);
    }
};

// create a new row to display content
function createRow() {
    var row = document.createElement("div");
    row.className += "row";
    return row;
};

// create a panel for our content
function createPanel(panelObject) {
    panelObject[0] = document.createElement("div");
    panelObject[0].className += "panel panel-default col-md-4";
    panelObject[1] = document.createElement("div");
    panelObject[1].className += "panel-body";
    return;
};

function addContent(panelObject, content) {
    var info = document.createElement("p");
    info.innerHTML = content.ID;

    panelObject[1].appendChild(info);
    panelObject[0].appendChild(panelObject[1]);
    panelObject[2].appendChild(panelObject[0]);
    return;
};

function movieQuery(movie, panelObject, retVal) {
    $(function (movie) {
        $.ajax({
            type: 'POST',
            url: 'Movies',
            data: JSON.stringify({
                'ID': movie,
                'Title': [],
                'Director': [],
                'Actors': [],
                'Country': [],
                'Locations': [],
                'Tags': [],
                'Genres': [],
                'ImageURL': [],
                'Year': [],
                'Rating': []
            }),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (data) {
                addContent(panelObject, data);
            }
        });
    });
};