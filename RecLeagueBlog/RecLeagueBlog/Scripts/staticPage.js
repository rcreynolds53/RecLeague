$(document).ready(function () {
    loadPages();
});

function loadPages() {
    var pageListItem = $('#staticPageDropdown');
    $.ajax({
        type: 'GET',
        url: "/api/StaticPages",
        success: function (staticPageList) {
            $.each(staticPageList, function (index, page) {
                var title = page.title;
                var pageId = page.staticPageId;
                var li = '<li><a href = "http://localhost:60542/Home/StaticPage/' + pageId + '">' + title + '</a></li>';
                //var li = '<li><a href = "Home/StaticPage/' + pageId + '">' + title + '</a></li>';
                pageListItem.append(li);

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
