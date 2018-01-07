using GalaSoft.MvvmLight;
using Shared.BaseModels;
using Shared.Interfaces;
using Simulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace CodingDojo3Horak.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        //Variablen Datum und Zeit
        private string date = DateTime.Now.ToLocalTime().ToShortDateString();
        private string time = DateTime.Now.ToLocalTime().ToLongTimeString();
        
        //Variablen Listen
        private List<ItemVm> modelItems = new List<ItemVm>();
        public ObservableCollection<ItemVm> SensorList { get; set; }
        public ObservableCollection<ItemVm> ActorList { get; set; }

        //Variablen Mode Listen
        public ObservableCollection<string> ModeSelectionList { get; private set; }
        public ObservableCollection<string> SensorModeSelectionList { get; private set; }
        public ObservableCollection<string> ActorModeSelectionList { get; private set; }

        //Property Datum
        public string Date
        {
            get { return date; }
            set { date = value; RaisePropertyChanged(); }
        }

        //Property Zeit
        public string Time
        {
            get { return time; }
            set { time = value; RaisePropertyChanged(); }
        }


        public MainViewModel()
        {
            //Zeit und Datumsaktualisierung
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += UpdateTime; //Hängt neue Methode an Timer

            //Timer starten für Zeit und Datum
            timer.Start();

            SensorList = new ObservableCollection<ItemVm>();
            ActorList = new ObservableCollection<ItemVm>();
            ModeSelectionList = new ObservableCollection<string>();
            SensorModeSelectionList = new ObservableCollection<string>();
            ActorModeSelectionList = new ObservableCollection<string>();

            //ModeType - Actor
            //SensorModeType - Sensor
            foreach (var item in Enum.GetNames(typeof(ModeType)))
            {
                ActorModeSelectionList.Add(item);
                ModeSelectionList.Add(item);
            }

            foreach (var item in Enum.GetNames(typeof(SensorModeType)))
            {
                SensorModeSelectionList.Add(item);
                ModeSelectionList.Add(item);
            }


            LoadData();



        }

        //Methode Zeit und Datum Update
        public void UpdateTime(object sender, EventArgs e)
        {
            Time = DateTime.Now.ToLocalTime().ToLongTimeString();
            Date = DateTime.Now.ToLocalTime().ToShortDateString();
        }

        public void LoadData()
        {
            Simulator sim = new Simulator(modelItems);

            foreach(var item in sim.Items)
            {
                if (item.ItemType.Equals(typeof(ISensor)))
                    SensorList.Add(item);
                else if (item.ItemType.Equals(typeof(IActuator)))
                    ActorList.Add(item);
            }
        }
        //Testing Commit
    }
}