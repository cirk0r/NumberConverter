using Autofac;
using NumberConverter.Service.Abstract;
using NumberConverter.Service.Concrete;
using NumberConverter.Service.Helpers;

namespace NumberConverter.IoC
{
    public static class AutofacConfiguration
    {
        public static ContainerBuilder CreateContainer()
        {
            var container = new ContainerBuilder();
            RegisterServices(container);
            RegisterLogic(container);

            return container;
        }

        private static void RegisterServices(ContainerBuilder container)
        {
            container.RegisterType(typeof(ConversionService)).As(typeof(IConversionService));
        }

        private static void RegisterLogic(ContainerBuilder container)
        {
            container.RegisterType(typeof(ConversionLogic)).As(typeof(IConversionLogic));
        }
    }
}
