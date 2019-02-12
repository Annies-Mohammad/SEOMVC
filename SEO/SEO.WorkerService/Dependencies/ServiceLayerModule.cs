using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SEO.WorkerService.Interfaces;
using SEO.WorkerService.SEOServiceLogic;

namespace SEO.WorkerService.Dependencies
{
    public class ServiceLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SEORequestService>().As<ISEORequestService>();
        }
    }
}
