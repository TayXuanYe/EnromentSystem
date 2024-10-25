// Get popups, close buttons and trigger links
var modalTerms = document.getElementById("popUpWindows");
var modalForgetPassword = document.getElementById("forgetPassword");
var openModalLink = document.getElementById("openModalLink");
var openModalLinkForgetPassword = document.getElementById("forgetPasswordWindows");

openModalLink.onclick = function (event) {
    modalTerms.style.display = "block";
}
openModalLinkForgetPassword.onclick = function (event) {
    modalForgetPassword.style.display = "block";
}

window.onclick = function (event) {
    if (event.target == modalTerms) {
        modalTerms.style.display = "none";
    }

    if (event.target == modalForgetPassword) {
        modalForgetPassword.style.display = "none";
    }
}
