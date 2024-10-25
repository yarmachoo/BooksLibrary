using Books.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        public readonly AppDbContext Context;
        public TestCommandBase()
        {
            Context = BooksContextFactory.Create();
        }
        public void Dispose()
        {
            BooksContextFactory.Destroy(Context);
        }
    }
}
