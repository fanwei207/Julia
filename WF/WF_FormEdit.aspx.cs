using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Data.SqlClient;
using System.IO;
using System.Text;

public partial class WF_ExcelEdit : BasePage
{

    WorkFlow wf = new WorkFlow();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string _Targ_file = Convert.ToString(Session["uID"]) + ".xml";
            string file_name = Server.MapPath("/Excel/" + _Targ_file);
            if (File.Exists(file_name))
                File.Delete(file_name);

            if (Request.QueryString["nbr"] != null)
            {
                SqlDataReader reader = wf.GetWorkFlowInstanceByNbr(Request.QueryString["nbr"], Convert.ToInt32(Session["plantCode"]));
                if (reader.Read())
                {
                    System.IO.File.WriteAllBytes(file_name, (byte[])reader["WFN_FormContent"]);
                }
                reader.Close();
            }
            if (!File.Exists(Server.MapPath("/Excel/test.xml")))
            {
                File.Copy(file_name, Server.MapPath("/Excel/test.xml"));
            }

            //取得Excel内容------------------------------------------
            XmlDocument xml = new XmlDocument();
            xml.Load(file_name);
            if (xml != null)
                _hf_ExcelXmlData.Value = xml.OuterXml;

            //取得Excel设置------------------------------------------
            XmlNode node = get_ExcelSetting();
            if (node != null)
                _hf_ExcelSetting.Value = node.OuterXml;

            if (wf.JudgeIsCanModifiedWithApplier(Request.QueryString["nbr"], Convert.ToInt32(Session["uID"])))
            {
                BtnSave.Visible = true;
            }
            else
            {
                BtnSave.Visible = false;
            }
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string _Targ_file = DateTime.Now.ToFileTime().ToString() + ".xml";
        string file_name = Server.MapPath("/Excel/" + _Targ_file);

        File.WriteAllText(file_name, _hf_ExcelXmlData.Value, Encoding.Unicode);

        byte[] datByte = File.ReadAllBytes(file_name);
        string nbr = Request.QueryString["nbr"];

        if (wf.UpdateWorkFlowInstanceFile(nbr, datByte))
        {
            ltlAlert.Text = "alert('保存成功!'); window.close()";
        }
        else
        {
            ltlAlert.Text = "alert('保存失败,请联系管理员!');";
        }
    }

    static public XmlNode get_ExcelSetting()
    {
        string file = HttpContext.Current.Server.MapPath("/App_Data/Excels.xml");
        XmlDocument doc = new XmlDocument();
        doc.Load(file);
        //-------------------------------------------------------
        if (doc != null)
        {
            XmlNode root = doc.DocumentElement;
            if (root != null)
            {
                string query = string.Format("sheet[@filename='{0}']", "test.xml");
                XmlNode node = root.SelectSingleNode(query);
                if (node != null)
                    return node;
            }
        }

        //-------------------------------------------------------
        return null;
    }

    protected void BtnClose_Click(object sender, EventArgs e)
    {
        ltlAlert.Text = "window.close();";
    }
}
