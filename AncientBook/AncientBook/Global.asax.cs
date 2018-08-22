using Autofac;
using AncientBook.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac.Integration.Mvc;
using AncientBook.Service;
using System.Configuration;
using System.Reflection;
using Autofac.Core;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;

namespace AncientBook
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //注册Service
            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterType<UserAccountService>().As<IUserAccountService>().InstancePerHttpRequest();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerHttpRequest();
            builder.RegisterType<CmsStatesService>().As<ICmsStatesService>().InstancePerHttpRequest();
            builder.Register<IDbContextUser>(p => new AncientBookDbContext_User(ConfigurationManager.AppSettings["CoreUserConString"].ToString())).Named<IDbContextUser>("CoreUserConString");
            builder.Register<IDbContextBook>(p => new AncientBookDbContext_Book(ConfigurationManager.AppSettings["CoreBookConString"].ToString())).Named<IDbContextBook>("CoreBookConString");
            builder.RegisterGeneric(typeof(EfRepositoryUser<>)).As(typeof(IRepositoryUser<>))
               .WithParameter(ResolvedParameter.ForNamed<IDbContextUser>("CoreUserConString"))
               .InstancePerHttpRequest();
            builder.RegisterGeneric(typeof(EfRepositoryBook<>)).As(typeof(IRepositoryBook<>))
               .WithParameter(ResolvedParameter.ForNamed<IDbContextBook>("CoreBookConString"))
               .InstancePerHttpRequest();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));  //注册MVC容器
            System.Net.ServicePointManager.DefaultConnectionLimit = 512;
            ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
        }
        /// <summary>
        /// https专用 防止报SSL/TLS 安全通道建立信任关系
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   // 总是接受  
            return true;
        }
    }
}