!function ($) {
    "use strict";

    $.fn['grid'] = function (method) {
        var methods = {
            init: function () {
                return this.each(function () {
                    var element = $(this);
                    element.data("rowCheckHandlers", []);
                    var controller = element.data('controller');
                    var actionSearch = element.data('action-search');

                    if (!actionSearch) {
                        actionSearch = 'Search';
                    }

                    element.addClass('table table-striped table-hover');

                    var columns = [];
                    var aoColumnDefs = [];
                    var sortColumns = [];
                    var columnsWidth = element.data('columns-width');

                    if (columnsWidth) {
                        columnsWidth = columnsWidth.split(',');
                    }

                    columns.push({ mDataProp: 0, sTitle: 'Id' });

                    var checkboxEnabled = element.data("checkbox-enabled");
                    if (checkboxEnabled) {
                        columns.push({
                            mDataProp: 1,
                            sTitle: '<input type="checkbox" id="gridAllRowsCheckBox" name="gridAllRowsCheckBox" />',
                            fnRender: function (o, v) {
                                return '<input type="checkbox" id="gridRowCheckBox' + o.iDataRow + '" name="gridRowCheckBox" />';
                            },
                            bSortable: false,
                            sWidth: '10px'
                        });
                    }

                    var sortEnabled = element.data('sort-enabled');
                    sortEnabled = (sortEnabled === undefined || sortEnabled === null || sortEnabled === true);

                    $.each(element.data('columns-title').split(','), function (titleIndex, title) {
                        var width = '34%';

                        if (columnsWidth && columnsWidth.length > titleIndex) {
                            width = columnsWidth[titleIndex];
                        }

                        var columnTemplate = element.data('column-' + title.trim().toLowerCase() + '-template');
                        columnTemplate = columnTemplate ? columnTemplate : element.data('column-' + titleIndex + '-template');

                        if (columnTemplate) {
                            var renderTemplate = function (data, type, row) {
                                var templateRendered = columnTemplate.replace(/\{0\}/g, data);
                                templateRendered = templateRendered.replace(/\{data\}/g, data);
                                templateRendered = templateRendered.replace(/\{id\}/g, row[0]);

                                return templateRendered;
                            };

                            columns.push({ mDataProp: titleIndex + 1 + (checkboxEnabled ? 1 : 0), sTitle: title, sWidth: width, mRender: renderTemplate, sClass: 'grid-data-column', bSortable: sortEnabled });
                        } else {
                            columns.push({ mDataProp: titleIndex + 1 + (checkboxEnabled ? 1 : 0), sTitle: title, sWidth: width, sClass: 'grid-data-column', bSortable: sortEnabled });
                        }
                    });

                    if (element.data('delete-msg')) {
                        var renderDeleteButton = function (idCol) {
                            return "<span class='glyphicon glyphicon-trash' name='deleteIcon' style='cursor: pointer;' onclick='$.grid.askForDeleteItem(" + idCol.iDataRow + ", \"" + idCol.aData[0] + "\", \"" + element.data("delete-msg") + "\", \"" + controller + "\", \"" + element.attr('id') + "\");'></span>";
                        };

                        columns.push({ mDataProp: columns.length, sTitle: '', sClass: 'grid-data-column-delete', fnRender: renderDeleteButton });
                        aoColumnDefs.push({ "bSortable": false, "aTargets": [-1] });
                    }

                    var showSearch = element.data("search-enabled");
                    if (showSearch === undefined || showSearch === null || showSearch === true)
                        showSearch = true;
                    else
                        showSearch = false;

                    var paginate = element.data("paginate-enabled");
                    if (paginate === undefined || paginate === null || paginate === true)
                        paginate = true;
                    else
                        paginate = false;

                    var defaultSorting = element.data('default-sorting');

                    if (defaultSorting) {

                        $.each(defaultSorting.split(','), function (index, sortParam) {
                            sortColumns[index] = sortParam.split('-');
                        });
                    }

                    var showInfo = element.data("info-enabled");
                    showInfo = (showInfo === undefined || showInfo === null || showInfo === true);

                    var enableSource = element.data("source-enabled");
                    enableSource = (enableSource === undefined || enableSource === null || enableSource === true);

                    var dataTable = element.dataTable({
                        bFilter: showSearch,
                        bInfo: showInfo,
                        bPaginate: paginate,
                        bLengthChange: false,
                        iDisplayLength: 10,
                        bAutoWidth: false,
                        sAjaxSource: enableSource ? controller + "/" + actionSearch : null,
                        aoColumns: columns,
                        aaSorting: sortColumns,
                        sDom: "<'toolbar'>frtip",
                        aoColumnDefs: [{ "bVisible": false, "aTargets": [0] },
                                        { "bSortable": false, "aTargets": [-1] }],
                        oLanguage: {
                            "sUrl": "/Scripts/Framework/Globalization/dataTables." + app.cultureCode + ".js"
                        },
                        bProcessing: false,
                        fnServerData: function (sSource, aoData, fnCallback, oSettings) {
                            $("div.toolbar").append($("#" + element.data("toolbar-id")));
                            $("#" + element.data("toolbar-id")).show();

                            if (!aoData || aoData.length === 0) {
                                aoData = $("form").serializeArray();
                            }

                            aoData.push({ 'name': 'checkboxEnabled', 'value': checkboxEnabled });

                            $.ajax({
                                "dataType": 'json',
                                "url": sSource,
                                "data": aoData,
                                "success": function (data, textStatus, jqXHR) {
                                    fnCallback(data, textStatus, jqXHR);

                                    if (checkboxEnabled) {
                                        $('#gridAllRowsCheckBox').click(function () {
                                            var check = $(this).is(':checked');

                                            $('input[name="gridRowCheckBox"]').each(function (index) {
                                                $(this).attr('checked', check).triggerHandler('click');
                                            });
                                        });

                                        $('input[name="gridRowCheckBox"]').each(function (index) {
                                            $(this).click(function () {
                                                var checkedLength = $('input[name="gridRowCheckBox"]:checked').length;
                                                var allLength = $('input[name="gridRowCheckBox"]').length;

                                                $('#gridAllRowsCheckBox').attr('checked', checkedLength == allLength);

                                                var eventObject = { selectedRowIndex: index, totalChecked: checkedLength, allChecked: checkedLength == allLength };
                                                $.each(element.data("rowCheckHandlers"), function (i, e) {
                                                    e(eventObject);
                                                });
                                            });
                                        });
                                    }
                                },
                                "error": function (data) {
                                    console.log(data);
                                }
                            });
                        },
                        fnDrawCallback: function () {
                            $('.glyphicon').hint();
                            if (!element.data("postprocess-func"))
                                eval(element.data("postprocess-func"));
                        }
                    });

                    var editEnabled = element.data('edit-enabled');

                    if (editEnabled === undefined || editEnabled === null || editEnabled === true) {
                        element.find('tbody').on('click', 'td.grid-data-column', function (event) {
                            var aPos = dataTable.fnGetPosition(this);
                            var aData = dataTable.fnGetData(aPos[0]);
                            location.href = controller + '/Edit/' + aData[0];
                        });

                        element.find('tbody').css('cursor', 'pointer');
                    }
                });

            },
            refresh: function () {
                $(this).dataTable().fnReloadAjax();
            },
            rowCheck: function (handler) {
                $(this).data("rowCheckHandlers").push(handler);
            },
            getCheckedIds: function () {
                var ids = [];
                var dt = this.dataTable();
                $(this).find('tr td > input:checked').each(function () {
                    var aPos = dt.fnGetPosition($(this).parent().get(0));
                    ids.push(parseInt(dt.fnGetData(aPos[0], 0), 10));
                });

                return ids;
            },
            getCheckedRowsData: function () {
                var element = $(this);
                var data = [];
                var dt = element.dataTable();
                element.find('tr td > input:checked').each(function () {
                    var aPos = dt.fnGetPosition($(this).parent().get(0));
                    var rowData = dt.fnGetData(aPos[0]);

                    if (element.data('checkboxEnabled')) {
                        rowData.splice(1, 1);
                    }

                    if (!element.data('delete-msg')) {
                        rowData.pop();
                    }

                    data.push(rowData);
                });

                return data;
            }
        };

        if (methods[method])
            return methods[method].apply(this, Array.prototype.slice.call(arguments, 1));
        else if (typeof method === 'object' || !method)
            return methods.init.apply(this, arguments);
        else {
            $.error('Method ' + method + ' does not exist!');
            return null;
        }
    };

    // Static methods.
    $.grid = {};
    $.grid.askForDeleteItem = function (index, id, message, controller) {
        var renderModal = function () {
            $("#modalDeleteLabel").html(globalization.texts.deleteTitle);
            $("#modalDeleteBody").html(message);
            $("#modalDeleteYesButton").attr("data-id", id);
            $("#modalDeleteYesButton").attr("data-controller", controller);
            $("#modalDeleteYesButton").html(globalization.texts.yes);
            $("#modalDeleteNoButton").html(globalization.texts.no);
            $('#modalDelete').modal();
        };

        if ($("#modalDeleteBody").length === 0) {
            $.ajax({
                url: '/Scripts/Framework/Grid/AskForDeleteModal.html',
                success: function (data) {
                    $('body').append(data);
                    $("#modalDeleteYesButton").click($.grid.deleteItem);
                    renderModal();
                },
                dataType: 'html'
            });
        }
        else {
            renderModal();
        }
    };

    $.grid.deleteItem = function () {
        var controller = $("#modalDeleteYesButton").data("controller");

        $.ajax({
            url: controller + "/Remove/" + $("#modalDeleteYesButton").data("id"),
            contentType: "application/json; charset=utf-8",
            type: "GET",
            success: function (data) {
            	if(data.deleted) {
                	window.location.href = controller;
                } else {
                	alert(data.message);
                }
            }
        });
    };

}(jQuery);

$(function () {
    console.log($('.grid').length + " grids found.");
    $('.grid').grid();
});