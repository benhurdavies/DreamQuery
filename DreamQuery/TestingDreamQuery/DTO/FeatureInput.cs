using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Attribute;

namespace TestingDreamQuery.DTO
{
    public class FeatureInput
    {
        [PField("max")]
        public int Max { get; set; }

        [PField("min")]
        public int Min { get; set; }
        public int result { get; set; }
    }
}
