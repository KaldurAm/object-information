var AppChecklistDocument = function () {

    var jGetTableDocumentChecklist = function (tableName, checklistId) {
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
                "url": "/DocumentTypes/jGetTableDocumentChecklist",
                "data": { checklistId: checklistId }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "DocumentTypeId" },
                { "data": "DocumentTypeName" },
                {
                    "data": "DocumentTypeId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.DocumentTypeId, 'AppChecklistDocument.GetModalWindowForEditandPopulate', 'AppChecklistDocument.DeleteRow'); }
                }
            ]
        });
    }
    
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
                "url": "/DocumentTypes/jGetTableChecklist",
                "data": { value: 0 }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "ChecklistId" },
                { "data": "Name" },
                { "data": "Description" },
                {
                    "data": "ChecklistId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.ChecklistId, 'AppCountry.GetModalWindowForEditandPopulate', 'AppCountry.DeleteRow'); }
                }
            ]
        });
    }

  
    return {
        init: function () {

            jGetTableCountries();
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