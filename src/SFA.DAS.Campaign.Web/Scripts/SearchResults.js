define(["require", "exports", "./Controller/mapController"], function (require, exports, mapController_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    var SearchResults = /** @class */ (function () {
        function SearchResults() {
            this.mainContentElement = document.getElementById("content");
            this.searchResultsElement = document.getElementById("vacancy-search-results");
            this.lat = parseFloat(document.getElementById("Latitude").value);
            this.lon = parseFloat(document.getElementById("Longitude").value);
            this.distance = parseFloat(document.getElementById("Distance").value);
            this.center = { latitude: this.lat, longitude: this.lon };
            this.markersData = document.getElementById("MapSearchResults").value;
            this.searchResultsMaps = [];
            var map = new mapController_1.MapController(this.mainContentElement, this.center, this.markersData);
            map.addRadius(this.distance);
            var searchResults = this.searchResultsElement.querySelectorAll("li.search-result");
            searchResults.forEach(function (element) {
                var center = { latitude: element.dataset.lat, longitude: element.dataset.lon };
                var resultMap = new mapController_1.MapController(element, center, null);
                resultMap.addMarker(element.dataset.id, parseFloat(element.dataset.lat), parseFloat(element.dataset.lon), false);
            });
        }
        return SearchResults;
    }());
    exports.SearchResults = SearchResults;
    function init() {
        var results = new SearchResults();
    }
    init();
});
//# sourceMappingURL=SearchResults.js.map