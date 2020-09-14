var AppObjectType = function () {

    var jGetTableObjectTypes = function () {
        $("#tableObjectTypes").dataTable({
            "processing": true,
            "serverSide": true,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "language": {
                "emptyTable": "Нет данных!"
            },
            "ajax": {
                "url": "/ObjectRealty/jGetTableObjectTypes",
                "data": { value: 0 }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "ObjectTypeId" },
                {
                    "data": "ObjectTypeName", "mRender": function (data, type, full) {
                        if (full.CountInObject == 0) {
                            return full.ObjectTypeName;
                        } else {
                            return full.ObjectTypeName + " <br />" +
                                "<span class='badge badge-primary'>Используется в " + full.CountInObject + " объектах</span>";
                        }
                    }
                },
                {
                    "data": "ObjectTypeId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.ObjectTypeId, 'AppObjectType.GetModalWindowForEditandPopulate', 'AppObjectType.DeleteRow'); }
                }
            ]
        });
    }

    return {

        init: function () {
            jGetTableObjectTypes();
        },

        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyObjectType').modal("show");
            $("#modifyObjectType form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'ViewForEditObjectType';
                validOption.modal = $('#modifyObjectType');
                validOption.form = $('#modifyObjectType form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'ObjectRealty';
            validOption.action = 'ModifyObjectType';
            validOption.json = null;
            validOption.modal = 'modifyObjectType';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "ObjectTypeName": "required" };

            formValidation(validOption);

            setTimeout(function () {
                var table = $('#tableObjectTypes').dataTable();
                table.fnReloadAjax();
            }, 1000);

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'DeleteObjectType';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };

}();