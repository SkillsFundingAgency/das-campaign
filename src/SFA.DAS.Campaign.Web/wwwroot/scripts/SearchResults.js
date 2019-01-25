import { MapController } from "./Controller/mapController";
var SearchResults = /** @class */ (function () {
    function SearchResults() {
        this.mainContentElement = document.getElementById("main-content");
        this.searchResultsElement = document.getElementById("vacancy-search-results");
        this.lat = parseFloat(document.getElementById("Latitude").value);
        this.lon = parseFloat(document.getElementById("Longitude").value);
        this.distance = parseFloat(document.getElementById("Distance").value);
        this.center = { latitude: this.lat, longitude: this.lon };
        this.markersData = document.getElementById("MapSearchResults").value;
        this.searchResultsMaps = [];
        var map = new MapController(this.mainContentElement, this.center, this.markersData);
        map.addRadius(this.distance);
        var searchResults = this.searchResultsElement.querySelectorAll("li.search-result");
        searchResults.forEach(function (element) {
            var center = { latitude: element.dataset.lat, longitude: element.dataset.lon };
            var resultMap = new MapController(element, center, null);
            resultMap.addMarker(element.dataset.id, parseFloat(element.dataset.lat), parseFloat(element.dataset.lon), false);
        });
    }
    return SearchResults;
}());
export { SearchResults };
function init() {
    var results = new SearchResults();
}
init();
//# sourceMappingURL=SearchResults.js.map