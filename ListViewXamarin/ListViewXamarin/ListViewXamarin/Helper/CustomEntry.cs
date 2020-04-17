using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewXamarin
{
    #region Custom Entry
    public class CustomEntry : Entry, IDisposable
    {
        public CustomEntry()
        {
            this.Placeholder = "Enter here";
            this.BackgroundColor = Color.LightGray;
        }
        
        public void Dispose()
        {
            System.Diagnostics.Debug.WriteLine("Custom control disposed");
        }
    }
    #endregion
}
