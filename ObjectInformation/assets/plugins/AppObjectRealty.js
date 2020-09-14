var AppObjectRealty = function () {

    var polygonsData = [];
    var polygons = [], size = 1;
    var instance;

    var lat = 0;
    var lng = 0;

    var map = null;

    function newPolygon(map, index, poly) 
    {
        console.log("newPolygon index: "+index);

        if (poly) 
        {
            polygons[index] = poly;
        } 
        else 
        {
            poly = {};
            polygons[index] = {
                coords: [],
                PolygonName: '',
                PolygonDescription: '',
                PolygonId: 0,
                ObjectRealtyId: $("#ObjectRealtyId").val()
            };
        }


        
        var polygon = new google.maps.Polygon({
            paths: poly.coords || [],
            strokeColor: '#FF0000',
            strokeOpacity: 0.8,
            strokeWeight: 2,
            fillColor: '#FF0000',
            fillOpacity: 0.35
        });
        var item = index;
        polygons[index].PolygonName = poly.PolygonName || '';
        polygons[index].PolygonDescription = poly.PolygonDescription || '';

        polygon.addListener('mouseover', function () {
            this.setOptions({ fillOpacity: 0.5 });
        });
        polygon.addListener('mouseout', function () {
            this.setOptions({ fillOpacity: 0.35 });
        });

        polygon.addListener('click', function () 
        {
            $("#PolygonId").val( polygons[index].PolygonId); 
            $("#PolygonName").val( polygons[index].PolygonName); 
            $("#PolygonDescription").val(polygons[index].PolygonDescription);


            $('#modifyObjectPolygon').modal('show');

            if (polygons[index].PolygonId != 0) {
                $("#save-polygon").val("Редактировать");
                
            } else {
                $("#save-polygon").val("Добавить");
            }


            $("#save-polygon").on('click', function () {
                console.log("save-polygon: "+polygons[index].PolygonId);

                polygons[index].PolygonName = $("#PolygonName").val();
                polygons[index].PolygonDescription = $("#PolygonDescription").val(); 
                polygons[index].PolygonId = $("#PolygonId").val();

                // Send data here
                var data = JSON.stringify(polygons[index]);
                console.log(data);

                $.post('/ObjectRealty/SavePolygonData', { json: data, objectRealtyId: $("#ObjectRealtyId").val(), lat: map.getCenter().lat(), lng: map.getCenter().lng(), zoom:  map.getZoom() }, function(data) {
                    $('#modifyObjectPolygon').modal('hide');
                    InitMap(map.getZoom(), $("#ObjectRealtyId").val());
                });
            });

            
            $("#delete-polygon").on('click', function () {
                console.log("delete-polygon: "+polygons[index].PolygonId);
                //polygonsData = polygons.filter((a, i) =>
                //    (i !== index && a.coords.length)
                //);
                //polygons = [];
                //size = 1;

                $.getJSON('/ObjectRealty/DeletePolygon', { polygonId: polygons[index].PolygonId }, function(json) {}).done(function(dataCountryFrom) 
                {
                    $('#modifyObjectPolygon').modal('hide');
                    InitMap(map.getZoom(), $("#ObjectRealtyId").val());
                });
            });

            //const modal = document.querySelector('#modal');
            //const textarea = modal.querySelector('textarea');
            //textarea.value = polygons[index].content;

            //const
            //    btnSaveContent = modal.querySelector('#save-content'),
            //    btnDeletePolygon = modal.querySelector('#delete-polygon');


            //instance.options.onCloseStart = () => {
            //    recreateButton(btnDeletePolygon)
            //    recreateButton(btnSaveContent);
            //}
            //instance.open();


            //btnSaveContent.addEventListener('click', function () {
            //    polygons[index].content = textarea.value;
            //    instance.close();
            //    console.log(polygons);
            //    //PLACE AFTER BTN SAVE
            //});

            //btnDeletePolygon.addEventListener('click', function () {
            //    console.log(index);
            //    polygonsData = polygons.filter((a, i) =>
            //        (i !== index && a.coords.length)
            //    );
            //    console.log('polyDta', polygonsData);
            //    instance.close();
            //    polygons = [];
            //    size = 1;
            //    initMap();
            //})

        });

        polygon.setMap(map);
        return polygon;
    }

    var saveMap = document.querySelector('#save-map');

    function recreateButton(element) {
        var clone = element.cloneNode();
        while (element.firstChild)
            clone.appendChild(element.lastChild);
        element.parentNode.replaceChild(clone, element);
    }

    function CoordMapType(tileSize) {
        this.tileSize = tileSize;
    }

    function InitMap(zoom, ObjectRealtyId) {

        //console.log("----");
        //console.log(lat);
        //console.log(lng);
        //console.log(parseFloat(zoom));
        //console.log(parseFloat(lng));
        //console.log("----");


        $.getJSON('/ObjectRealty/GetPolygons', { objectRealtyId: ObjectRealtyId }, function(json) {
        }).done(function(data) {

            $('#modifyObjectPolygon').modal('hide');
            polygonsData = data;

            //console.log("find Polugon");
            //console.log(polygonsData);

            var mapProp = { 
                center: myCenter, 
                zoom: parseInt(zoom), mapTypeId: google.maps.MapTypeId.ROADMAP 
            }; 

            map = new google.maps.Map(document.getElementById("map"), mapProp); 
            marker = new google.maps.Marker({ 
                position: myCenter, 
                draggable: false, 
                animation: google.maps.Animation.DROP, 
            }); 

            marker.setMap(map); 


            google.maps.event.trigger(map, 'resize'); 

            // Force the center of the map
            map.setCenter(marker.getPosition());


            //map= new google.maps.Map(document.getElementById('map'), {
            //    zoom: 5,
            //    center: {lat: 51.15899563720311, lng: 71.47267635604396},
            //    mapTypeId: 'terrain',
            //    draggableCursor:'crosshair',
            //    mapTypeControlOptions: {
            //        mapTypeIds: ['roadmap', 'satellite', 'hybrid', 'terrain','styled_map']
            //    }
            //});
            //console.log("map.getCenter().lat(): "+map.getCenter().lat());
            //console.log("map.getCenter().lng(): "+map.getCenter().lng());

            $("#map").width($(".form-actions").width());

            if (polygonsData)
                polygonsData.forEach((poly, index) => {
                    newPolygon(map, size - 1, poly);
                    size++;
                });

            var polygon = newPolygon(map, size - 1);

            map.addListener('click', function(e) {

                console.log("eLat: "+ e.latLng.lat());
                console.log("eLat: "+ e.latLng.lng());
               
                const { coords } = polygons[size - 1];
                coords.push({ lat: e.latLng.lat(), lng: e.latLng.lng() });
                polygon.setOptions({ paths: coords });
            });

            //const next = document.querySelector('#next');
            //next.addEventListener('click', function() {
            //    polygon = newPolygon(map, ++size - 1);
            //});

            //saveMap.addEventListener('click', function () {
            //    // Send data here
            //    const data = JSON.stringify(polygons);
            //    console.log(data);
            //    console.log(JSON.parse(data));

            //    console.log("getZoom: "+ map.getZoom());
            //    // mapObject.getZoom();



            //    $.post('/ObjectRealty/SavePolygonData', { json: data, objectRealtyId: $("#ObjectRealtyId").val(), lat: map.getCenter().lat(), lng: map.getCenter().lng(), zoom:  map.getZoom() }, function(data) {

            //    });
            //});

            google.maps.event.addListenerOnce(map, 'idle', function() {
                google.maps.event.trigger(map, 'dragend');
                console.log("trigger dragend");
           
                this.set('dragging',false);
                google.maps.event.trigger(this,'idle',{});
            });
        });

       
    }

    function getFormData($form){
        var unindexed_array = $form.serializeArray();
        var indexed_array = {};

        $.map(unindexed_array, function(n, i){
            indexed_array[n['name']] = n['value'];
        });

        return indexed_array;
    }

    return {
        initMap: function(lat, lng, objectRealtyId) {
            InitMap(lat, lng, objectRealtyId);
        },

        init: function (tab, objectRealtyId) {

            //$('.maskNumber').maskNumber({integer: false});

            $('.nav-tabs a[href="#' + tab + '"]').tab('show');

            $.post('/AjaxApi/GetObjectTypes', function (data) {
                $('#ObjectTypeId' + ' option').remove();
                $('#ObjectTypeId').append($("<option />").val("").text(""));

                for (var i = 0; i < data.length; i++) {
                    $("#ObjectTypeId").append($("<option/>").val(data[i].id).text(data[i].name));
                }
            });

            $.post('/AjaxApi/GetCurrency', function (data) {
                $('#CurrencyId' + ' option').remove();
                $('#CurrencyId').append($("<option />").val("").text(""));

                for (var i = 0; i < data.length; i++) {
                    $("#CurrencyId").append($("<option/>").val(data[i].id).text(data[i].name));
                }
            });

            $.post('/AjaxApi/GetDocumentType', function (data) {
                $('#DocumentTypeId' + ' option').remove();
                $('#DocumentTypeId').append($("<option />").val("").text(""));

                for (var i = 0; i < data.length; i++) {
                    $("#DocumentTypeId").append($("<option/>").val(data[i].id).text(data[i].name));
                }
            });

            $.post('/AjaxApi/GetPledgers', function (data) {
                $('#PledgersId' + ' option').remove();
                $('#PledgersId').append($("<option />").val("").text(""));

                for (var i = 0; i < data.length; i++) {
                    $("#PledgersId").append($("<option/>").val(data[i].id).text(data[i].name));
                }
            });

            var jqXHRData = null;
            $('#FileUploadImage').fileupload({
                url: '/ObjectRealty/UploadImages?objectId=' + objectRealtyId + "&uploadId=" + $("#UploadId").val(),
                dataType: 'json',
                acceptFileTypes: /(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$/i,
                add: function (e, data) {
                    jqXHRData = data;
                    console.log('-*-');
                    console.log(data.files[0].type);
                    console.log(data.files);
                    if (data.files[0].type != 'image/jpeg' && data.files[0].type != 'image/png') {
                        $("#tbx-file-path").val("No file chosen...");
                        alert(data.files[0].name + ' не соответствует формату!');
                    }
                },

                done: function (event, data) {
                    //console.log('done: ' + data.result.isUploaded);
                    //console.log(data);
                    if (data.result.isUploaded) {
                        //$("#tbx-file-path").val("No file chosen...");
                        window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=images");
                    }
                    //alert(data.result.message);
                },

                fail: function (event, data) {
                    console.log(data);
                    alert(data.jqXHR.responseJSON.message);
                    //LoadPhotos();
                }
            });

            $("#hl-start-upload").on('click', function () {
                if (jqXHRData) {
                    jqXHRData.submit();
                } else {
                    var upload = getFormData($("#modifyObjectImage form"));
                    event.preventDefault();
                    console.log(JSON.stringify(upload));

                    $.post('/ObjectRealty/EditUpload', {json:JSON.stringify(upload) },function(data) {
                        window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=images");
                    });
                }
                
                return false;
            });

            $("#BtnUploadImage").click(function () {
                $('#FileUploadImage').click();
            });

            $("#FileUploadImage").on('change', function () {

                $("#tbx-file-path").val(this.files[0].name);
                if (this.files[0].type != 'image/jpeg' && this.files[0].type != 'image/png') {

                    $("#tbx-file-path").val("...");
                }
            });


            $('#FileUploadDoc').fileupload({
                
                url: '/ObjectRealty/UploadDocument?objectId=' + objectRealtyId + "&uploadId=" + $("#DocUploadId").val(),
                dataType: 'json',
                //acceptFileTypes: /(.*?)\.(jpg|jpeg|png|gif|JPG|JPEG|PNG|GIF)$/i,
                add: function (e, data) {
                    jqXHRData = data;
                    
                    //console.log('-*-');
                    //console.log(data.files[0].type);
                    //console.log(data.files);
                    //if (data.files[0].type != 'image/jpeg' && data.files[0].type != 'image/png') {
                    //    $("#tbx-file-path").val("No file chosen...");
                    //    alert(data.files[0].name + ' не соответствует формату!');
                    //}
                },

                done: function (event, data) {
                    //console.log('done: ' + data.result.isUploaded);
                    //console.log(data);
                    if (data.result.isUploaded) {
                        //$("#tbx-file-path").val("No file chosen...");
                        window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=documents");
                    }
                    //alert(data.result.message);
                },

                fail: function (event, data) {
                    console.log(data);
                    alert(data.jqXHR.responseJSON.message);
                    LoadPhotos();
                }
            });

            $("#hl-start-upload-doc").on('click', function () {
                if (jqXHRData) {
                    jqXHRData.submit();
                } else {
                    var upload = getFormData($("#modifyObjectDocument form"));
                    event.preventDefault();
                    console.log(JSON.stringify(upload));

                    $.post('/ObjectRealty/EditUpload', {json:JSON.stringify(upload) },function(data) {
                        window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=documents");
                    });
                }
                
                return false;
            });

            $("#BtnUploadDoc").click(function () {
                $('#FileUploadDoc').click();
            });

            $("#FileUploadDoc").on('change', function () {

                $("#tbx-file-path-doc").val(this.files[0].name);
                //if (this.files[0].type != 'image/jpeg' && this.files[0].type != 'image/png') {

                //    $("#tbx-file-path-doc").val("...");
                //}
            });
        },

        EventLocation: function (CountryId, RegionId, CityId, DistrictId, ObjectTypeId, CurrencyId) {
            $.when(
               $.post('/AjaxApi/GetCountry', function (data) {
                   $('#CountryId' + ' option').remove();
                   $('#CountryId').append($("<option />").val("").text(""));

                   for (var i = 0; i < data.length; i++) {
                       $("#CountryId").append($("<option/>").val(data[i].id).text(data[i].name));
                   }
               })).then(function () {
                   $('#CountryId').on('change', function () {
                       $.post('/AjaxApi/GetRegionsInCountry', { countryId: $(this).val() }, function (data) {
                           $('#RegionId' + ' option').remove();
                           $('#RegionId').append($("<option />").val("").text(""));

                           for (var i = 0; i < data.length; i++) {
                               $("#RegionId").append($("<option/>").val(data[i].id).text(data[i].name));
                           }
                       });

                       $("#RegionId").removeAttr("disabled");

                       $("#CityId").attr("disabled", "disabled");
                       $("#DistrictId").attr("disabled", "disabled");

                       $('#CityId' + ' option').remove();
                       $('#DistrictId' + ' option').remove();
                   });

                   $('#RegionId').on('change', function () {
                       $.post('/AjaxApi/GetCityInRegion', { regionId: $(this).val() }, function (data) {
                           $('#CityId' + ' option').remove();
                           $('#CityId').append($("<option />").val("").text(""));

                           for (var i = 0; i < data.length; i++) {
                               $("#CityId").append($("<option/>").val(data[i].id).text(data[i].name));
                           }
                       });

                       $("#CityId").removeAttr("disabled");

                       $("#DistrictId").attr("disabled", "disabled");
                       $('#DistrictId' + ' option').remove();
                   });

                   $('#CityId').on('change', function () {
                       $.post('/AjaxApi/GetDistrictIdInCity', { cityId: $(this).val() }, function (data) {
                           $('#DistrictId' + ' option').remove();
                           $('#DistrictId').append($("<option />").val("").text(""));

                           for (var i = 0; i < data.length; i++) {
                               $("#DistrictId").append($("<option/>").val(data[i].id).text(data[i].name));
                           }
                       });

                       $("#DistrictId").removeAttr("disabled");
                   });


                   if (CountryId != 0) {
                       //console.log(CountryId);
                       $("#CountryId").removeAttr("disabled");
                       $("#CountryId").val(CountryId);
                       $("#CountryId").trigger("change");

                       setTimeout(function () {
                           $("#RegionId").removeAttr("disabled");
                           $("#RegionId").val(RegionId);
                           $("#RegionId").trigger("change");

                           setTimeout(function () {
                               $("#CityId").removeAttr("disabled");
                               $("#CityId").val(CityId);
                               $("#CityId").trigger("change");

                               setTimeout(function () {
                                   $("#DistrictId").removeAttr("disabled");
                                   $("#DistrictId").val(DistrictId);
                                   $("#DistrictId").trigger("change");

                               }, 500);

                           }, 500);

                       }, 500);

                   }

                   if (ObjectTypeId != 0) {
                       $("#ObjectTypeId").val(ObjectTypeId);
                   }

                   if (CurrencyId != 0) {
                       $("#CurrencyId").val(CurrencyId);
                   }
               });
        },



        ViewObjectProperty: function (rowId, o) {
            $('#modifyObjectProperty').modal("show");
            $("#modifyObjectProperty form")[0].reset();

            $.post('/AjaxApi/GetProperty', function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#PropertyId").append($("<option/>").val(data[i].id).text(data[i].name));
                }
            });

            $("#PropObjectRealtyId").val($("#ObjectRealtyId").val());

            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'ViewForEditObjectProperty';
                validOption.modal = $('#modifyObjectProperty');
                validOption.form = $('#modifyObjectProperty form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#ObjectPropertyId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateFormObjectProperty: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'ObjectRealty';
            validOption.action = 'ModifyObjectProperty';
            validOption.json = null;
            validOption.modal = 'modifyObjectProperty';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "PropertyId": "required", "Value": "required" };

            formValidation(validOption);

            setTimeout(function(){ 
                window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=props");
            }, 2000);

            return false;
        },

        DeleteObjectPropertyRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'DeleteObjectProperty';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        },



        ViewObjectImage: function (rowId, o) {
            $('#modifyObjectImage').modal("show");
            $("#modifyObjectImage form")[0].reset();


            $("#ImageObjectRealtyId").val($("#ObjectRealtyId").val());

            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'ViewForEditObjectImage';
                validOption.modal = $('#modifyObjectImage');
                validOption.form = $('#modifyObjectImage form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#hl-start-upload').val('Редактировать');
            } else {
                $("#UploadId").val("0");
                $('#hl-start-upload').val('Добавить');
            }
        },

        DeleteObjectImageRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'DeleteObjectImageRow';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        },


        ViewObjectDocument: function (rowId, o) {
            $('#modifyObjectDocument').modal("show");
            $("#modifyObjectDocument form")[0].reset();


            $("#DocObjectRealtyId").val($("#ObjectRealtyId").val());

            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'ViewForEditObjectDocument';
                validOption.modal = $('#modifyObjectDocument');
                validOption.form = $('#modifyObjectDocument form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#hl-start-upload-doc').val('Редактировать');
            } else {
                $("#DocUploadId").val("0");
                $('#hl-start-upload-doc').val('Добавить');
            }
        },

        DeleteObjectDocumentRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'DeleteObjectDocumentRow';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        },



        ViewObjectPledgers: function (rowId, o) {
            $('#modifyObjectRealtyPledgers').modal("show");
            $("#modifyObjectRealtyPledgers form")[0].reset();


            $("#orpObjectRealtyId").val($("#ObjectRealtyId").val());

            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'ViewForEditObjectPledgers';
                validOption.modal = $('#modifyObjectRealtyPledgers');
                validOption.form = $('#modifyObjectRealtyPledgers form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#hl-start-upload-doc').val('Редактировать');
            } else {
                $("#ObjectRealtyPledgersId").val("0");
                $('#hl-start-upload-doc').val('Добавить');
            }
        },

        ValidateFormObjectPledgers: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'ObjectRealty';
            validOption.action = 'ModifyObjectRealtyPledgers';
            validOption.json = null;
            validOption.modal = 'modifyObjectRealtyPledgers';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "ObjectRealtyId": "required", "PledgersId": "required" };

            formValidation(validOption);

            setTimeout(function(){ 
                window.location.replace(window.location.pathname + "?objectRealtyId=" + $("#ObjectRealtyId").val() + "&tab=pledgers");
            }, 2000);

            return false;
        },

        DeleteObjectRealtyPledgers: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'DeleteObjectRealtyPledgers';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        },
    };
}();