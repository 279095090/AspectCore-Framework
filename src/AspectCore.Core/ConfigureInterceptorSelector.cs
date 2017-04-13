﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AspectCore.Abstractions;

namespace AspectCore.Core
{
    public class ConfigureInterceptorSelector : IInterceptorSelector
    {
        private readonly IAspectConfigureProvider _aspectConfigureProvider;
        private readonly IServiceProvider _serviceProvider;

        public ConfigureInterceptorSelector(IAspectConfigureProvider aspectConfigureProvider, IServiceProvider serviceProvider)
        {
            _aspectConfigureProvider = aspectConfigureProvider;
            _serviceProvider = serviceProvider;
        }

        public IEnumerable<IInterceptor> Select(MethodInfo method, TypeInfo typeInfo)
        {
            return _aspectConfigureProvider.AspectConfigure.InterceptorFactories
                .Where(x => x.Predicate(method))
                .Select(x => x.CreateInstance(_serviceProvider));
        }
    }
}