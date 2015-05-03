using System;
using System.Transactions;

namespace Sample.Core.Infrastructure.Transactions
{
    public sealed class AppTransactionScope : IDisposable
    {
        private readonly TransactionScope _transactionScope;

        public AppTransactionScope()
        {
            _transactionScope = new TransactionScope(TransactionScopeOption.Required,new TransactionOptions{IsolationLevel = IsolationLevel.ReadCommitted});
        }
       
        public void Complete()
        {
            _transactionScope.Complete();            
        }

        public void Dispose()
        {
            _transactionScope.Dispose();
        }
    }
}
