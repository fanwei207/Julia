using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using System.Collections.Generic;
using adamFuncs;
using System.Data.OleDb;
using System.Text.RegularExpressions;

namespace APPWS
{
    /// <summary>
    /// Summary description for SID
    /// </summary>
    public class WS
    {
        adamClass adam = new adamClass();
        String StrSql = "";

        /// <summary>
        /// 默认构造方法.
        /// </summary>
        public WS()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region 次品分析

        /// <summary>
        /// 获得次品分析数据
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectFailureData(string _strDate, string _strDate1, string _strSite, string _strCC, string _strLine, string _strPart)
        {
            try
            {
               
                Decimal qty = 0;


                if (_strSite == "1")
                {
                    StrSql = " select sum(isnull(ls_qty,0)) from SZXWS.LineData_SZX.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')=N'正品' ";
                    StrSql += " and ls_plant =1 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";

                    try
                    {
                        qty = Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, StrSql));
                    }
                    catch
                    {
                        qty = 0;
                    }

                    StrSql = " select 1,N'上海振欣 SZX', ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品'),'" + qty + "' from SZXWS.LineData_SZX.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')<>N'正品' ";
                    StrSql += " and ls_plant =1 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";
                }
                else if (_strSite == "2")
                {
                    StrSql = " select sum(isnull(ls_qty,0)) from ZQLWS.LineData_ZQL.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')=N'正品' ";
                    StrSql += " and ls_plant =2 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";

                    try
                    {
                        qty = Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, StrSql));

                    }
                    catch
                    {
                        qty = 0;
                    }


                    StrSql = " select 2,N'镇江强凌 ZQL',ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品'),'" + qty + "'  from ZQLWS.LineData_ZQL.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')<>N'正品' ";
                    StrSql += " and ls_plant =2 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";
                }
                else if (_strSite == "5")
                {
                    StrSql = " select sum(isnull(ls_qty,0)) from YQLWS.LineData_YQL.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')=N'正品' ";
                    StrSql += " and ls_plant =5 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";

                    try
                    {
                        qty = Convert.ToDecimal(SqlHelper.ExecuteScalar(adam.dsnx(), CommandType.Text, StrSql));

                    }
                    catch
                    {
                        qty = 0;
                    }

                    StrSql = " select 5,N'扬州强凌 YQL', ls_cc,ls_line,ls_linename,isnull(ls_qty,0),isnull(ls_status,N'正品'),'" + qty + "'  from YQLWS.LineData_YQL.dbo.ls_data ";
                    StrSql += " where deletedBy is null  and isnull(ls_status,N'正品')<>N'正品' ";
                    StrSql += " and ls_plant =5 ";
                    if (_strCC.Trim() != "")
                    {
                        StrSql += " and ls_cc ='" + _strCC + "' ";
                    }

                    if (_strLine.Trim() != "")
                    {
                        StrSql += " and ls_line ='" + _strLine + "' ";
                    }
                    if (_strPart.Trim() != "")
                    {
                        StrSql += " and ls_part like '" + _strPart.Trim().Replace("*", "%") + "' ";
                    }
                    StrSql += " and createdDate>='" + _strDate + "' and createdDate<DateAdd(day,1,'" + _strDate1 + "') ";
                }
                StrSql += " order by ls_status ";

                //Response.Write(StrSql)
                //Exit Sub

                return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, StrSql);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        #endregion


        #region 10分钟次品流量

        /// <summary>
        /// 获得次品分析数据
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectFailureData10(string _strUser)
        {
            try
            {
                StrSql = "SELECT dispx, qty_bad from SZXWS.LineData_SZX.dbo.ls_display where userid='" + _strUser + "' order by id";

                //Response.Write(StrSql)
                //Exit Sub

                return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, StrSql);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        #endregion


        #region 10分钟正品流量

        /// <summary>
        /// 获得次品分析数据
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectGoodData10(string _strUser)
        {
            try
            {
                StrSql = "SELECT dispx, qty from SZXWS.LineData_SZX.dbo.ls_display where userid='" + _strUser + "' order by id";

                //Response.Write(StrSql)
                //Exit Sub

                return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, StrSql);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        #endregion

        #region 完工入库工时和考勤工时比较 

        /// <summary>
        /// 获得分析数据
        /// </summary>
        /// <param name="_strDate"></param>
        /// <param name="_strSite"></param>
        /// <param name="_strCC"></param>
        /// <param name="_strLine"></param>
        /// <param name="_strPart"></param>
        /// <returns>SqlDataReader</returns>
        public SqlDataReader SelectWOCompAttenData(string _userID, string _year, string _month, string _strSite, string _strCC)
        {
            try
            {
                StrSql = " select dispx,ddate,hr_qty_comp+hr_qty_rcomp+hr_qty_unplan, hr_comp+hr_rcomp+hr_unplan,hr_qty_atten,hr_atten from tcpc0.dbo.hr_Attendance_disp ";
                StrSql += " where userid='" + _userID + "' and year(ddate)='" + _year + "' and Month(ddate)='" + _month + "' ";
                StrSql += " and plantid='" + _strSite + "' and cc='" + _strCC + "'  order by ddate ";

                //Response.Write(StrSql)
                //Exit Sub

                return SqlHelper.ExecuteReader(adam.dsnx(), CommandType.Text, StrSql);
            }
            catch (Exception ex)
            {
                //throw ex;
                return null;
            }
        }


        #endregion
    }
}