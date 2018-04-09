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
using adamFuncs;
using Wage;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

public partial class hr_Train_app : BasePage
{
    HRTrain hr_train = new HRTrain();
    adamClass adam = new adamClass();
    public int repeateColumn = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //txtYear.Text = DateTime.Now.Year.ToString();
            //dropMonth.SelectedValue = DateTime.Now.Month.ToString();

            for (int i = 0; i < 8; i++)
            {
                LinkButton likbtn = new LinkButton();
                Label lbl = new Label();
                lbl.Text = "text";
                likbtn.ID = "likbtn" + i;
                likbtn.Text = i.ToString();
                //likbtn.Click += new EventHandler();
                this.Page.Controls.Add(likbtn);
                this.Page.Controls.Add(lbl);
            }
        }
    }

}
