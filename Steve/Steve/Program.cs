using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Steve {
	class Program {
		static void Main(string[] args) {
			// read our file
			string text = File.ReadAllText("text.txt");
			text = text.ToLower();

			FindWords(text);
			FindSentences(text);
			FindPhrases(text);

			Process.Start("explorer.exe", Environment.CurrentDirectory);
			Console.WriteLine("Done, press any key to close");
			Console.Read();
		}

		/// <summary>
		/// Replaces all non-alphanumeric characters with spaces
		/// </summary>
		/// <param name="text"></param>
		/// <param name="chars">Characters to also not replace</param>
		static string RemoveUnwantedCharacters(string text, string chars) {
			// replace all unwanted characters with spaces
			StringBuilder sb = new StringBuilder(text);
			for (int i = 0; i < text.Length; i++) {
				char c = text[i];
				if (!(chars.Contains(c) || char.IsLetterOrDigit(c))) {
					sb[i] = ' ';
				}
			}
			text = sb.ToString();

			text = text.Replace("	", " ");    // dont think this is needed, but it harms not
			text = text.Replace("--", " ");     // lots of books etc do this double dash joke instead of just using parentheses or quotation marks.
			for (int i = 0; i < 100; i++) {
				text = text.Replace("  ", " "); // replace duplicate spaces with a single one. Do it a couple of times to make sure we also get long rows of more than 2 spaces.
			}

			return text;
		}


		static void FindWords(string text) {
			Console.WriteLine("Finding Words");
			text = RemoveUnwantedCharacters(text, " '-");

			// Make a dictionary of each word and its occurence count
			string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, int> occurances = new Dictionary<string, int>();
			foreach (string word in words) {
				word.Trim();
				if (occurances.ContainsKey(word)) {
					occurances[word] += 1;
				}
				else {
					occurances.Add(word, 1);
				}
			}

			int dupes = WriteFile(occurances, "words.txt");
			Console.WriteLine("Found " + dupes + " duplicate words");
		}

		static void FindSentences(string text) {
			Console.WriteLine("Finding Sentences");
			text = RemoveUnwantedCharacters(text, " '-.?!:;");
			
			// Make a dictionary of each sentence and its occurence count
			string[] sentences = text.Split(new char[] { '?', '!', '.', ':', ';' }, StringSplitOptions.RemoveEmptyEntries);
			Dictionary<string, int> occurances = new Dictionary<string, int>();
			foreach (string sentence in sentences) {
				sentence.Trim();
				if (occurances.ContainsKey(sentence)) {
					occurances[sentence] += 1;
				}
				else {
					occurances.Add(sentence, 1);
				}
			}
			int dupes = WriteFile(occurances, "sentences.txt");
			Console.WriteLine("Found " + dupes + " duplicate sentences");
		}

		static void FindPhrases(string text) {
			text = RemoveUnwantedCharacters(text, " '-");

			string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			foreach (string word in words) {
				word.Trim();
			}

			int phraseLength = 2;
			bool stillHaveDuplicates;
			do {
				Console.WriteLine("Finding Phrases of length " + phraseLength);
				stillHaveDuplicates = false;
				Dictionary<string, int> occurances = new Dictionary<string, int>();
				for (int i = 0; i < words.Length - phraseLength + 1; i++) {
					string phrase = "";
					for (int j = i; j < i + phraseLength; j++) {
						phrase += words[j] + " ";
					}
					phrase = phrase.Trim();
					if (occurances.ContainsKey(phrase)) {
						occurances[phrase] += 1;
					}
					else {
						occurances.Add(phrase, 1);
					}
				}
				int dupes = WriteFile(occurances, string.Format("phrasesoflength{0}.txt", phraseLength));
				if (dupes > 0) {
					stillHaveDuplicates = true;
				}
				Console.WriteLine("Found " + dupes + " duplicate phrases of length " + phraseLength);
				phraseLength++;
				//if (phraseLength > 3000) {
				//	return;
				//}
			}
			while (stillHaveDuplicates);
		}


		static int WriteFile(Dictionary<string, int> occurances, string filename) {
			filename = "z" + filename;
			TextWriter file = new StreamWriter(filename);
			int duplicates = 0;
			foreach (KeyValuePair<string, int> o in occurances) {
				if (o.Value > 1) {
					duplicates++;
					file.Write(string.Format("{0}\t{1}\n", o.Key, o.Value));
				}
			}
			file.Close();
			return duplicates;
		}
	}
}
