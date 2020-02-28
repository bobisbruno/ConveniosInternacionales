using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections.Generic;
using log4net;
using System.Data.SqlClient;
using System.Text;

/// <summary>
/// Summary description for Grafica
/// </summary>
public class Exportar
{
    public Exportar()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static string DataTable2ExcelString(DataTable dt)
    {
        System.Text.StringBuilder sbTop = new System.Text.StringBuilder();
        sbTop.Append("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
        sbTop.Append("xmlns=\"http://www.w3.org/TR/REC-html40\"><head><meta http-equiv=Content-Type content=\"text/html; charset=windows-1252\">");
        sbTop.Append("<meta name=ProgId content=Excel.Sheet><meta name=Generator content=\"Microsoft Excel 9\"><!--[if gte mso 9]>");
        sbTop.Append("<xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>" + dt.TableName + "</x:Name><x:WorksheetOptions>");
        sbTop.Append("<x:Selected/><x:ProtectContents>False</x:ProtectContents><x:ProtectObjects>False</x:ProtectObjects>");
        sbTop.Append("<x:ProtectScenarios>False</x:ProtectScenarios></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets>");
        sbTop.Append("<x:ProtectStructure>False</x:ProtectStructure><x:ProtectWindows>False</x:ProtectWindows></x:ExcelWorkbook></xml>");
        sbTop.Append("<![endif]--></head><body><table ");
        //gaston
        sbTop.Append("style='font-size: 10px; font-family: Arial; text-align: center'>");
        //gaston
        string bottom = "</table></body></html>";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //Header
        sb.Append("<tr  >");
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            sb.Append("<td style='background-color: #000000; color: #FFFFFF; font-size: 12px; font-weight: bold; font-family: Arial; text-align: center; vertical-align:top ; width: 160px; height: 17px;'>" + dt.Columns[i].ColumnName + "</td>");
        }
        sb.Append("</tr>");

        //Items
        for (int x = 0; x < dt.Rows.Count; x++)
        {
            sb.Append("<tr>");
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                //switch (i)
                //{
                //    case 3:
                //        string fec = dt.Rows[x][i].ToString();
                //        string fec_ = fec.Substring(0, 10);
                //        sb.Append("<td x:str >" + fec_ + "</td>");
                //        break;
                //    case 6:
                //        string fec1 = dt.Rows[x][i].ToString();
                //        string fec1_ = fec1.Substring(0, 10);
                //        sb.Append("<td x:str >" + fec1_ + "</td>");
                //        break;
                //    default:
                //        sb.Append("<td x:str >" + dt.Rows[x][i] + "</td>");
                //        break;
                //}
                sb.Append("<td style='background-color: #FFFFFF; color: #000000; font-size: 10px; font-weight: bold; font-family: Arial; text-align: center; vertical-align:top ; width: 160px; height: 17px;' x:str >" + dt.Rows[x][i].ToString() + "</td>");

            }
            sb.Append("</tr>");
        }

        string SSxml = sbTop.ToString() + sb.ToString() + bottom;

        return SSxml;
    }
}
