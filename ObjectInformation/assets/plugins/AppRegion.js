var AppRegion = function () {

    var jGetTableRegions = function (tableName, contryId) {
        console.log(tableName +" - " + contryId);
        $("#" + tableName).dataTable({
            "processing": true,
            "serverSide": true,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "language": {
                "emptyTable": "Нет данных!"
            },
            "ajax": {
                "url": "/Region/jGetTableRegions",
                "data": { contryId: contryId }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "RegionId" },
                { "data": "RegionName" },
                {
                    "data": "RegionId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.RegionId, 'AppRegion.GetModalWindowForEditandPopulate', 'AppRegion.DeleteRow'); }
                }
            ]
        });
    }

    var createTableRegions = function() {
        $("#mainDiv").html("");
        $.getJSON('/Region/getCountryList', function (json) {
            $.each(json, function (index, value) {
                $("#mainDiv").append("<h3>" + value.CountryName + "</h3>");
                $("#mainDiv").append('<table class="table table-bordered table-advance table-hover table-striped" id="tableRegion_' + index + '"><thead><tr><th>Номер</th><th>Наименование</th><th></th></tr></thead></table>');

                jGetTableRegions("tableRegion_" + index, value.CountryId);
            });
        });
    }

    return {
        init: function () {

            createTableRegions();

            $.post('/Region/GetCountries',function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#CountryId").append($("<option/>").val(data[i].id).text(data[i].name));
                  }
              });
        },

        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyRegion').modal("show");
            $("#modifyRegion form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'Region';
                validOption.action = 'viewForEditRegion';
                validOption.modal = $('#modifyRegion');
                validOption.form = $('#modifyRegion form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#RegionId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'Region';
            validOption.action = 'modifyRegion';
            validOption.json = null;
            validOption.modal = 'modifyRegion';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "RegionName": "required", "CountryId": "required" };

            formValidation(validOption);

            setTimeout(function () {
                createTableRegions();

            }, 1000);

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'Region';
                validOption.action = 'deleteRegion';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();