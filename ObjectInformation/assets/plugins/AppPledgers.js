var AppPledgers = function () {

    var jGetTablePledgers = function () {
        $("#tablePledgers").dataTable({
            "processing": true,
            "serverSide": true,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "language": {
                "emptyTable": "Нет данных!"
            },
            "ajax": {
                "url": "/Pledgers/jGetTablePledgers",
                "data": { value: 0 }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "PledgersId" },
                { "data": "NameOfPledger" },
                { "data": "ControllingShareholder" },
                {
                    "data": "PledgersId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.PledgersId, 'AppPledgers.GetModalWindowForEditandPopulate', 'AppPledgers.DeleteRow'); }
                }
            ]
        });
    }

    return {
        init: function () {           
            jGetTablePledgers();
        },
        
        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyPledgers').modal("show");
            $("#modifyPledgers form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'Pledgers';
                validOption.action = 'viewForEditPledgers';
                validOption.modal = $('#modifyPledgers');
                validOption.form = $('#modifyPledgers form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#PledgersId").val("0");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'Pledgers';
            validOption.action = 'ModifyPledgers';
            validOption.json = null;
            validOption.modal = 'modifyPledgers';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "NameOfPledger": "required", "ControllingShareholder": "required" };

            formValidation(validOption);

            setTimeout(function () {
                var table = $('#tablePledgers').dataTable();
                table.fnReloadAjax();
            }, 1000);

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'Pledgers';
                validOption.action = 'DeletePledgers';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();