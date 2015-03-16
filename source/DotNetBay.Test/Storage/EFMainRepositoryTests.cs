using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            private readonly IList<EFMainRepository> repositories = new List<EFMainRepository>();

            public void Dispose()
            {
                foreach (var repository in this.repositories) repository.Database.Delete();
            }

            public IMainRepository CreateMainRepository()
            {
                var repository = new EFMainRepository();
                this.repositories.Add(repository);

                return repository;
            }
        }
    }
}