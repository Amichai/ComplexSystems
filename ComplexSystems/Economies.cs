﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Common;

namespace ComplexSystems {

	class Economy {
		Random rand = new Random();
		const int numberOfJobs = 20;
		const int numberOfPeople = 30;
		List<Person>[] jobs = new List<Person>[numberOfJobs];
		private void randomInitialize() {
			for (int i = 0; i < numberOfPeople; i++) {
				var idx = rand.Next(numberOfJobs);
				jobs[idx].Add(new Person(rand.NextDouble()));
			}
			for (int i = 0; i < numberOfJobs; i++) {
				salaries.Add(rand.NextDouble());
			}
		}
		public Economy() {
			randomInitialize();

		}

		private void printJobMarket() {
			for(int i=0; i < jobs.Count(); i++){
				for(int j=0; j < jobs[i].Count(); j++){
					Console.WriteLine("Job idx: " + i.ToString() + "\n");
					Console.WriteLine("Person number: " + jobs[i][j].ToString());
					Console.WriteLine(" assets: " + jobs[i][j].getAssets().ToString() + "\n");
				}
			}
		}

		private void findNewJobs() {
			for (int i = 0; i < jobs.Count(); i++) {
				for (int j = 1; j < jobs[i].Count(); j++) {
					//Get the highest paying job where you can beat out the last winner
					//else go to welfare
				}
			}
		}
		class Person{
			public Person(double assets) {
				this.assets = assets;
			}

			public double assets { get; set; }

			internal void award(double assetsToAdd) {
				this.assets += assetsToAdd;
			}

			internal double getAssets() {
				return this.assets;
			}
		}
		List<double> salaries = new List<double>();

		private void awardSalaries() {
			for (int j = 0; j < numberOfJobs; j++) {
				var idx = jobs[j].MaxIndex();
				jobs[j][idx].award(salaries[j]);
			}
		}
	}
	
	class Economies {
		double totalAssets = 0;
		Random rand = new Random();
		Dictionary<entity, List<entity>> socialGraph = new Dictionary<entity, List<entity>>();



		public Economies() {
			RandomInitialize(numberOfEntities:30);
			Iterate(300);
		}

		public void Iterate(int iterations) {
			//RedistributeAssets();
			//pay the dependencies correctly
			int numberOfEntities = socialGraph.Count();
			for(int i=0; i < iterations;i++){
				int idx = i % numberOfEntities;
				double profit = Math.Pow(socialGraph.ElementAt(idx).Key.RelativeVal(totalAssets), 3);
				//pay out to dependencies
				double totalDependencies = 0;
				for (int j = 0; j < socialGraph.ElementAt(idx).Value.Count(); j++) {
					totalDependencies += socialGraph.ElementAt(idx).Value[j].GetAssets();
				}
				for (int j = 0; j < socialGraph.ElementAt(idx).Value.Count(); j++) {
					var A = socialGraph.ElementAt(idx).Value[j];
					A.SetAssets(socialGraph.ElementAt(j).Key.RelativeVal(totalDependencies));
				}
				var B =  socialGraph.ElementAt(idx).Key;
				Debug.Print("Element " + idx.ToString() + " Assets: " +B.GetAssets().ToString()
					 + " Change: " + B.GetChange().ToString());
			}
		}

		private void RedistributeAssets() {
			double total = 0;
			foreach (var a in socialGraph.AsEnumerable()) {
				total += Math.Pow(a.Key.RelativeVal(totalAssets), 3);
			}
			//Increase the assets of the dependencies according to this profit
			foreach (var a in socialGraph.AsEnumerable()) {
				double profit = Math.Pow(a.Key.RelativeVal(totalAssets), 3) / total * totalAssets;
				Debug.Print(profit.ToString());
				a.Key.SetAssets(profit);
			}
			Debug.Print("\n");
		}

		class entity{
			double lastVal = 0;
			double change = 0;
			public entity(double Assets) {
				this.assets = Assets;
			}
			//represents value that this individual brings to the economy
			double assets = 0;
			public double RelativeVal(double totalAssets) {
				return assets / totalAssets;
			}

			public void SetAssets(double assets) {
				this.lastVal = this.assets;
				this.assets = assets;
				this.change = lastVal - assets;
			}

			internal double GetAssets() {
				return assets;
			}

			public double GetChange() {
				return change;
			}
		}

		public void RandomInitialize(int numberOfEntities) {
			//random initialize dependencies as well
			for (int i = 0; i < numberOfEntities; i++) {
				double assets = rand.NextDouble();
				socialGraph.Add(
					new entity(assets), new List<entity>()
				);
				totalAssets += assets;
			}
			Debug.Print("Total assets: " + totalAssets.ToString());
			foreach(var i in socialGraph){
				int numOfDependencies = rand.Next(0, numberOfEntities);
				for (int j = 0; j < numOfDependencies; j++) {
					int dependencyIdx = 0;
					entity entityToAdd = null;
					var addedIndicies = new HashSet<int>();
					do{
						dependencyIdx = rand.Next(0, numberOfEntities);
						entityToAdd = socialGraph.ElementAt(dependencyIdx).Key;
					} while(addedIndicies.Contains(dependencyIdx));
					socialGraph.ElementAt(j).Value.Add(entityToAdd);
				}
			}
		}
	}
}
