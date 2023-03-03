using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using PBLLibrary.Models;

namespace PBLLibrary.DataAccess
{
    public class MySQLConnector
    {
        private MySqlConnection _DatabaseConnection;

        public MySQLConnector()
        {
            _DatabaseConnection = new MySqlConnection(GlobalConfig.ConnectionString("Dictionary"));
        }

        public List<SynsetModel> GetSynset_All()
        {
            try
            {
                List<SynsetModel> results = null;
                string query = "SELECT wn_synset.synset_id, `definition`, word " +
                                "FROM wn_synset JOIN wn_word ON wn_synset.synset_id = wn_word.synset_id";

                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    results = new List<SynsetModel>();
                    SynsetModel synset = null;
                    while (reader.Read())
                    {
                        if (synset != null)
                            if (synset.ID.Equals(reader.GetString(0)))
                            {
                                synset.Words.Add(new WordModel(reader.GetString(2)));
                                continue;
                            }

                        List<WordModel> words = new List<WordModel>
                        {
                            new WordModel(reader.GetString(2))
                        };

                        synset = new SynsetModel(Convert.ToInt32(reader.GetString(0)), reader.GetString(1), words);
                        results.Add(synset);
                    }

                }

                _DatabaseConnection.Close();

                return results;

            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }

        }

        public List<SynsetModel> GetSynset_ByWord(string word)
        {
            try
            {
                List<SynsetModel> results = new List<SynsetModel>();
                string query = "SELECT wn_synset.synset_id, `definition`, word " +
                                "FROM wn_synset JOIN wn_word ON wn_synset.synset_id = wn_word.synset_id " +
                                "WHERE wn_word.word = '" + word + "'";

                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    SynsetModel synset = null;
                    while (reader.Read())
                    {
                        if (synset != null)
                            if (synset.ID.Equals(reader.GetString(0)))
                            {
                                synset.Words.Add(new WordModel(reader.GetString(2)));
                                continue;
                            }

                        List<WordModel> words = new List<WordModel>
                        {
                            new WordModel(reader.GetString(2))
                        };

                        synset = new SynsetModel(Convert.ToInt32(reader.GetString(0)), reader.GetString(1), words);
                        results.Add(synset);
                    }
                }

                _DatabaseConnection.Close();

                return results;
            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }
        }

        public List<WordModel> GetWord_Random(int limit = 10)
        {
            try
            {
                List<WordModel> results = null;

                string query = "SELECT t1.word " +
                                "FROM wn_word AS t1 " +
                                "JOIN (SELECT t3.synset_id FROM wn_word AS t3 ORDER BY RAND() LIMIT " + limit * 3 +") AS t2 " +
                                "ON t1.synset_id=t2.synset_id " +
                                "WHERE t1.w_num = CEIL(RAND(t1.synset_id) * 6) " +
                                "LIMIT " + limit;
                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new List<WordModel>();
                    while (reader.Read())
                    {
                        results.Add(new WordModel(reader.GetString(0)));
                    }
                }

                _DatabaseConnection.Close();

                return results;
            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }
        }
        public List<WordModel> GetWord_BySynsetID(int synsetID)
        {
            try
            {
                List<WordModel> results = null;

                string query = "SELECT wn_word.word " +
                                "FROM wn_word " +
                                "WHERE wn_word.synset_id = synsetID";
                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new List<WordModel>();
                    while (reader.Read())
                    {
                        results.Add(new WordModel(reader.GetString(0)));
                    }
                }

                _DatabaseConnection.Close();

                return results;
            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }
        }
        public List<WordModel> GetWord_ByFilter(string filter, int limit = 10, bool distinct = false)
        {
            try
            {
                List<WordModel> results = null;

                string query = "SELECT " + (distinct ? "DISTINCT " : "") + "wn_word.word " +
                                "FROM wn_word " +
                                "WHERE wn_word.word LIKE '" + filter + "' " +
                                "LIMIT " + limit;
                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new List<WordModel>();
                    while (reader.Read())
                    {
                        results.Add(new WordModel(reader.GetString(0)));
                    }
                }

                _DatabaseConnection.Close();

                return results;
            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }
        }

        public List<WordModel> GetWord_ByFilter_Random(string filter, int limit = 10, bool distinct = false)
        {
            try
            {
                List<WordModel> results = null;

                string query = "SELECT " + (distinct ? "DISTINCT " : "") + "wn_word.word " +
                                "FROM wn_word " +
                                "WHERE wn_word.word LIKE '" + filter + "' " +
                                "ORDER BY RAND() " +
                                "LIMIT " + limit;
                MySqlCommand command = new MySqlCommand(query, _DatabaseConnection);
                command.Connection = _DatabaseConnection;

                _DatabaseConnection.Open();

                MySqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    results = new List<WordModel>();
                    while (reader.Read())
                    {
                        results.Add(new WordModel(reader.GetString(0)));
                    }
                }

                _DatabaseConnection.Close();

                return results;
            }
            catch (MySqlException except)
            {
                if (_DatabaseConnection.State == ConnectionState.Open)
                    _DatabaseConnection.Close();
                MessageBox.Show("Database Error: " + except.Number + "-" + except.Message);

                return null;
            }
        }

    }

}
