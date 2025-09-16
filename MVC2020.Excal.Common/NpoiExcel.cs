using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVC2020.Excal.Common
{
    public class NpoiExcel
    {
        /// <summary>
        /// 在导入Excel数据的时候，有时候会有空行，用RemoveEmpty方法去空
        /// </summary>
        /// <param name="dt"></param>
        public static DataTable RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            for (int i = 0; i < removelist.Count; i++)
            {
                dt.Rows.Remove(removelist[i]);
            } return dt;
        }

        /////<summary>
        ///// #region 两种不同版本的操作excelc  -- 临时
        ///// 扩展名*.xlsx
        ///// </summary>
        //public static DataTable ImExportForDataTable(DataTable dt, XSSFWorkbook hssfworkbook)
        //{
        //    //List<HRYT> liyt = new List<HRYT>();


        //    NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
        //    System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
        //    for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
        //    {
        //        dt.Columns.Add(sheet.GetRow(0).Cells[j].ToString());
        //    }
        //    while (rows.MoveNext())
        //    {
        //        XSSFRow row = (XSSFRow)rows.Current;
        //        DataRow dr = dt.NewRow();
        //        for (int i = 0; i < row.LastCellNum; i++)
        //        {
        //            NPOI.SS.UserModel.ICell cell = row.GetCell(i);
        //            if (cell == null)
        //            {
        //                dr[i] = null;
        //            }
        //            else
        //            {
        //                dr[i] = cell.ToString();
        //            }
        //        }
        //        dt.Rows.Add(dr);
        //    }
        //    dt.Rows.RemoveAt(0);

        //    //if (dt != null && dt.Rows.Count != 0)
        //    //{
        //    //    //HRYT yt = new HRYT();

        //    //    for (int i = 0; i < dt.Rows.Count; i++)
        //    //    {
        //    //        //yt.序号 = Convert.ToInt32(dt.Rows[i]["序号"]);
        //    //        //yt.客户 = dt.Rows[i]["客户"].ToString();
        //    //        //yt.片区 = dt.Rows[i]["片区"].ToString();
        //    //        //yt.合同编号 = dt.Rows[i]["合同编号"].ToString();
        //    //        //yt.规格浓度 = Convert.ToInt32(dt.Rows[i]["规格浓度"]);
        //    //        //yt.规格DE值 = dt.Rows[i]["规格DE值"].ToString();
        //    //        //yt.包装罐车 = dt.Rows[i]["包装罐车"].ToString();
        //    //        //yt.包装75KG = dt.Rows[i]["包装75KG"].ToString();
        //    //        //yt.包装280KG = dt.Rows[i]["包装280KG"].ToString();
        //    //        //yt.数量 = Convert.ToInt32(dt.Rows[i]["数量"]);
        //    //        //yt.含税单价 = Convert.ToInt32(dt.Rows[i]["含税单价"]);
        //    //        //yt.现金 = dt.Rows[i]["现金"].ToString();
        //    //        //yt.承兑 = dt.Rows[i]["承兑"].ToString();
        //    //        //yt.运费 = dt.Rows[i]["运费"].ToString();

        //    //        string categary = dt.Rows[i]["页面"].ToString();
        //    //        string fcategary = dt.Rows[i]["分类"].ToString();
        //    //        string fTitle = dt.Rows[i]["标题"].ToString();
        //    //        string fUrl = dt.Rows[i]["链接"].ToString();
        //    //        FooterDAL.Addfoot(categary, fcategary, fTitle, fUrl);
                
        //    //    }
        //    //}

        //    return dt;
        //}


        ///// <summary>
        ///// 大数据插入
        ///// </summary>
        ///// <param name="connectionString">目标库连接</param>
        ///// <param name="TableName">目标表</param>
        ///// <param name="dtSelect">来源数据</param>
        //public static void SqlBulkCopyByDatatable(string connectionString, string TableName, DataTable dtSelect)
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        using (SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction))
        //        {
        //            try
        //            {
        //                sqlbulkcopy.DestinationTableName = TableName;
        //                sqlbulkcopy.BatchSize = 20000;
        //                sqlbulkcopy.BulkCopyTimeout = 0;//不限时间
        //                for (int i = 0; i < dtSelect.Columns.Count; i++)
        //                {
        //                    sqlbulkcopy.ColumnMappings.Add(dtSelect.Columns[i].ColumnName, dtSelect.Columns[i].ColumnName);
        //                }
        //                sqlbulkcopy.WriteToServer(dtSelect);
        //            }
        //            catch (System.Exception ex)
        //            {
        //                throw ex;
        //            }
        //        }
        //    }
        //}
    }
}
