System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    /**
     * Simple to use Template Function which will replace
     * placeholder like `{{title}` with the property which has
     * the key title in the data Object
     *
     * @export
     * @param {string} template HTML as string
     * @param {IIterableObject<any>} [data] Data with key's which should replace the placeholder
     * @returns HTML as string
     */
    function template(template, data) {
        for (var key in data) {
            if (data.hasOwnProperty(key)) {
                var value = data[key];
                template = template.replace(new RegExp('\{\{' + key + '\}\}', 'g'), value);
            }
        }
        return template;
    }
    exports_1("template", template);
    return {
        setters: [],
        execute: function () {
        }
    };
});
//# sourceMappingURL=Template.js.map