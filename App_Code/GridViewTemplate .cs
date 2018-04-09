using System;

using System.Data;

using System.Configuration;

using System.Web;

using System.Web.Security;

using System.Web.UI;

using System.Web.UI.WebControls;

using System.Web.UI.WebControls.WebParts;

using System.Web.UI.HtmlControls;





namespace QCProgress

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

                            txt.Attributes.Add("onkeyup", "this.value=this.value.replace(/[^0-9A-Z-]/g,'')");

                            txt.Attributes.Add("onblur", "if(this.value =='') {alert('不能为空!');this.focus();}");

                            txt.Width = Unit.Pixel(40);

                            txt.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(txt);

                            break;
                        case "checkbox":

                            CheckBox chk = new CheckBox();

                            chk.ID = "chk";

                            //chk.Attributes.Add("onclick", "this.value = 1;");

                            chk.Width = Unit.Pixel(40);

                            chk.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(chk);

                            break;
                        case "chk_txt":

                            CheckBox subChk = new CheckBox();

                            subChk.ID = "chk";

                            subChk.Width = Unit.Pixel(40);

                            subChk.Attributes.Add("onclick", "SetTxtValue(this);");

                            subChk.DataBinding += new EventHandler(this.BindData);

                            TextBox subTxt = new TextBox();

                            subTxt.ID = "txt";

                            subTxt.Width = Unit.Pixel(0);

                            subTxt.DataBinding += new EventHandler(this.BindData);

                            ctlContainer.Controls.Add(subChk);
                            ctlContainer.Controls.Add(subTxt);

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

                case "checkbox":

                    CheckBox chk = (CheckBox)sender;

                    GridViewRow checkboxcontainer = (GridViewRow)chk.NamingContainer;

                    chk.Checked = bool.Parse(((DataRowView)((GridViewRow)checkboxcontainer).DataItem)[strColumnName].ToString());

                    break;
                case "chk_txt":

                    try
                    {
                        CheckBox subChk = (CheckBox)sender;
                        GridViewRow subChkcontainer = (GridViewRow)subChk.NamingContainer;

                        subChk.Checked = bool.Parse(((DataRowView)((GridViewRow)subChkcontainer).DataItem)[strColumnName].ToString());
                    }
                    catch 
                    {
                        TextBox subTxt = (TextBox)sender;
                        GridViewRow subTxtcontainer = (GridViewRow)subTxt.NamingContainer;

                        subTxt.Text = ((DataRowView)((GridViewRow)subTxtcontainer).DataItem)[strColumnName].ToString().Trim();
                    }

                    break;
            }

        }

    }

}