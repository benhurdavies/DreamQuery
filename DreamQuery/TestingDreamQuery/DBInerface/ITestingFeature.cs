using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingDreamQuery.DTO;
using DreamQuery.Attribute;

namespace TestingDreamQuery.DBInerface
{
    public interface ITestingFeature
    {
        //DataTable GetFeature(int min, int max);
        [SPName("GetFeature")]
        DataTable GetFeatureBetween(int min, int max,out int result);

        [SPName("GetFeature")]
        IEnumerable<Feature> GetFeatureBetween(FeatureInput Feature);

        IEnumerable<Feature> GetFeatureAll();
    }
}
