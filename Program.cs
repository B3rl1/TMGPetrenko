using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
	class Program
	{
		//Создание словаря с хранением всех русских слов с определенным индекос Петренко-Гольцмана и с таким же индексом английских строк.
		public static Dictionary<float, TextList> RusDictionary;
		static void Main(string[] args)
		{
			//Входные данные для русских текстов.
			List<string> rusStrList = new List<string>
			{
				"Не выходи из комнаты, не совершай ошибку.",
				"Не выходи из комнаты, не совершай ошибку.",
				"Не выходи, не совершай ошибку.",
				"Проверочная строка"
			};

			//При добавлении в словарь новых значений, если текущая заполненность словаря не превышает его емкости, то добавление выполняется за O(1). Если же превышает, то за O(n).
			//Но мы можем столкнуться с такой проблемой, когда у нас во входной строке 10 строчек, и все они повторяются, а значит в словаре будет только 1 запись о такой строчке,
			//поэтому из выделенной памяти под 10 элементов по факту будет использоваться только 1 элементом.
			RusDictionary = new Dictionary<float, TextList>(rusStrList.Count); 

			//Входные данные для английских текстов.
			List<string> engStrList = new List<string>
			{
				"Hello world. Hi Dmitry. How are you todayy. | Some comments",
				"Heyll babic. My Orator. Iss god for todayy  | Some comments",
				"Ye mamoci, ne soverhay osibku | Some comments",
				"Test string"
			};

			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();

			GetIndexPetrenkoGolcman(rusStrList);
			GetIndexPetrenkoGolcman(engStrList, false);

			foreach (var textList in RusDictionary)
			{
				Console.WriteLine();
				Console.WriteLine($"Для строк(и) с индексом Петренко-Гольцмана {textList.Key}:");
				foreach (var str in textList.Value.RusStringList.Distinct())
				{
					Console.WriteLine("\t" + str);
				}
				Console.WriteLine($"Подходят следующие английские строк(и) с индексом Петренко-Гольцмана {textList.Key}:");
				foreach (var str in textList.Value.EngStringList.Distinct())
				{
					Console.WriteLine("\t" + str);
				}
			}

			stopwatch.Stop();
			Console.WriteLine("Программа выполнилась за - {0} ms", stopwatch.ElapsedMilliseconds);
		}

		public static void GetIndexPetrenkoGolcman(List<string> str, bool isRus = true)
		{
			int count = 0;
			float startNum = 0.5f;
			float lastNum = 0.0f;
			int incrementer = 1;
			float sumByString = 0.0f;

			for (int i = 0; i < str.Count; i++)
			{
				lastNum = 0;
				sumByString = 0;
				count = 0;

				for (int j = 0; j < str[i].Length; j++)
				{
					if (!isRus && str[i][j] == '|')
						break;

					if (Char.IsLetter(str[i][j]))
					{
						count++;
					}

					//Используем формулы алгебраической прогрессии, чтобы узнать общую сумму.
					lastNum = 0.5f + incrementer * (count - 1);
					sumByString = (((0.5f + lastNum) * count) / 2) * count;
				}

				if (isRus)
				{
					//Метод словаря ContainsKey выполняется за время близкое к O(1).
					if (RusDictionary.ContainsKey(sumByString))
					{
						RusDictionary[sumByString].RusStringList.Add(str[i]);
					}
					else
					{
						RusDictionary.Add(sumByString, new TextList(str[i], sumByString));
					}
				}
				else
				{
					if (RusDictionary.ContainsKey(sumByString))
						RusDictionary[sumByString].EngStringList.Add(str[i]);
				}
			}
		}
	}
}
