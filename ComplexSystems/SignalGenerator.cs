using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using Common;

namespace ComplexSystems {
	public class SignalGenerator {
		public static Signal CityDevelopment(int startingCities, int iterations, double b = .1) {
			Random rand = new Random();
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

		public static Signal RandomNumber(int outputValues, int minVal, int maxVal) {
			Random rand = new Random();
			Signal vals = new Signal(outputValues);
			for (int i = 0; i < outputValues; i++) {
				vals.Add(rand.Next(minVal, maxVal) + rand.NextDouble());
			}
			return vals;
		}

		public static Signal GaussianDistribution(int outputValues, int minVal, int maxVal, int elementsToSum) {
			Signal vals = new Signal(outputValues);
			Random rand = new Random();
			for (int i = 0; i < outputValues; i++) {
				double sum = 0;
				for (int j = 0; j < elementsToSum; j++) {
					sum += rand.Next(minVal, maxVal) + rand.NextDouble();
				}
				vals.Add(sum);
			}
			return vals;
		}

		public static Signal IteratedRandomMultiplication(int iterations, int additiveConstant) {
			Signal newValues = new Signal(iterations);
			Random rand = new Random();
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
