$(document).ready(function () {
    //Событие при изменение выбора страны
    $("#CountryId").change(function() {
        var id = 0;
        console.log("Country");
        id = $(this)[0].selectedOptions[0].value; // id страны по его value
        //ChangeAllRegions(id); // вызов метода для изменения всех регионов под эту страну
    });

    // Событие при изменение выбора региона
    $("#RegionId").change(function() {
        var id = 0;
        id = $(this)[0].selectedOptions[0].value; // id региона по его value
        ChangeAllCities(id); // вызов метода для изменения всех городов под этот регион
    });

    // Событие при изменение выбора города
    $("#CitiId").change(function() {
        var id = 0;
        id = $(this)[0].selectedOptions[0].value; // id города по его value
        ChangeAllDistricts(id); // вызов метода для изменения всех регионов под этот город
    });
 
    $("#AddP").click(function () {

        var row = $("#divProperty").html();
        row = row.replace("ObjectProperties[]", "ObjectProperties[" + rowCount + "]");
        row = row.replace("ObjectProperties[]", "ObjectProperties[" + rowCount + "]");
        row = row.replace("ObjectProperties[]", "ObjectProperties[" + rowCount + "]");
        row = row.replace("divProperty", "divProperty-" + rowCount);
        row = row.replace("DeleteProperty()", "DeleteProperty('" + "divProperty-" + rowCount + "')");
        $(".props").append(row);
        rowCount = rowCount + 1;
    });
});

// Метод для изменения всех регионов страны по его id
function ChangeAllRegions(countryId) {
    var regionsSelect = $("#RegionId"); // получение элемента со списком текущих регионов
    regionsSelect[0].options.length = 0; // обнуление этого списка
    $.when(
        // запрос на контроллер для получения регионов
        $.post('/ObjectRealty/GetRegions',
            { countryId: countryId },
            function(data) {
                for (var i = 0; i < data.length; i++) {
                    regionsSelect.append($("<option/>").val(data[i].id)
                        .text(data[i].name)); // добавление региона в список
                }
            })
        // как только отработает запрос происхоид изменение всех городов
    ).then(function() {
        var id = 0;
        id = regionsSelect[0].options[0].value; // id первого региона в списке
        ChangeAllCities(id);
    });
}
// Метод для изменения всех городов региона по его id
function ChangeAllCities(regionId) {
    var citiesSelect = $("#CitiId"); // получение элемента со списком текущих городов
    citiesSelect[0].options.length = 0; // обнуление этого списка
    $.when(
        // запрос на контроллер для получения городов
        $.post('/ObjectRealty/GetCities',
            { regionId: regionId },
            function(data) {
                for (var i = 0; i < data.length; i++) {
                    citiesSelect.append($("<option/>").val(data[i].id)
                        .text(data[i].name)); // добавление города в список
                }
            })
        // как только отработает запрос происхоид изменение всех регионов
    ).then(function() {
        var id = 0;
        id = citiesSelect[0].options[0].value; // id первого города в списке
        ChangeAllDistricts(id);
    });
}
// Метод для изменения всех районов города по его id
function ChangeAllDistricts(citiId) {
    var districtSelect = $("#DistrictId"); // получение элемента со списком текущих районов
    districtSelect[0].options.length = 0; // обнуление этого списка
    // запрос на контроллер для получения районов
    $.post('/ObjectRealty/GetDistricts',
        { citiId: citiId },
        function(data) {
            for (var i = 0; i < data.length; i++) {
                districtSelect.append($("<option/>").val(data[i].id).text(data[i].name)); // добавление района в список
            }
        });
}

function DeleteProperty(cls) {
    $("." + cls).remove();
}