using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Core.AOP.AspectInterceptors;
using TechCareer.Service.Abstracts;
using TechCareer.Service.Concretes;

namespace TechCareer.Service.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<EventService>().As<IEventService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();
            builder.RegisterType<VideoEducationService>().As<IVideoEducationService>().InstancePerLifetimeScope();
            builder.RegisterType<InstructorService>().As<IInstructorService>().InstancePerLifetimeScope();

            builder.RegisterType<AspectInterceptorSelector>().AsSelf().InstancePerDependency();
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
