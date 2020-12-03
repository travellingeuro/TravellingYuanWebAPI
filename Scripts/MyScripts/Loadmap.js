"use strict";
//Defining map as a global variable to access from other functions
var map;
var polyline;
var lineCoordinates = [];
var lineCoordinatesAddress = [];
var qnotes;


async function getdata() {

    var disploader = document.getElementById("loader");
    disploader.style.display = "block";
    var s = document.getElementById('serialinput').value;
    let response = await fetch('https://travellingyuanwebapi.azurewebsites.net/api/notes/?serial=' + s);
    console.log(response);
    var k = document.getElementById('bill_message');
    var vermapa = document.getElementById('mapa');


    if (response.status !== 200) {
        disploader.style.display = "none";
        vermapa.style.display = 'none';
        k.innerHTML = "The bill  " + s + "  has not been tracked. Upload it with the app";
        k.scrollIntoView({ behavior: "smooth" });


    } else {
        disploader.style.display = "none";
        vermapa.style.display = 'block';
        k.innerHTML = "Yeah!, we've got bill " + s + " but you can do even more with the app";
        vermapa.scrollIntoView({ behavior: "smooth" });
        let data = await response.json();
        return (data);
    }

}

function populatelineCoordinates(qnotes) {
    var arr = qnotes;
    lineCoordinates = qnotes.map(function (item) {
        return [item.Latitude, item.Longitude];
    })
    console.log('lines', lineCoordinates);
    lineCoordinatesAddress = qnotes.map(function (item) {
        return [item.Latitude, item.Longitude, item.Address, item.Comments, item.UploadDate, item.Name];
    })
    //Enabling new cartography and themes
    google.maps.visualRefresh = true;

    //Setting starting options of map
    var mapOptions = {
        center: new google.maps.LatLng(lineCoordinates[0][0], lineCoordinates[0][1]),
        zoom: 8,
        mapTypeId: 'terrain',
        streetViewControl: false,
        mapTypeControl: false,
        fullscreenControl: false
    };

    //Getting map DOM element
    var mapElement = document.getElementById('mapa');

    //Creating a map with DOM element which is just obtained
    map = new google.maps.Map(mapElement, mapOptions);
    addAnimatedPolyline();
    addMarkers();
    zoom();

}

function addMarkers() {

    var image = {
        url: 'http://maps.google.com/mapfiles/kml/pal4/icon57.png',
        size: new google.maps.Size(32, 32),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(16, 16)
    };
    for (var i = 0; i < lineCoordinatesAddress.length; i++) {
        var place = lineCoordinatesAddress[i];
        var marker = new google.maps.Marker({
            icon: image,
            position: new google.maps.LatLng(place[0], place[1]),
            map: map,
            title: 'date: ' + new Date(place[4]).toLocaleDateString() + '\n' + 'comments: ' + place[3] + '\n' + place[5] + '\n' + place[2]
        }
        );
    }
}


function addAnimatedPolyline() {
    //First we iterate over the coordinates array to create a
    // new array which includes objects of LatLng class.
    var pointCount = lineCoordinates.length;
    var linePath = [];
    for (var i = 0; i < pointCount; i++) {
        var tempLatLng = new google.maps.LatLng(lineCoordinates[i][0], lineCoordinates[i][1]);
        linePath.push(tempLatLng);
    }

    // Defining arrow symbol
    var arrowSymbol = {
        strokeColor: '#464f53',
        strokeOpacity: 1,
        scale: 3,
        fillOpacity: 1,
        path: google.maps.SymbolPath.FORWARD_CLOSED_ARROW
    };

    // Polyline properties are defined below
    var lineOptions = {
        path: linePath,
        icons: [{
            icon: arrowSymbol
            //offset: '100%'
        }],
        strokeWeight: 3,
        geodesic: true,
        strokeColor: '#98a3a7',
        strokeOpacity: 1
    }
    polyline = new google.maps.Polyline(lineOptions);

    // Polyline is set to current map
    polyline.setMap(map);

    // Calling the arrow animation function
    animateArrow();
}

function animateArrow() {
    var counter = 0;
    var accessVar = window.setInterval(function () {
        counter = (counter + 1) % 350;

        var arrows = polyline.get('icons');
        arrows[0].offset = (counter / 2) + '%';
        polyline.set('icons', arrows);
    }, 50);
}
function zoom() {
    var bounds = new google.maps.LatLngBounds();
    polyline.getPath().forEach(function (latLng) {
        bounds.extend(latLng);
    });
    map.fitBounds(bounds);
}