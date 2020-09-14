let instance, modalContent;
const polygons = [
    {
        content: "Content",
        coords: [
            {
                lat: 43.223252591298646,
                lng: 76.93992567211922
            },
            {
                lat: 43.20298495801755,
                lng: 76.9418139472657
            },
            {
                lat: 43.219249642023044,
                lng: 76.97357130200203
            }
        ],
    },
    {
        content: 'Second',
        coords: [
            {
                lat: 43.203252591298646,
                lng: 76.893992567211922
            },
            {
                lat: 43.20298495801755,
                lng: 76.9118139472657
            },
            {
                lat: 43.229249642023044,
                lng: 76.93357130200203
            }
        ]
    }
];

document.addEventListener('DOMContentLoaded', function() {
    instance = M.Modal.init(modal, {});
    modalContent = document.querySelector('#modal .modal-content');
    initMap();
});

function initMap() {
    let map = new google.maps.Map(document.getElementById('map'), {
        zoom: 13,
        center: {lat: 43.238062, lng: 76.918468},
        mapTypeId: 'terrain'
    });
    polygons.forEach(poly => newPolygon(map, poly));
}

function newPolygon(map, poly) {
    let polygon = new google.maps.Polygon({
        paths: poly.coords,
        strokeColor: '#FF0000',
        strokeOpacity: 0.8,
        strokeWeight: 2,
        fillColor: '#FF0000',
        fillOpacity: 0.35
    });
    polygon.addListener('mouseover', function() {
        this.setOptions({ fillOpacity: 0.5 });
    });
    polygon.addListener('mouseout', function() {
        this.setOptions({ fillOpacity: 0.35 });
    });

    polygon.addListener('click', function() {
        modalContent.textContent = poly.content;
        instance.open();
    });
    polygon.setMap(map);
}
