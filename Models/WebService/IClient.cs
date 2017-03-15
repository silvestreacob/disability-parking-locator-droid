using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dpark.Models.WebService
{
    interface IClient
    {
        Task<bool> CheckConnection();
    }
}
