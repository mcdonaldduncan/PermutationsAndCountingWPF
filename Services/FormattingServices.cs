using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PermutationsAndCountingWPF.Services
{
    /// <summary>
    /// Used to format strings for display
    /// </summary>
    internal class FormattingServices
    {
        /// <summary>
        /// Formats a list of permutations, partitions or combinations into a presentable string
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        public string FormatList(List<string> collection)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var str in collection)
            {
                sb.Append("(");

                for (int i = 0; i < str.Length; i++)
                {
                    if (i == str.Length - 1)
                    {
                        sb.Append(str[i]);
                    }
                    else
                    {
                        sb.Append($"{str[i]}, ");
                    }
                }

                sb.AppendLine(")");
            }

            return sb.ToString();
        }

    }
}
