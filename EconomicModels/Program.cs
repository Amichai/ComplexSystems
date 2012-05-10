using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Diagnostics;
using Common.DataStructure;

namespace EconomicModels {
	class Program {
		static void Main(string[] args) {
			//labeledStorageTest();
			new Economy2(members:500, iterations:400);
		}

		static void graphTest() {

			Graph g = new Graph();
			for (int i = 0; i < 10000; i++) { }
				//g.RandomNode().InvestMoney().TakeProfitAndPayInvestors();
			g.FullyConnectedGraph(10);
			g.ToString().PrintToOutputWindow();
			//Distribute profit according to asset rank
			g.DistributeAssetsByRank(100);
			g.ToString().PrintToOutputWindow();
			//Redistribute profit based on investment connections
			g.PayInvestors();
			//Reinvest the net profit into new investments
		}

		static void labeledStorageTest() {
			LabeledStorage<string> testing = new LabeledStorage<string>();
			testing.Add("A", "letter", "uppercase", "a");
			testing.Add("C", "letter", "uppercase", "c");
			testing.Add("B", "letter", "uppercase", "b");
			testing.Add("b", "letter", "lowercase", "b");
			testing.Add("D", "letter", "uppercase", "d");
			testing.Add("e", "letter", "lowercase", "e");
			testing.Add("f", "letter", "lowercase", "f");
			testing.Add("F", "letter", "uppercase", "f");
			testing.Add("g", "letter", "lowercase", "g");


			testing["g"].PrintToOutputWindow();
			testing.LookupValue("uppercase").PrintToOutputWindow();
			//Debug.Print(string.Concat(testing.LookupValue("uppercase").Select(i => i.ToString() + "\n")));
		}
	}
}
