/**
 * Loads variable from window according to
 * the name parameter.
 *
 * @export
 * @param {string} name
 * @returns {*} window[name]
 */
export function shim(name) {
    var global = window;
    return global[name];
}
//# sourceMappingURL=Shim.js.map