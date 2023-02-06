using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;
using DevExtreme.AspNet.Mvc.Factories;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace MvcMovie.DXCustomControls
{
    public class DropDownBoxWithTreeList
    {
        public static WidgetBuilder Render(
            FormItemEditorFactory editor, 
            List<int>? avaliableItems = null, 
            string onValueChanged = ""
        )
        {
            var avaliableItemsJSON = JsonConvert.SerializeObject(avaliableItems);
            string treeListId = Guid.NewGuid().ToString();
            
            onValueChanged = onValueChanged == "" 
                ? $"(e) => {{ dropDownBoxWithTreeList_valueChanged(e, '{treeListId}'); }}"
                : $"(e) => {{ dropDownBoxWithTreeList_valueChanged(e, '{treeListId}'); {onValueChanged} }}";

            return editor.DropDownBox()
                    .ContentTemplate(new JS($"(e) => renderTreeList(e, '{treeListId}', '{avaliableItemsJSON}')"))
                    .OnValueChanged(onValueChanged)
                    .DataSource(d => d.Mvc()
                        .Controller("DictionaryDE")
                        .LoadAction("Get")
                        .Key("Id")
                    )
                    .DisplayExpr(new JS("dictionaryExpr"))
                    .DropDownOptions(o => o.Height("auto"))
                    .Placeholder("Wybierz pozycję słownikową...")
                    .ShowClearButton(true)
                    .ValueExpr("Id");
        }
    }
}
