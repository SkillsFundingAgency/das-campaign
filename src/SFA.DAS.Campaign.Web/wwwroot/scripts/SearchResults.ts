import { MapController } from "./Controller/mapController"

export class SearchResults {

    private mainContentElement: HTMLElement = document.getElementById("content");
    private searchResultsElement: HTMLElement = document.getElementById("vacancy-search-results");
    private lat: number = parseFloat((<HTMLInputElement>document.getElementById("Latitude")).value);
    private lon: number = parseFloat((<HTMLInputElement>document.getElementById("Longitude")).value);
    private distance: number = parseFloat((<HTMLInputElement>document.getElementById("Distance")).value);
    private center: any = { latitude: this.lat, longitude: this.lon };
    private markersData: string = (<HTMLInputElement>document.getElementById("MapSearchResults")).value;

    private searchResultsMaps: Array<MapController> = [];

    constructor() {
        var map = new MapController(this.mainContentElement, this.center, this.markersData);

        map.addRadius(this.distance);

        var searchResults = this.searchResultsElement.querySelectorAll("li.search-result");

        searchResults.forEach(function (element:HTMLElement) {
            var center = { latitude: element.dataset.lat, longitude: element.dataset.lon };
            
            var resultMap = new MapController(element, center, null);


            resultMap.addMarker(element.dataset.id,parseFloat(element.dataset.lat),parseFloat(element.dataset.lon),false);

        });

    }

}

function init() {
    var results = new SearchResults();
}

init()