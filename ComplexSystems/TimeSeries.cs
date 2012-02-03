using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComplexSystems {
	public class DataSeries {
		List<double> data = new List<double>();
		public void GraphRankOrder(int pointsToGraph){
			data.Sort();
			data.Reverse();
			Series ser = new Series();
			for (int i = 0; i < data.Count(); i++) {
				ser.Points.AddXY(i + 1, data[i]);
			}
			ser.Graph(pointsToGraph);
		}

		public void GraphLogRankOrder(int pointsToGraph) {
			data.Sort();
			data.Reverse();
			Series ser = new Series();
			for (int i = 0; i < data.Count(); i++) {
				ser.Points.AddXY(Math.Log10(i + 1), data[i]);
			}
			ser.Graph(pointsToGraph);
		}


		double MaxVal = double.MinValue;
		double MinVal = double.MaxValue;
		double Sum = 0;
		double sumSquared = 0;

		public DataSeries(){}

		public DataSeries(int count) {
			data = new List<double>(count);
		}

		public DataSeries Transform(Func<double, double> f) {
			DataSeries ser = new DataSeries(data.Count());
			for(int i=0; i < data.Count(); i++){
				ser.Add(f(data[i]));
			}
			return ser;
		}

		public void Add(double val) {
			if (val < MinVal)
				MinVal = val;
			if (val > MaxVal)
				MaxVal = val;
			Sum += val;
			sumSquared += val.Sqrd();
			data.Add(val);
		}

		public int Count() {
			return data.Count();
		}

		public double this[int i] {
			get { return data[i]; }
			set { data[i] = value;  }
		}

		public Histogram GetHistogram(double binSize) {
			return new Histogram(this, binSize);
		}

		public void Graph(int startIdx, int endIdx = 0) {
			Series ser = new Series();
			if (endIdx == 0)
				endIdx = data.Count();
			
			for(int i=startIdx; i < endIdx; i++){
				ser.Points.AddXY(i + 1, data[i]);
			}
			ser.Graph();
		}

		public void Graph() {
			Series ser = new Series();
			for (int i = 0; i < data.Count(); i++) {
				ser.Points.AddXY(i + 1, data[i]);
			}
			ser.Graph();
		}

		public double Mean() {
			return Sum / data.Count();
		}

		double variance = double.MinValue;

		public double Variance() {
			if (variance == double.MinValue) {
				variance =(sumSquared / data.Count()) - Mean().Sqrd();
			} return variance;
		}

		public DataSeries AutoCorrelation() {
			DataSeries ser = new DataSeries();
			for (int j = 0; j < data.Count() / 2; j++) {
				int numOftests = data.Count() - j;
				double sum = 0;
				var A = 1 / (numOftests * Variance().Sqrd());
				for (int i = 0; i < numOftests; i++) {
					sum += ((data[i] - Mean()) * (data[i + j] - Mean()));
				}
				ser.Add(A*sum);
			}
			return ser;
		}
	}
}
