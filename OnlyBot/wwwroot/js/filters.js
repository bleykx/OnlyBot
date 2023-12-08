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