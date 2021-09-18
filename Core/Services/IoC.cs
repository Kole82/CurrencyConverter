using Core.Data;
using Core.Data.Models;
using Core.ViewModels;
using Core.ViewModels.Screens;
using Ninject;

namespace Core.Services
{
    public static class IoC
    {
        #region Private Fields

        private static IKernel _kernel = new StandardKernel();

        #endregion

        #region Constructors

        static IoC()
        {
            _kernel.Bind<IRepository<Currency>>().To<CurrencyRepo>().InSingletonScope();
            _kernel.Bind<ICurrencyProcess>().To<CurrencyProcessor>();

            _kernel.Bind<ApplicationViewModel>().ToSelf().InSingletonScope();
            _kernel.Bind<CurrencyConverterViewModel>().ToSelf().InSingletonScope();
            _kernel.Bind<LoadScreenViewModel>().ToSelf().InTransientScope();
        }

        #endregion

        #region Public Methods

        public static T Resolve<T>() => _kernel.Get<T>();

        #endregion
    }
}
