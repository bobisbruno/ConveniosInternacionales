<%@ Application Language="C#" %>
<%@ Import namespace="log4net"%>

<script runat="server">


    private static readonly ILog log = log4net.LogManager.GetLogger(typeof(global_asax).Name);
    
    void Application_Start(object sender, EventArgs e) 
    {
        //Application["MenuTotal"] = null;

        string ruta = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Config.log4Net"]);
        System.IO.FileInfo arch = new System.IO.FileInfo(ruta);
        log4net.Config.XmlConfigurator.ConfigureAndWatch(arch);

    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs
        Exception err = Server.GetLastError().GetBaseException();

        string ErrorID = Guid.NewGuid().GetHashCode().ToString();
        //              string oUsuario =Pagina.User.Identity.Name.ToString();

        //Escribo el Error en el Log de Eventos
        StringBuilder MsgErr = new StringBuilder();

        MsgErr.Append("ID Error       : " + ErrorID.ToString() + "\n");
        MsgErr.Append("Mensaje Error : " + err.Message.ToString() + "\n");
        MsgErr.Append("Stack     : " + err.StackTrace.ToString() + "\n");

        log.Error(MsgErr.ToString());
        //EventLog.WriteEntry("MiHlab", MsgErr.ToString(), EventLogEntryType.Error); 

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
