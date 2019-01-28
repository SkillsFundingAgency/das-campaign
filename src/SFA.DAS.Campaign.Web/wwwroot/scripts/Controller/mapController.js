/// <reference types="@types/googlemaps" />
System.register(["../shims/MarkerClusterer", "../shims/InfoBox", "../Lib/Template", "./Controller"], function (exports_1, context_1) {
    "use strict";
    var __extends = (this && this.__extends) || (function () {
        var extendStatics = function (d, b) {
            extendStatics = Object.setPrototypeOf ||
                ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
                function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
            return extendStatics(d, b);
        }
        return function (d, b) {
            extendStatics(d, b);
            function __() { this.constructor = d; }
            d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
        };
    })();
    var MarkerClusterer_1, InfoBox_1, Template_1, Controller_1, MapController;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (MarkerClusterer_1_1) {
                MarkerClusterer_1 = MarkerClusterer_1_1;
            },
            function (InfoBox_1_1) {
                InfoBox_1 = InfoBox_1_1;
            },
            function (Template_1_1) {
                Template_1 = Template_1_1;
            },
            function (Controller_1_1) {
                Controller_1 = Controller_1_1;
            }
        ],
        execute: function () {/// <reference types="@types/googlemaps" />
            MapController = /** @class */ (function (_super) {
                __extends(MapController, _super);
                /**
                 * Creates an instance of MapController.
                 *
                 * @param {HTMLElement} element Selected Element from MapController.canvas
                 */
                function MapController(element, center, markerString) {
                    var _this = _super.call(this, element) || this;
                    /**
                     * Default Location for initialization if no
                     * current Location was found.
                     *
                     * @static
                     * @type {Object} center
                     */
                    _this.center = new google.maps.LatLng(48.2, 16.3667);
                    /**
                     * Google Maps Markers Array.
                     */
                    _this.markers = new Array();
                    _this.center = new google.maps.LatLng(center.latitude, center.longitude);
                    if (markerString != null) {
                        _this.markerString = markerString;
                    }
                    _this.initMap();
                    return _this;
                }
                /**
                 * Initialize the current map with default values.
                 */
                MapController.prototype.initMap = function () {
                    var options = {
                        center: this.center,
                        scrollwheel: false,
                        zoom: 10,
                        maxZoom: 14,
                        zoomControlOptions: {
                            position: google.maps.ControlPosition.RIGHT_CENTER
                        },
                        mapTypeControl: false,
                        streetViewControl: false
                    };
                    // init Google Maps itself
                    this.map = new google.maps.Map(document.querySelector(MapController.canvas), options);
                    // initialize markers
                    if (this.markerString != null) {
                        this.initMarkers(this.markerString);
                    }
                };
                /**
                 * Get Current Markers from Json
                 */
                MapController.prototype.initMarkers = function (data) {
                    var markerData = JSON.parse(data);
                    this.setMarkersOnMap(markerData);
                };
                MapController.prototype.addRadius = function (distance) {
                    var self = this;
                    var circ = new google.maps.Circle({
                        strokeColor: "#111111",
                        strokeOpacity: 0.8,
                        strokeWeight: 2,
                        fillColor: "#111111",
                        fillOpacity: 0.35,
                        map: this.map,
                        center: this.center,
                        radius: distance * 1609.0
                    });
                    this.map.fitBounds(circ.getBounds());
                };
                MapController.prototype.addMarker = function (title, lat, lon, enableInfobox) {
                    if (enableInfobox === void 0) { enableInfobox = true; }
                    var markerData = {
                        Title: title,
                        Latitude: lat,
                        Longitude: lon,
                        Location: { Latitude: lat, Longitude: lon }
                    };
                    this.setMarkerOnMap(markerData, enableInfobox);
                };
                /**
                 * Transforms the current MarkerData to google maps markers
                 * and saves them in the markes array.
                 */
                MapController.prototype.setMarkersOnMap = function (markerData, enableInfobox) {
                    if (enableInfobox === void 0) { enableInfobox = true; }
                    var icon = {
                        //url: '/images/icon.png?v=' + config.project.version,
                        // This marker is 45 pixels wide by 40 pixels high.
                        size: new google.maps.Size(45, 40),
                        scaledSize: new google.maps.Size(45, 40),
                        // The origin for this image is (0, 0).
                        origin: new google.maps.Point(0, 0),
                        // The anchor for this image is the base of the flagpole at (0, 0).
                        anchor: new google.maps.Point(0, 0)
                    };
                    // iterate over marker data and create a marker
                    // also we will append the current marker data to the
                    // google marker itself - maybe we will need it later
                    for (var i = 0, max = markerData.length; i < max; i++) {
                        this.setMarkerOnMap(markerData[i], enableInfobox);
                    }
                    // initialize MarkerClusterer        
                    this.initMarkerClusterer();
                    // Resize Event will be triggered once after markers are set.
                    google.maps.event.trigger(this.map, 'resize');
                };
                MapController.prototype.setMarkerOnMap = function (currentMarkerData, enableInfobox) {
                    var _this = this;
                    if (enableInfobox === void 0) { enableInfobox = true; }
                    var markerObject = {
                        position: new google.maps.LatLng(currentMarkerData.Location.Latitude, currentMarkerData.Location.Longitude),
                        //  icon: icon,
                        map: this.map
                    };
                    if (InfoBox_1.infoBox && enableInfobox) {
                        markerObject['infobox'] = this.getInfoBox(currentMarkerData);
                    }
                    var marker = new google.maps.Marker(markerObject);
                    if (InfoBox_1.infoBox && enableInfobox) {
                        // add on click handler to the marker itself
                        // so it will open our infobox.
                        marker.addListener('click', function () {
                            if (_this.openInfoBox) {
                                _this.openInfoBox.close();
                                if (_this.openInfoBox === marker.infobox) {
                                    _this.openInfoBox = null;
                                    return;
                                }
                            }
                            marker.infobox.open(_this.map, marker);
                            _this.openInfoBox = marker.infobox;
                        });
                    }
                    // add to controllers markers array.
                    this.markers.push(marker);
                };
                MapController.prototype.getLatLngByPostcode = function (postcode) {
                    var geocoder = new google.maps.Geocoder();
                };
                //**
                // * Generates an InfoBox Element using the InfoBox.JS Library
                // * replacing the placeholder from the <script> tag and retrieves it.
                // * 
                // * @param {IMarkerData} markerData current markerData
                // * @returns Instance of an InfoBox
                // */
                MapController.prototype.getInfoBox = function (markerData) {
                    var infoBoxTemplate = this.$(MapController.infoboxTemplate)[0].innerHTML.trim();
                    var infoBoxTemplateData = {
                        Title: markerData.Title,
                        ShortDescription: markerData.ShortDescription,
                        Url: markerData.VacancyUrl
                    };
                    var currentInfoBox = new InfoBox_1.infoBox({
                        content: Template_1.template(infoBoxTemplate, infoBoxTemplateData),
                        disableAutoPan: false,
                        maxWidth: 'auto',
                        pixelOffset: new google.maps.Size(-132, 20),
                        infoBoxClearance: new google.maps.Size(1, 1),
                        closeBoxMargin: "5px 5px 2px 2px",
                        closeBoxURL: "http://www.google.com/intl/en_us/mapfiles/close.gif"
                    });
                    return currentInfoBox;
                };
                ///**
                // * Initialize MarkerClusterer with current Map & Markers
                // * 
                // * 
                // * @memberOf MapController
                // */
                MapController.prototype.initMarkerClusterer = function () {
                    if (MarkerClusterer_1.MarkerClusterer) {
                        this.markerClusterer = new MarkerClusterer_1.MarkerClusterer(this.map, this.markers, { imagePath: 'https://googlemaps.github.io/js-marker-clusterer/images/m' });
                    }
                };
                /**
                 * Selector for the Controller which contains the
                 * google maps canvas
                 *
                 * @static
                 * @type {string} selector
                 */
                MapController.selector = '[data-google-map-component]';
                /**
                 * Selector for the Google Map Canvas Container
                 *
                 * @static
                 * @type {string} Canvas Selector
                 */
                MapController.canvas = '[data-google-map-canvas]';
                /**
                 * Selector for your infobox Template
                 *
                 * @static
                 * @type {string} Infobox Template Selector
                 */
                MapController.infoboxTemplate = '[data-google-map-infobox-template]';
                return MapController;
            }(Controller_1.Controller));
            exports_1("MapController", MapController);
        }
    };
});
//# sourceMappingURL=mapController.js.map