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
