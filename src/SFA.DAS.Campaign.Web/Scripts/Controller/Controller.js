/**
 * Generic controller class to entcapsulate the parsing
 * and other generic stuff
 *
 * @export
 * @class Controller
 */
var Controller = /** @class */ (function () {
    /**
     * Creates an instance of a Controller.
     *
     * @param {Element} root The element where the controller has been applied
     */
    function Controller(element) {
        this._element = element;
    }
    Object.defineProperty(Controller.prototype, "$", {
        /**
         * Access element or query by selector
         */
        get: function () {
            return function (selector) {
                if (!(this._element instanceof Element)) {
                    throw new Error('This controller has no element!');
                }
                return selector ? this._element.querySelectorAll(selector) : this._element;
            };
        },
        set: function (value) {
            throw new Error('Setting $ is not allowed');
        },
        enumerable: true,
        configurable: true
    });
    Controller.prototype.getControllersByClass = function (controllerClass) {
        var instances = Controller._instances;
        var result = [];
        for (var i = 0, max = instances.length; i < max; i++) {
            var instance = instances[i];
            if (instance instanceof controllerClass) {
                result.push(instance);
            }
        }
        return result;
    };
    /**
     * Hook for running code before the controller is instantiated
     *
     * @static
     * @param {NodeListOf<Element>} elements List of elements where the controller will be applied
     */
    Controller.beforeInstantiating = function (elements) { };
    /**
     * Hook for running code after the controller has been instantiated.
     *
     * @static
     * @param {NodeListOf<Element>} elements List of elements where the controller has been applied
     * @param {Array<Controller>} instances List of controller instances created
     */
    Controller.afterInstantiating = function (elements, instances) { };
    /**
     * Look for elements with a specific selector and creates an instance for
     * every element.
     *
     * @static
     * @param {string} selector Dom selector
     * @param {Element} [root=document.body] Starting element for parsing
     */
    Controller.parse = function (selector, root) {
        if (root === void 0) { root = document.body; }
        if (typeof this.selector === 'string' && this.selector.length) {
            selector = this.selector;
        }
        else if (!selector) {
            throw new Error('No Selector for Controller found!');
        }
        var elements = root.querySelectorAll(selector);
        this.beforeInstantiating(elements);
        for (var i = 0; i < elements.length; i++) {
            var element = elements[i];
            var className = /^function\s+([\w\$]+)\s*\(/.exec(this.toString())[1];
            className = className.toLocaleLowerCase();
            if (element.getAttribute(Controller.PARSE_ID_ATTRIBUTE + '-' + className)) {
                continue;
            }
            element.setAttribute(Controller.PARSE_ID_ATTRIBUTE + '-' + className, Math.floor(Math.random() * 10 + 1) * Date.now() + '');
            Controller._instances.push(new this(element));
        }
        this.afterInstantiating(elements, Controller._instances);
    };
    /**
     * Instances of the Controller
     *
     * @static
     * @type {Array<Controller>}
     */
    Controller._instances = [];
    /**
     * HTML attribute to mark elements which are already parsed
     *
     * @static
     */
    Controller.PARSE_ID_ATTRIBUTE = 'data-parse-id';
    return Controller;
}());
export { Controller };
//# sourceMappingURL=Controller.js.map