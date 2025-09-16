using System;
using System.Data;
using MVC2020.Model;
using NPOI.HSSF.UserModel;

namespace MVC2020.Excel.Server
{
    public class CountManager : ModelBaseManager<HRYT>
    {




        ///<summary>
        /// #region 两种不同版本的操作excel
        /// 扩展名*.xlsx
        /// </summary>
        public DataTable ImExport(DataTable dt, HSSFWorkbook hssfworkbook)
        {
            //List<HRYT> liyt = new List<HRYT>();
             

            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(sheet.GetRow(0).Cells[j].ToString());
            }
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            dt.Rows.RemoveAt(0);


            if (dt != null && dt.Rows.Count != 0)
            {
                HRYT yt = new HRYT();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    yt.序号 = Convert.ToInt32(dt.Rows[i]["序号"]);

                    yt.客户 = dt.Rows[i]["客户"].ToString();
                    yt.片区 = dt.Rows[i]["片区"].ToString();
                    yt.合同编号 = dt.Rows[i]["合同编号"].ToString();
                    yt.规格浓度 = Convert.ToInt32(dt.Rows[i]["规格浓度"]);
                    yt.规格DE值 = dt.Rows[i]["规格DE值"].ToString();
                    yt.包装罐车 = dt.Rows[i]["包装罐车"].ToString();
                    yt.包装75KG = dt.Rows[i]["包装75KG"].ToString();
                    yt.包装280KG = dt.Rows[i]["包装280KG"].ToString();
                    yt.数量 = Convert.ToInt32(dt.Rows[i]["数量"]);
                    yt.含税单价 = Convert.ToInt32(dt.Rows[i]["含税单价"]);

                    yt.现金 = dt.Rows[i]["现金"].ToString();
                    yt.承兑 = dt.Rows[i]["承兑"].ToString();
                    yt.运费 = dt.Rows[i]["运费"].ToString();


                    //liyt.Add(yt);

                    base.Add(yt);
                    //Addfoot(categary, fcategary, fTitle, fUrl);
                }
            }
            return dt;
        }



        /// <summary>
        /// ADD数据
        /// </summary>
        /// <param name="yt"></param>
        public void ImExcelIn(HRYT yt)
        {
            base.Add(yt);

            return  ;

            //List<HRYT> liyt = new List<HRYT>();//列表

            //if (dt != null && dt.Rows.Count != 0)
            //{
            //    HRYT yt = new HRYT();

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        yt.序号 = Convert.ToInt32(dt.Rows[i]["序号"]);

            //        yt.客户 = dt.Rows[i]["客户"].ToString();
            //        yt.片区 = dt.Rows[i]["片区"].ToString();
            //        yt.合同编号 = dt.Rows[i]["合同编号"].ToString();
            //        yt.规格浓度 = Convert.ToInt32(dt.Rows[i]["规格浓度"]);
            //        yt.规格DE值 = dt.Rows[i]["规格DE值"].ToString();
            //        yt.包装罐车 = dt.Rows[i]["包装罐车"].ToString();
            //        yt.包装75KG = dt.Rows[i]["包装75KG"].ToString();
            //        yt.包装280KG = dt.Rows[i]["包装280KG"].ToString();
            //        yt.数量 = Convert.ToInt32(dt.Rows[i]["数量"]);
            //        yt.含税单价 = Convert.ToInt32(dt.Rows[i]["含税单价"]);

            //        yt.现金 = dt.Rows[i]["现金"].ToString();
            //        yt.承兑 = dt.Rows[i]["承兑"].ToString();
            //        yt.运费 = dt.Rows[i]["运费"].ToString();


            //        liyt.Add(yt);

            //        base.Add(yt);
            //        //Addfoot(categary, fcategary, fTitle, fUrl);
            //    }
            //}


           
        }

    }
}
