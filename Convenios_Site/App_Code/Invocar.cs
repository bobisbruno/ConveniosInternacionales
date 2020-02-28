using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Web.UI.WebControls;
using log4net;


public class Invocar
{
    static readonly ILog log = LogManager.GetLogger(typeof(Invocar));


    public static string VersionSitio()
    {
        ILog log = LogManager.GetLogger(typeof(Invocar).Name);
        try
        {
            return ConfigurationManager.AppSettings["VersionSitio"];
        }
        catch (Exception exc)
        {
            log.Error(String.Concat("en VersionSitio se produjo el siguiente error => ", exc.ToString()));
            throw exc;
        }
    }

    public static List<DateTime> TraerUltimosPeriodos()
    {
        List<DateTime> lperiodos = new List<DateTime>();
        DateTime datetemp = new DateTime();
        int periodos = int.Parse(ConfigurationManager.AppSettings["CantidadPeriodosCombo"]);
        datetemp = System.DateTime.Today;
        lperiodos.Add(datetemp);
        for (int i = 0; i < periodos -1; i++)
        {
            datetemp = datetemp.AddMonths(-1);
            lperiodos.Add(datetemp);
        }

        return lperiodos;
    }

    //public static TreeNode CargarNodo(VoceroPadre v)
    //{
    //    TreeNode node = new TreeNode(v.DEnumeracion.Equals(string.Empty) ? v.DVocero : v.DEnumeracion + " - " + v.DVocero, v.CVocero.ToString());
    //    foreach (VoceroPadre VoceroHijo in v.Voceros)
    //    {
    //        node.ChildNodes.Add(CargarNodo(VoceroHijo));
    //    }
    //    return node;
    //}

}