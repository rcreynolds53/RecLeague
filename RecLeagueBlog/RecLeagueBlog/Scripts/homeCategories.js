$(document).ready(function () {
    loadCategories();
    $('#createCategoryBtn').on('click', function () {
        $('#addCategoryDiv').toggle('slow');
        $('#categoryTableDiv').hide();
        $('editCategoryDiv').hide();
    });

    $('#addCategoryBtn').click(function (event) {
        // var haveValidationErrors = checkAndDisplayValidationErrors($('#addMovieFormDiv').find('input'));

        // if (haveValidationErrors) {
        //     return false;
        // }
        $.ajax({
            type: 'POST',
            url: 'http://localhost:60542/category',
            data: JSON.stringify({
                categoryName: $('#addCategory').val()

            }),
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            'dataType': 'json',
            success: function (data, status) {
                $('#errorMessages').empty();
                $('#addCategory').val('');
                $('#addCategoryDiv').hide('');
                $('#editCategoryDiv').hide('');
                loadCategories();



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
function hideAddCategory() {
    $('#categoryTableDiv').show();
    $('#addCategoryDiv').hide();
}
function loadCategories() {
    clearCategoriesTable();
    var contentRows = $('#contentRows');
    $('#addCategoryDiv').hide();
    $('#categoryTableDiv').show();
    $('#editCategoryDiv').hide();

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/categories',
        success: function (categoriesArray) {
            $.each(categoriesArray, function (index, category) {
                var categoryName = category.categoryName;
                var categoryId = category.categoryName;

                var row = '<tr>';
                row += '<td>' + categoryName + '</td>';
                row += '<td><a onclick ="showEditCategory(' + categoryId + ')">Edit |</a><a onclick ="deleteCategory(' + categoryId + ')"> Delete</a></td>';
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

$('#editCategoryBtn').click(function (event) {

    var categoryId = $('#categoryId').val();
    $.ajax({
        type: 'PUT',
        url: 'http://localhost:60542/category/' + categoryId,
        data: JSON.stringify({
            categoryId: parseInt(categoryId),
            categoryName: $('#editCategory').val()

        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function (data, status) {
            $('#errorMessages').empty();
            $('#editCategory').val('');
            loadCategories();



        },
        error: function (jpXHR, textStatus, errorThrown) {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling webservice. Please try again later.'));

        }
    });
});
function deleteCategory(categoryId) {

    var deleteCategory = confirm("Are you sure you want to delete this category from all posts?");
    if (deleteCategory) {

        $.ajax({
            type: "DELETE",
            url: 'http://localhost:60542/category/' + categoryId,
            success: function () {
                loadCategories();
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


function clearCategoriesTable() {
    $('#contentRows').empty();
}
function showEditCategory(categoryId) {
    $('#errorMessages').empty();
    $('#categoryTableDiv').hide();
    $('#editCategoryDiv').show();
    $('#categoryId').val(categoryId);

    $.ajax({
        type: 'GET',
        url: 'http://localhost:60542/category/' + categoryId,
        success: function (category, status) {
            $('#editCategory').val(category.categoryName);

        },
        error: function (jqXHR, testStatus, errorThrow) {
            $('#editErrorMessages').append($('<li>')
                .attr({ class: 'list-group-item list-group-item' })
                .text('Error communicating with web service'));
        }

    });
}

function hideEditCategoryForm() {
    $('#categoryTableDiv').show();
    $('#editCategoryDiv').hide();
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
