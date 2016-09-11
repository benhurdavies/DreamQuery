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
        IEnumerable<Feature> GetFeature(int min, int max);

        IEnumerable<Feature> GetFeatureAll();
    }
}
