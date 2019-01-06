using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IconPacks_Vs_mahapps
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            var uc_base = new uc_BaseControl();
            // MainGrid.Children.Add(uc_base);
            // Grid.SetRow(uc_base, 1);

            Content = uc_base;
            PreviewMouseDoubleClick+= delegate
            {
               uc_base. flout.IsOpen = !uc_base.flout.IsOpen;
            };



            DataContext = new mc_test(); ;

            
          
        }

        

       async private void Test_Click(object sender, RoutedEventArgs e)
        {
           var prg =  await this.ShowProgressAsync($"Window Size Mode is:  {this.SizeToContent.ToString()}", "", true);
            prg.Canceled += async delegate
            {
                await prg.CloseAsync();
            };
        }
    }

    public class mc_test : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        public mc_test()
        {
            Items = new ObservableCollection<string>();
            Items.Add("123");
            Items.Add("ddas");
            Items.Add("asda");
            Items.Add("12adas3");
        }
        #region Items

        private ObservableCollection<string> _Items;

        public ObservableCollection<string> Items
        {
            get
            {
                return _Items;
            }
            set {  _Items= value;Notify(nameof(Items)); }

        }
        #endregion


        public void Notify(string prop)
        {
            if (PropertyChanged != null)
            {


                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
