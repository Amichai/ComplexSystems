using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Common;

namespace ComplexSystems {
	public class SignalGenerator {
		public static Signal CityDevelopment(int startingCities, int iterations, double b = .1) {
			Signal cities = new Signal();
			for (int i = 0; i < startingCities; i++) {
				cities.Add(1);
			}
			for (int i = 0; i < iterations - startingCities; i++) {
				if (rand.NextDouble() < b) {
					cities.Add(1);
				} else {
					var randNum = (double)rand.Next(0, i+startingCities);
					for (int j = 0; j < cities.Count(); j++) {
						randNum -= cities[j];
						if (randNum < 0) {
							cities[j]++;
							break;
						}
					}
				}
			}
			return cities;
		}
		/// <summary>P(x) = (mu - 1) /x^ mu</summary>
		public static Signal PowerLawDistribution(int iterations, int mu = 1) {
			Signal sig = new Signal();
			for (int i = 0; i < iterations; i++) {
				var a = 1 / rand.NextDouble();
				sig.Add(a);
			}
			return sig;
		}

		public static Signal ExponentialDistribution() {
			///P(x) = 1/x_0 exp(-x/x_0)  0 < x < infinity >
			/// Homework!
			throw new NotImplementedException();
		}

		/// <summary>Sigma is the standard deviation</summary>
		/// <param name="sigma">The standard deviation</param>
		public static Signal GaussianDistribution(int iterations, double sigma = 1) {
			//Sigma is the SD
			Signal sig = new Signal();
			for (int i = 0; i < iterations; i++) {
				double r1 = rand.NextDouble();
				double r2 = rand.NextDouble();
				double r = Math.Sqrt(-2 * Math.Log(r1)) * Math.Sin(2 * Math.PI* r2) * sigma;
				sig.Add(r);
			}
			return sig;
		}

		public static Signal PossionDistribution(int iterations, double lambda = 5) {
			Signal sig = new Signal();
			for (int i = 0; i < iterations; i++) {
				double p = Math.Exp(-lambda);
				double x = 1;
				int k = -1;
				while (x > p) {
					k++;
					x *= rand.NextDouble();
				}
				sig.Add(k);
			}
			return sig;
		}

		public static Signal RandomNumber(int outputValues, int minVal, int maxVal) {
			Signal vals = new Signal(outputValues);
			for (int i = 0; i < outputValues; i++) {
				vals.Add(rand.Next(minVal, maxVal) + rand.NextDouble());
			}
			return vals;
		}

		public static Signal GaussianDistribution(int outputValues, int minVal, int maxVal, int elementsToSum) {
			Signal vals = new Signal(outputValues);
			for (int i = 0; i < outputValues; i++) {
				double sum = 0;
				for (int j = 0; j < elementsToSum; j++) {
					sum += rand.Next(minVal, maxVal) + rand.NextDouble();
				}
				vals.Add(sum);
			}
			return vals;
		}

		static Random rand = new Random();
		public static Signal IteratedRandomMultiplication(int iterations, int additiveConstant) {
			Signal newValues = new Signal(iterations);
			double product = 1;
			for (int i = 0; i < iterations; i++) {
				product *= rand.NextDouble();
				product += (rand.NextDouble()* additiveConstant);
				newValues.Add(product);
			}
			return newValues;
		}

		public static Signal IterativeFunctionApplication(List<double> seedVals, int iterations) {
			Signal newValues = new Signal(seedVals.Count() + iterations);
			for (int i = 0; i < seedVals.Count(); i++) {
				newValues.Add(Math.Log10(seedVals[i]));
			}
			for (int i = 0; i < iterations; i++) {
				double first = Math.Pow(10,Math.Abs(newValues[i]));
				double second = Math.Abs(newValues[i + 1]);
				double third = Math.Abs(newValues[i + 2]);
				switch ((int)first % 4) {
					case 0:
						newValues.Add(Math.Log10(second + third));
						break;
					case 1:
						newValues.Add(Math.Log10(second * third));
						break;
					case 2:
						newValues.Add(Math.Log10(second / (third + 1)));
						break;
					case 3:
						newValues.Add(Math.Log10(Math.Abs(second - third)));
						break;
				}
			}
			return newValues;
		}
	}
}
