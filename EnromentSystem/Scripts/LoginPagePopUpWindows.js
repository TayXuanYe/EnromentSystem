// Get popups, close buttons and trigger links
var modal = document.getElementById("popUpWindows");
var openModalLink = document.getElementById("openModalLink");

openModalLink.onclick = function (event) {
    modal.style.display = "block";
}

window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
