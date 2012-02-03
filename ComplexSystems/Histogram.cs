using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComplexSystems {
	public class Histogram  {
		DataSeries data;
		double binSize = 1;
		bool comulative = false;
		List<int> positiveBins = new List<int>();
		List<int> negativeBins = new List<int>();
		public Histogram(DataSeries data, double binSize) {
			this.data = data;
			this.binSize = binSize;
			for (int i = 0; i < data.Count(); i++) {
				IncrementAt((int)Math.Floor(data[i] / binSize));
			}
		}

		private void IncrementAt(int idx) {
			if (idx > 0) {
				var dif = idx - positiveBins.Count();
				for (int i = 0; i <= dif; i++) {
					positiveBins.Add(0);
				}
				positiveBins[idx]++;
			} else {
				idx *= -1;
				var dif =idx - negativeBins.Count() ;
				for (int i = 0; i <= dif; i++) {
					negativeBins.Add(0);
				}
				negativeBins[idx]++;
			}
		}

		public void Graph() {
			Series ser = new Series();
			for (int i = 0; i < negativeBins.Count(); i++) {
				ser.Points.AddXY(-1 * (negativeBins.Count() - i), negativeBins[i]);
			} for (int j = 0; j < positiveBins.Count(); j++) {
				ser.Points.AddXY(j, positiveBins[j]);
			}
			//TODO: overload this method so that we have a control to manipulate parameters and redraw the control
			ser.Graph();
		}

		public void VarParamGraph() {


			throw new NotImplementedException();
		}

		public double GetEntropy() {
			throw new NotImplementedException();
		}
		//Find the probability density function
		//assuming power law, and assuming exponential law
	}
}
