using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Mvc.Builders;
using DevExtreme.AspNet.Mvc.Factories;
using static System.Net.Mime.MediaTypeNames;
using Newtonsoft.Json;

namespace MvcMovie.DXCustomControls
{
    public class DropDownBoxWithTreeList
    {
        public static WidgetBuilder Render(FormItemEditorFactory editor)
        {
            var items = new List<int>{
                1, 2, 3, 4
            };

            var itemsJSON = JsonConvert.SerializeObject(items);
            string treeListId = Guid.NewGuid().ToString();
            return editor.DropDownBox()
                    .ContentTemplate(new JS($"(e) => renderTreeList(e, '{treeListId}', '{itemsJSON}')"))
                    .OnValueChanged($"(e) => dropDownBoxWithTreeList_valueChanged(e, '{treeListId}')")
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
