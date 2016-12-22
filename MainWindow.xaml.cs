using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
           Loaded+= delegate { testmessage(); };
            
        }

        public async Task<bool> testmessage()
        {
          await  this.ShowMessageAsync("test Title", "test Message", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
          {
               AffirmativeButtonText="OK",
               ColorScheme = MetroDialogColorScheme.Theme
          });
            return true;
        } 
    }
}
