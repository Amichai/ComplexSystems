using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace BindingTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		BindingSource bsA = new BindingSource(); // Airplanes
		BindingSource bsP = new BindingSource(); // Passengers

		private void Form1_Load(object sender, EventArgs e)
		{
			bsP.ListChanged += new ListChangedEventHandler(bsP_ListChanged);

			// Create some example data.
			Airplane a1, a2, a3;
			bsA.Add(a1 = new Airplane("Boeing 747", 800));
			bsA.Add(a2 = new Airplane("Airbus A380", 1023));
			bsA.Add(a3 = new Airplane("Cessna 162", 67));
			a1.Passengers.Add(new Passenger("Joe Shmuck"));
			a1.Passengers.Add(new Passenger("Jack B. Nimble"));
			a1.Passengers.Add(new Passenger("Jib Jab"));
			a2.Passengers.Add(new Passenger("Jackie Tyler"));
			a2.Passengers.Add(new Passenger("Jane Doe"));
			a3.Passengers.Add(new Passenger("John Smith"));

			// Set up data binding for the parent Airplanes
			grid.DataSource = bsA;
			grid.AutoGenerateColumns = true;
			txtModel.DataBindings.Add("Text", bsA, "Model");

			// Set up data binding for the child Passengers
			bsP.DataSource = bsA; // connect the two sources
			bsP.DataMember = "Passengers";
			lstPassengers.DataSource = bsP;
			lstPassengers.DisplayMember = "Name";
			txtName.DataBindings.Add("Text", bsP, "Name");

			// Allow the user to add rows
			((BindingList<Airplane>)bsA.List).AllowNew = true;
			((BindingList<Airplane>)bsA.List).AllowRemove = true;
		}

		void bsP_ListChanged(object sender, ListChangedEventArgs e)
		{
			if (e.ListChangedType == ListChangedType.Reset)
				txtName.Enabled = bsP.Current != null;
		}
	}
}

class Airplane
{
	public Airplane()
	{
		_id = ++lastID;
	}
	public Airplane(string model, int fuelKg)
	{
		_id = ++lastID; Model = model; _fuelKg = fuelKg;
	}
	private static int lastID = 0;
	public int _id;
	public int ID { get { return _id; } }
	public int _fuelKg;
	public int FuelLeftKg { get { return _fuelKg; } set { _fuelKg = value; } }
	public string _model;
	public string Model { get { return _model; } set { _model = value; } }
	public List<Passenger> _passengers = new List<Passenger>();
	public List<Passenger> Passengers { get { return _passengers; } }
}

class Passenger
{
	public Passenger(string name)
	{
		_id = ++lastID; Name = name;
	}
	private static int lastID = 0;
	public int _id;
	public int ID { get { return _id; } }
	public string _name;
	public string Name { get { return _name; } set { _name = value; } }
}
