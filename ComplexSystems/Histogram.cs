using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComplexSystems {
	public class Histogram  {
		Signal data;
		double binSize = 1;
		bool cumulative = false;
		List<int> positiveBins = new List<int>();
		List<int> negativeBins = new List<int>();
		public Histogram(Signal data, double binSize) {
			this.data = data;
			this.binSize = binSize;
			for (int i = 0; i < data.Count(); i++) {
				IncrementAt((int)Math.Floor(data[i] / binSize));
			}
		}

		public Histogram Normalize() {
			int totalPoints = positiveBins.Count() + negativeBins.Count();
			for (int i = 0; i < positiveBins.Count(); i++) {
				positiveBins[i] /= totalPoints;
			}
			for (int i = 0; i < negativeBins.Count(); i++) {
				negativeBins[i] /= totalPoints;
			}
			throw new NotImplementedException();
			//return this;
		}

		public Histogram(Signal data, double binSize, bool cumulative) {
			this.cumulative = cumulative;
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
			if (cumulative == false) {
				for (int i = 0; i < negativeBins.Count(); i++) {
					ser.Points.AddXY(-1 * (negativeBins.Count() - 1 - i), negativeBins[i]);
				} for (int j = 0; j < positiveBins.Count(); j++) {
					ser.Points.AddXY(j, positiveBins[j]);
				}
			} else {
				int sum = 0;
				for (int i = 0; i < negativeBins.Count(); i++) {
					sum += negativeBins[i];
					ser.Points.AddXY(-1 * (negativeBins.Count() - i), sum);
				} for (int j = 0; j < positiveBins.Count(); j++) {
					sum += positiveBins[j];
					ser.Points.AddXY(j, sum);
				}
			}


			//TODO: overload this method so that we have a control to manipulate parameters and redraw the control
			ser.Graph();
		}

		public double GetEntropy() {
			throw new NotImplementedException();
		}
		//Find the probability density function
		//assuming power law, and assuming exponential law
	}
}
