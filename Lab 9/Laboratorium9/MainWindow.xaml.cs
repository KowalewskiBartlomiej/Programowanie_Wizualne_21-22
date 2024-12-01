using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Cars.Interfaces;
using Laboratorium9.ViewModels;
using System.Collections.ObjectModel;

namespace Laboratorium9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IParking parking = new Cars.DB_1.Parking();
            lista.ItemsSource = new ObservableCollection<CarViewModel>();
            lista.DisplayMemberPath = "Name";
        }
    }
}
