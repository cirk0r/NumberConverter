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
            container.RegisterType(typeof(TranslationService)).As(typeof(ITranslationService));
        }

        private static void RegisterLogic(ContainerBuilder container)
        {
            container.RegisterType(typeof(TranslationLogic)).As(typeof(ITranslationLogic));
        }
    }
}
