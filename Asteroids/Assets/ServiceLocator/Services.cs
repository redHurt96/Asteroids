using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    public class Services
    {
        private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

        public Services RegisterSingle<T>(T service) where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
                throw new Exception($"Already instantiated service {typeof(T)}");

            _services[typeof(T)] = service;

            return this;
        }

        private T GetSingle<T>() where T : IService
        {
            if (!_services.ContainsKey(typeof(T)))
                throw new UnavailableServiceException(typeof(T));

            return (T) _services[typeof(T)];
        }
    }
}
