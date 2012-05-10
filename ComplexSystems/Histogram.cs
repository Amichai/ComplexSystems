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
		List<double> positiveBins = new List<double>();
		List<double> negativeBins = new List<double>();
		public Histogram(Signal data, double binSize) {
			this.data = data;
			this.binSize = binSize;
			for (int i = 0; i < data.Count(); i++) {
				IncrementAt((int)Math.Floor(data[i] / binSize));
			}
		}

		/// <summary>Uses Scott's choice algorithm to assign bin width:
		/// http://en.wikipedia.org/wiki/Histogram#Number_of_bins_and_width</summary>
		public Histogram(Signal data) {
			this.data = data;
			var bS = (3.5 * data.StandardDeviation()) / Math.Pow(data.Count(), .33333333333333);
			if(bS <= 0)
				bS = .01;
			this.binSize = bS;
			for (int i = 0; i < data.Count(); i++) {
				IncrementAt((int)Math.Floor(data[i] / binSize));
			}
		}

		public Histogram Normalize() {
			for (int i = 0; i < positiveBins.Count(); i++) {
				positiveBins[i] /= totalNumberOfPoints;
			}
			for (int i = 0; i < negativeBins.Count(); i++) {
				negativeBins[i] /= totalNumberOfPoints;
			}
			return this;
		}

		public Histogram(Signal data, double binSize, bool cumulative) {
			this.cumulative = cumulative;
			this.data = data;
			this.binSize = binSize;
			for (int i = 0; i < data.Count(); i++) {
				IncrementAt((int)Math.Floor(data[i] / binSize));
			}
		}

		private int totalNumberOfPoints = 0;

		private void IncrementAt(int idx) {
			if (idx > 100000)
				throw new Exception();
			totalNumberOfPoints++;
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
		public void GraphLogLog(string label = "") {
			Series ser = new Series(label);
			double A;
			if (!cumulative) {
				for (int i = 0; i < negativeBins.Count(); i++) {
					A = -1 * i * binSize;
					if(A >0 && negativeBins[i] >0)
						ser.Points.AddXY(Math.Log(A), Math.Log(negativeBins[i]));
				} for (int j = 0; j < positiveBins.Count(); j++) {
					A = j* binSize;
					if (A > 0 && positiveBins[j] > 0)
					ser.Points.AddXY(Math.Log(A), Math.Log(positiveBins[j]));
				}
			}
			ser.ChartType = SeriesChartType.Point;
			//TODO: overload this method so that we have a control to manipulate parameters and redraw the control
			ser.Graph();

		}
		public void Graph(string label = "") {
			Series ser = new Series(label);
			if (!cumulative) {
				for (int i = 0; i < negativeBins.Count(); i++) {
				//for (int i = negativeBins.Count() - 1; i >= 0 ; i--) {
					ser.Points.AddXY(-1 * (/*negativeBins.Count() - 1 -*/ i) * binSize, negativeBins[i]);
				} for (int j = 0; j < positiveBins.Count(); j++) {
					ser.Points.AddXY(j * binSize, positiveBins[j]);
				}
			} else {
				double sum = 0;
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

		internal Signal GetSignal() {
			Signal signal = new Signal();
			for (int i = 0; i < negativeBins.Count(); i++)
				signal.Add(negativeBins[i]);
			for (int i = 0; i < positiveBins.Count(); i++)
				signal.Add(positiveBins[i]);
			return signal;
		}

		public void SaveImage(string filename) {
			Series ser = new Series();
			if (!cumulative) {
				for (int i = 0; i < negativeBins.Count(); i++) {
					//for (int i = negativeBins.Count() - 1; i >= 0 ; i--) {
					ser.Points.AddXY(-1 * (/*negativeBins.Count() - 1 -*/ i) * binSize, negativeBins[i]);
				} for (int j = 0; j < positiveBins.Count(); j++) {
					ser.Points.AddXY(j * binSize, positiveBins[j]);
				}
			} else {
				double sum = 0;
				for (int i = 0; i < negativeBins.Count(); i++) {
					sum += negativeBins[i];
					ser.Points.AddXY(-1 * (negativeBins.Count() - i), sum);
				} for (int j = 0; j < positiveBins.Count(); j++) {
					sum += positiveBins[j];
					ser.Points.AddXY(j, sum);
				}
			}

			ser.SaveImage(filename);
		}
	}
}
