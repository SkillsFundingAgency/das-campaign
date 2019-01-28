System.register([], function (exports_1, context_1) {
    "use strict";
    var __moduleName = context_1 && context_1.id;
    /**
     * Loads variable from window according to
     * the name parameter.
     *
     * @export
     * @param {string} name
     * @returns {*} window[name]
     */
    function shim(name) {
        var global = window;
        return global[name];
    }
    exports_1("shim", shim);
    return {
        setters: [],
        execute: function () {
        }
    };
});
//# sourceMappingURL=Shim.js.map