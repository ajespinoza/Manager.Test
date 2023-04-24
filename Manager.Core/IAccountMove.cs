using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core
{
    public interface IAccountMove
    {
        event EventHandler<AccountArgs> changeAmount;
        void Debit(decimal amount);
        void Credit(decimal amount);
        decimal GetAmount();

    }
}
