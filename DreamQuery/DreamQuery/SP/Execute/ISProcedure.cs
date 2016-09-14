using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamQuery.SP.Execute
{
    interface ISProcedure
    {
        /// <summary>
        /// Set Connection String for executing Metadata gathering query
        /// </summary>
        /// <param name="ConnectionString"></param>
        void SetConnectionString(string ConnectionString);
        IEnumerable<ParameterMetadata> GetParameterMetadata(string SProcedureName);
    }
}
