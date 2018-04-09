<%@ WebHandler Language="C#" Class="QAD" %>

using System;
using System.Web;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using adamFuncs;
using System.Text;
using System.Web.SessionState;

public class QAD : IHttpHandler, IRequiresSessionState
{

    adamClass adam = new adamClass();

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        string qad = context.Request.Params["qad"].ToString();
        string type = context.Request.Params["type"].ToString();
        string vends = context.Request.Params["vend"].ToString();
        string vendsName = context.Request.Params["vendName"].ToString();
        string um = context.Request.Params["um"].ToString();
        string _plantCode = context.Session["PlantCode"].ToString();
        string domain =string .Empty;
        switch (_plantCode)
        {
            case "1":
                domain = "SZX";

                break;
            case "2":
                domain = "ZQL";

                break;
            case "5":
                domain = "YQL";

                break;
            case "8":
                domain = "HQL";

                break;
        }
        Object result;
        string sql = "";
        string sqlS = "";
        if (type == "vend")
        {
            if (vends != "")
            {

                sqlS = "SELECT ad_name FROM QAD_Data.dbo.ad_mstr WHERE ad_type = 'supplier' AND ad_addr = '" + vends + "' and ad_domain ='" + domain + "'";

                try
                {
                    vendsName = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sqlS).ToString();
                }
                catch
                {
                    vendsName = "";
                }


                if (um != "")
                {
                    sql += "Select top 1 isnull('vend:' + '" + vends + "' + ';vender:' + N'" + vendsName + "' + ';um:' + N'" + um + "' + ';ptum:' + REPLACE(REPLACE(pt_um,char(10),''),CHAR(13),'') + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                }
                else
                {
                    sql += "Select top 1 isnull('vend:' + '" + vends + "' + ';vender:' + N'" + vendsName + "' + ';um:' + isnull(pc_um,pt_um) + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                }
            }
            else
            {
                if (um != "")
                {
                    sql += "Select top 1 isnull('vend:' + isnull(pc_list,'') + ';vender:' + isnull(vendname,'') + ';um:' + N'" + um + "' + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                }
                else
                {
                    sql += "Select top 1 isnull('vend:' + isnull(pc_list,'') + ';vender:' + isnull(vendname,'') + ';um:' + isnull(pc_um,pt_um) + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                }
            }
            sql += " From tcpc0.dbo.PC_mstr pm ";
            sql += " Right join qad_data.dbo.pt_mstr pt on pm.pc_part = pt.pt_part ";
            sql += " And getdate() >= isnull(pc_start,1900-01-01) ";
            sql += " And (pc_expire is null or (pc_expire is not null And getdate() <= pc_expire)) ";
            if (vends != "")
            {
                sql += " And pm.pc_List = '" + vends + "' ";
            }
            //if (um != "")
            //{
            //    sql += " And pc_um = '" + um + "'";
            //}
            sql += " Left join ";
            sql += " ( ";
            sql += "     SELECT ad_addr , MAX(ad_name) vendname ";
            sql += "     FROM QAD_Data.dbo.ad_mstr ";
            sql += "     WHERE ad_type = 'supplier' ";
            sql += "     GROUP BY ad_addr ";
            sql += " ) vend on pm.pc_list = vend.ad_addr";
            sql += " Where pt_domain = '"+ domain + "'";



            if (vends == "")
            {
                sql += "     And isnull(pc_list,'') <> 'S9999999' ";
            }

            if (string.IsNullOrEmpty(qad) || qad.Trim().Length < 14)
                qad = "*";
            sql += "     And pt_part like '" + qad + "%' ";
            //if (vends != "")
            //{
            //    sql += "     And pc_list = '" + vends + "' ";
            //}
            sql += " And  pt.pt_status<>'STOP' ";
            sql += " Order by case when pc_price <= 0.00000 then 99999 else pc_price end asc ";
            result = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql);

            if (result == null)
            {
                sql = "";
                if (vends != "")
                {
                    sqlS = "SELECT ad_name FROM QAD_Data.dbo.ad_mstr WHERE ad_type = 'supplier' AND ad_addr = '" + vends + "' and ad_domain ='" + domain + "'";
                    try
                    {
                        vendsName = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql).ToString();
                    }
                    catch
                    { vendsName = ""; }

                    if (um != "")
                    {
                        sql += "Select top 1 isnull('vend:' + '" + vends + "' + ';vender:' + N'" + vendsName + "' + ';um:' + N'" + um + "' + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                    }
                    else
                    {
                        sql += "Select top 1 isnull('vend:' + '" + vends + "' + ';vender:' + N'" + vendsName + "' + ';um:' + isnull(pc_um,pt_um) + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                    }
                }
                else
                {
                    if (um != "")
                    {
                        sql += "Select top 1 isnull('vend:' + isnull(pc_list,'') + ';vender:' + isnull(vendname,'') + ';um:' + N'" + um + "' + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                    }
                    else
                    {
                        sql += "Select top 1 isnull('vend:' + isnull(pc_list,'') + ';vender:' + isnull(vendname,'') + ';um:' + isnull(pc_um,pt_um) + ';ptum:' + pt_um + ';price:' + cast(isnull(pc_price1,0.00000) as varchar) + ';desc1:' + replace(replace(pt_desc1,char(10),N''),char(13),N'') + ';desc2:' + replace(replace(pt_desc2,char(10),N''),char(13),N''),'')";
                    }
                }
                sql += " From tcpc0.dbo.PC_mstr pm ";
                sql += " Right join qad_data.dbo.pt_mstr pt on pm.pc_part = pt.pt_part ";
                sql += " And getdate() >= isnull(pc_start,1900-01-01) ";
                sql += " And (pc_expire is null or (pc_expire is not null And getdate() <= pc_expire)) ";
                if (vends != "")
                {
                    sql += " And pm.pc_List = '" + vends + "' ";
                }
                if (um != "")
                {
                    sql += " And pc_um = '" + um + "'";
                }
                sql += " Left join ";
                sql += " ( ";
                sql += "     SELECT ad_addr , MAX(ad_name) vendname ";
                sql += "     FROM QAD_Data.dbo.ad_mstr ";
                sql += "     WHERE ad_type = 'supplier' ";
                sql += "     GROUP BY ad_addr ";
                sql += " ) vend on pm.pc_list = vend.ad_addr";
                sql += " Where pt_domain = '" + domain + "'";

                if (string.IsNullOrEmpty(qad) || qad.Trim().Length < 14)
                    qad = "*";
               
                sql += "     And pt_part like '" + qad + "%' ";
                //if (vends != "")
                //{
                //    sql += "     And pc_list = '" + vends + "' ";
                //}
                sql += "And pt.pt_status<>'STOP' ";
                sql += " Order by case when pc_price <= 0.00000 then 99999 else pc_price end asc ";
                result = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql);

            }

            if (result != null)
            {
                context.Response.Write(result.ToString());
            }
            else
            {
                context.Response.Write("Select top 1 isnull('vend:;vender:;um:;ptum:;price:0.00000;desc1:;desc2:");
                //context.Response.Write("");
            }
        }
        else if (type == "supplier")
        {
            //sql = "Select Top 10 Json = '{\"Code\":\"' + ad_addr + '\",\"Name\":\"' + max(ad_name) + '\",\"Addr1\":\"' + Case When Len(max(ad_line1)) = 0 Then '&nbsp;' Else max(ad_line1) End + '\",\"Addr2\":\"' + Case When Len(max(ad_line2)) = 0 Then '&nbsp;' Else max(ad_line2) End + '\"}' ";
            sql += "Select top 1 isnull('Code:' + ad_addr + ';Name:' + max(ad_name),'')";
            sql += " From QAD_DATA..ad_mstr";
            sql += " Where ad_type = 'supplier'";
            sql += "     And (ad_addr Like '" + vends + "%'";
            sql += "     Or ad_name Like N'%" + vends + "%')";
            sql += " Group by ad_addr";
            result = SqlHelper.ExecuteScalar(adam.dsn0(), CommandType.Text, sql);
            if (result != null)
            {
                context.Response.Write(result.ToString());
            }
            else
            {
                context.Response.Write("");
            }
        }
        context.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}