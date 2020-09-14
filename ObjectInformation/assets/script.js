//let polygonsData = [
//    {
//        content: "Content",
//        coords: [
//            {
//                lat: 43.223252591298646,
//                lng: 76.93992567211922
//            },
//            {
//                lat: 43.20298495801755,
//                lng: 76.9418139472657
//            },
//            {
//                lat: 43.219249642023044,
//                lng: 76.97357130200203
//            }
//        ],
//    },
//    {
//        content: 'Second',
//        coords: [
//            {
//                lat: 43.203252591298646,
//                lng: 76.893992567211922
//            },
//            {
//                lat: 43.20298495801755,
//                lng: 76.9118139472657
//            },
//            {
//                lat: 43.229249642023044,
//                lng: 76.93357130200203
//            }
//        ]
//    }
//];

//let polygons = [], size = 1;
//let instance;

document.addEventListener('DOMContentLoaded', function() {
   // instance = M.Modal.init(modal, {});
  //  initMap();
});

function initMap() {
    //let map = new google.maps.Map(document.getElementById('map'), {
    //    center: {lat: 43.238062, lng: 76.918468},
    //    zoom: 12,
    //   // mapTypeId: 'terrain',
    //   // draggableCursor:'crosshair'
    //});

    //if (polygonsData)
    //    polygonsData.forEach((poly, index) => {
    //        newPolygon(map, size - 1, poly);
    //        size++;
    //    });

    //let polygon = newPolygon(map, size - 1);
    //map.addListener('click', function(e) {
    //    const { coords } = polygons[size - 1];
    //    coords.push({ lat: e.latLng.lat(), lng: e.latLng.lng() });
    //    polygon.setOptions({ paths: coords });
    //});

    //const next = document.querySelector('#next');
    //next.addEventListener('click', function() {
    //    polygon = newPolygon(map, ++size - 1);
    //});
}

//function newPolygon(map, index, poly) {
//    console.log(index);
//    if (poly) {
//        polygons[index] = poly;
//    } else {
//        poly = {};
//        polygons[index] = {
//            coords: [],
//            content: ''
//        };
//    }
//    console.log(polygons);
//    let polygon = new google.maps.Polygon({
//        paths: poly.coords || [],
//        strokeColor: '#FF0000',
//        strokeOpacity: 0.8,
//        strokeWeight: 2,
//        fillColor: '#FF0000',
//        fillOpacity: 0.35
//    });
//    const item = index;
//    polygons[index].content = poly.content || '';
//    polygon.addListener('mouseover', function() {
//        this.setOptions({ fillOpacity: 0.5 });
//    });
//    polygon.addListener('mouseout', function() {
//        this.setOptions({ fillOpacity: 0.35 });
//    });

//    polygon.addListener('click', function() {
//        const modal = document.querySelector('#modal');
//        const textarea = modal.querySelector('textarea');
//        textarea.value = polygons[index].content;

//        const
//            btnSaveContent = modal.querySelector('#save-content'),
//            btnDeletePolygon = modal.querySelector('#delete-polygon');


//        instance.options.onCloseStart = () => {
//            recreateButton(btnDeletePolygon)
//            recreateButton(btnSaveContent);
//        }
//        instance.open();
//        btnSaveContent.addEventListener('click', function() {
//            polygons[index].content = textarea.value;
//            instance.close();
//            console.log(polygons);
//			  //PLACE AFTER BTN SAVE
//        });

//        btnDeletePolygon.addEventListener('click', function () {
//            console.log(index);
//            polygonsData = polygons.filter((a, i) =>
//                (i !== index && a.coords.length)
//            );
//            console.log('polyDta', polygonsData);
//            instance.close();
//            polygons = [];
//            size = 1;
//            initMap();
//        })

//    });
//    polygon.setMap(map);
//    return polygon;
//}

//const saveMap = document.querySelector('#save-map');

//saveMap.addEventListener('click', function() {
//     // Send data here
//    const data = JSON.stringify(polygons);
//    console.log(data);
//    console.log(JSON.parse(data));
//});

//function recreateButton(element) {
//    let clone = element.cloneNode();
//    while (element.firstChild)
//        clone.appendChild(element.lastChild);
//    element.parentNode.replaceChild(clone, element);
//}
