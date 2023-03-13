function renderTreeList(e, treeListid, avaliableItemsJSON, selectLeavesOnlyJSON) {
    const avaliableItemsArr = JSON.parse(avaliableItemsJSON);
    const selectLeavesOnlyBool = JSON.parse(selectLeavesOnlyJSON);
    const canSelectTr = (isTrExpandable) => selectLeavesOnlyBool ? !isTrExpandable : true;

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
                    const tableRow = container[0].parentElement.parentElement;
                    tableRow.dataset.id = options.data.Id;
                    if (isAvaliable(avaliableItemsArr, options.data.Id)) {
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
            const keys = selectedItems.selectedRowKeys;
            const data = selectedItems.selectedRowsData[0];
            const expandableTr = document.querySelector(`tr[data-id="${keys[0]}"][aria-expanded]`);
            const isTrExpandable = Boolean(expandableTr);
            const componentValue = e.component.option('value') !== null ? e.component.option('value')[0] : null;
            const setValueFromComponent = () => {
                const $treeList = $(`#${treeListid}`);
                if ($treeList.length) {
                    const treeList = $treeList.dxTreeList("instance");
                    treeList.selectRows(componentValue, false);
                }
            };
            if (!data) return;
            if (isAvaliable(avaliableItemsArr, keys[0])
                && componentValue !== keys[0]
                && canSelectTr(isTrExpandable)) {
                e.component.option('value', keys);
                e.component.option('inputAttr', { title: data.Name });
                e.component.close();
                e.component.focus();
            } else if (!canSelectTr(isTrExpandable)) {
                DevExpress.ui.notify(
                    {
                        message: 'Nie można wybrać pozycji, która nie jest na najniższym szczeblu drzewa',
                        width: 'fit-content',
                        position: {
                            my: 'bottom',
                            at: 'bottom',
                            of: `#${treeListid}`
                        }
                    },
                    'warning',
                    2000
                );
                setValueFromComponent();
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
                setValueFromComponent();
            }
        },
        wordWrapEnabled: true
    });

    return $treeList;
}

function isAvaliable(avaliableItems, id) {
    return avaliableItems ? avaliableItems.includes(id) : true; 
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

function customValueChanged(e) {
    console.log(`Custom value changed, current value is ${e.value}`);
}