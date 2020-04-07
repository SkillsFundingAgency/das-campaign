var $keywordsInput = $('#Keywords.input');
var configUrl = $('body').data('fat-api');

    if ($keywordsInput.length > 0 && configUrl) {

        var container, apiUrl;
        
        $keywordsInput.wrap('<div id="fat-autocomplete-container"></div>');
        container = document.querySelector('#fat-autocomplete-container'),
        apiUrl = configUrl + '/v3/apprenticeship-programmes/autocomplete';

        $(container).empty();
        var getSuggestions = function (query, updateResults) {

            var results = [];
            $.ajax({
                url: apiUrl,
                dataType: 'json',
                data: { searchString: query }

            }).done(function (data) {
                results = data.Results.map(function (r) {
                    return r.Title;
                });
                updateResults(results);
            });
        };

        accessibleAutocomplete({
            element: container,
            id: 'Keywords',
            name: 'Keywords',
            displayMenu: 'overlay',
            showNoOptionsFound: false,
            source: getSuggestions,
            placeholder: "Apprenticeship name or job role..."
        });
    }