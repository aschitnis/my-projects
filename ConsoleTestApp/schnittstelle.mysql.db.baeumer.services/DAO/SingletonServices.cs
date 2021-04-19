using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace schnittstelle.mysql.db.baeumer.services.DAO
{
    public class SingletonServices
    {
        private Dictionary<string, List<string>> diColumnNames = new Dictionary<string, List<string>>();
        private DataAccessModel objDataAccess ; 

        private SingletonServices()
        {
             objDataAccess = new DataAccessModel();
        }

        //private void ReadXml()
        //{
        //    XmlDocument doc = new XmlDocument();
        //    DirectoryInfo dInfo = new DirectoryInfo(System.IO.Path.Combine(this.GetType().Assembly.Location, "config"));
        //}

        private static SingletonServices instance = new SingletonServices();
        public static SingletonServices Instance => instance;

        public DataAccessModel GetDataAccessModel()
        {
            return objDataAccess;
        }
    }
}
