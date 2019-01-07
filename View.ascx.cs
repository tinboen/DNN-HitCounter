using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
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
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Services.Exceptions;

using DotNetNuke.Security;

using DotNetNuke.Services.Exceptions;
using System.Collections.Generic;

namespace DotNetNuke.Modules.HitCounter
{
    public partial class View : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            long num = (long)0;
            int length = 6;
            int.TryParse((string)Settings["NoOfDigits"], out length);
            string item = (string)Settings["Counter"];
            string str = (string)Settings["CounterType"];
            num = (!string.IsNullOrEmpty(item) ? long.Parse(item) : (long)0);
            if (str == "2")
            {
                int UrlId = ModuleId;
                //check if the user opening the site for the first time 
                if (Session["URLHistory"] != null)
                {
                    //The session variable exists. So the user has already visited this site and sessions is still alive. Check if this page is already visited by the user
                    List<int> HistoryURLs = (List<int>)Session["URLHistory"];
                    if (HistoryURLs.Exists((element => element == UrlId)))
                    {
                        //If the user has already visited this page in this session, then we can ignore this visit. No need to update the counter.
                        Session["VisitedURL"] = 0;
                    }
                    else
                    {
                        //if the user is visting this page for the first time in this session, then count this visit and also add this page to the list of visited pages(URLHistory variable)
                        HistoryURLs.Add(UrlId);

                        // Outside of Web Forms page class, use HttpContext.Current.  
                        HttpContext context = HttpContext.Current;
                        context.Session["URLHistory"] = HistoryURLs;

                        //Make a note of the page Id to update the database later 
                        context.Session["VisitedURL"] = UrlId;

                        num += (long)1;
                        (new ModuleController()).UpdateTabModuleSetting(TabModuleId, "Counter", num.ToString());
                    }
                }
                else
                {
                    //if there is no session variable already created, then the user is visiting this page for the first time in this session. Then create a session variable and take the count of the page Id
                    List<int> HistoryURLs = new List<int>();
                    HistoryURLs.Add(UrlId);

                    // Outside of Web Forms page class, use HttpContext.Current.  
                    HttpContext context = HttpContext.Current;
                    context.Session["URLHistory"] = HistoryURLs;
                    context.Session["VisitedURL"] = UrlId;

                    num += (long)1;
                    (new ModuleController()).UpdateTabModuleSetting(TabModuleId, "Counter", num.ToString());
                }
            }
            else
            {
                num += (long)1;
                (new ModuleController()).UpdateTabModuleSetting(TabModuleId, "Counter", num.ToString());
            }
            length -= item.Length;
            StringBuilder stringBuilder = new StringBuilder();
            if (length > 0)
            {
                for (int i = 1; i <= length; i++)
                {
                    stringBuilder.Append("<span class='counter'>0</span>");
                }
            }
            for (int j = 0; j < num.ToString().Length; j++)
            {
                stringBuilder.Append(string.Concat("<span class='counter'>", num.ToString().Substring(j, 1), "</span>"));
            }
            litModule.Text = stringBuilder.ToString();
        }

        #region overriden properties
        public DotNetNuke.Framework.CDefault BasePage
        {
            get
            {
                return (DotNetNuke.Framework.CDefault)this.Page;
            }
        }
        #endregion
    }
}
    