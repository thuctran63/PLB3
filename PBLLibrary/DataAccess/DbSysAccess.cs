using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using PBLLibrary.Models.sys;

namespace PBLLibrary.DataAccess
{
    class DbSysAccess
    {
        public static string SearchByWord(string word)
        {
            string rs = "";
            using (var dbContext = new sysContext())
            {
                TblEdict i = dbContext.TblEdict.Where(tbl => tbl.Word == word).FirstOrDefault();


                if (i != null)
                {
                    /*EntityEntry<TblEdict> entry = dbContext.Entry(i);
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Detached;*/

                    string html = i.Detail;
                    const string tagWhiteSpace = @"(>|$)(\W|\n|\r)+<";
                    const string stripFormatting = @"<[^>]*(>|$)";
                    const string lineBreak = @"<(br|BR)\s{0,1}\/{0,1}>";
                    var lineBreakRegex = new Regex(lineBreak, RegexOptions.Multiline);
                    var stripFormattingRegex = new Regex(stripFormatting, RegexOptions.Multiline);
                    var tagWhiteSpaceRegex = new Regex(tagWhiteSpace, RegexOptions.Multiline);
                    var text = html;
                    text = System.Net.WebUtility.HtmlDecode(text);
                    text = tagWhiteSpaceRegex.Replace(text, "><");
                    text = lineBreakRegex.Replace(text, Environment.NewLine);
                    text = stripFormattingRegex.Replace(text, string.Empty);


                    text = text.Replace("=", "  example: ");
                    text = text.Replace("+", " --");
                    text = text.Replace("\n-", "*-");
                    text = text.Replace("* ", "* >");
                    text = text.Replace("!", "  example: ");

                    /* Printing the text "C#" to the console. */
                    // Console.WriteLine(text);



                    List<string> wordType = text.Split(new[] { "* " }, StringSplitOptions.None).ToList();



                    List<string[]> details = new List<string[]>();

                    foreach (string item in wordType)
                    {
                        details.Add(item.Split('*'));

                    }



                    foreach (var detail in details)
                    {
                        int n = detail.Count();

                        /* Printing the first 6 lines*/
                        int k = (n > 6) ? 6 : n;

                        for (int j = 0; j < k; j++)
                        {
                            rs += "\n\n" + detail[j];

                        }
                    }

                }
            }
            return rs;
        }
    }
}
