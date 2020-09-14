var AppDistrict = function () {

    var jGetTableDistrict = function (tableName, cityId) {
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
                "url": "/District/jGetTableDistrict",
                "data": { cityId: cityId }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "DistrictId" },
                { "data": "DistrictName" },
                {
                    "data": "DistrictId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.DistrictId, 'AppDistrict.GetModalWindowForEditandPopulate', 'AppCity.DeleteRow'); }
                }
            ]
        });
    }



    return {
        init: function () {

         

            $.post('/District/GetCities', function (data) {
                for (var i = 0; i < data.length; i++) {
                    $("#CityId").append($("<option/>").val(data[i].id).text(data[i].name));
                  }
              });
        },

        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyDistrict').modal("show");
            $("#modifyDistrict form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'District';
                validOption.action = 'viewForEditDistrict';
                validOption.modal = $('#modifyDistrict');
                validOption.form = $('#modifyDistrict form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#DistrictId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'District';
            validOption.action = 'modifyDistrict';
            validOption.json = null;
            validOption.modal = 'modifyDistrict';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "DistrictName": "required", "CityId": "required" };

            formValidation(validOption);

            location.reload();

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'District';
                validOption.action = 'deleteDistrict';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();