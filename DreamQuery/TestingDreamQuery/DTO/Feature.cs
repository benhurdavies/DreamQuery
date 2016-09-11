using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DreamQuery.Attribute;

namespace TestingDreamQuery.DTO
{
    public class Feature
    {
        [Field("FeatureId")]
        public int Feature_Id { get; set; }
        public string FileName { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
