using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cars.Interfaces;

namespace Laboratorium9.ViewModels
{
    internal class CarViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICar car;

        public CarViewModel(ICar _car)
        {
            this.car = _car;
        }
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Name
        {
            get => car.Name;
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public string Engine
        {
            get => car.Engine.ToString();
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Engine));
            }
        }

        public string Id
        {
            get => car.Id.ToString();
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Id));
            }
        }

        public string Transmission
        {
            get => car.Transmission.ToString();
            set
            {
                car.Name = value;
                RaisePropertyChanged(nameof(Transmission));
            }
        }
    }
}