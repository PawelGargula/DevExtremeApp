document.addEventListener("DOMContentLoaded", () => {
    focusSearchPanel();
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
