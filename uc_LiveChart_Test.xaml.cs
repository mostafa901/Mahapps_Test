using LiveCharts.Core.Drawing;
using LiveCharts.Core.Coordinates;
using LiveCharts.Core.DataSeries;
using LiveCharts.Core.Interaction.Series;
using LiveCharts.Wpf;
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
            LiveCharts.Core.Charting.Settings.LearnType<mv_Chart.item, PointCoordinate>(
                             (player, index) => new PointCoordinate(index, (float)player.Value));
            


            LiveCharts.Core.Charting.GetCurrentMapperFor<mv_Chart.item,PointCoordinate>().When(it => true, (s, e) =>
            {
                var br = LiveCharts.Core.Drawing.Brushes.Red;
                var sh = (System.Windows.Shapes.Shape)e.Shape;
               
                sh.Stroke = br.AsWpf();
            });


        }
    }

    public class mv_Chart:BaseVModel
    {
        public mv_Chart()
        {
            SQLite.SQLiteConnection sql = new SQLite.SQLiteConnection(@"D:\Revit_API\GRAPHICS\SitDoc\Sample\Rac_Basic Building Sample.db");


            ObservableCollection<item> values = new ObservableCollection<item>();
            ObservableCollection<item> values1 = new ObservableCollection<item>();

            // add some values...
            values.Add(new item { Value = 4,Name="A" });
            values.Add(new item { Value = 7,Name = "B" });
            values.Add(new item { Value = 0, Name = "C" });
            values.Add(new item { Value = 5,Name="D" });

            values1.Add(new item { Value = 1, Name = "x" });
            values1.Add(new item { Value = 3, Name = "y" });
            values1.Add(new item { Value = 6, Name = "z" });
            values1.Add(new item { Value = 15, Name = "l" });

            var barSeries = new BarSeries<item>();
            var barSeries1 = new LineSeries<item>();

            barSeries.Values = values;
            barSeries1.Values = values1;

            // a custom fill and stroke, if we don't set these properties
            // LiveCharts will set them for us according to our theme.
            barSeries.StrokeThickness = 0f;
            barSeries.Stroke = LiveCharts.Core.Drawing.Brushes.Purple;
            barSeries.Fill = LiveCharts.Core.Drawing.Brushes.MediumPurple;
            barSeries.DelayRule = LiveCharts.Core.Animations.DelayRules.LeftToRight;

              

            // limit the column width to 65.
            barSeries.MaxColumnWidth = 15f;
            // do not display a label for every point
            barSeries.DataLabels = true;
            barSeries.DataLabelFormatter = x => x.X.ToString() ;
            barSeries.DataLabelsForeground = LiveCharts.Core.Drawing.Brushes.White;
            
            // create a collection to store our series.
            // you can use any IEnumerable, it is recommended to use
            // the ChartingCollection<T> class, it inherits from
            // ObservableCollection and adds the AddRange/RemoveRange methods
           //----> SeriesItems = new ObservableCollection<ISeries>();
            // finally lets add the series to our series collection,
            // we can add as many series as we need, in this case we will
            // only display one series.
            SeriesItems.Add(barSeries);
            SeriesItems.Add(barSeries1);

            var ax1 = new LiveCharts.Core.Dimensions.Axis()
            {
                Title = "Names",
                Labels = values.Select(o => o.Name).ToList(),
                ShowLabels = true,
                LabelsForeground = LiveCharts.Core.Drawing.Brushes.White,
                XSeparatorStyle = new LiveCharts.Core.Drawing.Styles.ShapeStyle(LiveCharts.Core.Drawing.Brushes.DimGray, null, 1, new float[] { 2, 4 }),
                Sections = new List<LiveCharts.Core.Dimensions.Section>()
                {
                     new LiveCharts.Core.Dimensions.Section()
                     {
                          Fill = LiveCharts.Core.Drawing.Brushes.DimGray,
                          LabelContent = "Testing Label",
                          Value=0,
                          Length=1,
                          
                     },
                       new LiveCharts.Core.Dimensions.Section()
                     {
                          Fill = LiveCharts.Core.Drawing.Brushes.RosyBrown,
                          LabelContent = "Testing Label1",
                          LabelVerticalAlignment = LiveCharts.Core.Drawing.Styles.VerticalAlignment.Centered,
                           StrokeThickness=2,
                          Value=2,
                          Length = 1
                     }
                }


            };

            var ax2 = new LiveCharts.Core.Dimensions.Axis()
            {
                Title = "Names",
                Labels = values1.Select(o => o.Name).ToList(),
                ShowLabels = true,
                LabelsForeground = LiveCharts.Core.Drawing.Brushes.White,
            
                Sections = new List<LiveCharts.Core.Dimensions.Section>(),
                Position = LiveCharts.Core.Dimensions.AxisPosition.Top


            };
      

            X_Axis.Add(ax1);

            X_Axis.Add(ax2);

            Y_Axis.Add(new LiveCharts.Core.Dimensions.Plane()
            {
                 Sections = new List<LiveCharts.Core.Dimensions.Section>()

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

        #region Y_Axis

        private LiveCharts.Core.Dimensions.PlaneCollection _Y_Axis;

        public LiveCharts.Core.Dimensions.PlaneCollection Y_Axis
        {
            get
            {
                if (_Y_Axis == null) _Y_Axis = new LiveCharts.Core.Dimensions.PlaneCollection();
                return _Y_Axis;
            }
            set { SetProperty(ref _Y_Axis, value); }

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
