using System;
using System.Collections.Generic;
using System.IO;
using Slave.Framework.Entities;

namespace Slave.Framework
{
	/// <summary>
	/// This class offers helper methods to interact with SlickRun files
	/// </summary>
	public static class SlavesHelper
	{
		/// <summary>
		/// Imports the specified file.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public static List<Commands> ImportFile(string path)
		{
			// TODO validate that it is a qrs file
			var words = new List<Commands>();

			var reader = File.OpenText(path);

			while (!reader.EndOfStream)
			{
				var word = new Commands();

				var aliasLine = reader.ReadLine();
				word.Alias = aliasLine.Substring(1, aliasLine.Length - 2);

				var filenameLine = reader.ReadLine();
				word.FileName = filenameLine.Split('=')[1];

				var pathLine = reader.ReadLine();
				word.WorkingDirectory = pathLine.Split('=')[1];

				var paramsLine = reader.ReadLine();
				word.Arguments = paramsLine.Split('=')[1];

				var notesLine = reader.ReadLine();
				word.Notes = notesLine.Split('=')[1];

				// GUID
				reader.ReadLine();

				var startModeLine = reader.ReadLine();
				var slickRunStartMode = Convert.ToInt32(startModeLine.Split('=')[1]);
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

		public static void ImportFile(List<Commands> words, string path)
		{
		}
	}
}
