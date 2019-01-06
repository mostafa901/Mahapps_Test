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

namespace OrgSitu.Controls.TypicalControls
{
    /// <summary>
    /// Interaction logic for uc_Toast.xaml
    /// </summary>
    public partial class uc_Toast : UserControl
    {
        public uc_Toast()
        {
            InitializeComponent();
            Layout.DataContext = this;
        }

         #region Header
        public static readonly DependencyProperty Headerprop = DependencyProperty.Register(nameof(Header), typeof(string), typeof(uc_Toast));


        public string Header
        {
            get
            {
                return (string)GetValue(Headerprop);
            }
            set { SetValue(Headerprop, value); }

        }
        #endregion
    }
}
