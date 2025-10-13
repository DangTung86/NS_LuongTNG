using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using System.Web.Http;
using System.Configuration;
using System.Web.Configuration;

namespace TNGLuong
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {            
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //try
            //{
            //    Configuration config = GetConfiguration();
            //    ConfigurationSection configSection = config.GetSection("connectionStrings");
            //    if (!configSection.SectionInformation.IsProtected)
            //    {
            //        configSection.SectionInformation.ProtectSection("RSAProtectedConfigurationProvider");
            //        config.Save();
            //    }
            //}
            //catch (Exception ex)
            //{ }
        }

        void Application_End(object sender, EventArgs e)
        {

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
            Exception ex = HttpContext.Current.Server.GetLastError();
            if (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            if (ex is HttpException)
            {
                Response.Redirect("ErrorPage.aspx");
            }

            HttpContext.Current.Server.ClearError();

        }

        //protected Configuration GetConfiguration()
        //{
        //    if (System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath == "/")
        //        return WebConfigurationManager.OpenWebConfiguration("~/web.config");

        //    WebConfigurationFileMap fileMap = CreateFileMap(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        //    // Get the Configuration object for the mapped virtual directory.
        //    return WebConfigurationManager.OpenMappedWebConfiguration(fileMap, System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
        //}

        //protected WebConfigurationFileMap CreateFileMap(string applicationVirtualPath)
        //{

        //    WebConfigurationFileMap fileMap =
        //           new WebConfigurationFileMap();

        //    // Get he physical directory where this app runs. 
        //    // We'll use it to map the virtual directories 
        //    // defined next.  
        //    string physDir = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;

        //    // Create a VirtualDirectoryMapping object to use 
        //    // as the root directory for the virtual directory 
        //    // named config.  
        //    // Note: you must assure that you have a physical subdirectory 
        //    // named config in the curremt physical directory where this 
        //    // application runs.
        //    VirtualDirectoryMapping vDirMap =
        //        new VirtualDirectoryMapping(physDir, true);

        //    // Add vDirMap to the VirtualDirectories collection  
        //    // assigning to it the virtual directory name.
        //    fileMap.VirtualDirectories.Add(applicationVirtualPath, vDirMap);

        //    // Create a VirtualDirectoryMapping object to use 
        //    // as the default directory for all the virtual  
        //    // directories.
        //    VirtualDirectoryMapping vDirMapBase =
        //        new VirtualDirectoryMapping(physDir, true, "web.config");

        //    // Add it to the virtual directory mapping collection.
        //    fileMap.VirtualDirectories.Add("/", vDirMapBase);

        //    // Return the mapping. 
        //    return fileMap;
        //}
    }
}