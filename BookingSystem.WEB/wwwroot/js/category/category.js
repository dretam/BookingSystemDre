(function () {
    addCloseButtonListener();
    addDeleteButtonListener();
    getAuthToken();
}())

function getAuthToken() {
    let ipPort = "http://localhost:5085";
    let endPoint = `${ipPort}/api/Account/authentication`;
    let username = localStorage.getItem("username");
    let password = localStorage.getItem("password");
    let dto = {
        "username": username,
        "password": password,
        "subject": "Test",
        "secretKey": "generate-secret-key-for-client-side-request-liberate-tutum-ex-inferis-ad-astra-per-aspera",
        "audience": "Indocyber"
    }

    $.ajax({
        method: "POST",
        url: endPoint,
        data: JSON.stringify(dto),
        contentType: "application/json",
        success: function (response) {
            localStorage.setItem("token", response.token);
            localStorage.removeItem("password");
        },
        error: function (response) {
            console.log(response);
        }
    });
}

function getBearerToken() {
    let token = localStorage.getItem("token");
    let bearerToken = {
        "Authorization": `Bearer ${token}`
    }
    return bearerToken;
}

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
        let categoryId = $(this).attr("data-id");
        $.ajax({
            method: "DELETE",
            headers: getBearerToken(),
            url: `http://localhost:5085/api/category/${categoryId}`,
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
    $('div.category-dependency-dialog form div.dependencies').text(response.responseText);
}