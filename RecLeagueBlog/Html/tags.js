var url = "http://localhost:60542";

function fail(response) {
    console.log("FAILED!", response);
}

function getLastResult() {
    if (vue.searchCategory && vue.searchTerm) {
        search(vue.searchCategory, vue.searchTerm);
    } else {
        getAll();
    }
}

function getAll() {
    $.get(url + "/tags")
        .done(function (response) {
            vue.tags = response;
        }).fail(fail);
}

function post(tag) {
    $.ajax({
        type: "POST",
        url: url + "/tag",
        data: JSON.stringify(tag),
        contentType: "application/json"
    }).done(function (response) {
        getLastResult();
    }).fail(fail);
}

function put(tag) {
    $.ajax({
        type: "PUT",
        url: url + "/tag/" + tag.tagId,
        data: JSON.stringify(tag),
        contentType: "application/json"
    }).done(function (response) {
        getLastResult();
    }).fail(fail);
}

function remove(dvd) {
    $.ajax({
        type: "DELETE",
        url: url + "/tag/" + tag.tagId
    }).done(function (response) {
        getLastResult();
    }).fail(fail);
}

function search(searchCategory, searchTerm) {
    $.get(url + "/tags/" + searchCategory + "/" + searchTerm)
        .done(function (response) {
            vue.tags = response;
        }).fail(fail);
}

function save() {

    var tag = {
        "title": this.current.tagName,
        // "releaseYear": this.current.releaseYear,
        // "director": this.current.director,
        // "rating": this.current.rating,
        // "notes": this.current.notes
    };

    this.errorMessage = validate(tag);

    if (this.errorMessage) {
        return;
    }

    if (this.current.tagId) {
        tag.tagId = this.current.tagId;
        put(tag);
    } else {
        post(tag);
    }
    $("#modalForm").modal("hide");
}

function validate(tag) {
    var message = "";
    if (!tag.tagName) {
        message += "The tag name is required.<br />"
    }
    // var regex = /^\d{4}$/;
    // if (!regex.test(dvd.releaseYear)) {
    //     message += "Release Year must be a four digit number.<br />"
    // }

    // if (!dvd.director) {
    //     message += "The Director is required.<br />"
    // }
    return message;
}

var vue = new Vue({
    el: "#content",
    data: {
        searchCategory: "",
        searchTerm: "",
        searchIsInvalid: false,
        errorMessage: "",
        modalTitle: "Create Tag",
        current: {},
        tags: []
    },
    methods: {
        confirmDelete: function (tag) {
            this.current = tag;
        },
        executeDelete: function () {
            remove(this.current);
        },
        create: function () {
            this.modalTitle = "Create Tag";
            // this.current = {
            //     rating: "G"
            // };
            this.errorMessage = "";
        },
        edit: function (tag) {
            this.modalTitle = "Edit: " + tag.tagName;
            this.errorMessage = "";
            this.current = tag;
        },
        save: save,
        search: function () {
            var valid = this.searchCategory && this.searchTerm;
            this.searchIsInvalid = !valid;
            if (valid) {
                search(this.searchCategory, this.searchTerm);
            } else {
                setTimeout(function () { vue.searchIsInvalid = false; }, 2000);
            }
        },
        clear: function () {
            this.searchCategory = "";
            this.searchTerm = "";
            getAll();
        }
    }
});

getAll();