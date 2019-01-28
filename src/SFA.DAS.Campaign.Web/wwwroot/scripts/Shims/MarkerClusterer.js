System.register(["../shims/Shim"], function (exports_1, context_1) {
    "use strict";
    var Shim_1, MarkerClusterer;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (Shim_1_1) {
                Shim_1 = Shim_1_1;
            }
        ],
        execute: function () {
            /**
             * Loads variable from window with the
             * name 'MarkerClusterer'
             *
             * @export
             * @returns {*} window['MarkerClusterer']
             */
            exports_1("MarkerClusterer", MarkerClusterer = Shim_1.shim('MarkerClusterer'));
        }
    };
});
//# sourceMappingURL=MarkerClusterer.js.map