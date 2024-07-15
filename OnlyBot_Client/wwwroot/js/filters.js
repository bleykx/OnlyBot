window.checkboxInterop = {
    checkCheckbox: function (checkboxId) {
        var checkbox = document.getElementById(checkboxId);
        if (checkbox) {
            return checkbox.checked;
        }
        return false;
    },
    initCheckbox: function (checkboxId, value) {
        var checkbox = document.getElementById(checkboxId);
        if (checkbox) {
            checkbox.checked = value;
        }
    },
    clearCheckboxes: function (checkboxId) {
        var checkbox = document.getElementById(checkboxId);
        if (checkbox) {
            checkbox.checked = false;
        }
    }
};

window.selectInterop = {
    getSelectedOption: function (selectId) {
        var selectElement = document.getElementById(selectId);
        if (selectElement) {
            return selectElement.value;
        }
        return null;
    }
};

window.searchInterop = {
    initializeTypeaheadForScripts: function (inputId, scriptNames) {
        // Vérifiez que Bloodhound est défini
        if (typeof Bloodhound !== 'undefined') {
            var scripts = new Bloodhound({
                datumTokenizer: function (datum) {
                    return Bloodhound.tokenizers.whitespace(datum);
                },
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: scriptNames,
                identify: function (obj) { return obj; }
            });

            var substringMatcher = function (strs) {
                return function findMatches(q, cb) {
                    var matches = [];
                    var substrRegex = new RegExp(q, 'i');
                    $.each(strs, function (i, str) {
                        if (substrRegex.test(str)) {
                            matches.push(str);
                        }
                    });
                    cb(matches);
                };
            };

            var $input = $('#' + inputId);

            // Détruire le typeahead existant s'il existe
            if ($input.data('ttTypeahead')) {
                $input.typeahead('destroy');
                $input.off('typeahead:selected');
            }

            $input.typeahead(
                {
                    hint: false,
                    highlight: true,
                    minLength: 1
                },
                {
                    name: 'scripts',
                    source: substringMatcher(scriptNames),
                    limit: 10, // Limiter le nombre de suggestions à 10
                    templates: {
                        suggestion: function (data) {
                            // Highlight the matching part of the text
                            var query = $input.typeahead('val');
                            var regex = new RegExp('(' + query + ')', 'gi');
                            var highlightedText = data.replace(regex, "<strong class='tt-highlight'>$1</strong>");
                            return '<p>' + highlightedText + '</p>';
                        }
                    }
                }
            ).on('typeahead:selected', async function (event, suggestion) {
                $(this).typeahead('val', suggestion);
                await DotNet.invokeMethodAsync('OnlyBot_Client', 'UpdateSearchQuery', suggestion);
            });
        } else {
            console.error("Bloodhound is not defined.");
        }
    },
    updateTypeaheadList: function (inputId, newScriptNames) {
        var $input = $('#' + inputId);

        // Détruire le typeahead existant s'il existe
        if ($input.data('ttTypeahead')) {
            $input.typeahead('destroy');
            $input.off('typeahead:selected');
        }

        // Réinitialiser le typeahead avec les nouveaux scripts
        searchInterop.initializeTypeaheadForScripts(inputId, newScriptNames);
    }
};
