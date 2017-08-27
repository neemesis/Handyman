using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Handyman.Core.Utilities
{
	public static class SlickRunHelper
	{
		public static List<Entities.Handyman> ImportFile(string path)
		{
			List<Entities.Handyman> words = new List<Handyman.Core.Entities.Handyman>();

			StreamReader reader = File.OpenText(path);

			while (!reader.EndOfStream)
			{
				Entities.Handyman word = new Handyman.Core.Entities.Handyman();

				string aliasLine = reader.ReadLine();
				word.Alias = aliasLine.Substring(1, aliasLine.Length - 2);

				string filenameLine = reader.ReadLine();
				word.FileName = filenameLine.Split('=')[1];

				string pathLine = reader.ReadLine();
				word.WorkingDirectory = pathLine.Split('=')[1];

				string paramsLine = reader.ReadLine();
				word.Arguments = paramsLine.Split('=')[1];

				string notesLine = reader.ReadLine();
				word.Notes = notesLine.Split('=')[1];

				// GUID
				reader.ReadLine();

				string startModeLine = reader.ReadLine();
				int slickRunStartMode = Convert.ToInt32(startModeLine.Split('=')[1]);
				switch (slickRunStartMode)
				{
					case 5:
						word.StartUpMode = System.Diagnostics.ProcessWindowStyle.Normal;
						break;

					case 7:
						word.StartUpMode = System.Diagnostics.ProcessWindowStyle.Minimized;
						break;

					case 3:
						word.StartUpMode = System.Diagnostics.ProcessWindowStyle.Maximized;
						break;
					default:
						break;
				}
				word.StartUpMode = System.Diagnostics.ProcessWindowStyle.Normal;
			
				words.Add(word);
			}
			
			return words;
		}

		public static void ImportFile(List<Entities.Handyman> words, string path)
		{
		}
	}
}
