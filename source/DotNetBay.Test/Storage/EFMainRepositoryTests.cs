using DotNetBay.Data.EF;
using DotNetBay.Interfaces;

namespace DotNetBay.Test.Storage
{
    public class EFMainRepositoryTests : MainRepositoryTestBase
    {
        protected override IRepositoryFactory CreateFactory()
        {
            return new EFMainRepositoryFactory();
        }

        private class EFMainRepositoryFactory : IRepositoryFactory
        {
            private readonly EFMainRepository repository;

            public EFMainRepositoryFactory()
            {
                this.repository = new EFMainRepository();
            }

            public void Dispose()
            {
                this.repository.Database.Delete();
            }

            public IMainRepository CreateMainRepository()
            {
                return this.repository;
            }
        }
    }
}