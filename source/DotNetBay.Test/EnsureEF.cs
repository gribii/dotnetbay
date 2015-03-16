using System.Data.Entity.SqlServer;

namespace DotNetBay.Test
{
    internal class EnsureEf
    {
        static EnsureEf()
        {
            // ensure reference to EF
            // ReSharper disable once UnusedVariable
            var ensureDllIsCopied = SqlProviderServices.Instance;
        }
    }
}