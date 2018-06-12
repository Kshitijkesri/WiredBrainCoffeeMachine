using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using WiredBrainCoffee.EventHub.Sender;
using WiredBrainCoffee.EventHub.Sender.Model;

namespace WiredBrainCoffeeMachine.Simulator.ViewModel
{
    class MainViewModel:BindableBase
    {
        private int _CounterCapuccino;
        private int _CounterEspresso;
        private int _boilerTemp;
        private int _beanLevel;
        private bool _IssendingPeriodically;
        private string _City;
        private string _SerialNumber;
        private ICoffeeMachineDataSender _coffeeMachineDataSender;
        private DispatcherTimer _dispatcherTimer;
        public MainViewModel(ICoffeeMachineDataSender coffeeMachineDataSender)
        {
            _coffeeMachineDataSender = coffeeMachineDataSender;
            SerialNumber = Guid.NewGuid().ToString().Substring(0,8);
            MakeCappucinoCommand = new DelegateCommand(MakeCapuccino);
            MakeEspressoCommand = new DelegateCommand(MakeEspresso);
            Logs = new ObservableCollection<string>();
            _dispatcherTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(2)
            };
            _dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private async void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            var boilerTempData = CreateCoffeeMchineData(nameof(BoilerTemp),BoilerTemp);
            var beanLevelData = CreateCoffeeMchineData(nameof(BeanLevel), BeanLevel);
           await SendDataAsync(new[] { boilerTempData,beanLevelData });

        }

        public ICommand MakeCappucinoCommand { get; }
        public ICommand MakeEspressoCommand { get; }

        public ObservableCollection<string> Logs { get; }

        public string City
        {
            get { return _City; }
            set { _City = value;RaisePropertyChanged(); }
        }
        public string SerialNumber
        {
            get { return _SerialNumber; }
            set { _SerialNumber = value; RaisePropertyChanged(); }
        }

        public int CounterCapuccino
        {
            get { return _CounterCapuccino; }
            set { _CounterCapuccino = value; RaisePropertyChanged(); }
        }

        public int CounterEspresso
        {
            get { return _CounterEspresso; }
            set { _CounterEspresso = value; RaisePropertyChanged(); }
        }
        public int BoilerTemp
        {
            get { return _boilerTemp; }
            set { _boilerTemp = value; RaisePropertyChanged(); }
        }
        public int BeanLevel
        {
            get { return _beanLevel; }
            set { _beanLevel = value; RaisePropertyChanged(); }
        }
        public bool IssendingPeriodically
        {
            get { return _IssendingPeriodically; }
            set {
                if (_IssendingPeriodically != value)
                {
                    _IssendingPeriodically = value;
                    if (_IssendingPeriodically)
                    {
                        _dispatcherTimer.Start();
                    }
                    else
                    {
                        _dispatcherTimer.Stop();
                    }

                }


                   RaisePropertyChanged(); }
        }

        private async void MakeCapuccino()
        {
            CounterCapuccino++;
            CoffeeMachineData coffemachineData = CreateCoffeeMchineData(nameof(CounterCapuccino),CounterCapuccino);

           await SendDataAsync(coffemachineData);
        }


        private async void  MakeEspresso()
        {
            CounterEspresso++;
            CoffeeMachineData coffemachineData = CreateCoffeeMchineData(nameof(CounterEspresso), CounterEspresso);

           await SendDataAsync(coffemachineData);
        }
        private CoffeeMachineData CreateCoffeeMchineData(string sensorType, int sensorValue)
        {
           
            var coffemachineData = new CoffeeMachineData
            {
                city = City,
                SerialNumber = SerialNumber,
                SensorType = sensorType,
                SensorValue = sensorValue,
                RecordingTime = DateTime.Now
            };
            return coffemachineData;
        }

        private async Task SendDataAsync(CoffeeMachineData coffemachineData)
        {
            try
            {
                await _coffeeMachineDataSender.SendDataAsync(coffemachineData);
                WriteLog($"Sent Data:{coffemachineData}");
            }
            catch(Exception ex)
            {
                WriteLog($"Exception: {ex.Message}");
            }
        }
        private async Task SendDataAsync(IEnumerable<CoffeeMachineData> coffemachineDatas)
        {
            try
            {
                await _coffeeMachineDataSender.SendDataAsync(coffemachineDatas);
                foreach (var coffeemachinedata in coffemachineDatas)
                {
                    WriteLog($"Sent Data:{coffeemachinedata}");
                }
               
            }
            catch (Exception ex)
            {
                WriteLog($"Exception: {ex.Message}");
            }
        }

        private void WriteLog(string logMessage)
        {
            Logs.Insert(0,logMessage);
        }
    }
}
