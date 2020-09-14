var oDataTable = { processing: null, serverSide: null, searching: null, ordering: null, language: null, paging: null, pageLength: null, info: null, columns: null, ajax: null, svalue: null, fnPreDrawCallback: null, fnDrawCallback: null, fnRowCallback: null, fnInitComplete: null, processingDiv: null };
        var oPostJsonAction = { controller: null, action: null, json: null, modal: null, form: null, reloadAjax: null, options: null, svalue: null, data: null, ddname: null, ddselected: null, blockUI: null, fadeOut: null };
        var oValidate = { rules: null, errorElement: null, errorClass: null, errorPlacement: null, errorMessage: null, focusInvalid: null, ignore: null, invalidHandler: null, highlight: null, unhighlight: null, success: null, submitHandler: null, pageContent: null };
        var userlocation = null;
        var intComponentId = null;
        var ComponentTypeID = null;
        var serviceHistory = null;
        var intEvalutionId = null;
        var GlobalVariablesPageLength = null;

        //Делаем сериализацию формы
        function serializedForm(serializedFrom) {
            var sFrom = serializedFrom;

            sFrom.find(':disabled').removeAttr('disabled');

            var json;
            var serialized = sFrom.serializeArray();
            var s = '';
            var data = {};
            for (s in serialized) {
                console.log('name: ' + data[serialized[s]['name']]);
                console.log('value: ' + serialized[s]['value']);
                data[serialized[s]['name']] = serialized[s]['value']
            }
            json = JSON.stringify(data);

            console.log("json - " + json);

            return json;
        }

        //Button Действие - Добавить - Редактировать - Удалить
        function onlyAddRowButtons(rowId) {
            return '<div class="btn-group"> ' +
                       ' <button data-toggle="dropdown" type="button" class="btn blue btn-sm dropdown-toggle">Действие <i class="icon-angle-down"></i></button> ' +
                       ' <ul role="menu" class="dropdown-menu" style="text-align:left"> ' +
                        '    <li><span  onclick="GetModalWindowForEditandPopulate(' + rowId + ', this)" class="edit-button">Добавить</span></li> ' +
                      '  </ul> ' +
                    '</div>';
        }

        //Button Действие - Редактировать - Удалить
        function EditRowButtons(rowId, editFunc, deleteFunc) {
            return '<div class="btn-group  btn-group-solid"> ' +
                       ' <button type="button" class="btn blue dropdown-toggle" data-toggle="dropdown" aria-expanded="false"> ' +
                       ' <i class="fa fa-ellipsis-horizontal"></i> Дейтсвие <i class="fa fa-angle-down"></i> ' +
                       ' </button> ' +
                       ' <ul class="dropdown-menu" style="text-align:left"> ' +
                       '     <li><a  onclick="' + editFunc + '(' + rowId + ', this)" class="edit-button">Редактировать</a></li> ' +
                       '     <li><a  onclick="' + deleteFunc + '(' + rowId + ', this)" class="delete-button">Удалить</a></li> ' +
                      '  </ul> ' +
                    '</div>';
        }

        //Button Действие - Редактировать - Удалить
        function AddEditRowButtons(rowId) {
            return '<div class="btn-group"> ' +
                       ' <button data-toggle="dropdown" type="button" class="btn blue btn-sm dropdown-toggle">Действие <i class="icon-angle-down"></i></button> ' +
                       ' <ul role="menu" class="dropdown-menu" style="text-align:left"> ' +
                        '     <li><span  onclick="GetModalWindowForAdd(' + rowId + ', this)" class="edit-button">Добавить</span></li> ' +
                       '     <li><span  onclick="GetModalWindowForEditandPopulate(' + rowId + ', this)" class="edit-button">Редактировать</span></li> ' +
                       '     <li><span  onclick="DeleteRow(' + rowId + ', this)" class="delete-button">Удалить</span></li> ' +
                      '  </ul> ' +
                    '</div>';
        }

        //Button Действие - Редактировать - Удалить
        function onlyEditRowButtons(rowId) {
            return '<div class="btn-group"> ' +
                       ' <button data-toggle="dropdown" type="button" class="btn blue btn-sm dropdown-toggle">Действие <i class="icon-angle-down"></i></button> ' +
                       ' <ul role="menu" class="dropdown-menu"> ' +
                       '     <li><span  onclick="GetModalWindowForEditandPopulate(' + rowId + ', this)" class="edit-button">редактировать</span></li> ' +
                      '  </ul> ' +
                    '</div>';
        }

        //Button Действие - Удалить
        function DeleteRowButtons(rowId) {
            return '<div class="btn-group"> ' +
                       ' <button data-toggle="dropdown" type="button" class="btn blue btn-sm dropdown-toggle">Действие <i class="icon-angle-down"></i></button> ' +
                       ' <ul role="menu" class="dropdown-menu"> ' +
                       '     <li><span  onclick="DeleteRow(' + rowId + ', this)" class="delete-button">Удалить</span></li> ' +
                      '  </ul> ' +
                    '</div>';
        }

        //Отображаем сообщение о незаполненных поля (для валидации)
        function ShowErrorToastr(validOption) {
            var error = validOption.form;
            error.show();
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-left",
                "onclick": null,
                "showDuration": "1000",
                "hideDuration": "3000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr.error(validOption.errorMessage, 'Ошибка');
        }

        function ShowSuccessToastr(validOption) {
            var editForm = validOption.form;
            editForm.show();
            toastr.options = {
                "closeButton": true,
                "debug": false,
                "positionClass": "toast-top-left",
                "onclick": null,
                "showDuration": "1000",
                "hideDuration": "3000",
                "timeOut": "5000",
                "extendedTimeOut": "1000",
                "showEasing": "swing",
                "hideEasing": "linear",
                "showMethod": "fadeIn",
                "hideMethod": "fadeOut"
            }
            toastr.success("ОК!", validOption.errorMessage);
        }

        //Глабальная переменная, для удаления
        function DeleteRowPost(validOption) {
            $.post("/" + validOption.controller + "/" + validOption.action, { value: validOption.svalue }, function (data) { }).done(function (data) {
                validOption.errorMessage = 'Удаление прошло успешно';
                ShowSuccessToastr(validOption);

                if (validOption.fadeOut != null) {
                    if (validOption.fadeOut.toString().toLowerCase() == 'true') { }
                    else
                    {
                        var fd = validOption.form;
                        fd.fadeOut('slow');
                    }
                }
                else {
                    var fd = validOption.form;
                    fd.fadeOut('slow');
                }
            }).error(function (jqXHR, textStatus, errorThrown) {
                validOption.errorMessage = jqXHR.responseText;
                ShowErrorToastr(validOption);
                //alert("Во время редактирования возникла ошибка: " + jqXHR.responseText);
                console.log("jqXHR: " + jqXHR);
                console.log("textStatus: " + textStatus);
                console.log("errorThrown: " + errorThrown);
            });
        }

        //Обновляем данные в таблице
        function ReloadAjaxTable(validOption) {
            var table = $('#' + validOption.reloadAjax).dataTable();
            table.fnReloadAjax();
        }

        //отправляет json в контроллер для обработки
        function SentPostAfterSubbmit(validOption) {
            $.post("/" + validOption.controller + "/" + validOption.action, { json: validOption.json }, function (data, status, jqxhr) {
                $('#' + validOption.modal).modal('hide');
                ReloadAjaxTable(validOption);
                validOption.errorMessage = 'Изменение прошло успешно';
                ShowSuccessToastr(validOption);
            }, "json").error(function (jqXHR, textStatus, errorThrown) {
                validOption.errorMessage = jqXHR.responseText;
                ShowErrorToastr(validOption);
                //alert("Во время редактирования возникла ошибка: " + jqXHR.responseText);
                console.log("jqXHR: " + jqXHR);
                console.log("textStatus: " + textStatus);
                console.log("errorThrown: " + errorThrown);
            });
        }

        //получаем данные с БД и прогружаем их в форму для редактирования
        function jGetModalWindowForEditandPopulate(validOption, callback) {
            //console.log("ROWID: " + validOption.svalue);
            App.blockUI(validOption.blockUI);

            $.getJSON('/' + validOption.controller + '/' + validOption.action, { value: validOption.svalue }, function (json) {
                console.log(json);
                var form = validOption.form;
                form.populate(json);

                if (validOption.modal != null) {
                    var modal = validOption.modal;
                    modal.modal("show");
                }

                //console.log("json (jGetModalWindowForEditandPopulate): " + json);
                callback(json);
            }).done(function () {
                if (validOption.blockUI != null) {
                    App.unblockUI(validOption.blockUI);
                    console.log(validOption.blockUI);
                    alert('Ajax success');
                }


            });
        }



        //Функция делает валидацию формы
        function formValidation(validOption) {
            var editForm = validOption.form;
            var erroreditForm = $('.alert-danger', editForm);

            var fOptions = oValidate;

            fOptions.rules = validOption.options;
            fOptions.errorElement = "span";
            fOptions.errorClass = 'help-block',
            fOptions.errorPlacement = function (error, element) { erroreditForm.appendTo(element.parents('.form-group')); };
            fOptions.focusInvalid = false;
            fOptions.ignore = "";
            fOptions.invalidHandler = function (event, validator) { ShowErrorToastr(validOption); };//display error alert on form submit
            fOptions.highlight = function (element) { $(element).parents('.form-group').addClass('has-error'); }; // set error class to the control group
            fOptions.unhighlight = function (element) { $(element).parents('.form-group').removeClass('has-error'); }; // set error class to the control group
            fOptions.success = function (label) { label.parents('.form-group').removeClass('has-error'); };
            fOptions.submitHandler = function (form) {
                validOption.json = serializedForm(editForm);
                SentPostAfterSubbmit(validOption);
            };

            jQuery.extend(jQuery.validator.messages, { required: "*" });
            return editForm.validate(fOptions);
        }

        function convertJsonDate(date) {
            var dx = new Date(date.match(/\d+/)[0] * 1);
            //Console.log();
            var dd = dx.getDate();
            var mm = dx.getMonth() + 1;
            var yy = dx.getFullYear();

            if (dd <= 9) {
                dd = "0" + dd;
            }
            if (mm <= 9) {
                mm = "0" + mm;
            }
            return dd + "." + mm + "." + yy;
        }

        function setActive(o) {
            var parentLI = $(o).closest("ul");
            //var parentLI2 = $(o).parent();

            var paren = $(parentLI).closest("li");

            //alert(parentLI.attr('class'));
            paren.addClass("active");
        }

        //функция по созданию таблицы
        function disainerToCreateDataTable(validOption) {
            //App.blockUI(validOption.form);
            var dOptions = oDataTable;
            dOptions.processing = validOption.options.processing;
            dOptions.serverSide = validOption.options.serverSide;
            dOptions.columns = validOption.options.columns;
            dOptions.ajax = { url: "/" + validOption.controller + "/" + validOption.action, data: validOption.data };
            dOptions.searching = validOption.searching;
            dOptions.ordering = validOption.ordering;
            dOptions.paging = validOption.paging;
            dOptions.pageLength = 10;
            //dOptions.searching = false;
            dOptions.ordering = false;

            //dOptions.fnPreDrawCallback = function (oSettings) { console.log(oSettings); };
            //dOptions.fnDrawCallback = function (oSettings) { alert('TEST2'); };
            //dOptions.fnRowCallback = function (nRow, aData, iDisplayIndex) {alert('TEST3') };
            dOptions.fnInitComplete = function (oSettings) { /*App.unblockUI(validOption.form);*/ };

            if (validOption.language != null) { dOptions.language = validOption.language; }
            else { dOptions.language = { "sInfo": "Показано от _START_ до _END_ из _TOTAL_ записей", "sInfoEmpty": "Показано от 0 до 0 из 0 записей" }; }

            dOptions.info = validOption.info;

            var table = validOption.form;
            table.dataTable(dOptions);
        }

        jQuery.fn.populate = function (g, h) {
            function parseJSON(a, b) { b = b || ''; if (a == undefined) { } else if (a.constructor == Object) { for (var c in a) { var d = b + (b == '' ? c : '[' + c + ']'); parseJSON(a[c], d) } } else if (a.constructor == Array) { for (var i = 0; i < a.length; i++) { var e = h.useIndices ? i : ''; e = h.phpNaming ? '[' + e + ']' : e; var d = b + e; parseJSON(a[i], d) } } else { if (k[b] == undefined) { k[b] = a } else if (k[b].constructor != Array) { k[b] = [k[b], a] } else { k[b].push(a) } } }; function debug(a) { if (window.console && console.log) { console.log(a) } } function getElementName(a) { if (!h.phpNaming) { a = a.replace(/\[\]$/, '') } return a } function populateElement(a, b, c) { var d = h.identifier == 'id' ? '#' + b : '[' + h.identifier + '="' + b + '"]'; var e = jQuery(d, a); c = c.toString(); c = c == 'null' ? '' : c; e.html(c) } function populateFormElement(a, b, c) {
                var b = getElementName(b); var d = a[b]; if (d == undefined) { d = jQuery('#' + b, a); if (d) { d.html(c); return true } if (h.debug) { debug('No such element as ' + b) } return false } if (h.debug) { _populate.elements.push(d) } elements = d.type == undefined && d.length ? d : [d]; for (var e = 0; e < elements.length; e++) {
                    var d = elements[e];
                    if (!d || typeof d == 'undefined' || typeof d == 'function') { continue }

                    switch (d.type || d.tagName) {
                        case 'radio': d.checked = (d.value != '' && c.toString() == d.value);
                        case 'checkbox': var f = c.constructor == Array ? c : [c]; for (var j = 0; j < f.length; j++) { d.checked |= d.value == f[j] } break;
                        case 'select-multiple':
                            {
                                var f = c.constructor == Array ? c : [c];
                                for (var i = 0; i < d.options.length; i++) {
                                    for (var j = 0; j < f.length; j++)
                                    { d.options[i].selected |= d.options[i].value == f[j] }
                                } break;
                            }
                        case 'select': case 'select-one':
                            {

                                d.value = c.toString() || c;

                                var str = $('#' + d.id);
                                if (str.attr("class") != null && str.attr("class") != "") {
                                    //console.log(str.attr("class").indexOf('select2me'));
                                    if (str.attr("class").indexOf('populate2') > 0) {
                                        console.log(d.id + " / " + c.toString());
                                        $('#' + d.id).select2("val", c.toString());
                                    }

                                }
                            } break;
                        case 'text': case 'button': case 'textarea': case 'submit': default: c = c == null ? '' : c;


                            //for date json  Sergey
                            if (typeof c === 'string' && /\/Date\((\d*)\)\//.test(c)) {
                                a = /\/Date\((\d*)\)\//.exec(c);
                                if (a) {
                                    $(d).parent(".date-picker").datepicker("setDate", new Date(+a[1]));
                                }
                            }
                            else if ($(d)[0].className.indexOf("select2") != -1) {
                                var element = c.split(",");
                                $("#" + $(d)[0].id).val(element).trigger('change');
                            }
                            else {
                                d.value = c;
                            }
                            //-----------------
                    }
                }
            } if (g === undefined) { return this }; var h = jQuery.extend({ phpNaming: true, phpIndices: false, resetForm: true, identifier: 'id', debug: false }, h); if (h.phpIndices) { h.phpNaming = true } var k = []; parseJSON(g); if (h.debug) { _populate = { arr: k, obj: g, elements: [] } } this.each(function () { var a = this.tagName.toLowerCase(); var b = a == 'form' ? populateFormElement : populateElement; if (a == 'form' && h.resetForm) { this.reset() } for (var i in k) { b(this, i, k[i]) } }); return this
        };

        jQuery.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
            // DataTables 1.10 compatibility - if 1.10 then `versionCheck` exists.
            // 1.10's API has ajax reloading built in, so we use those abilities
            // directly.
            if (jQuery.fn.dataTable.versionCheck) {
                var api = new jQuery.fn.dataTable.Api(oSettings);

                if (sNewSource) {
                    api.ajax.url(sNewSource).load(fnCallback, !bStandingRedraw);
                }
                else {
                    api.ajax.reload(fnCallback, !bStandingRedraw);
                }
                return;
            }

            if (sNewSource !== undefined && sNewSource !== null) {
                oSettings.sAjaxSource = sNewSource;
            }

            // Server-side processing should just call fnDraw
            if (oSettings.oFeatures.bServerSide) {
                this.fnDraw();
                return;
            }

            this.oApi._fnProcessingDisplay(oSettings, true);
            var that = this;
            var iStart = oSettings._iDisplayStart;
            var aData = [];

            this.oApi._fnServerParams(oSettings, aData);

            oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
                /* Clear the old information from the table */
                that.oApi._fnClearTable(oSettings);

                /* Got the data - add it to the table */
                var aData = (oSettings.sAjaxDataProp !== "") ?
                    that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

                for (var i = 0 ; i < aData.length ; i++) {
                    that.oApi._fnAddData(oSettings, aData[i]);
                }

                oSettings.aiDisplay = oSettings.aiDisplayMaster.slice();

                that.fnDraw();

                if (bStandingRedraw === true) {
                    oSettings._iDisplayStart = iStart;
                    that.oApi._fnCalculateEnd(oSettings);
                    that.fnDraw(false);
                }

                that.oApi._fnProcessingDisplay(oSettings, false);

                /* Callback user function - for event handlers etc */
                if (typeof fnCallback == 'function' && fnCallback !== null) {
                    fnCallback(oSettings);
                }
            }, oSettings);
        };