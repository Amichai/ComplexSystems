using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Common;
using ComplexSystems;

namespace EconomicModels {
	public class Member {
		public Member(double a) {
			assets = a;
			memberId = Guid.NewGuid();
		}

		Guid memberId;

		private double assets { get; set; }

		public void AddAssets(double a) {
			assets += a;
		}

		public double AssetsToAddNextTurn = 0;

		public double GetAssets() {
			return assets;
		}


		public override bool Equals(object obj) {
			Guid g = ((Member)obj).memberId;
			return memberId.Equals(g);
		}

		public override int GetHashCode() {
			return memberId.GetHashCode();
		}

		Dictionary<Member, double> investors = new Dictionary<Member, double>();
		double totalInvestment = 0;

		public void TakeInvestment(Member e, double amount) {
			totalInvestment += amount;
			if (investors.ContainsKey(e)) {
				throw new Exception();
			} else {
				investors.Add(e, amount);
			}
		}

		public void UpdateAssets() {
			AddAssets(AssetsToAddNextTurn);
			AssetsToAddNextTurn = 0;
		}

		public void ClearAllInvestors() {
			totalInvestment = 0;
			investors = new Dictionary<Member, double>();
		}

		public void PayInvestors() {
			double interest = 1;
			///double interest = 7 / 50;
			double totalPaid = 0;
			double fullAssets = assets;
			for (int i = 0; i < investors.Count(); i++) {
				var a = investors.ElementAt(i);
				double amountToPay = (fullAssets * (a.Value / totalInvestment));
				//amountToPay *= interest;
				totalPaid += amountToPay;
				assets -= amountToPay;
				//a.Key.AddAssets(amountToPay)
				a.Key.AssetsToAddNextTurn += amountToPay;
			}
		}
	}

	public class Economy2 {
		List<Member> Members = new List<Member>();
		static Random rand = new Random();
		static public int iterationIdx= 0;
		int numberOfMembers;
		public Economy2(int members, int iterations) {
			numberOfMembers = members;
			for(int i=0;i < members; i++){
				//Members.Add(new Member(rand.Next(10,20)));
				Members.Add(new Member(15));
			}
			Debug.Print(members.ToString() + " members in this economy, iterating " + iterations.ToString() + " times.");
			PrintWealth();
			while (iterationIdx++ < iterations) {
				Debug.Print("Iteration IDX: " + iterationIdx.ToString());
				InvestMoney();
				PayInvesors();
				ClearInvestors();
				PrintWealth();
			}
		}

		public void ClearInvestors() {
			foreach (var a in Members) {
				a.ClearAllInvestors();
				a.UpdateAssets();
			}
		}

		public void InvestMoney() {
			int numberOfInvestments = numberOfMembers;
			//The index doing the investing:
			for(int i=0 ;i < numberOfMembers; i++){
				var investor = this.Members[i];
				List<double> partitions = new List<double>();
				//Generate N random numbers
				for (int j = 0; j < numberOfInvestments -1 ; j++) {
					partitions.Add(rand.NextDouble());
				}
				partitions.Add(1);
				//Sort the numbers
				partitions.Sort();

				//Each partition corresponds to the investment amount in order
				//The index getting the investment
				double totalPercentInvested = 0;
				double totalMoneyInvested = 0;
				for (int j = 0; j < numberOfInvestments; j++) {
					double percentToInvest = (partitions[j] - totalPercentInvested);
					var investee = this.Members[j];
					investee.TakeInvestment(investor, percentToInvest * investor.GetAssets());
					totalMoneyInvested += percentToInvest * investor.GetAssets() ;
					totalPercentInvested += percentToInvest;
				}
				//Debug.Print("Total percent invested: " + totalPercentInvested.ToString());
				//Debug.Print("Total monies invested: " + totalMoneyInvested.ToString());
			}
		}

		public void PayInvesors() {
			foreach (var a in Members) {
				a.PayInvestors();
				//Check that assets are now zero and if not throw an exception
			}
		}

		Signal cumulativeSignal = new Signal();

		public void PrintWealth() {
			double totalValue = 0;
			List<double> assets = new List<double>();
			for(int i=0; i < Members.Count(); i++){
				var a = Members[i].GetAssets();
				//Debug.Print(i.ToString() + ": " + Members[i].GetAssets().ToString());
				totalValue += a;
				assets.Add(a);
				//cumulativeSignal.Add(a);
			}
			//if (iterationIdx > 300) {
				var h = new Histogram(new Signal(assets), .3);
				//h.Graph();
				h.SaveImage("chart " + iterationIdx.ToString() + ".bmp");
				//cumulativeSignal.RankOrder().Graph();
			//}
			
			
			
			double average = assets.Average();
			double SD = assets.CalculateStdDev();
			Debug.Print("Total value: " + totalValue.ToString() +" average wealth: " + average.ToString() + " standard deviation: " + SD.ToString());
		}
	}
}
