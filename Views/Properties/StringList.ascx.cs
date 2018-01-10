using System;
using EPiServer.Web;

namespace EpiserverSite.Views.Properties
{
    public partial class StringList : PropertyControlBase<string[]>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (CurrentData != null)
            {
                list.DataSource = CurrentData;
                list.DataBind();
            }
        }
    }
}
