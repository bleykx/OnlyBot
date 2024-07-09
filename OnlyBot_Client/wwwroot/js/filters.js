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
    initializeTypeaheadForScripts: function(inputId, scriptNames) {
        // Vérifiez que Bloodhound est défini
        if (typeof Bloodhound !== 'undefined') {
            var scripts = new Bloodhound({
                datumTokenizer: Bloodhound.tokenizers.whitespace,
                queryTokenizer: Bloodhound.tokenizers.whitespace,
                local: scriptNames,
                limit: 10
            });

            $('#' + inputId).typeahead(
                {
                    hint: false,
                    highlight: false,
                    minLength: 1
                },
                {
                    name: 'scripts',
                    source: scripts,
                    limit: 10, // Limiter le nombre de suggestions à 10
                    templates: {
                        suggestion: function (data) {
                            return '<p>' + data + '</p>';
                        }
                    }
                }
            ).on('typeahead:selected', function (event, suggestion) {
                $(this).typeahead('val', suggestion);
                DotNet.invokeMethodAsync('OnlyBot_Client', 'UpdateSearchQuery', suggestion);
            });
        } else {
            console.error("Bloodhound is not defined.");
        }
    }
}
