"use strict";

if (!validation) {
    var validation = {};
}

$.extend(validation, {
    current: null,
    init: function() {
        jQuery.validator.addMethod("urlNoProtocol", function (value, element) {
            return this.optional(element) || (/^(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$/i).test(value);
        }, jQuery.validator.messages["url"]);

        jQuery.validator.addMethod("requiredSegmentation", function (value, element, params) {
            var segmentation = element.id.split("-")[0];
            var required;

            if (typeof params.required === "function") {
                required = params.required();
            }
            else {
                required = params.required;
            }

            if (!required) {
                return true;
            }

            $('tbody>tr>td.dataTables_empty').parent().remove();

            return $('#' + segmentation + '-table tbody tr').length > 0;

        }, jQuery.validator.messages["required"]);

        jQuery.validator.addMethod("dateCulture", function (value, element) {
            if (app.cultureCode == 'pt' || app.cultureCode == 'es') {
                var check = false;
                var re = /^\d{1,2}\/\d{1,2}\/\d{4}$/;
                if (re.test(value)) {
                    var adata = value.split('/');
                    var gg = parseInt(adata[0], 10);
                    var mm = parseInt(adata[1], 10);
                    var aaaa = parseInt(adata[2], 10);
                    var xdata = new Date(aaaa, mm - 1, gg);
                    if ((xdata.getFullYear() === aaaa) && (xdata.getMonth() === mm - 1) && (xdata.getDate() === gg)) {
                        check = true;
                    } else {
                        check = false;
                    }
                } else {
                    check = false;
                }
                return this.optional(element) || check;
            }
            else {
                return jQuery.validator.date(value, element);
            }
        }, jQuery.validator.messages.date);
    },
    validate: function (form, rules) {
        var formObj;

        if (typeof (form) === 'string')
            formObj = $(form);
        else if (typeof (form) === 'object')
            formObj = form;

        var getElement = function (element) {
            if (element.hasClass('input-file')) {
                element = element.parent().parent();
            }

            return element;
        };

        this.current = $(formObj).validate({
            focusInvalid: true,
            rules: rules,
            errorClass: 'hidden',
            showErrors: function (errorMap, errorList) {                
                $('.alert-success').remove();
                
                $.each(errorList, function (i, e) {
                    var element = getElement($(e.element));

                    var errorMarkup = element.nextAll('.validation-field-error');
                    errorMarkup.remove();

                    element.parent().addClass("form-group has-error");

                    element.after($('<div class="alert alert-danger validation-field-error">{0} {1}</div>'.format(
                            globalization.texts.theField,                            
                            e.message
                        )));
                });

                for (var i = 0; this.successList[i]; i++) {                        
                    var element = getElement($(this.successList[i]));

                    element.parent().removeClass("form-group has-error");
                    var errorMarkup = element.nextAll('.validation-field-error');
                    errorMarkup.remove();
                }
            }           
        });

        $('[data-minlength]').each(function (i, e) {
            var input = $(e);
            input.rules('add', { minlength: input.data('minlength') });
        });

        return this.current;
    },
    validElement: function (element) {
        this.current.element('#' + element.attr('id'));
    }
});