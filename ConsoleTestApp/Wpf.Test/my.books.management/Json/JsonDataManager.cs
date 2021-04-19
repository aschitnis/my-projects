using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf.Test.my.books.management
{
    public interface IModel
    {

    }
    // class is responsible for serializing & deserilizing objects
    internal static class JsonDataManager
    {
        public static Exception SerializeModel(IEnumerable<IModel> models, out string json)
        {
            Exception ex = null;
            json = null;
            try
            {
                json = JsonConvert.SerializeObject(models);
            }
            catch(Exception e)
            {
                ex = e;
            }
            return ex;
        }
    }
}
