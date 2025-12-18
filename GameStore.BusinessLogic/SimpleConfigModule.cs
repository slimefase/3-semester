using GameStore.DataAccessLayer;
using Ninject.Modules;
using GameStore.BusinessLogic.Interfaces;
using GameStore.BusinessLogic.Services;

namespace GameStore.BusinessLogic
{
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует зависимости для DI-контейнера
        /// </summary>
        public override void Load()
        {
            Bind<IGameRepository>().To<EntityRepository>().InSingletonScope();
            Bind<IDiscountService>().To<DiscountService>().InSingletonScope();
            Bind<IGroupingService>().To<GroupingService>().InSingletonScope();

            Bind<GameStore.Shared.IModel>().To<GameStore.BusinessLogic.Logic>().InTransientScope();
        }
    }
}

