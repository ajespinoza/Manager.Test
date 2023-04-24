using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager.Core
{
    [Serializable]
    public class AccountArgs : EventArgs
    {
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}
