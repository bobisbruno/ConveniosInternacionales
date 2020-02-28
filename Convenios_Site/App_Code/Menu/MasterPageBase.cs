using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;
using WSGrantForSystemCache;
using System.Configuration;

/// <summary>
/// Summary description for MasterPageBase
/// </summary>
[Serializable]
public class MasterPageBase : MasterPage
{
    #region Propiedades
    /// <summary>
    /// HMenu contendra todo el parseo del web.sitemap para que no se tenga que recorrer repetidamente el menu durante al ejecución
    /// </summary>
    /// 
    //private string NodoPadre = string.Empty;
    private string NodoPadre
    {
        get { return Session["__NodoPadre"] == null ? string.Empty : Session["__NodoPadre"].ToString(); }
        set { Session["__NodoPadre"] = value; }
    }
    private HashSet<Nodo> HMenu
    {
        get { return (HashSet<Nodo>)Application["MenuTotal"]; }
        set { Application["MenuTotal"] = value; }
    }

    private string MenuPerfil
    {
        get { return Session["MenuPerfil"] == null ? "" : Session["MenuPerfil"].ToString(); }
        set { Session["MenuPerfil"] = value; }
    }


    private HashSet<PermisosPerfil> Permisos
    {
        get { return (HashSet<PermisosPerfil>)Session["PermisosPerfil"]; }
        set { Session["PermisosPerfil"] = value; }
    }
    #endregion

    public MasterPageBase()
    {
    }

    /// <summary>
    /// Metodo que carga los nodos del menu en una variable de session para que perdure en la misma
    /// </summary>
    /// <param name="titulo">Nombre o identificador del nombre del nodo del menu a generar</param>
    /// <param name="link">vinculo al que direccionará el menú</param>
    /// <param name="nvl">Nivel del item del menu. de esta manera puedo conocer el anidamiento del mismo</param>
    private void CargarMenu(string titulo, string link, int nvl, string resourceKey)
    {
        if (HMenu == null)
            HMenu = new HashSet<Nodo>();
        Nodo Item = new Nodo() { Titulo = titulo, Vinculo = link, Nivel = nvl, ResouceKey = resourceKey };
        HMenu.Add(Item);
    }



    /// <summary>
    /// Función Recursiva que analiza nodo por nodo el web.sitemap
    /// </summary>
    /// <param name="nodo">Nodo a analizar del tipo XElement</param>
    /// <param name="nivel">Nivel de anidamiento - comienza con 0</param>
    private void ExaminaNodo(XElement nodo, int nivel)
    {
        if (nodo != null)
        {
            //CargarMenu(NodoPadre, nodo.Attribute("title").Value, nodo.Attribute("url").Value, nivel, nodo.Attribute("resourceKey").Value);
            CargarMenu(nodo.Attribute("title").Value, nodo.Attribute("url").Value, nivel, nodo.Attribute("resourceKey").Value);

            if (nodo.Descendants().FirstOrDefault() != null)
            {
                if (!nodo.Descendants().FirstOrDefault().FirstAttribute.Value.Contains("#"))
                    NodoPadre = nodo.FirstAttribute.Value;
                else
                    NodoPadre = string.Empty;

                ExaminaNodo(nodo.Descendants().FirstOrDefault(), ++nivel);
            }
            else
                if (nodo.ElementsAfterSelf().FirstOrDefault() != null)
                {
                    if (!nodo.ElementsAfterSelf().FirstOrDefault().FirstAttribute.Value.Contains("#"))
                        NodoPadre = nodo.Ancestors().FirstOrDefault().FirstAttribute.Value;//GetNodoPadre(nodo, nivel);
                    else
                        NodoPadre = string.Empty;

                    ExaminaNodo(nodo.ElementsAfterSelf().FirstOrDefault(), nivel);
                }
                else
                {
                    if (nodo.Ancestors().FirstOrDefault().ElementsAfterSelf().FirstOrDefault() != null &&
                        !nodo.Ancestors().FirstOrDefault().ElementsAfterSelf().FirstOrDefault().FirstAttribute.Value.Contains("#"))
                        NodoPadre = nodo.Ancestors().FirstOrDefault().FirstAttribute.Value;//GetNodoPadre(nodo, nivel);                    
                    else
                        NodoPadre = string.Empty;

                    ExaminaNodo(nodo.Ancestors().FirstOrDefault().ElementsAfterSelf().FirstOrDefault(), --nivel);
                }
        }
    }

