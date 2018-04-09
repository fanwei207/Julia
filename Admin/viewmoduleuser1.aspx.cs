using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using adamFuncs;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Text;
using System.Web.Services;
public partial class Admin_viewmoduleuser1 : System.Web.UI.Page
{
    static adamClass adam = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AddTree(0, null);
        }
    }
    private void AddTree(int Pid, TreeNode PNode)
    {
        string sqlStr = "Select id, name, parentID From Menu Where isDisable = 0 Order By sortOrder " ;        
        DataTable dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, sqlStr).Tables[0];
        if (dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            //过滤ParentID,得到当前的所有子节点 ParentID为父节点ID
            dv.RowFilter = "[parentID] = " + Pid;
            //dv.RowFilter = "[Parent_ID] = " + Pid;
            //循环递归
            foreach (DataRowView Row in dv)
            {
                //声明节点
                TreeNode Node = new TreeNode();
                //绑定超级链接
                //Node.NavigateUrl = Bind(Row["name"].ToString(), Row["id"].ToString(), Row["parentID"].ToString());
                //Node.NavigateUrl = String.Format("javascript:show1('{0}','{1}')", Row["name"].ToString(), Row["id"].ToString());
                //Node.DataItem = Bind2(Row["name"].ToString(), Row["id"].ToString(), Row["parentID"].ToString());
                //Node.NavigateUrl = String.Format("javascript:show('{0}')", Row["name"].ToString());
                //Node.NavigateUrl = String.Format("javascript:show('{0}','{1}')", Row["Item_Name"].ToString(), Row["Item_id"].ToString());
                //开始递归
                if (PNode == null)
                {
                    //添加根节点
                    Node.Text = Row["name"].ToString();
                    Node.Value = Row["id"].ToString();
                    //Node.Text = Row["Item_Name"].ToString();
                    treeT.Nodes.Add(Node);
                    Node.Expanded = true; //节点状态展开
                    AddTree(Int32.Parse(Row["id"].ToString()), Node);    //再次递归
                    //AddTree(Int32.Parse(Row["Item_ID"].ToString()), Node);
                }
                else
                {
                    //添加当前节点的子节点
                    Node.Text = Row["name"].ToString();
                    Node.Value = Row["id"].ToString();
                    //Node.Text = Row["Item_Name"].ToString();
                    PNode.ChildNodes.Add(Node);
                    Node.Expanded = true; //节点状态展开
                    AddTree(Int32.Parse(Row["id"].ToString()), Node);     //再次递归
                    //AddTree(Int32.Parse(Row["Item_ID"].ToString()), Node); 
                }
                Node.Expanded = false;  //树默认不展开
                //Node.Font.Size = 14;
            }
        }
    }   
       
    protected void gv_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string uid = gv.DataKeys[e.RowIndex].Values["userID"].ToString();
        string mid = gv.DataKeys[e.RowIndex].Values["moduleID"].ToString();
        if (DeleteLadingList(uid, mid))
        {
            ltlAlert.Text = "alert('删除失败！');";
            return;
        }
        else
        {
            Baind1();
        }
    }
    private bool DeleteLadingList(string uid, string mid)
    {
        string str = "Delete From tcpc0.dbo.AccessRule where userID='" + Convert.ToInt32(uid) + "' and moduleID='" + Convert.ToInt32(mid) + "'";

        return Convert.ToBoolean(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, str));
    }
    private void Baind1()
    {
        string str = "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d1.name " +
                        ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                        "from tcpc0.dbo.AccessRule a " +
                        "left join tcpc0..Users u on a.userID = u.userID " +
                        "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                        "left join tcpc1..Departments d1 on u.departmentID = d1.departmentID " +
                        "where moduleID= " + Convert.ToInt32(Label2.Text) + " and u.plantCode = 1 ) " +
                        "union all " +
                        "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d2.name " +
                        ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                        "from tcpc0.dbo.AccessRule a " +
                        "left join tcpc0..Users u on a.userID = u.userID " +
                        "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                        "left join tcpc2..Departments d2 on u.departmentID = d2.departmentID " +
                        "where moduleID= " + Convert.ToInt32(Label2.Text) + " and u.plantCode = 2 ) " +
                        "union all " +
                        "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d5.name  " +
                        ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                        "from tcpc0.dbo.AccessRule a " +
                        "left join tcpc0..Users u on a.userID = u.userID " +
                        "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                        "left join tcpc5..Departments d5 on u.departmentID = d5.departmentID " +
                        "where moduleID= " + Convert.ToInt32(Label2.Text) + " and u.plantCode = 5 ) " +
                        "union all " +
                        "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d8.name  " +
                        ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                        "from tcpc0.dbo.AccessRule a " +
                        "left join tcpc0..Users u on a.userID = u.userID " +
                        "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                        "left join tcpc8..Departments d8 on u.departmentID = d8.departmentID " +
                        "where moduleID= " + Convert.ToInt32(Label2.Text) + " and u.plantCode = 8 ) ";
        DataSet dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str);
        gv.DataSource = dt;
        gv.DataBind();
    }
    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Convert.ToString(gv.DataKeys[e.Row.RowIndex].Values["isleave"]) == "离职")
            {
                e.Row.Cells[1].ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void treeT_SelectedNodeChanged(object sender, EventArgs e)
    {
        Label1.Text = this.treeT.SelectedNode.Text.Trim();
        Label2.Text = this.treeT.SelectedNode.Value.Trim();
        string pidsql = "select parentid  from tcpc0..Menu where id = '" + this.treeT.SelectedNode.Value.Trim() + "'";
        int pid = Convert.ToInt32(SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, pidsql).ToString());
        if (pid == 0)
        {
            gv.DataSource = null;
            gv.DataBind();
            return;
        }
        else
        {
            string str = "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d1.name " +
                            ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                            "from tcpc0.dbo.AccessRule a " +
                            "left join tcpc0..Users u on a.userID = u.userID " +
                            "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                            "left join tcpc1..Departments d1 on u.departmentID = d1.departmentID " +
                            "left join tcpc0..Menu m on a.moduleID = m.id " +
                            "where m.id= '" + this.treeT.SelectedNode.Value.Trim() + "' and u.plantCode = 1 )" +
                            "union all " +
                            "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d2.name " +
                            ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                            "from tcpc0.dbo.AccessRule a " +
                            "left join tcpc0..Users u on a.userID = u.userID " +
                            "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                            "left join tcpc2..Departments d2 on u.departmentID = d2.departmentID " +
                            "left join tcpc0..Menu m on a.moduleID = m.id " +
                            "where m.id= '" + this.treeT.SelectedNode.Value.Trim() + "' and u.plantCode = 2 )" +
                            "union all " +
                            "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d5.name  " +
                            ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                            "from tcpc0.dbo.AccessRule a " +
                            "left join tcpc0..Users u on a.userID = u.userID " +
                            "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                            "left join tcpc5..Departments d5 on u.departmentID = d5.departmentID " +
                            "left join tcpc0..Menu m on a.moduleID = m.id " +
                            "where m.id= '" + this.treeT.SelectedNode.Value.Trim() + "' and u.plantCode = 5 ) " +
                            "union all " +
                            "(select p.plantCode,moduleID,a.userID,u.userName,u.userNo,u.departmentID,d8.name  " +
                            ",case when (isnull(u.leaveDate,0) = 0) then N'在职' else N'离职'	end as isleave " +
                            "from tcpc0.dbo.AccessRule a " +
                            "left join tcpc0..Users u on a.userID = u.userID " +
                            "left join tcpc0..Plants p on u.plantCode = p.plantID " +
                            "left join tcpc8..Departments d8 on u.departmentID = d8.departmentID " +
                            "left join tcpc0..Menu m on a.moduleID = m.id " +
                            "where m.id= '" + this.treeT.SelectedNode.Value.Trim() + "' and u.plantCode = 8 )";
            DataSet dt = SqlHelper.ExecuteDataset(adam.dsn0(), CommandType.Text, str);
            gv.DataSource = dt;
            gv.DataBind();
        }
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        int chkNum = 0;
        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox chkUsers = (CheckBox)row.FindControl("chkUsers");
            if (chkUsers.Checked)
            {
                chkNum = chkNum + 1;
            }
        }
        if (chkNum == 0)
        {
            ltlAlert.Text = "alert('请选择回收权限的人员!');";
            return;
        }
        else
        {
            foreach (GridViewRow row1 in gv.Rows)
            {
                CheckBox chkUsers1 = (CheckBox)row1.FindControl("chkUsers");
                if (chkUsers1.Checked)
                {
                    string uid = gv.DataKeys[row1.RowIndex].Values["userID"].ToString();
                    string mid = gv.DataKeys[row1.RowIndex].Values["moduleID"].ToString();
                    if (DeleteLadingList(uid, mid))
                    {
                        ltlAlert.Text = "alert('删除失败！');";
                        return;
                    }
                }
            }
            Baind1();
        }
    }
}