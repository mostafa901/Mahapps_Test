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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace MetroWin_Lib
{
    /// <summary>
    /// Interaction logic for Class_Windows.xaml
    /// </summary>
    public partial class Class_Windows : MetroWindow
    {
        public Class_Windows()
        {
            InitializeComponent();
            Loaded += delegate { testmessage(); };

        }

        public async Task<bool> testmessage()
        {
            await this.ShowMessageAsync("test Title", "test Message", MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings()
            {
                AffirmativeButtonText = "OK",
                ColorScheme = MetroDialogColorScheme.Theme
            });
            return true;
        }
    }
}