    private void CargarPermisosPerfil()
    {
        try
        {
            //invocamos el servicio del Director
            string grupo = (String)Session["Grupo_TK"];

            WSGrantForSystemCache.WSGrantForSystemCache ws = new WSGrantForSystemCache.WSGrantForSystemCache();
            ws.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //int cant = grupos.Count;
            //for (int i = 0; i < cant; i++)
            //{
            var Response = ws.GetGrantFromSystemGroup(ConfigurationManager.AppSettings["Sistema"].ToString(), grupo);

            foreach (var item in Response)
            {
                CargarPermiso(item.SoapFile, item.accion, item.servicio);
            }
            //}
        }
        catch (Exception)
        {
            Permisos = new HashSet<PermisosPerfil>();
        }
    }

    /// <summary>
    /// Carga todos los permisos que posee el usuario sin importar al grupo o perfil que posea. De esta forma se podrán "sumar" los 
    /// accesos por la concurrencia de los grupos a los que pertenezca.
    /// </summary>
    /// <param name="soapFile">Indica el Nombre del Archivo al qeu se invoca</param>
    /// <param name="accion">indica el metodo al que puede acceder. Para el caso del menú, indicará la opción a la que puede acceder</param>
    /// <param name="servicio">Nombre del Servicio Web</param>
    private void CargarPermiso(string soapFile, string accion, string servicio)
    {
        if (Permisos == null)
        {
            Permisos = new HashSet<PermisosPerfil>();
        }
        PermisosPerfil pp = new PermisosPerfil() { SoapFile = soapFile, Accion = accion, Servicio = servicio };
        Permisos.Add(pp);
    }

    /// <summary>
    /// Funcion que realiza la consulta Linq sobre el web.sitemap. Se lanza la consulta recursiva con nivel = 0
    /// </summary>
    private void LeerWebSiteMap()
    {
        if (HMenu == null)
        {
            XElement documento = XElement.Load(Server.MapPath("~/Web.sitemap"));

            var nodos = from n in documento.Elements().First().Elements()
                        select n;

            ExaminaNodo(nodos.FirstOrDefault(), 0);
        }
    }

    #region Todo el menu
    public string ObtenerMenu2()
    {
        string cadena = string.Empty;
        int nvl = 0;
        bool primeraVuelta = true;
        LeerWebSiteMap();

        cadena = "\n<div class=\"jquerycssmenu\" id=\"menuanses\">\n<ul>\n";

        foreach (var item in HMenu)
        {
            if (nvl < item.Nivel)
            {
                cadena += "\n";
                if (item.Nivel - nvl > 1)
                    nvl = nvl + (item.Nivel - nvl);
                else
                    ++nvl;

                for (int i = 0; i < nvl; i++)
                {
                    cadena += "\t";
                }

                cadena += "<ul>\n";
            }
            else if (nvl > item.Nivel)
            {
                if (nvl - item.Nivel > 1)
                    nvl = nvl - (nvl - item.Nivel);
                else
                    --nvl;

                cadena += "</li>\n";
                for (int i = 0; i < nvl + 1; i++)
                {
                    cadena += "\t";
                }
                cadena += "</ul>\n";
                for (int i = 0; i < nvl + 1; i++)
                {
                    cadena += "\t";
                }
                cadena += "</li>\n";
            }
            else if (!primeraVuelta)
            {
                cadena += "</li>\n";
            }
            for (int i = 0; i < nvl + 1; i++)
            {
                cadena += "\t";
            }
            //cadena += "<li><a href=\"" + item.vinculo + "\">" + item.Titulo + "</a>";
            if (item.ResouceKey.Substring(0, 10).Equals("OpMenuGrup"))
                cadena += "<li><a href=\"" + "#nogo" + "\">" + item.Titulo + "</a>";
            else
                cadena += "<li><a href=\"" + item.Vinculo + "\">" + item.Titulo + "</a>";

            if (primeraVuelta)
                primeraVuelta = false;

        }
        cadena += "</li>\n</ul>\n</div>\n";
        return cadena;

    }

