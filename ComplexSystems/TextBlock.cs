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
			foreach (var word in words) {
				word.ToUpper();
				string cleanWord = string.Empty;
				foreach (var letter in word) {
					if (!string.IsNullOrWhiteSpace(letter.ToString())) {
						cleanWord += char.ToLower(letter);
					}
				} if (wordCount.ContainsKey(cleanWord)) {
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

		public Signal GetSeries() {
			Signal ser = new Signal();
			foreach (var keyVal in wordCount) {
				ser.Add(keyVal.Value);
			}
			return ser;
		}
	}

	public class SemanticNetwork {
		//Semantic network parameters:
		//Propagation strength
		//Current excitation value
		//Input excitation
		Signal propagationStrength = new Signal();
		Signal excitationState = new Signal();


		public void Propagate(int times) {
			for (int i = 0; i < times; i++) {
				propagateSignal(excitationState);
			}
		}

		private void propagateSignal(Signal excitedNodes) {
			for (int i = 0; i < excitedNodes.Count(); i++) {
				int nodeToExcite = (int)(excitedNodes[i] * propagationStrength[i] + excitedNodes.Count()) % excitationState.Count();
				excitationState[nodeToExcite] *= excitedNodes[i] * propagationStrength[i];
			}
		}

		public double Energy() {
			throw new NotImplementedException();
		}

		public string PrintOutput() {
			excitationState.RankOrder(20);
			throw new NotImplementedException();
		}

		//Energy Function
		//Present output text

		//Genetic algorithm and SA

		Signal networkExcitation = new Signal();
		Dictionary<string, Guid> knownWords = new Dictionary<string, Guid>();
		Dictionary<Guid, int> numericalRepresentation = new Dictionary<Guid,int>();

		Dictionary<Guid, Signal> excitationSignal = new Dictionary<Guid, Signal>();
		//iterate over the excitation signal

		//Serialization and de-serialization methods
		//Visualization methods

		//this is a library of all known words and their properties
		//should be static and serialized
	}
}
