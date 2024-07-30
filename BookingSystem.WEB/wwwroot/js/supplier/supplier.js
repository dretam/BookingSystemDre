(function () {
    addCloseButtonListener();
    addDeleteButtonListener();
}())

function addCloseButtonListener() {
    $('.close-button').click(function (event) {
        $('.modal-layer').removeClass('modal-layer-opened');
        $('.form-dialog').removeClass('popup-dialog-opened');
        $('.form-dialog input').val("");
        $('.form-dialog textarea').val("");
        $('.form-dialog .validation-message').text("");
    });
}

function addDeleteButtonListener() {
    $('.delete-button').click(function (event) {
        event.preventDefault();
        let supplierId = $(this).attr("data-id");
        $.ajax({
            method: "DELETE",
            url: `http://localhost:5085/api/supplier/${supplierId}`,
            success: function (response) {
                location.reload();
            },
            error: function (response) {
                console.log(response.responseText);
                populateFormDependencies(response);
                $('.modal-layer').addClass('modal-layer-opened');
                $('.popup-dialog').addClass('popup-dialog-opened');
            }
        });
    });
}

function populateFormDependencies(response) {
    $('div.supplier-dependency-dialog form div.dependencies').text(response.responseText);
}