    #endregion Todo el menu

    #region Menu segun rol
    /// <summary>
    /// funcion que permite leer y devolver con etiquetas ul y li los items del web.sitemap
    /// </summary>
    /// <returns>Devuelve un string con el menu generado a base de etiquetas ul y li</returns>
    public string ObtenerMenu()
    {
        bool noAcceso = false;
        if (Permisos == null)
        {
            CargarPermisosPerfil();
        }
        if (MenuPerfil.Equals(""))
        {
            string cadena = string.Empty;
            int nvl = 0;
            bool primeraVuelta = true;
            LeerWebSiteMap();

            cadena = "\n<div class=\"jquerycssmenu\" id=\"menuanses\">\n<ul>\n";

            foreach (var item in HMenu)
            {
                //TODO:SACAR 1==1 
                if (PoseeAcceso(item.ResouceKey))
                {
                    //pone la variable en 1 ya que tiene al menos un item
                    noAcceso = true;
                    if (nvl < item.Nivel)
                    {
                        cadena += "\n";
                        if (item.Nivel - nvl > 1)
                            nvl = nvl + (item.Nivel - nvl);
                        else
                            ++nvl;

                        for (int i = 0; i < nvl; i++)
                        {
                            cadena += "\t";
                        }

                        cadena += "<ul>\n";
                    }
                    else if (nvl > item.Nivel)
                    {
                        if (nvl - item.Nivel > 1)
                            nvl = nvl - (nvl - item.Nivel);
                        else
                            --nvl;

                        cadena += "</li>\n";
                        for (int i = 0; i < nvl + 1; i++)
                        {
                            cadena += "\t";
                        }
                        cadena += "</ul>\n";
                        for (int i = 0; i < nvl + 1; i++)
                        {
                            cadena += "\t";
                        }
                        cadena += "</li>\n";
                    }
                    else if (!primeraVuelta)
                    {
                        cadena += "</li>\n";
                    }
                    for (int i = 0; i < nvl + 1; i++)
                    {
                        cadena += "\t";
                    }


                    #region enlaces manuales comentado
                    //string CUIT = VariableSession.unPrestador.Cuit.ToString();

                    //if (item.Titulo.ToUpper().Equals("DESCARGAR MANUAL"))
                    //{

                    //if (CUIT.Equals(ConfigurationManager.AppSettings["cuit_correo"].ToString()))
                    //{
                    //    item.Vinculo = ConfigurationManager.AppSettings["url_manual_correo"].ToString();
                    //}
                    //else if (CUIT.Equals(ConfigurationManager.AppSettings["cuit_aerolineas"].ToString()))
                    //{
                    //    item.Vinculo = ConfigurationManager.AppSettings["url_manual_aerolineas"].ToString();
                    //}
                    //}

                    //if (item.Titulo.ToUpper().Equals("Reposición".ToUpper()))
                    //{
                    //if (!CUIT.Equals(ConfigurationManager.AppSettings["cuit_correo"].ToString()))
                    //{
                    //    continue;
                    //}
                    //}

                    #endregion enlaces manuales comentado

                    if (item.ResouceKey.Substring(0, 10).Equals("OpMenuGrup"))
                        cadena += "<li><a href=\"" + "#nogo" + "\">" + item.Titulo + "</a>";
                    else
                        cadena += "<li><a href=\"" + item.Vinculo + "\">" + item.Titulo + "</a>";
                    //cadena += "<li><a href=\"" + ResolveUrl(item.Vinculo) + "\">" + item.Titulo + "</a>";

                    if (primeraVuelta)
                        primeraVuelta = false;
                }
            }
            cadena += "</li>\n</ul>\n</div>\n";

            //si no obtuvo acceso a nada envia el script vacio
            MenuPerfil = noAcceso ? cadena : string.Empty;

            //MenuPerfil = cadena;
        }
        return MenuPerfil;
    }

    #endregion Menu segun rol


    private bool PoseeAcceso(string resourceKey)
    {
        foreach (var item in Permisos)
        {
            if (item.Accion == resourceKey)
                return true;
        }
        return false;
    }

}
