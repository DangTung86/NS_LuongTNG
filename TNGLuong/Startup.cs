using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Web.Configuration;

[assembly: OwinStartupAttribute(typeof(TNGLuong.Startup))]
namespace TNGLuong
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            
        }        
    }
}
