let gridInstance, editing;

document.addEventListener("DOMContentLoaded", () => {
    focusSearchPanel();
    gridInstance = $("#dictionary-definition-grid").dxDataGrid("instance");
    editing = gridInstance.option('editing');
});

function focusSearchPanel() {
    const searachPanel = document.querySelector(".dx-datagrid-search-panel input");
    searachPanel.focus();
}

function isEditButtonVisible(item) {
    const hasPermision = '@hasPermision' === 'True' ? true : false;
    return item.row.data.IsSystemic || !hasPermision;
}

function isDeleteButtonVisible(item) {
    return item.row.data.IsSystemic ? false : true;
}

function onRowInserted(item) {
    const key = item.key;
    const element = item.element[0];
    const rowEditButton = element.querySelector(`a[data-id="${key}"]`);
    rowEditButton.focus();
}

function onRowUpdated() {
    focusSearchPanel();
}

function onRowRemoved() {
    focusSearchPanel();
}

function onInitNewRow(e) {
    editing.popup.title = "Dodanie słownika";
}

function onEditingStart(e) {
    console.log(e)
    editing.popup.title = "Edycja słownika";
}

function onDisposing(e) {
    console.log(e);
}

function customizeItem(item) {
    //console.log(item);
    //console.log(editing);

    const editRowKey = editing.editRowKey;
    const rowIndex = gridInstance.getRowIndexByKey(editRowKey);

    // RowIndex === 0 oznacza, że dodajemy nowy słownik

    if (item.dataField === 'Code' && gridInstance.cellValue(rowIndex, "Name") === 'Lokalizacja') {
        item.visible = false;
    }
}

const getPrefixActiveDX = () => $("#prefix-active").dxCheckBox("instance");
const getPrefixValueDX = () => $("#prefix-value").dxRadioGroup("instance");
const getPostfixActiveDX = () => $("#postfix-active").dxCheckBox("instance");
const getPostfixValueDX = () => $("#postfix-value").dxRadioGroup("instance");

function prefixActiveChanged(e) {
    getPrefixValueDX().option("disabled", !e.value);
    regeneratePreview();
}

function postfixActiveChanged(e) {
    getPostfixValueDX().option("disabled", !e.value);
    regeneratePreview();
}

function regeneratePreview(e) {
    class Format {
        constructor(active, value) {
            this.active = active;
            this.value = value;
        }
    }

    const prefixActiveDX = getPrefixActiveDX();
    const prefixValueDX = getPrefixValueDX();
    const postfixActiveDX = getPostfixActiveDX();
    const postfixValueDX = getPostfixValueDX();

    const prefix = new Format(
        prefixActiveDX.option("value"),
        prefixValueDX.option("dataSource").store._array.find(
            x => x.value === prefixValueDX.option("value")
        ).text
    );

    const postfix = new Format(
        postfixActiveDX.option("value"),
        postfixValueDX.option("dataSource").store._array.find(
            x => x.value === postfixValueDX.option("value")
        ).text
    );

    const preview = $("#preview").dxTextBox("instance");

    preview.option("value", `${prefix.active ? prefix.value : ""}-${postfix.active ? postfix.value : ""}`);
}
