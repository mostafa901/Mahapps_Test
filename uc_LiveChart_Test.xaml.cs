using LiveCharts.Core.DataSeries;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Utility.IO;
using Utility.MVVM;

namespace IconPacks_Vs_mahapps
{
    /// <summary>
    /// Interaction logic for uc_LiveChart_Test.xaml
    /// </summary>
    public partial class uc_LiveChart_Test : UserControl
    {
        mv_Chart mv;
        public uc_LiveChart_Test()
        {
            InitializeComponent();
            mv = new mv_Chart();
            DataContext = mv;

          

        }
    }

    public class mv_Chart:BaseVModel
    {
        public mv_Chart()
        {
            ObservableCollection<item> values = new ObservableCollection<item>();

            // add some values...
            values.Add(new item { Value = 4,Name="A" });
            values.Add(new item { Value = 7,Name = "B" });
            values.Add(new item { Value = 0, Name = "C" });
            values.Add(new item { Value = 5,Name="D" });

            var barSeries = new BarSeries<double>();

            barSeries.Values = values.Select(o=>o.Value);

            // a custom fill and stroke, if we don't set these properties
            // LiveCharts will set them for us according to our theme.
            barSeries.StrokeThickness = 3f;
            barSeries.Stroke = LiveCharts.Core.Drawing.Brushes.Purple;
            barSeries.Fill = LiveCharts.Core.Drawing.Brushes.MediumPurple;
            barSeries.DelayRule = LiveCharts.Core.Animations.DelayRules.LeftToRight;
            

            // limit the column width to 65.
            barSeries.MaxColumnWidth = 65f;
            
            // do not display a label for every point
            barSeries.DataLabels = true;
            barSeries.DataLabelFormatter = x => x.X.ToString() ;
            
            // create a collection to store our series.
            // you can use any IEnumerable, it is recommended to use
            // the ChartingCollection<T> class, it inherits from
            // ObservableCollection and adds the AddRange/RemoveRange methods
           //----> SeriesItems = new ObservableCollection<ISeries>();
            // finally lets add the series to our series collection,
            // we can add as many series as we need, in this case we will
            // only display one series.
            SeriesItems.Add(barSeries);

        

             X_Axis.Add(new LiveCharts.Core.Dimensions.Axis()
                     {
                          Title = "Names",
                          Labels = values.Select(o => o.Name).ToList(),
                            ShowLabels = true
                          
             });
                
        }





        #region X_Axis

        private LiveCharts.Core.Dimensions.PlaneCollection _X_Axis;

        public LiveCharts.Core.Dimensions.PlaneCollection X_Axis
        {
            get
            {
                if (_X_Axis == null) _X_Axis = new LiveCharts.Core.Dimensions.PlaneCollection();
                return _X_Axis;
            }
            set { SetProperty(ref _X_Axis, value); }

        }
        #endregion




        public class item:BaseDataObject
        {

            #region Value

            private double _Value;

            public double Value
            {
                get
                {
                    return _Value;
                }
                set { SetProperty(ref _Value, value); }

            }
            #endregion

        }

        #region SeriesItems

        private ObservableCollection<ISeries> _SeriesItems;

        public ObservableCollection<ISeries> SeriesItems
        {
            get
            {
                if (_SeriesItems == null) _SeriesItems = new ObservableCollection<ISeries>();
                return _SeriesItems;
            }
            set { SetProperty(ref _SeriesItems, value); }

        }
        #endregion

    }
}
