// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function showPassword() {
    var x = document.getElementById("pwd");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
function showPassword2() {
    var x = document.getElementById("pwd2");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}
