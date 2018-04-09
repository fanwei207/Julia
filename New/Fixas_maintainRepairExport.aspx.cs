using System;
using System.Collections.Generic;
using adamFuncs;
using Portal.Fixas;

public partial class new_Fixas_maintainRepairExport : System.Web.UI.Page
{
    adamClass chk = new adamClass();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ty"] == "repairApply" || Request.QueryString["ty"] == "repairRecord")
        {
            #region 维修单导出
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产维修单");

            #region 设置列宽
            AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col1.ColumnIndexStart = 0;
            col1.ColumnIndexEnd = 0;
            col1.Width = 150 * 6000 / 164;
            sheet.AddColumnInfo(col1);

            AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col2.ColumnIndexStart = 1;
            col2.ColumnIndexEnd = 1;
            col2.Width = 250 * 6000 / 164;
            sheet.AddColumnInfo(col2);

            AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col3.ColumnIndexStart = 2;
            col3.ColumnIndexEnd = 2;
            col3.Width = 150 * 6000 / 164;
            sheet.AddColumnInfo(col3);

            AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col4.ColumnIndexStart = 3;
            col4.ColumnIndexEnd = 3;
            col4.Width = 250 * 6000 / 164;
            sheet.AddColumnInfo(col4);
            #endregion

            FixasRepair fixasRepair = new FixasRepair();
            IList<FixasRepair> fixasRepairList = FixasRepairHelper.SelectRepairOrder(string.Empty, Request.QueryString["repairOrder"], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, Convert.ToInt32(Session["plantCode"]), string.Empty);
            foreach (FixasRepair repair in fixasRepairList)
            {
                fixasRepair = repair;
            }
            //Response.Write(fixasRepair.FixasInfo.FixasNo);
            PrintRepairOrder(doc, sheet, 1, fixasRepair.RepairOrder, fixasRepair.FixasInfo.FixasNo, fixasRepair.FixasInfo.FixasName, fixasRepair.FixasInfo.Domain, fixasRepair.FixasInfo.CC, fixasRepair.FixasInfo.FixasDesc, fixasRepair.FixasInfo.FixasType, fixasRepair.FixasInfo.FixasSubType,
                fixasRepair.FixasInfo.FixasEntity, fixasRepair.FixasInfo.FixasVouDate, fixasRepair.FixasInfo.FixasSupplier,
                fixasRepair.ApplyRepairDate, fixasRepair.ProblemDesc, fixasRepair.ApplyCreator.Name, fixasRepair.ApplyCreator.Date,
                fixasRepair.RepairedName, fixasRepair.RepairBeginDate, fixasRepair.RepairEndDate, fixasRepair.RepairRecord, fixasRepair.RecordModifier.Name, fixasRepair.RecordModifier.Date, fixasRepair.RepairConfirmer.Name, Request.QueryString["ty"]);

            doc.Send();
            Response.Flush();
            Response.End();
            #endregion
        }
        else if (Request.QueryString["ty"] == "maintainPlan" || Request.QueryString["ty"] == "maintainRecord")
        {
            #region 保养单导出
            AppLibrary.WriteExcel.XlsDocument doc = new AppLibrary.WriteExcel.XlsDocument();
            doc.FileName = "report-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            AppLibrary.WriteExcel.Worksheet sheet = doc.Workbook.Worksheets.Add("固定资产保养单");

            #region 设置列宽
            AppLibrary.WriteExcel.ColumnInfo col1 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col1.ColumnIndexStart = 0;
            col1.ColumnIndexEnd = 0;
            col1.Width = 150 * 6000 / 164;
            sheet.AddColumnInfo(col1);

            AppLibrary.WriteExcel.ColumnInfo col2 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col2.ColumnIndexStart = 1;
            col2.ColumnIndexEnd = 1;
            col2.Width = 250 * 6000 / 164;
            sheet.AddColumnInfo(col2);

            AppLibrary.WriteExcel.ColumnInfo col3 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col3.ColumnIndexStart = 2;
            col3.ColumnIndexEnd = 2;
            col3.Width = 150 * 6000 / 164;
            sheet.AddColumnInfo(col3);

            AppLibrary.WriteExcel.ColumnInfo col4 = new AppLibrary.WriteExcel.ColumnInfo(doc, sheet);
            col4.ColumnIndexStart = 3;
            col4.ColumnIndexEnd = 3;
            col4.Width = 250 * 6000 / 164;
            sheet.AddColumnInfo(col4);
            #endregion

            FixasMaintain fixasMaintain = new FixasMaintain();
            IList<FixasMaintain> fixasMaintainList = FixasMaintainHelper.SelectMaintainOrder(string.Empty, Request.QueryString["maintainOrder"], string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, Convert.ToInt32(Session["plantCode"]), string.Empty);
            foreach (FixasMaintain maintain in fixasMaintainList)
            {
                fixasMaintain = maintain;
            }

            PrintMaintainOrder(doc, sheet, 1, fixasMaintain.MaintainOrder, fixasMaintain.FixasInfo.FixasNo, fixasMaintain.FixasInfo.FixasName, fixasMaintain.FixasInfo.Domain, fixasMaintain.FixasInfo.CC, fixasMaintain.FixasInfo.FixasDesc, fixasMaintain.FixasInfo.FixasType, fixasMaintain.FixasInfo.FixasSubType,
                fixasMaintain.FixasInfo.FixasEntity, fixasMaintain.FixasInfo.FixasVouDate, fixasMaintain.FixasInfo.FixasSupplier, fixasMaintain.PlanMaintainDate, fixasMaintain.MaintainDesc, fixasMaintain.PlanCreator.Name,
                fixasMaintain.PlanCreator.Date, fixasMaintain.MaintainedName, fixasMaintain.MaintainBeginDate, fixasMaintain.MaintainEndDate, fixasMaintain.MaintainedRecord, fixasMaintain.MaintainModifier.Name, fixasMaintain.MaintainModifier.Date, fixasMaintain.PlanConfirmer.Name, Request.QueryString["ty"]);

            doc.Send();
            Response.Flush();
            Response.End();
            #endregion
        }
    }

    protected void PrintRepairOrder(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
    Object RepairOrder, Object FixasNo, Object FixasName, Object FixasDomain, Object FixasCC, Object FixasDesc, Object FixasType, Object FixasSubType, Object FixasEntity, Object FixasVouDate, Object FixasSupplier,
    Object ApplyRepairedDate, Object ProblemDesc, Object ApplyCreator, Object ApplyCreateDate,
    Object RepairedName, Object RepaiedBeginDate, Object RepairedEndDate, Object RepairedRecord, Object RecordModifier, Object RecordModifiedDate, Object confirmName, string ty)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = true;//头部标题使用加粗样式
        xf.Font.Height = 9 * 256 / 6;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        //头部
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, "维修单：" + RepairOrder.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 10;

        //资产基本信息
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "资产编号", xf);
        sheet.Cells.Add(rowIndex, 2, FixasNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "资产名称", xf);
        sheet.Cells.Add(rowIndex, 4, FixasName.ToString(), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "所在公司", xf);
        sheet.Cells.Add(rowIndex, 2, FixasDomain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "成本中心", xf);
        sheet.Cells.Add(rowIndex, 4, FixasCC.ToString(), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "资产规格", xf);
        sheet.Cells.Add(rowIndex, 2, FixasDesc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "资产类型", xf);
        sheet.Cells.Add(rowIndex, 4, FixasType.ToString() + (FixasSubType.ToString() == string.Empty ? string.Empty : " - " + FixasSubType.ToString()), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "入账公司", xf);
        sheet.Cells.Add(rowIndex, 2, FixasEntity.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "入账时间", xf);
        sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd}", FixasVouDate), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "供应商", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, FixasSupplier.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        //空一行
        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        //维修申请信息
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "申请维修时间", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, string.Format("{0:yyyy-MM-dd}", ApplyRepairedDate), xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex + 2, 1, 1);
        sheet.Cells.Merge(rowIndex, rowIndex + 2, 2, 4);
        sheet.Cells.Add(rowIndex, 1, "问题描述", xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ProblemDesc.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        for (int i = 1; i <= 2; i++)
        {
            sheet.Cells.Add(rowIndex + i, 1, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 2, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 4, string.Empty, xf);
        }

        rowIndex += 2;
        //sheet.Cells.Add(rowIndex, 1, "申请人", xf);
        //sheet.Cells.Add(rowIndex, 2, ApplyCreator.ToString(), xf);
        //sheet.Cells.Add(rowIndex, 3, "申请时间", xf);
        //if (ApplyCreateDate.ToString() == DateTime.MinValue.ToString())
        //{
        //    sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        //}
        //else
        //{
        //    sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd HH:mm:ss}", ApplyCreateDate), xf);
        //}

        //空一行
        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        //维修结果信息
        if (ty == "repairRecord")
        {
            rowIndex++;
            sheet.Cells.Add(rowIndex, 1, "维修人", xf);
            sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 2, RepairedName.ToString(), xf);
            sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

            rowIndex++;
            sheet.Cells.Add(rowIndex, 1, "维修开始时间", xf);
            sheet.Cells.Add(rowIndex, 2, string.Format("{0:yyyy-MM-dd HH:mm:ss}", RepaiedBeginDate), xf);
            sheet.Cells.Add(rowIndex, 3, "维修结束时间", xf);
            sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd HH:mm:ss}", RepairedEndDate), xf);
        }
        else
        {
            rowIndex++;
            sheet.Cells.Add(rowIndex, 1, "维修人", xf);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 2, RepairedName.ToString(), xf);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
            sheet.Cells.Add(rowIndex, 3, "维修结束时间", xf);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
            sheet.Cells.Add(rowIndex, 4, "      年   月   日   时   分", xf);
            xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        }

        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex + 3, 1, 1);
        sheet.Cells.Merge(rowIndex, rowIndex + 3, 2, 4);
        sheet.Cells.Add(rowIndex, 1, "维修记录", xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ty == "repairRecord" ? RepairedRecord.ToString() : string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        for (int i = 1; i <= 3; i++)
        {
            sheet.Cells.Add(rowIndex + i, 1, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 2, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 4, string.Empty, xf);
        }

        //空一行
        rowIndex += 4;
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        //确认人
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "确认人", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ty == "repairRecord" ? confirmName.ToString() : "(签字)  ", xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        if (ty == "repairRecord")
        {
            //空一行
            rowIndex++;
            sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
            sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
            sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
            sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

            rowIndex++;
            sheet.Cells.Add(rowIndex, 1, "记录人", xf);
            sheet.Cells.Add(rowIndex, 2, RecordModifier.ToString(), xf);
            sheet.Cells.Add(rowIndex, 3, "记录时间", xf);
            if (RecordModifiedDate.ToString() == DateTime.MinValue.ToString())
            {
                sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
            }
            else
            {
                sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd HH:mm:ss}", RecordModifiedDate), xf);
            }
        }
    }

    protected void PrintMaintainOrder(AppLibrary.WriteExcel.XlsDocument doc, AppLibrary.WriteExcel.Worksheet sheet, int rowIndex,
        Object MaintainOrder, Object FixasNo, Object FixasName, Object FixasDomain, Object FixasCC, Object FixasDesc, Object FixasType, Object FixasSubType, Object FixasEntity, Object FixasVouDate, Object FixasSupplier,
        Object PlanMaintainedDate, Object MaintainDesc, Object PlanCreator, Object PlanCreateDate,
        Object MaintainedName, Object MaintainBeginDate, Object MaintainEndDate, Object MaintainedRecord, Object MaintainModifier, Object MaintainModifiedDate, Object confirmName, string ty)
    {
        AppLibrary.WriteExcel.XF xf = doc.NewXF();
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        xf.VerticalAlignment = AppLibrary.WriteExcel.VerticalAlignments.Centered;
        xf.Font.FontName = "宋体";
        xf.UseMisc = true;
        xf.Font.Bold = true;//头部标题使用加粗样式
        xf.Font.Height = 9 * 256 / 6;

        xf.LeftLineStyle = 1;
        xf.TopLineStyle = 1;
        xf.RightLineStyle = 1;
        xf.BottomLineStyle = 1;

        //头部
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, "保养单：" + MaintainOrder.ToString(), xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        xf.Font.Bold = false;
        xf.Font.Height = 9 * 256 / 10;

        //资产基本信息
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "资产编号", xf);
        sheet.Cells.Add(rowIndex, 2, FixasNo.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "资产名称", xf);
        sheet.Cells.Add(rowIndex, 4, FixasName.ToString(), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "所在公司", xf);
        sheet.Cells.Add(rowIndex, 2, FixasDomain.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "成本中心", xf);
        sheet.Cells.Add(rowIndex, 4, FixasCC.ToString(), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "资产规格", xf);
        sheet.Cells.Add(rowIndex, 2, FixasDesc.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "资产类型", xf);
        sheet.Cells.Add(rowIndex, 4, FixasType.ToString() + (FixasSubType.ToString() == string.Empty ? string.Empty : " - " + FixasSubType.ToString()), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "入账公司", xf);
        sheet.Cells.Add(rowIndex, 2, FixasEntity.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, "入账时间", xf);
        sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd}", FixasVouDate), xf);

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "供应商", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, FixasSupplier.ToString(), xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        //空一行
        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        //保养计划信息
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "计划保养时间", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, string.Format("{0:yyyy-MM-dd HH:mm}", PlanMaintainedDate), xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex + 2, 1, 1);
        sheet.Cells.Merge(rowIndex, rowIndex + 2, 2, 4);
        sheet.Cells.Add(rowIndex, 1, "保养描述", xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, MaintainDesc.ToString(), xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        for (int i = 1; i <= 2; i++)
        {
            sheet.Cells.Add(rowIndex + i, 1, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 2, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 4, string.Empty, xf);
        }

        rowIndex += 2;
        //sheet.Cells.Add(rowIndex, 1, "计划人", xf);
        //sheet.Cells.Add(rowIndex, 2, PlanCreator.ToString(), xf);
        //sheet.Cells.Add(rowIndex, 3, "计划时间", xf);
        //if (PlanCreateDate.ToString() == DateTime.MinValue.ToString())
        //{
        //    sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        //}
        //else
        //{
        //    sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd HH:mm:ss}", PlanCreateDate), xf);
        //}

        //空一行
        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex, 1, 4);
        sheet.Cells.Add(rowIndex, 1, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 2, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        //保养结果信息
        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "保养人", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ty == "maintainRecord" ? MaintainedName : string.Empty, xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        rowIndex++;
        sheet.Cells.Add(rowIndex, 1, "保养开始时间", xf);
        sheet.Cells.Add(rowIndex, 2, ty == "maintainRecord" ? string.Format("{0:yyyy-MM-dd HH:mm:ss}", MaintainBeginDate) : "      年   月   日   时   分", xf);
        sheet.Cells.Add(rowIndex, 3, "保养结束时间", xf);
        sheet.Cells.Add(rowIndex, 4, ty == "maintainRecord" ? string.Format("{0:yyyy-MM-dd HH:mm:ss}", MaintainEndDate) : "      年   月   日   时   分", xf);

        rowIndex++;
        sheet.Cells.Merge(rowIndex, rowIndex + 3, 1, 1);
        sheet.Cells.Merge(rowIndex, rowIndex + 3, 2, 4);
        sheet.Cells.Add(rowIndex, 1, "保养记录", xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ty == "maintainRecord" ? MaintainedRecord.ToString() : string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);

        for (int i = 1; i <= 3; i++)
        {
            sheet.Cells.Add(rowIndex + i, 1, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 2, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 3, string.Empty, xf);
            sheet.Cells.Add(rowIndex + i, 4, string.Empty, xf);
        }

        //确认人
        rowIndex += 4;
        sheet.Cells.Add(rowIndex, 1, "确认人", xf);
        sheet.Cells.Merge(rowIndex, rowIndex, 2, 4);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Left;
        sheet.Cells.Add(rowIndex, 2, ty == "maintainRecord" ? confirmName.ToString() : "(签字)  ", xf);
        sheet.Cells.Add(rowIndex, 3, string.Empty, xf);
        sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
        xf.HorizontalAlignment = AppLibrary.WriteExcel.HorizontalAlignments.Centered;

        if (ty == "maintainRecord")
        {
            rowIndex++;
            sheet.Cells.Add(rowIndex, 1, "记录人", xf);
            sheet.Cells.Add(rowIndex, 2, MaintainModifier.ToString(), xf);
            sheet.Cells.Add(rowIndex, 3, "记录时间", xf);
            if (MaintainModifiedDate.ToString() == DateTime.MinValue.ToString())
            {
                sheet.Cells.Add(rowIndex, 4, string.Empty, xf);
            }
            else
            {
                sheet.Cells.Add(rowIndex, 4, string.Format("{0:yyyy-MM-dd HH:mm:ss}", MaintainModifiedDate), xf);
            }
        }
    }
}