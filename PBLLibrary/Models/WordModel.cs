using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBLLibrary.Models
{
    public class WordModel
    {
        public string Word { get; set; }

        public WordModel() { }
        public WordModel(string word)
        {
            Word = word;
        }
    }
}
