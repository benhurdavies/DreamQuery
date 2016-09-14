using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Attribute;

namespace DreamQuery.SP.Execute
{

    [Serializable]
    class ParameterMetadata
    {
        /// <summary>
        /// Parameter Name
        /// </summary>
        [RField("ParameterName")]
        public string Name { get; set; }

        /// <summary>
        /// Parameter Type
        /// </summary>
        [RField("DataType")]
        public string Type { get; set; }

        /// <summary>
        /// Parameter Mode..
        /// </summary>
        [RField("ParameterMode")]
        public string PMode { get; set; }

        public bool IsOut(string DBKey)
        {
            bool Result = false;
            if(DB.SQLSERVER.Equals(DBKey,StringComparison.OrdinalIgnoreCase))
            {
                Result = PMode.Equals("INOUT", StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                throw new Exception("Not Implemented for :" + DBKey);
            }
            return Result;
        }
    }
}
