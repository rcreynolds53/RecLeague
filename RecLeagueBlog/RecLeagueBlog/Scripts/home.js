$(document).ready(function () {
    loadTags();
    $('#createTagBtn').on('click', function () {
        $('#addTagDiv').toggle('slow');
        $('#tagTableDiv').hide();
        $('editTagDiv').hide();
    });

    $('#addTagBtn').click(function (event) {
        // var haveValidationErrors = checkAndDisplayValidationErrors($('#addMovieFormDiv').find('input'));

        // if (haveValidationErrors) {
        //     return false;
        // }
        $.ajax({
            type: 'POST',
            url: 'http://localhost:60542/tag',
            data: JSON.stringify({
                tagName: $('#addTag').val()

            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#errorMessages').empty();
                $('#addTag').val('');
                $('#addTagDiv').hide('');
                $('#editTagDiv').hide('');
                loadTags();



            },
            error: function (jpXHR, textStatus, errorThrown) {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling webservice. Please try again later.'));

            }
        })
    });
});
function hideAddTag() {
    $('#tagTableDiv').show();
    $('#addTagDiv').hide();
}
function loadTags() {
    clearTagsTable();
    var contentRows = $('#contentRows');
    $('#addTagDiv').hide();
    $('#tagTableDiv').show();
    $('#editTagDiv').hide();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tags',
        success: function (tagsArray) {
            $.each(tagsArray, function (index, tag) {
                var tagName = tag.tagName;
                var tagId = tag.tagId;

                var row = '<tr>';
                row += '<td>' + tagName + '</td>';
                row += '<td><a onclick ="showEditTag(' + tagId + ')">Edit |</a><a onclick ="deleteTag(' + tagId + ')"> Delete</a></td>';
                row += '</tr>';

                contentRows.append(row);
            });
        },
        error: function (jpXHR, textStatus, errorThrown) {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling webservice. Please try again later.'));

        }
    });
}

$('#editTagBtn').click(function (event) {

    var tagId = $('#tagId').val();
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:60542/tag/' + tagId,
        data: JSON.stringify({
           tagId: parseInt(tagId),
            tagName: $('#editTag').val(),

        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function (data, status) {
            $('#errorMessages').empty();
            $('#editTag').val('');
            loadTags();



        },
        error: function (jpXHR, textStatus, errorThrown) {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling webservice. Please try again later.'));

        }
    })
});
function deleteTag(tagId) {

    var deleteTag = confirm("Are you sure you want to delete this tag from all posts?");
    if (deleteTag) {

        $.ajax({
            type: "DELETE",
            url: 'http://localhost:60542/tag/' + tagId,
            success: function () {
                loadTags();
            },
            error: function (jpXHR, textStatus, errorThrown) {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling webservice. Please try again later.'));
            }
        });
    }
}

$('')
function clearTagsTable() {
    $('#contentRows').empty();
}
function showEditTag(tagId) {
    $('#errorMessages').empty();
    $('#tagTableDiv').hide();
    $('#editTagDiv').show();
    $('#tagId').val(tagId);

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/tag/' + postId,
        success: function (tag, status) {
            $('#editTag').val(tag.tagName)

        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }

    });
};

function hideEditTagForm() {
    $('#tagTableDiv').show();
    $('#editTagDiv').hide();
}
function checkAndDisplayValidationErrors(input) {
    // clear displayed error message if there are any
    $('#errorMessages').empty();
    // check for HTML5 validation errors and process/display appropriately
    // a place to hold error messages
    var errorMessages = [];

    // loop through each input and check for validation errors
    input.each(function () {
        // Use the HTML5 validation API to find the validation errors
        if (!this.validity.valid) {
            var errorField = $('label[for=' + this.id + ']').text();
            errorMessages.push(errorField + ' ' + this.validationMessage);
        }
    });

    // put any error messages in the errorMessages div
    if (errorMessages.length > 0) {
        $.each(errorMessages, function (index, message) {
            $('#errorMessages').append($('<li>').attr({ class: 'list-group-item list-group-item-danger' }).text(message));
        });
        // return true, indicating that there were errors
        return true;
    } else {
        // return false, indicating that there were no errors
        return false;
    }
};
// $('#tags').tagsInput();
// $('#categories').tagsInput();

// function postTags() {
//     var tagsArray = [];
//     $.each($('#tags_tagsinput>.tag>span'), function (index, span) {
//         tagsArray.push(span.textContent.trim());

//     });
//     return tagsArray;
// }

// function postCategories() {
//     var categoriesArray = [];
//     $.each($('#categories_tagsinput>.tag>span'), function (index, span) {
//         categoriesArray.push(span.textContent.trim());

//     });
//     return categoriesArray;
// }
// function editTags() {
//     var tagsArray = [];
//     $.each($('#editTags_tagsinput>.tag>span'), function (index, span) {
//         tagsArray.push(span.textContent.trim());

//     });
//     return tagsArray;
// }

// function editCategories() {
//     var categoriesArray = [];
//     $.each($('#editCategories_tagsinput>.tag>span'), function (index, span) {
//         categoriesArray.push(span.textContent.trim());

//     });
//     return categoriesArray;
// }

