define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
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
    exports.shim = shim;
});
//# sourceMappingURL=Shim.js.map