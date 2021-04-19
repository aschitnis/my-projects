using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.Encryption.Interfaces
{
    public interface ISaltEncryptionString
    {
        bool EncryptString(string text);
    }
}
