using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
	class TextList
	{
		public float IndexGolcmana { get; set; }
		public List<string> rusStringList { get; set; } = new List<string>();
		public List<string> engStringList { get; set; } = new List<string>();

		public TextList(string str, float indexGolcmana)
		{
			IndexGolcmana = indexGolcmana;
			rusStringList.Add(str);
		}
	}
}
