using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace PBLLibrary.Models
{
    public class SynsetModel
    {
        public int ID { get; set; }
        public string Definition { get; set; }
        public List<WordModel> Words { get; set; }

        public SynsetModel() { }

        public SynsetModel(int iD, string definition, List<WordModel> words)
        {
            ID = iD;
            Definition = definition;
            Words = words;
        }

        //public SynsetModel(int iD, string definition, string words)
        //{
        //    ID = iD;
        //    Definition = definition;
        //    Words.
        //}
    }
}
