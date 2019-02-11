using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using SEO.BusinessLogicLayer.Models.Implementation;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.BusinessLogicLayer.Dependencies
{
    public class BusinessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchUrl>().As<ISearchURL>();
        }
    }
}
