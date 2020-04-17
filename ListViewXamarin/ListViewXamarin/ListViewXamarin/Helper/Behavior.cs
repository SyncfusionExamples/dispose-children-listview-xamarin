using Syncfusion.DataSource.Extensions;
using Syncfusion.GridCommon.ScrollAxis;
using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region Behavior
    public class Behavior : Behavior<ContentPage>
    {
        #region Fields

        SfListView ListView;
        Button DisposeButton;
        #endregion

        #region Overrides
        protected override void OnAttachedTo(ContentPage bindable)
        {
            ListView = bindable.FindByName<SfListView>("listView");
            DisposeButton = bindable.FindByName<Button>("disposeButton");

            DisposeButton.Clicked += DisposeButton_Clicked;
            this.ListView.ItemGenerator = new ItemGeneratorExt(this.ListView);

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            ListView = null;
            base.OnDetachingFrom(bindable);
        }
        #endregion

        #region Events

        private void DisposeButton_Clicked(object sender, EventArgs e)
        {
            this.ListView.Dispose();
        }
        #endregion
    }
    #endregion

    #region ItemGeneratorExt
    public class ItemGeneratorExt : ItemGenerator
    {
        #region Fields

        public SfListView listView;
        #endregion

        #region Constructor

        public ItemGeneratorExt(SfListView listView) : base(listView)
        {
            this.listView = listView;
        }
        #endregion

        #region Overrides

        protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
        {
            if (type == ItemType.Record)
                return new ListViewItemExt(this.listView);
            return base.OnCreateListViewItem(itemIndex, type, data);
        }
        #endregion
    }
    #endregion

    #region ListViewItemExt
    public class ListViewItemExt : ListViewItem
    {
        #region Fields

        private SfListView listView;
        #endregion

        #region Constructor

        public ListViewItemExt(SfListView listView)
        {
            this.listView = listView;
        }
        #endregion

        #region Overrides

        protected override void Dispose(bool disposing)
        {
            if (this.Content != null)
            {
                var grid = this.Content as Grid;
                var customEntry = (CustomEntry)grid.Children.FirstOrDefault(o => o is CustomEntry);
                customEntry.Dispose();
            }

            base.Dispose(disposing);
        }
        #endregion
    }
    #endregion
}