using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace ComplexSystems {
	class Program {
		static void Main(string[] args) {
			List<double> list = new List<double>(){.10,2,3,1,8,.8};
			//var ser = DataSeriesGenerator.IterativeFunctionApplication(list, 10000);
			//var ser = DataSeriesGenerator.GaussianDistribution(1000, 5, 20, 25);
			//var ser = DataSeriesGenerator.RandomNumber(1000, 0, 10);

			var A = new TextBlock(@"C:\Users\Amichai\Documents\My Dropbox\Share Folder\Literature\books\alexandre dumas\ten years later.txt");
			var ser = A.GetSeries();
			ser.GraphRankOrder(200);
		}
	}
}
