$(document).ready(function () {
    loadPosts();
    $('#createPostBtn').on('click', function () {
        $('#addPostDiv').toggle('slow');
        $('#postTableDiv').hide();
        $('editPostDiv').hide();
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
                content: $('#addPostContent').val().replace(/<\/?[^>]+>/gi, ''),
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
                $('#editPostDiv').hide('');
                loadPosts();


            },
            error: function (jpXHR, textStatus, errorThrown) {
                $('#errorMessages')
                    .append($('<li>')
                        .attr({ class: 'list-group-item list-group-item-danger' })
                        .text('Error calling webservice. Please try again later.'));

            }
        });
    });
});
function hideAddPostForm() {
    $('#postTableDiv').show();
    $('#addMovieFormDiv').hide();
}
function loadPosts() {
    clearMoviesTable();
    var contentRows = $('#contentRows');
    $('#addPostDiv').hide();
    $('#postTableDiv').show();
    $('#editPostDiv').hide();

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
                row += '<td><a onclick ="showEditPost(' + blogPostId + ')">Edit |</a><a onclick ="deletePost(' + blogPostId + ')"> Delete</a></td>';
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

$('#editPostBtn').click(function (event) {

    var postId = $('#postId').val();
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:60542/post/' + postId,
        data: JSON.stringify({
            blogPostId: parseInt(postId),
            title: $('#editPostTitle').val(),
            content: $('#editPostContent').val().replace(/<\/?[^>]+>/gi, ''),
            tagsToPost: editTags(),
            categories: editCategories()

        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function (data, status) {
            $('#errorMessages').empty();
            $('#editPostTitle').val('');
            $('#editPostContent').val('');
            loadPosts();



        },
        error: function (jpXHR, textStatus, errorThrown) {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling webservice. Please try again later.'));

        }
    });
});
function deletePost(postId) {

    var deletePost = confirm("Are you sure you want to delete this DVD from the collection?");
    if (deletePost) {

        $.ajax({
            type: "DELETE",
            url: 'http://localhost:60542/post/' + postId,
            success: function () {
                loadPosts();
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

function clearMoviesTable() {
    $('#contentRows').empty();
}
function showEditPost(postId) {
    $('#errorMessages').empty();
    $('#postTableDiv').hide();
    $('#editPostDiv').show();
    $('#postId').val(postId);

    var tagNames = "";
    var catagoryNames = "";
    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/post/' + postId,
        success: function (blogPost, status) {
            $('#editPostTitle').val(blogPost.title),
                $('#editPostContent').val(blogPost.content),
                $.each(blogPost.tags, function (index, tag) {
                    tagNames += String(tag.tagName) + ",";
                });
            $('#editTags').val(String(tagNames)),
                $('#editTags').tagsInput(),
                $.each(blogPost.categories, function (index, catagory) {
                    catagoryNames += String(catagory.categoryName) + ",";
                });
            $('#editCategories').val(String(catagoryNames)),

                $('#editCategories').tagsInput();


        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }

    });
}

function hideEditPostForm() {
    $('#postTableDiv').show();
    $('#editPostDiv').hide();
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
}
$('#tags').tagsInput();
$('#categories').tagsInput();

function postTags() {
    var tagsArray = [];
    $.each($('#tags_tagsinput>.tag>span'), function (index, span) {
        tagsArray.push(span.textContent.trim());

    });
    return tagsArray;
}

function postCategories() {
    var categoriesArray = [];
    $.each($('#categories_tagsinput>.tag>span'), function (index, span) {
        categoriesArray.push(span.textContent.trim());

    });
    return categoriesArray;
}
function editTags() {
    var tagsArray = [];
    $.each($('#editTags_tagsinput>.tag>span'), function (index, span) {
        tagsArray.push(span.textContent.trim());

    });
    return tagsArray;
}

function editCategories() {
    var categoriesArray = [];
    $.each($('#editCategories_tagsinput>.tag>span'), function (index, span) {
        categoriesArray.push(span.textContent.trim());

    });
    return categoriesArray;
}


// Ignore this for now, it doesnt work quite yet.


function searchFilter() {
    var searchTerm = $('#searchTerm').val();
    var filter = searchTerm.toUpperCase();
    var posts = document.getElementById('postTableDiv');
    var post = posts.getElementsByTagName('tr');
    
    for (i = 0; i < post.length; i++)
    {
        var postContents = post[i].getElementsByTagName('td');
    }
    for (j = 0; j < postContents.length; j++)
    {
        var items = postContents[j].getElementsByTagName('ul');
    }
    for (k = 0; k < items.length; k++) {
        var value = items[k].attr('value')[0];
        if (value.toUpperCase().indexOf(filter) > -1) {
            items[i].style.display = "";
        }
        else {
            items[i].style.display = "none";
        }

    }
  
}

