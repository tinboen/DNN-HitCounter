using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using DotNetNuke;
using DotNetNuke.Common;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Users;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Security.Roles;

namespace DotNetNuke.Modules.HitCounter
{
    public partial class Settings : ModuleSettingsBase 
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
		
        protected void ShowSettings()
        {
            ddCounterType.Items.Add(new ListItem("PAGELOAD", "1"));
            ddCounterType.Items.Add(new ListItem("SESSION", "2"));
            try
            {
                if (Page.IsPostBack == false)
                {
                    if ((string)TabModuleSettings["NoOfDigits"] == null)
                    {
                        txtNoOfDigits.Text = "6";
                    }
                    else
                    {
                        txtNoOfDigits.Text = (string)TabModuleSettings["NoOfDigits"];
                    }

                    if ((string)TabModuleSettings["CounterType"] != null)
                    {
                        ddCounterType.Items.FindByValue((string)TabModuleSettings["CounterType"]).Selected = true;
                    }
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
		
        protected void SaveSettings()
        {
            try
            {
                ModuleController modCtrl = new ModuleController();
                modCtrl.UpdateTabModuleSetting(TabModuleId, "NoOfDigits", txtNoOfDigits.Text);
                modCtrl.UpdateTabModuleSetting(TabModuleId, "CounterType", ddCounterType.SelectedValue);
                if ((string)TabModuleSettings["Counter"] == null)
                {
                    modCtrl.UpdateTabModuleSetting(TabModuleId, "Counter", "0");
                }
            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }
		
        public override void LoadSettings()
        {
            base.LoadSettings();
            ShowSettings();
        }
		
        public override void UpdateSettings()
        {
            SaveSettings();
            base.UpdateSettings();
        }

    }
}
