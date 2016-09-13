using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestingDreamQuery.DTO;

namespace TestingDreamQuery.DBInerface
{
    public interface ITestingFeature
    {
        //DataTable GetFeature(int min, int max);
        DataTable GetFeature(int min, int max,out int result);

        IEnumerable<Feature> GetFeatureAll();
    }
}
