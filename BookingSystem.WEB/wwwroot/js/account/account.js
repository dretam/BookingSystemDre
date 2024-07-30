(function () {
    addLoginButtonListener();
}())

function addLoginButtonListener() {
    $(".login-button").click(function (event) {
        let username = $(".username").val();
        let password = $(".password").val();

        localStorage.setItem("username", username);
        localStorage.setItem("password", password);
    });
}