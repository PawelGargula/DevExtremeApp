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
                    if (arr.includes(options.data.Id)) {
                        container.append($('<span>', {
                            text: displayValue
                        }));
                    } else {
                        container.append($('<span>', {
                            style: 'color: #EBEBE4;',
                            title: 'Zablokowana pozycja',
                            text: displayValue
                        }));
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
                e.component.option('inputAttr', { title: selectedItems.selectedRowsData[0].Name });
                e.component.close();
                e.component.focus();
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