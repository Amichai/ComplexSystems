using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComplexSystems {
	public class SeriesTrialAnalysis {
		static public void CompareAListOfSeries(List<Signal> signals){
			int numberOfMomentsToTest = 8;
			List<Signal> momentVals = new List<Signal>(numberOfMomentsToTest);
			for(int k=0; k < numberOfMomentsToTest;k++){
				var A = new Signal(signals.Count());
				momentVals.Add(A);
			}
			for (int i = 0; i < signals.Count(); i++) {
				for(int j= 0; j < numberOfMomentsToTest; j++){
					momentVals[j].Add(signals[i].Moment(j+1));
				}
			}
			for (int i = 0; i < numberOfMomentsToTest ; i++) {
				momentVals[i].Graph();
			}
		}
	}
}
