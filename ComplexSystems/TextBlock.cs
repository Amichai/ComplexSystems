using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ComplexSystems {
	public class TextBlock {
		string text = string.Empty;

		public TextBlock(string filename) {
			var reader = File.OpenText(filename);
			text = reader.ReadToEnd();
			count();
		}

		Dictionary<string, int> wordCount = new Dictionary<string, int>();

		private void count() {
			var words = text.Split(' ');
			//Get rid of all whitespace and null values
			//ignore punctuation
			foreach (var word in words) {
				string cleanWord = string.Empty;
				foreach (var letter in word) {
					if (!string.IsNullOrWhiteSpace(letter.ToString()))
						cleanWord += letter;
				}
				if (wordCount.ContainsKey(cleanWord)) {
					wordCount[cleanWord]++;
				} else {
					wordCount.Add(cleanWord, 1);
				}
			}
			
		}

		List<KeyValuePair<string, int>> orderedWords = new List<KeyValuePair<string, int>>();

		public void PrintWordCount(string path) {
			string output = string.Empty;
			var orderedWords = wordCount.OrderByDescending(i => i.Value);
			for (int i = 0; i < wordCount.Count(); i++) {
				output += i.ToString() + ": " + orderedWords.ElementAt(i).Value.ToString() + " " + orderedWords.ElementAt(i).Key + "  \n";
			}
			StreamWriter writer = new StreamWriter(path, false);
			writer.Write(output);
			writer.Flush();	
			writer.Close();
		}

		public DataSeries GetSeries() {
			DataSeries ser = new DataSeries();
			foreach (var keyVal in wordCount) {
				ser.Add(keyVal.Value);
			}
			return ser;
		}
	}

	public class SemanticNetwork {	
		Dictionary<string, Guid> knownWords = new Dictionary<string, Guid>();
		//this is a library of all known words and their properties
		//should be static and serialized
	}
}
