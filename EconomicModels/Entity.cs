using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;

namespace EconomicModels {
	class Entity {
		public double assets { get; set; }

		public void addAssets(double a) {
			assets += a;
		}

		public int lastRank { get; set; }

		private List<Tuple<Entity, double>> investors = new List<Tuple<Entity, double>>();

		public void payInvestors(double profit) {
			foreach (var a in investors) {
				a.Item1.addAssets(profit * a.Item2);
			}

		}
	}

	class Investment {
		public double cost { get; set; }
		public double share { get; set; }
	}

	class INode<T> {
		public INode(T content){
			this.node = content;
			this.nodeId = Guid.NewGuid();
		}
		T node;
		public Guid nodeId {get; set;}
	}
	class IEdge<T> {
		public IEdge(INode<T> startNode, INode<T> endNode) {
			this.startNode = startNode;
			this.endNode = endNode;
			this.edgeId = Guid.NewGuid();
		}
		INode<T> startNode;
		INode<T> endNode;
		Guid edgeId;
		double connectionStrength;
	}

	class Graph<T> where T : new() {
		//T is the structure that lives on the node.
		//Each node is characterized by a Guid. Guids are mapped to each other in a dictionary:
		Dictionary<Guid, List<Guid>> dictionary = new Dictionary<Guid, List<Guid>>();
		Dictionary<Guid, T> allNodes = new Dictionary<Guid, T>();

		DataTable table = new DataTable();

		public void GenerateCompleteGraph(int numberOfNodes)  {
			for (int i = 0; i < numberOfNodes; i++) {
				var id = Guid.NewGuid();
				T node = new T();
				dictionary.Add(id, new List<Guid>());
				allNodes.Add(id, node);
			}

			foreach (var a in allNodes.Keys) {
				foreach (var b in allNodes.Keys) {
					if (a != b) {
						dictionary[a].Add(b);
					}
				}
			}
		}
		
	}

	class Table {
		//Build a table with constant time lookup all around
		//This table can be used as a base class for building a graph
		//Many to many relationship
		//This should extend the current Labeled storage class
		DataTable table = new DataTable();
		Dictionary<Guid, List<Guid>> fastLookup = new Dictionary<Guid, List<Guid>>();
		TwoWayDictionary<object, Guid> allObjects = new TwoWayDictionary<object, Guid>();
		public void AddColumn(string title, Type type){
			table.Columns.Add(title, type);
		}
		public void AddRow(params object[] values){
			table.Rows.Add(values);
			foreach(var a in values){
				allObjects.Add(a, Guid.NewGuid());
			}
		}
	}

	class Economy {
		Graph<Entity> economy = new Graph<Entity>();

		

		double totalValue = 10000;
		List<Entity> entities = new List<Entity>();

		private void iterate() {
			//what is the data structure we need to use to hold all entities and have them sorted by rank and their differences sorted by rank

			entities = entities.OrderBy(i => i.assets).ToList();
			List<double> diffBetweenEntities = new List<double>();
			for (int i = 0; i < entities.Count() - 1; i++) {
				diffBetweenEntities.Add(entities[i].assets - entities[i + 1].assets);
			}
			for (int i = 0; i < entities.Count(); i++) {
				var a = entities[i];
				var profitFromRank = totalValue * Math.Pow(.5, i); //Represents company profit for services provided to others
				//Pay out to investors:
				if (i > a.lastRank) {
					//Pay out to investors who made this possible 
					a.payInvestors(profitFromRank);
				}
			}
			//Invest in the largest difference between entities which is less than the amount of money that you have to invest
			//and invest all your money there

			for (int i = 0; i < entities.Count(); i++) {


				//Make new investments
				///This depends on how much money you have
				///Who's rank you can raise
			}
		}
	}
}
