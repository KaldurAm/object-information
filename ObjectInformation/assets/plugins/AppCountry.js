var AppCountry = function () {

    var jGetTableCountries = function () {
        $("#tableCountry").dataTable({
            "processing": true,
            "serverSide": true,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "language": {
                "emptyTable": "Нет данных!"
            },
            "ajax": {
                "url": "/Country/jGetTableCountries",
                "data": { value: 0 }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "CountryId" },
                { "data": "CountryName" },
                {
                    "data": "CountryId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.CountryId, 'AppCountry.GetModalWindowForEditandPopulate', 'AppCountry.DeleteRow'); }
                }
            ]
        });
    }

    return {
        init: function () {           
            jGetTableCountries();
        },
        
        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyCountry').modal("show");
            $("#modifyCountry form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'Country';
                validOption.action = 'viewForEditCountry';
                validOption.modal = $('#modifyCountry');
                validOption.form = $('#modifyCountry form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#CountryId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'Country';
            validOption.action = 'modifyCountry';
            validOption.json = null;
            validOption.modal = 'modifyCountry';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "CountryName": "required" };

            formValidation(validOption);

            setTimeout(function () {
                var table = $('#tableCountry').dataTable();
                table.fnReloadAjax();
            }, 1000);

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'Country';
                validOption.action = 'deleteCountry';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();