"use strict";

(function ($) {
    $.extend({
        tip: {
            wrapElement: function (element) {
                var container = element;

                if (element.is('input,textarea')) {
                    element
                        .wrap('<div class="controls with-icon-over-input" />')
                        .after('<i class="glyphicon glyphicon-info-sign"></i>');

                    container = element.parent();

                    if (container.next().hasClass("input-label")) {
                        container.append(container.next());
                    }
                }

                return container;
            }
        }
    });

    $.extend($.fn, {
        tip: function () {
            return this.each(function () {
                var element = $(this);
                var id = element.attr('id');
                var help, sample, container;

                if (id) {
                    id = id.replace(/-/g, '');
                    help = globalization.getText('{0}Help'.format(id));

                    if (help !== null) {
                        container = $.tip.wrapElement(element);
                        sample = globalization.getText('{0}Sample'.format(id));

                        if (sample) {
                            sample = '<p>{0}: {1}</p>'.format(globalization.texts.sample, sample);                                
                        }
                        else {
                            sample = '';
                        }

                        help = $("<div/>").html(help).text();

                        container.popover({
                            placement: function (context, source) {
                                var result =  "right";
                                var width = $(window).width();
                                
                                if (width <= 980) {
                                    result = "top";
                                }
                               
                                return result;
                            },
                            trigger: 'focus hover',
                            container: 'body',
                            html: true,
                            content: '<div class="tip-container"><h4>{0}</h4>{1}</div>'.format(help, sample)
                        });
                    }
                }
            });
        }
    });
})(jQuery);