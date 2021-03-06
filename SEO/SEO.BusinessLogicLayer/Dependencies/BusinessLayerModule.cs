﻿using Autofac;
using SEO.BusinessLogicLayer.Models.Implementation;
using SEO.BusinessLogicLayer.Models.Interfaces;

namespace SEO.BusinessLogicLayer.Dependencies
{
    public class BusinessLayerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SearchUrl>().As<ISearchUrl>();
        }
    }
}
