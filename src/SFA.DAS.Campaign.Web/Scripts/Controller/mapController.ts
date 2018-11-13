/// <reference types="@types/googlemaps" />

import { MarkerClusterer } from '../shims/MarkerClusterer';
import { infoBox } from '../shims/InfoBox';
import { template } from '../Lib/Template';

import { Controller } from './Controller';
import MarkerData = require("../Models/IMarkerData");
import IMarkerData = MarkerData.IMarkerData;

export class MapController extends Controller {

    /**
     * Selector for the Controller which contains the
     * google maps canvas
     * 
     * @static
     * @type {string} selector
     */
    static selector: string = '[data-google-map-component]';

    /**
     * Selector for the Google Map Canvas Container
     * 
     * @static
     * @type {string} Canvas Selector
     */
    static canvas: string = '[data-google-map-canvas]';

    /**
     * Selector for your infobox Template
     * 
     * @static
     * @type {string} Infobox Template Selector
     */
    static infoboxTemplate: string = '[data-google-map-infobox-template]';

    /**
     * Default Location for initialization if no
     * current Location was found.
     * 
     * @static
     * @type {Object} center
     */
    private center: google.maps.LatLng = new google.maps.LatLng(48.2, 16.3667);

    /**
     * Current instance of a Google Maps
     * 
     * @private
     * @type {*} Google Map
     */
    private map: any;


    /**
     * Current instance of a MarkerClusterer
     * 
     * @private
     * @type {*}
     * @memberOf MapController
     */
    private markerClusterer: any;

    /**
     * Currently Open Infobox - saved so we can close it.
     * 
     * @private
     * @type {*}
     */
    private openInfoBox: any;

    /**
     * Google Maps Markers Array.
     */
    private markers: Array<any> = new Array<any>();

    private markerString;


    /**
     * Creates an instance of MapController.
     * 
     * @param {HTMLElement} element Selected Element from MapController.canvas
     */
    constructor(element: HTMLElement, center: any, markerString?: string) {
        super(element);
        this.center = new google.maps.LatLng(center.latitude, center.longitude);

        if (markerString != null) {
            this.markerString = markerString;
        }

        this.initMap();
    }


    /**
     * Initialize the current map with default values.
     */
    initMap() {


        const options: google.maps.MapOptions = {

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
        this.map = new google.maps.Map(this.$(MapController.canvas)[0], options);

        // initialize markers
        if (this.markerString != null) {
            this.initMarkers(this.markerString);
        }
    }

    /**
     * Get Current Markers from Json
     */
    initMarkers(data: any) {

        let markerData: Array<IMarkerData> = JSON.parse(data);
        this.setMarkersOnMap(markerData);

    }

    addRadius(distance: number) {
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
    }

    addMarker(title: string, lat: number, lon: number, enableInfobox: boolean = true) {

        let markerData: IMarkerData = {
            Title: title,
            Latitude: lat,
            Longitude: lon,
            Location: { Latitude: lat, Longitude: lon }
            
        };

        this.setMarkerOnMap(markerData, enableInfobox);
    }
    /**
     * Transforms the current MarkerData to google maps markers
     * and saves them in the markes array.
     */
    setMarkersOnMap(markerData: Array<IMarkerData>, enableInfobox: boolean = true ) {
        let icon: any = {
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
        for (let i: number = 0, max: number = markerData.length; i < max; i++) {

            this.setMarkerOnMap(markerData[i], enableInfobox);
        }

        // initialize MarkerClusterer        
        this.initMarkerClusterer();

        // Resize Event will be triggered once after markers are set.
        google.maps.event.trigger(this.map, 'resize');

    }
    setMarkerOnMap(currentMarkerData: IMarkerData, enableInfobox: boolean = true) {

        let markerObject: google.maps.MarkerOptions = {
            position: new google.maps.LatLng(currentMarkerData.Location.Latitude, currentMarkerData.Location.Longitude),
            //  icon: icon,
            map: this.map
        }

        if (infoBox && enableInfobox) {
            markerObject['infobox'] = this.getInfoBox(currentMarkerData);
        }

        let marker: any = new google.maps.Marker(markerObject);

        if (infoBox && enableInfobox) {
            // add on click handler to the marker itself
            // so it will open our infobox.
            marker.addListener('click', () => {
                if (this.openInfoBox) {
                    this.openInfoBox.close();
                    if (this.openInfoBox === marker.infobox) {
                        this.openInfoBox = null;
                        return;
                    }
                }
                marker.infobox.open(this.map, marker);
                this.openInfoBox = marker.infobox;
            });
        }

        // add to controllers markers array.
        this.markers.push(marker);
    }

    getLatLngByPostcode(postcode: string) {
        var geocoder = new google.maps.Geocoder();


    }

    //**
    // * Generates an InfoBox Element using the InfoBox.JS Library
    // * replacing the placeholder from the <script> tag and retrieves it.
    // * 
    // * @param {IMarkerData} markerData current markerData
    // * @returns Instance of an InfoBox
    // */
    getInfoBox(markerData: IMarkerData) {

        const infoBoxTemplate: string = this.$(MapController.infoboxTemplate)[0].innerHTML.trim();

        const infoBoxTemplateData: any = {
            Title: markerData.Title,
            ShortDescription: markerData.ShortDescription,
            Url: markerData.VacancyUrl
        };
        
        const currentInfoBox: any = new infoBox({
            content: template(infoBoxTemplate, infoBoxTemplateData),
            disableAutoPan: false,
            maxWidth: 'auto',
            pixelOffset: new google.maps.Size(-132, 20),
            infoBoxClearance: new google.maps.Size(1, 1),
            closeBoxMargin: "5px 5px 2px 2px",
            closeBoxURL: "http://www.google.com/intl/en_us/mapfiles/close.gif"
        });

        return currentInfoBox;
    }

    ///**
    // * Initialize MarkerClusterer with current Map & Markers
    // * 
    // * 
    // * @memberOf MapController
    // */
    initMarkerClusterer() {
        if (MarkerClusterer) {
            this.markerClusterer = new MarkerClusterer(this.map, this.markers, { imagePath: 'https://googlemaps.github.io/js-marker-clusterer/images/m' });
        }
    }

}