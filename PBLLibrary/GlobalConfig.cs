using PBLLibrary.DataAccess;
using PBLLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBLLibrary
{
    public static class GlobalConfig
    {
        private static MySQLConnector Connector;

        public static void InitializeConnection()
        {
            Connector = new MySQLConnector();
        }

        public static List<SynsetModel> GetSynset_All()
        {
            return Connector.GetSynset_All();
        }

        public static List<SynsetModel> GetSynset_ByWord(string word)
        {
            return Connector.GetSynset_ByWord(word);
        }

        public static List<WordModel> GetWord_Random(int limit = 10)
        {
            return Connector.GetWord_Random(limit);
        }

        public static List<WordModel> GetWord_BySynsetID(int synsetID)
        {
            return Connector.GetWord_BySynsetID(synsetID);
        }

        public static List<WordModel> GetWord_ByFilter(string filter, int limit = 10, bool distinct = false)
        {
            return Connector.GetWord_ByFilter(filter, limit, distinct);
        }

        public static List<WordModel> GetWord_ByFilter_Random(string filter, int limit = 10, bool distinct = false)
        {
            return Connector.GetWord_ByFilter_Random(filter, limit, distinct);
        }

        

        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


        public static string Vi_GetDescription_ByWord(string word)
        {
            return PBLLibrary.DataAccess.DbSysAccess.SearchByWord(word);
        }


    }
}
