var AppCity = function () {

    var jGetTableCity = function (tableName, contryId, regionId) {
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
                "url": "/City/jGetTableCity",
                "data": { contryId: contryId, regionId: regionId }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "CityId" },
                { "data": "CityName" },
                {
                    "data": "CityId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.CityId, 'AppCity.GetModalWindowForEditandPopulate', 'AppCity.DeleteRow'); }
                }
            ]
        });
    }

    var createTableCountries = function () {
   
    }

    return {
        init: function () {

            $.post('/City/GetRegions', function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#RegionId").append($("<option/>").val(data[i].id).text(data[i].name));
                  }
              });
        },

        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyCity').modal("show");
            $("#modifyCity form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'City';
                validOption.action = 'viewForEditCity';
                validOption.modal = $('#modifyCity');
                validOption.form = $('#modifyCity form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#CityId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'City';
            validOption.action = 'modifyCity';
            validOption.json = null;
            validOption.modal = 'modifyCity';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "CityName": "required", "RegionId": "required" };

            formValidation(validOption);

            location.reload();

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'City';
                validOption.action = 'deleteCity';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();