using DreamQuery.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TestingDreamQuery.DTO;
using TestingDreamQuery.DBInerface;
using DreamQuery.SP;
using DreamQuery;
using System.CodeDom.Compiler;
using System.CodeDom;
using System.IO;

namespace TestingDreamQuery
{
    class Program
    {
       public static void Main(string[] args)
       {
         var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
         ITestingFeature Obj = SpFactory.GetInstance<ITestingFeature>(DB.SQLSERVER, connection);
         var data = Obj.GetFeature(4500, 5000);

         var data2 = Obj.GetFeatureAll();
       }
    }
}
