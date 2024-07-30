(function () {
    addCloseButtonListener();
    addDeleteButtonListener();
    // getAuthenticationToken();
}())

/*
function getAuthenticationToken() {
    let ipPort = "http://localhost:5085";
    let endPoint = `${ipPort}/api/Account/authentication`;
    let payload = {
        "username": "ali",
        "password": "123",
        "subject": "Tryout",
        "secretKey": "generate-secret-key-for-client-side-request-liberate-tutum-ex-inferis-ad-astra-per-aspera",
        "audience": "BasiliskWebUI"
    };
    $.ajax({
        type: "POST",
        url: endPoint,
        data: JSON.stringify(payload),
        contentType: "application/json",
        success: function (response) {
            saveAuthenticationToken(response);
        }
    });
}

function saveAuthenticationToken({ token, username, role }) {
    localStorage.setItem("token", token);
    localStorage.setItem("username", username);
    localStorage.setItem("role", role);
}
*/

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
        let productId = $(this).attr("data-id");
        $.ajax({
            method: "DELETE",
            headers: getBearerToken(),
            url: `http://localhost:5085/api/product/${productId}`,
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
    $('div.product-dependency-dialog form div.dependencies').text(response.responseText);
}