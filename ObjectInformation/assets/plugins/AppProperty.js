var AppProperty = function () {

    var jGetTableProperties= function () {
        $("#tableProperties").dataTable({
            "processing": true,
            "serverSide": true,
            "paging": false,
            "info": false,
            "pageLength": 10,
            "language": {
                "emptyTable": "Нет данных!"
            },
            "ajax": {
                "url": "/ObjectRealty/jGetTableProperties",
                "data": { value: 0 }
            },
            "searching": false,
            "ordering": false,
            //"paging": false,
            "columns": [
                { "data": "PropertyId" },
                {
                    "data": "Name",
                    "mRender": function (data, type, full) {
                        if (full.CountInObject == 0) {
                            return full.Name;
                        } else {
                            return full.Name + " <br />" +
                                "<span class='badge badge-primary'>Используется в " + full.CountInObject + " объектах</span>";
                        }
                    }
                },
                { "data": "UnitName" },
                {
                    "data": "ObjectType",
                    "mRender": function (data, type, full) {
                        var result = "";
                        $.each(full.ObjectType, function (key, value) {
                            result += "<span class='badge badge-success'>" + value + "</span> ";
                        });
                        return result;
                    }
                },
                {
                    "data": "PropertyId",
                    "mRender": function (data, type, full) { return EditRowButtons(full.PropertyId, 'AppProperty.GetModalWindowForEditandPopulate', 'AppProperty.DeleteRow'); }
                }
            ]
        });
    }

    return {
        init: function () {
            var tags = [];

            jGetTableProperties();

            $.getJSON("/ObjectRealty/GetObjectTypes", function (json) {

                $.each(json.ObjectType, function (key, value) {
                    tags.push(value);
                });

                $("#ObjectTypeId").select2({
                    tags: tags
                });
            });
          
            $.post('/ObjectRealty/GetListsUnits', function (dataUnit) {
                $('#UnitId' + ' option').remove();
                $('#UnitId' + ' optgroup').remove();
                $('#UnitId').append($("<option />").val("").text(""));

                var result = "";
                for (i = 0; i < dataUnit.dGroupListsUnits.length; i++)
                {
                    result += "<optgroup label='" + dataUnit.dGroupListsUnits[i].group + "'>";
                    
                    for (var j = 0; j < dataUnit.dListsUnits.length; j++)
                    {
                        if (dataUnit.dGroupListsUnits[i].group == dataUnit.dListsUnits[j].group)
                        {
                            result += "<option value='"+dataUnit.dListsUnits[j].id+"'>"+dataUnit.dListsUnits[j].name+"</option>";
                        }
                    }

                    result +="</optgroup>";
                }

                $('#UnitId').append(result);
            })
        },
        
        GetModalWindowForEditandPopulate: function (rowId, o) {
            $('#modifyProperty').modal("show");
            $("#modifyProperty form")[0].reset();
            //редактируем
            if (rowId != null) {
                var validOption = null;
                validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'viewForEditProperties';
                validOption.modal = $('#modifyProperty');
                validOption.form = $('#modifyProperty form');
                validOption.svalue = rowId;

                jGetModalWindowForEditandPopulate(validOption, function (data) { });

                //работаем с кнопками
                $('#addButton').val('Редактировать');
            } else {
                $("#ObjectTypeId").val("");
                $('#addButton').val('Добавить');
            }
        },

        ValidateForm: function (o) {
            var validOption = oPostJsonAction;
            validOption.controller = 'ObjectRealty';
            validOption.action = 'modifyProperty';
            validOption.json = null;
            validOption.modal = 'modifyProperty';
            validOption.form = $(o).closest("form");
            validOption.reloadAjax = '';
            validOption.options = oValidate.rules = { "Name": "required" };

            formValidation(validOption);

            setTimeout(function () {
                var table = $('#tableProperties').dataTable();
                table.fnReloadAjax();
            }, 1000);

            return false;
        },

        DeleteRow: function (rowId, o) {
            if (confirm("Вы действительно хотите удалить запись?")) {
                var validOption = oPostJsonAction;
                validOption.controller = 'ObjectRealty';
                validOption.action = 'deleteProperty';
                validOption.json = null;
                validOption.form = $(o).closest("tr");
                validOption.svalue = rowId;

                DeleteRowPost(validOption);
            }
            return false;
        }
    };
}();