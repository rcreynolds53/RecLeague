//var url = "http://localhost:60542";

$(document).ready(function () {
    loadDvds();

    $('#backToMainPage').click(function (event) {
        showMainPage();
    });

    $('#createDvdButton').click(function (event) {
        clearDvdTable();
        clearErrMessage();
        $('#pageContent').children().hide();
        $('#add-content').show();
        $('#addErrorMessages').show();
    });

    $('#addDvdButton').click(function (event) {
        var checkYear = "^[0-9]{4}$";
        if ($('#add-title').val() === null) {
            $('#addErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('A valid tag name is required'));
        }
        else {
            clearErrMessage();
            addDVD();
        }
    });

    $('#cancelAddButton').click(function (event) {
        showMainPage();
    });

    $('#searchButton').click(function (event) {

        clearDvdTable();

        var category = $('#searchCategoryDropList').val();
        var term = $('#searchTerm');
        var contentRows = $('#contentRows');

        if (category === null || term === null) {
            loadDvds();
            $('#errorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Both search category and search term are required'));
        }
        else {
            clearErrMessage();
            searchDVD(category, term, contentRows);
        }
    });

    $('#editSaveButton').click(function (event) {
        var checkYear = "^[0-9]{4}$";
        if ($('#edit-title').val() === null || $('#edit-year').val() === null || !$('#edit-year').val().match(checkYear)) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Title and valid year (2015) entry is required'));
        }
        else {
            clearErrMessage();
            editDVD();
        }
    });

    $('#cancelEditButton').click(function (event) {
        showMainPage();
    });

    $('#clearSearchButton').click(function (event) {
        $('#searchCategoryDropList').val('');
        $('#searchTerm').val('');
        showMainPage();
    });
});

function addDVD() {

    $.ajax({
        type: 'POST',
        url: 'http://localhost:60542/tag',
        data: JSON.stringify({
            tagName: $('#add-title').val(),

        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function () {
            clearDvdTable();
            $('#pageContent').children().hide();
            $('#mainContent').show();
            loadDvds();
        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#addErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }
    });
}

function searchDVD(category, term, contentRows) {
    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tags/' + category + '/' + term.val(),
        success: function (dvdArray) {
            $.each(dvdArray, function (index, dvd) {
                var title = dvd.title;
                var releaseYear = dvd.releaseYear;
                var director = dvd.director;
                var rating = dvd.rating;
                var dvdId = dvd.dvdId;

                var row = '<tr>';
                row += '<td><a onclick ="displayDvd(' + dvdId + ')">' + title + '</a></td>';
                row += '<td>' + releaseYear + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';
                row += '<td><a onclick ="showEditDvd(' + dvdId + ')">Edit</a></td>';
                row += '<td><a onclick ="deleteDvd(' + dvdId + ')">Delete</a></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#errorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }
    });
}

function clearErrMessage() {
    $('#errorMessages').empty();
}

function clearDvdTable() {
    $('#contentRows').empty();
}

function loadDvds() {

    clearDvdTable();

    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tags',
        success: function (dvdArray) {
            $.each(dvdArray, function (index, tag) {
                var tagName = tag.tagName;
                var tagId = tag.tagId;

                var row = '<tr>';
                row += '<td><a onclick ="displayDvd(' + tagId + ')">' + tagName + '</a></td>';
                row += '<td><a onclick ="showEditDvd(' + tagId + ')">Edit</a></td>';
                row += '<td><a onclick ="deleteDvd(' + tagId + ')">Delete</a></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function (jqXHR, testStatus, errorThrow) {
        }
    });
}

function showEditDvd(tagId) {
    clearDvdTable();
    clearErrMessage();
    $('#pageContent').children().hide();
    $('#edit-content').show();
    $('#editErrorMessages').show();
    $('#textDvdId').val(tagId);

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tag/' + tagId,
        success: function (tag, status) {
            $('#edit-title').val(tag.tagName);
        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }
    });
}

function editDVD() {

    var editId = $('#textDvdId').val();

    $.ajax({
        type: 'Put',
        url: 'http://localhost:60542/tag/' + editId,
        data: JSON.stringify({
            dvdId: $('#textDvdId').val(),
            title: $('#edit-title').val(),
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function () {
            $('#pageContent').children().hide();
            $('#mainContent').show();
            loadDvds();
        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#addErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }
    });
}

function deleteDvd(tagId) {

    var result = confirm("Are you sure you want to delete this DVD?");

    if (result) {
        $.ajax({
            type: 'DELETE',
            url: 'http://localhost:60542/tag/' + tagId,
            success: function () {
                loadDvds();
            },
            error: function (jqXHR, testStatus, errorThrow) {
                $('#addErrorMessages').append($('<li>')
                    .attr({ class: 'list-group-item list-group-item' })
                    .text('Error communicating with web service'));
            }
        });
    }
}

function displayDvd(tagId) {
    clearDvdTable();
    $('#pageContent').children().hide();
    $('#display-content').show();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tag/' + tagId,
        success: function (tag) {
            $('#displayTitle').text(tag.tagName);
        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }
    });
}

function showMainPage() {
    $('#pageContent').children().hide();
    $('#mainContent').show();
    clearDvdTable();
    loadDvds();
}