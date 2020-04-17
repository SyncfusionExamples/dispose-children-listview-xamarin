# How to dispose of children of ListView in Xamarin.Forms (SfListView)
You can dispose the custom view that was loaded in [SfListView.ItemTemplate](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemsSource.html?) in Xamarin.Forms [SfListView](https://help.syncfusion.com/xamarin/listview/overview?) by customizing the [ItemGenerator](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ItemGenerator.html?).

You can also refer the following article.

https://www.syncfusion.com/kb/11417/how-to-dispose-of-children-of-listview-in-xamarin-forms-sflistview 

**C#**

Created the custom [Editor](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/editor) control in the **PCL** project that implements [IDisposable](https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=netframework-4.8).
``` c#
namespace ListViewXamarin
{
    public class CustomEntry : Entry, IDisposable
    {
        public CustomEntry()
        {
            this.Text = "Country";
            BackgroundColor = Color.LightGray;
        }
        
        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("custom control disposed");
        }
    }
}
```
**XAML**

Add the custom control in the **ItemTemplate**.
``` xml
<syncfusion:SfListView x:Name="listView" ItemSize="60" ItemsSource="{Binding ContactsInfo}">
    <syncfusion:SfListView.ItemTemplate >
        <DataTemplate>
            <Grid x:Name="grid">
                <Grid.ColumnDefinitions>
                    <ColumnDefinition Width="70" />
                    <ColumnDefinition Width="250" />
                    <ColumnDefinition Width="*" />
                </Grid.ColumnDefinitions>
                <Image Source="{Binding ContactImage}" VerticalOptions="Center" HorizontalOptions="Center" HeightRequest="50" WidthRequest="50"/>
                <Label LineBreakMode="NoWrap" TextColor="#474747" Text="{Binding ContactName}" Grid.Column="1"/>
                <local:CustomEntry Placeholder="Enter here" Grid.Column="2"/>
            </Grid>
        </DataTemplate>
    </syncfusion:SfListView.ItemTemplate>
</syncfusion:SfListView>
```
**C#**

Extend the ItemGenerator and [ListViewItem](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem.html?), and override the Dispose method to [dispose](https://help.syncfusion.com/cr/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.ListViewItem~Dispose.html?) the custom control.
``` c#
public class ItemGeneratorExt : ItemGenerator
{
    public SfListView listView;
 
    public ItemGeneratorExt(SfListView listView) : base(listView)
    {
        this.listView = listView;
    }
 
    protected override ListViewItem OnCreateListViewItem(int itemIndex, ItemType type, object data = null)
    {
        if (type == ItemType.Record)
            return new ListViewItemExt(this.listView);
        return base.OnCreateListViewItem(itemIndex, type, data);
    }
}
 
public class ListViewItemExt : ListViewItem
{
    private SfListView listView;
 
    public ListViewItemExt(SfListView listView)
    {
        this.listView = listView;
    }
 
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
}
```
