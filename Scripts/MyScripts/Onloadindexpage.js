window.onload = function () {
    var h = window.location.hash;
    if (h !== "") { document.getElementById('serialinput').value = h.substring(1); initMap(); }
};