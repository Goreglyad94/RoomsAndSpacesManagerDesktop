using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RoomsAndSpacesManagerLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomsAndSpacesManagerLib.Models.RevitModels
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class SelectRoomEventHendler : IExternalEventHandler
    {
        public static event Action<object> ChangeUI;

        public void Execute(UIApplication app)
        {
            Autodesk.Revit.UI.UIDocument uiDoc = app.ActiveUIDocument;

            // Get the element selection of current document.
            Autodesk.Revit.UI.Selection.Selection selection = uiDoc.Selection;
            var selectedIds = uiDoc.Selection.GetElementIds();

            Element dd = uiDoc.Document.GetElement(selectedIds.First());

            MainWindowViewModel.roomId = dd.Id.IntegerValue;
            ChangeUI.Invoke(this);


        }

        public string GetName() => nameof(SelectRoomEventHendler);
    }
}
