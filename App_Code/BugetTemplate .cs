using System;

using System.Data;

using System.Configuration;

using System.Web;

using System.Web.Security;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;





namespace BudgetProcess

{

    /// <summary>

    /// GridViewTemplate 的摘要说明

    /// </summary>



    public class GridViewTemplate : ITemplate

    {

        private string strControlType;

        private string strColumnName;

        private DataControlRowType dcrtColumnType;



        public GridViewTemplate()

        {

            //

            // TODO: 在此处添加构造函数逻辑

            //

        }



        /**/

        /// <summary>

        /// 动态添加模版列

        /// </summary>

        /// <param name="strColumnName">列名</param>

        /// <param name="dcrtColumnType">列的类型</param>

        public GridViewTemplate(string strControlType, string strColumnName, DataControlRowType dcrtColumnType)

        {

            this.strControlType = strControlType;

            this.strColumnName = strColumnName;

            this.dcrtColumnType = dcrtColumnType;

        }



        public void InstantiateIn(Control ctlContainer)

        {

            switch (dcrtColumnType)

            {

                case DataControlRowType.Header: //列标题

                    Literal ltr = new Literal();

                    ltr.Text = strColumnName;

                    ctlContainer.Controls.Add(ltr);

                    break;

                case DataControlRowType.DataRow: //模版列内容

                    switch (strControlType.ToLower())

                    {

                        case "label":

                            Label lbl = new Label();

                            lbl.ID = "lbl";

                            lbl.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(lbl);

                            break;

                        case "textbox":

                            TextBox txt = new TextBox();

                            txt.ID = "txt";

                            txt.Width = Unit.Pixel(70);

                            txt.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(txt);

                            break;

                        case "hyperlink":

                            HyperLink hlink = new HyperLink();
                            hlink.Width = Unit.Pixel(70);
                            hlink.NavigateUrl = "";
                            hlink.Target = "_blank";
                            hlink.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(hlink);

                            break;
                    }

                    break;

            }

        }

        // Create a public method that will handle the

        // DataBinding event called in the InstantiateIn method.

        public void BindData(object sender, EventArgs e)

        {

            switch (strControlType.ToLower())

            {

                case "label":

                    Label lbl = (Label)sender;

                    GridViewRow labelcontainer = (GridViewRow)lbl.NamingContainer;

                    lbl.Text = ((DataRowView)((GridViewRow)labelcontainer).DataItem)[strColumnName].ToString();

                    break;

                case "textbox":

                    TextBox txt = (TextBox)sender;

                    GridViewRow textboxcontainer = (GridViewRow)txt.NamingContainer;

                    txt.Text = ((DataRowView)((GridViewRow)textboxcontainer).DataItem)[strColumnName].ToString();

                    break;
                case "hyperlink":

                    HyperLink hlink = (HyperLink)sender;

                    GridViewRow hlinkcontainer = (GridViewRow)hlink.NamingContainer;

                    hlink.Text = ((DataRowView)((GridViewRow)hlinkcontainer).DataItem)[strColumnName].ToString();

                    break;
            }

        }

    }

}