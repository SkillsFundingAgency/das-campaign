var require = {
    // Note: baseUrl set at optimization stage (release) or in _Layout (dev)
    // baseUrl = ./generated
    paths: {
        // Map library names to their physical location relative to baseUrl
        async: 'lib/requirejs-async/async',
        // Typescript support library
        tslib: "lib/tslib/tslib",
        "../shims/InfoBox": "lib/google-maps-infobox/infobox"
    }
};
if (typeof exports === 'object') {
    exports.config = require;
}