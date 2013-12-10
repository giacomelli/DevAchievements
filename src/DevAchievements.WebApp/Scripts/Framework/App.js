"use strict";

var app = {

    _isInitiated: false,

    containers: {},

    init: function () {

        if (app._isInitiated !== false)
            return false;

        app._isInitiated = true;

        validation.init();

        return true;
    },
    initForms: function () {
        $('.dataTables_filter').find('input[type=text]').addClass('form-control');
    },
	cultureCode: "en"
};


$(function () {
    app.containers.window = $(window);
    app.containers.body = $('body');

    app.init();

    // Fix the DataTable's ordenation bug.
    jQuery.fn.dataTableExt.oSort['string-asc'] = function (a, b) {
        var x = a.toLowerCase();
        var y = b.toLowerCase();
        return x.localeCompare(y);
    };

    jQuery.fn.dataTableExt.oSort['string-desc'] = function (a, b) {
        var x = a.toLowerCase();
        var y = b.toLowerCase();
        return y.localeCompare(x);
    };

    $.fn.dataTableExt.oApi.fnReloadAjax = function (oSettings, sNewSource, fnCallback, bStandingRedraw) {
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
        var aData = $("form").serializeArray();

        this.oApi._fnServerParams(oSettings, aData);

        oSettings.fnServerData.call(oSettings.oInstance, oSettings.sAjaxSource, aData, function (json) {
            /* Clear the old information from the table */
            that.oApi._fnClearTable(oSettings);

            /* Got the data - add it to the table */
            var aData = (oSettings.sAjaxDataProp !== "") ?
            that.oApi._fnGetObjectDataFn(oSettings.sAjaxDataProp)(json) : json;

            for (var i = 0; i < aData.length; i++) {
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

    setTimeout(function () {
        $(".char-counter").each(function () {
            var item = $(this);
            item.charCount({
                allowed: item.attr('maxlength'),
                warning: (item.attr('maxlength') * 0.2),
                cssWarning: "text-warning",
                cssExceeded: "text-error",
                css: "counter-caracter"
            });
        });
    }, 200);
});