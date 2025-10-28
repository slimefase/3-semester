using GameStore.DataAccessLayer;
using Ninject.Modules;

namespace GameStore.BusinessLogic
{
    public class SimpleConfigModule : NinjectModule
    {
        /// <summary>
        /// Регистрирует зависимости для DI контейнера.
        /// </summary>
        public override void Load()
        {
            //EntityRepository:
            Bind<IGameRepository>().To<EntityRepository>().InSingletonScope();

            // DapperRepository:
            // Bind<IGameRepository>().To<DapperRepository>().InSingletonScope();
        }
    }
}
