using PBLLibrary.DataAccess;
using PBLLibrary.Mailing;
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
        private static EmailConnector EmailConnector;

        public static void InitializeConnection()
        {
            Connector = new MySQLConnector();
            EmailConnector = new EmailConnector();
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

        public static void SendMessage(string desEmail, string subject, string body)
        {
            Task.Run(() => EmailConnector.SendMessage(desEmail, subject, body));
        }

        public static string GenerateVerifyCode()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }

        public static string ConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
