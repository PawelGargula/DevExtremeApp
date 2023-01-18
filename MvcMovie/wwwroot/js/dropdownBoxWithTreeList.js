function renderTreeList(e, treeListid) {
    const $treeList = $('<div>').dxTreeList({
        columns: [
            {
                dataField: 'Code',
                visible: false
            },
            {
                dataField: 'Name',
                cellTemplate(container, options) {
                    const displayValue = `${options.data.Code} - ${options.value}`;
                    const arr = [2, 3, 4, 5, 6, 11];
                    const tableRow = container[0].parentElement.parentElement;
                    tableRow.dataset.id = options.data.Id;
                    if (arr.includes(options.data.Id)) {
                        container.append($('<span>', {
                            text: displayValue
                        }));
                    } else {
                        container.append($('<span>', {
                            style: 'color: #EBEBE4;',
                            text: displayValue
                        }));
                        tableRow.title = 'Zablokowana pozycja';
                    }
                }
            }
        ],
        dataSource: e.component.getDataSource(),
        elementAttr: {
            class: 'glossary-treelist',
            id: treeListid
        },
        height: '100%',
        hoverStateEnabled: true,
        keyExpr: 'Id',
        noDataText: 'Brak słowników',
        parentIdExpr: 'ParentId',
        scrolling: {
            mode: 'virtual',
            renderAsync: true
        },
        searchPanel: {
            placeholder: 'Szukaj...',
            visible: true
        },
        showColumnHeaders: false,
        selection: {
            mode: 'single'
        },
        selectedRowKeys: e.component.option('value') ? [component.option('value')] : [],
        onSelectionChanged(selectedItems) {
            const arr = [2, 3, 4, 5, 6, 11];
            const keys = selectedItems.selectedRowKeys;
            const data = selectedItems.selectedRowsData[0];
            if (!data) return;
            if (arr.includes(keys[0])) {
                e.component.option('value', keys);
                e.component.option('inputAttr', { title: data.Name });
                e.component.close();
                e.component.focus();
            } else {
                DevExpress.ui.notify(
                    {
                        message: 'Nie można wybrać zablokowanej pozycji',
                        width: 'fit-content',
                        position: {
                            my: 'bottom',
                            at: 'bottom',
                            of: `#${treeListid}`
                        }
                    },
                    'warning',
                    1000
                );

                const element = selectedItems.element[0];
                const currentSelectedRowKey = selectedItems.currentSelectedRowKeys[0];
                const currentSelectedRow = element.querySelector(`tr[data-id="${currentSelectedRowKey}"]`);
                currentSelectedRow.classList.remove('dx-selection');
                currentSelectedRow.ariaSelected = 'false';
            }
        },
        wordWrapEnabled: true
    });

    return $treeList;
}

function dropDownBoxWithTreeList_valueChanged(e, treeListId) {
    const $treeList = $(`#${treeListId}`);
    if ($treeList.length) {
        const treeList = $treeList.dxTreeList("instance");
        e.component._labelContainerElement.title = "";
        treeList.selectRows(e.value, false);
    }
}

function dictionaryExpr(dictionary) {
    return `${dictionary.Code} - ${dictionary.Name}`;
}