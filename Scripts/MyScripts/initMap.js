function initMap() {
    getdata().then(data => populatelineCoordinates(data));
}