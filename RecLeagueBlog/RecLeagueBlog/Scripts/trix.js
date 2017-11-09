$("#searchTerm").keyup(function () {
    var value = this.value.toLowerCase().trim();

    $("div.panel.panel-default").each(function (index) {
        if (!index) return;
        $(this).find("h2, p, h3").each(function () {
            var id = $(this).text().toLowerCase().trim();
            var not_found = (id.indexOf(value) == -1);
            $(this).closest('div.panel.panel-default').toggle(!not_found);
            return not_found;
        });
    });
})