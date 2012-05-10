using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;

namespace ComplexSystems {
	//Signal generators
	//Extenders/processors
	//Genetic algorithm
	//Signal visualization
	//Signal analysis (pdf)


	//Nodes 
	//Signals are processed through a graph


	//Develop series/histogram analysis tools so that we can have a histogram of histograms 
	//and study the evolution of Series' over time

	class Program {
		static void Main(string[] args) {

			//Open some input object
			//Generate heuristics from that input object (possibly do some iterations)
			//Add the input object and labels to dictionary
			//Perform a probabilistic lookup against the library
			LabeledStorage<string> testing = new LabeledStorage<string>();




			//SignalGenerator.GaussianDistribution(10000).GetHistogram(.1).Graph();
			//SignalGenerator.PowerLawDistribution(100000).GetHistogram(10).GraphLogLog();

			//Random rand = new Random();
			//Signal randomNumbers = new Signal();
			//for(int i=0; i< 100000; i++){
			//    randomNumbers.Add(rand.NextDouble());
			//}
			//randomNumbers.GetHistogram(.0005).GetSignal().GetHistogram(1).Graph();

			

			
		}

		static void SignalComparer() {
			var signals = new List<Signal>();
			for (int i = 0; i < 20; i++) {
				var signal = SignalGenerator.IteratedRandomMultiplication(10000, 3);
				signals.Add(signal);
			}
			var A = SeriesTrialAnalysis.CompareAListOfSeries(signals, numberOfMomentsToTest: 8);
			for (int i = 0; i < A.Count(); i++)
				Debug.Print(A[i].ToString() + "\n");
		}

		static void WordRankOrder() {
			var A = new TextBlock(@"C:\Users\Amichai\Documents\My Dropbox\Share Folder\Literature\books\alexandre dumas\ten years later.txt");
			var ser = A.GetSeries();
			ser.GraphRankOrder(200);
		}




		//TODO: genetic algorithms
		//Construct text using a genetic algorithms
		///Use genetic algorithms to build your semantic network (propagation weights)
	}
}
