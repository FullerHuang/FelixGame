using System;
using System.Collections.Generic;

namespace FelixGame.Core.DI
{
    public interface IContainer
    {
        void Register<TInterface, TImplementation>() where TImplementation : TInterface;
        void RegisterInstance<TInterface>(TInterface instance);
        TInterface Resolve<TInterface>();
    }

    public class SimpleContainer : IContainer
    {
        private readonly Dictionary<Type, object> _instances = new Dictionary<Type, object>();
        private readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _registrations[typeof(TInterface)] = typeof(TImplementation);
        }

        public void RegisterInstance<TInterface>(TInterface instance)
        {
            _instances[typeof(TInterface)] = instance;
        }

        /// <summary>
        /// 根据接口类型解析实现实例
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public TInterface Resolve<TInterface>()
        {
            var type = typeof(TInterface);
            if (_instances.TryGetValue(type, out var instance))
            {
                return (TInterface)instance;
            }

            if (_registrations.TryGetValue(type, out var implementationType))
            {
                // Simple constructor injection (first constructor found)
                var constructor = implementationType.GetConstructors()[0];
                var parameters = constructor.GetParameters();
                var parameterInstances = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramType = parameters[i].ParameterType;    //paramType 运行时才确定，需要使用反射调用Resolve方法
                    parameterInstances[i] = typeof(SimpleContainer)
                        .GetMethod(nameof(Resolve))
                        .MakeGenericMethod(paramType)
                        .Invoke(this, null);
                }

                var newInstance = Activator.CreateInstance(implementationType, parameterInstances);
                _instances[type] = newInstance;
                return (TInterface)newInstance;
            }

            throw new Exception($"Type {type.Name} not registered.");
        }
    }
}
