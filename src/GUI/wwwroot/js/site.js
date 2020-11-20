// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
function ajaxPost(element, handler) {
    const data = {
        handler
    };

    const siblings = element.parentElement.children;
    for (let i = 0; i < siblings.length; i += 1) {
        const element = siblings[i];

        if (element.tagName === "INPUT" && element.attributes["name"]) {
            data[element.attributes["name"].value] = encodeURIComponent(element.value);
        }
    }

    $.ajax({
        type: "POST",
        url: `/editor?${$.param(data)}`,
        contentType: "application/json",
        beforeSend: function (xhr) {
            xhr.setRequestHeader("XSRF-TOKEN",
                $('input:hidden[name="__RequestVerificationToken"]').val());
        },
        data: JSON.stringify(data),
        success: function (response) {
            console.log("success");
            document.location.reload();
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function dropdownButtonClick(element) {
    const children = element.parentElement.children;
    for (let i = 0; i < children.length; i++) {
        if (children[i].classList.contains("dropdown-content")) {
            children[i].classList.toggle("clicked");
        }
    }
}
