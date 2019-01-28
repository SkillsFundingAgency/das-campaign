System.register(["../shims/Shim"], function (exports_1, context_1) {
    "use strict";
    var Shim_1, infoBox;
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
             * name 'infoBox'
             *
             * @export
             * @returns {*} window['infoBox']
             */
            exports_1("infoBox", infoBox = Shim_1.shim('InfoBox'));
        }
    };
});
//# sourceMappingURL=InfoBox.js.map