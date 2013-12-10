"use strict";

(function ($) {
    $.extend($.fn, {
        hint: function () {
            return this.each(function () {
                var element = $(this);
                var name = element.attr('name');
                var hint;

                if (name) {
                    hint = globalization.getText('{0}Hint'.format(name));

                    if (hint !== null) {
                        element.attr('title', hint.decode());
                    }
                }
            });
        }
    });
})(jQuery);