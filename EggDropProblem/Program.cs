using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace EggDropProblem {
	class Program {
		static void Main(string[] args) {
			//hashedSolutions = (Dictionary<Tuple<int, int>, Solution>)SystemLog.OpenSavedObject();

			CurrentState state = new CurrentState(2, 10);
			Console.WriteLine("Worst case: " + (state.WorstCase().WorstCase.ToString()));
			for (int i = 0; i < state.GlobalDropHeights.Count(); i++) {
				Console.WriteLine(state.GlobalDropHeights[i].ToString());
			}

			//hashedSolutions.SerializeAndSave();
			Console.ReadLine();

		}

		//Make extension methods to automatically serialize and de serialize this (and any) data structure
		static int depthCounter = 0;
		public static Dictionary<Tuple<int, int>, Solution> hashedSolutions = new Dictionary<Tuple<int, int>, Solution>();
		public class CurrentState {
			int M, N;
			Tuple<int, int> state() {
				return new Tuple<int, int>(M, N);
			}
			public CurrentState(int m, int n) {
				this.M = m;
				this.N = n;
			}
			public List<int> GlobalDropHeights = new List<int>();
			public Solution WorstCase() {
				depthCounter++;
				List<int> localDropHeights = new List<int>();
				int worstCaseToReturn = int.MinValue;
				if (hashedSolutions.ContainsKey(state())) {
					var drop = hashedSolutions[state()];
					localDropHeights.AddRange(drop.DropHeights);
					worstCaseToReturn = drop.WorstCase;
				}
				if (N == 1) {
					worstCaseToReturn = 0;
				}
				if (N == 2) {
					worstCaseToReturn = 2;
					localDropHeights.Add(1);
				}
				if (M == 1) {
					worstCaseToReturn = N + 1;
					for (int i = 1; i < N + 1; i++)
						localDropHeights.Add(i);
				}
				if (worstCaseToReturn == int.MinValue) {
					int bestWorstCaseDrops = int.MaxValue;
					int bestWorstCaseFloor = 0;
					for (int dropFloor = 1; dropFloor < N / 2 + 1; dropFloor++) {
						var breakScenario = new CurrentState(M - 1, dropFloor).WorstCase();
						int breakScenarioTrials = breakScenario.WorstCase + 2;
						var surviveScenario = new CurrentState(M, N - dropFloor).WorstCase();
						int surviveScenarioTrials = breakScenario.WorstCase + 1;
						int temp = 0;
						if (breakScenarioTrials < surviveScenarioTrials) {
							temp = surviveScenarioTrials;
							localDropHeights.AddRange(surviveScenario.DropHeights);
						} else {
							temp = breakScenarioTrials;
							localDropHeights.AddRange(breakScenario.DropHeights);
						}

						if (temp < bestWorstCaseDrops) {
							bestWorstCaseDrops = temp;
							bestWorstCaseFloor = dropFloor;
						}
					}
					localDropHeights.Add(bestWorstCaseFloor);
					hashedSolutions.Add(state(), new Solution(bestWorstCaseDrops, localDropHeights));
					worstCaseToReturn = bestWorstCaseDrops;
				}
				if (depthCounter-- == 1)
					GlobalDropHeights.AddRange(localDropHeights);
				if (worstCaseToReturn == int.MinValue || worstCaseToReturn == int.MaxValue)
					throw new Exception();
				return new Solution(worstCaseToReturn, localDropHeights);
			}
		}
		public  class Solution {
			public Solution(int worstCase, List<int> heights) {
				this.WorstCase = worstCase;
				this.DropHeights = heights;
			}
			public int WorstCase;
			public List<int> DropHeights = new List<int>();
		}
	}
}
