$(document).ready(function () {
    loadPosts();
    $('#create-dvd-button').on('click', function () {
        $('#addMovieFormDiv').toggle('slow');
        $('#dvdTableDiv').hide();
        $('editMovieFormDiv').hide();
    });

    $('#addPostBtn').click(function (event) {
        // var haveValidationErrors = checkAndDisplayValidationErrors($('#addMovieFormDiv').find('input'));

        // if (haveValidationErrors) {
        //     return false;
        // }
        $.ajax({
            type: 'POST',
            url: 'http://localhost:60542/post',
            data: JSON.stringify({
                title: $('#addPostTitle').val(),
                content: $('#addPostContent').val(),
                tagsToPost: postTags(),
                categories: postCategories()
                                
            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#errorMessages').empty();
                $('#addPostTitle').val('');
                $('#addPostContent').val('');
                loadPosts();



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
function hideAddPostForm() {
    $('#dvdTableDiv').show();
    $('#addMovieFormDiv').hide();
}
function loadPosts() {
    clearMoviesTable();
    var contentRows = $('#contentRows');
    $('#addMovieFormDiv').hide();
    $('#dvdTableDiv').show();
    $('#editMovieFormDiv').hide();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/posts',
        success: function (blogPostArray) {
            $.each(blogPostArray, function (index, post) {
                var title = post.title;
                var blogPostId = post.blogPostId;
                var blogPostTags = post.tags;
                var blogPostCats = post.categories;

                var row = '<tr>';
                row += '<td>' + title + '</td>';
                row += '<td><ul>';
                $.each(blogPostTags, function (index, tags) {
                    var tagName = tags.tagName;
                    row += '<li>' + tagName + '</li>';
                });
                row += '</ul></td>';
                row += '<td><ul>';
                $.each(blogPostCats, function (index, categories) {
                    var categoryName = categories.categoryName;
                    row += '<li>' + categoryName + '</li>';
                });
                row += '</ul></td>';
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
function deleteMovie() {
    var deleteMovie = confirm("Are you sure you want to delete this DVD from the collection?");
    if (deleteMovie = true) {
        function deleteMovieFromCollection(postId) {
            $.ajax({
                type: "DELETE",
                url: 'http://localhost:60542/post/' + postId,
                success: function (status) {
                    loadContacts();
                }
            });
        }
    }
}
function clearMoviesTable() {
    $('#contentRows').empty();
}
function showEditForm() {
    $('#errorMessages').empty();

    $('#dvdTableDiv').hide();
    $('#editMovieFormDiv').show();
}
function hideEditPostForm() {
    $('#dvdTableDiv').show();
    $('#editMovieFormDiv').hide();
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

$('#tags').tagsInput();
$('#categories').tagsInput();
$(function () {
    $('textarea').froalaEditor()
});

function postTags() {
    var tagsArray = [];
    $.each($('#tags_tagsinput>.tag>span'), function(index, span) {
        tagsArray.push(span.textContent.trim());
    
    });
    return tagsArray;
}

function postCategories() {
    var categoriesArray = [];
    $.each($('#categories_tagsinput>.tag>span'), function(index, span) {
        categoriesArray.push(span.textContent.trim());
    
    });
    return categoriesArray;
}

