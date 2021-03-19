using IconPacks_Vs_mahapps;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Test_Repo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = mv = new ViewModel(DialogCoordinator.Instance);
        }

        public ViewModel mv { get; private set; }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            mv.StartNewThread();
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
            set { _Items = value; Notify(nameof(Items)); }
        }

        #endregion Items

        public void Notify(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }

    public class ViewModel
    {
        private IDialogCoordinator _dilaogCoordinator;

        public ViewModel(IDialogCoordinator instance)
        {
            this._dilaogCoordinator = instance;
        }

        async public Task StartNewThread()
        {
            Dispatcher externaldisp = null;
            var thread = new Thread(new ThreadStart(() =>
            {
                var win = new ThreadWindow();
                externaldisp = win.Dispatcher;
                win.ShowDialog();
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();

            await Task.Delay(1000); //give time to open the above window

            //the below statement works fine if the above thread did not start
            //Failed
            await externaldisp.BeginInvoke(DispatcherPriority.Background, new Action(async () => await _dilaogCoordinator.ShowMessageAsync(this, "Test", "Message")));

            //Failed
            //await instance.ShowMessageAsync(this, "Test", "Message");

            //test before branch1
        }
    }
}