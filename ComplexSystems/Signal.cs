using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Windows.Forms.DataVisualization.Charting;

namespace ComplexSystems {
	public class Signal {
		List<double> data = new List<double>();
		public Signal(params double[] seed) {
			this.data = seed.ToList();
			this.MaxVal = this.data.Max();
			this.MinVal = this.data.Min();
			this.Sum = this.data.Sum();
			this.sumSquared = this.Sum.Sqrd();
		}

		public Signal(Func<double, double> signal, double dt, double startTime, double endTime) {
			for (double t = startTime; t < endTime; t+= dt) {
				this.Add(signal(t));
			}
		}

		public override string ToString() {
			string output = string.Empty;
			for(int i=0;i < data.Count();i++){
				output += data[i].ToString() + " ";
			}
			return output;
		}

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

		public Signal(){}

		public Signal(int count) {
			data = new List<double>(count);
		}

		public Signal Transform(Func<double, double> f) {
			Signal ser = new Signal(data.Count());
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

		public Signal SignalAnalysisSignal() {
			int numberOfTests = 12;
			Signal newSignal = new Signal(numberOfTests -1);
			for (int i = 1; i < numberOfTests; i++)
				newSignal.Add(Moment(i));
			return newSignal;
		}

		public Histogram GetHistogram() {
			return new Histogram(this);
		}

		public Histogram GetHistogram(double binSize) {
			return new Histogram(this, binSize);
		}

		public Histogram GetHistogram(double binSize, bool cumulative) {
			return new Histogram(this, binSize, cumulative);
		}

		public double Moment(int degree) {
			Signal signal = new Signal(data.Count());
			double mean = Mean();
			for (int i = 0; i < data.Count(); i++) {
				signal.Add(Math.Pow(data[i] - mean, degree));
			}
			return signal.Mean() / (Math.Pow(StandardDeviation(), degree));
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

		public double StandardDeviation(){
			return Math.Sqrt(Variance());
		}

		public double Variance() {
			if (variance == double.MinValue) {
				variance =(sumSquared / data.Count()) - Mean().Sqrd();
			}
			if (variance < 0) {
				throw new Exception();
				//return 0;
			}
			return variance;
		}

		public Signal AutoCorrelation() {
			Signal ser = new Signal();
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

		public Signal RankOrder(int amount) {
			Signal sig = new Signal();
			data.Sort();
			data.Reverse();
			for (int i = 0; i < amount; i++) {
				sig.Add(data[i]);
			}
			return sig;
		}

		public Signal RankOrder() {
			return RankOrder(data.Count());
		}
	}
}
