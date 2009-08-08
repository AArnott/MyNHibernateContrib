<%@ Application Language="C#" %>
<%@ Import Namespace="ExampleLibrary" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
		log4net.Config.DOMConfigurator.Configure();
		Database.ConstructDatabase(); // DANGER: Recreates the database
    }
    
    void Application_End(object sender, EventArgs e) 
    {
		//Database.DestructDatabase(); // DANGER: Destroys the database
    }
        
</script>
