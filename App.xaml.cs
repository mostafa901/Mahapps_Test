using LiveCharts.Core;
using LiveCharts.Core.Coordinates;
using LiveCharts.Core.Interaction.Series;
using LiveCharts.Core.Themes;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IconPacks_Vs_mahapps
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Charting.Configure(charting =>
            {

                charting
                    .LearnPrimitiveAndDefaultTypes()
                    .SetTheme(Themes.MaterialDesign)
                    .UsingWpf()
#if GEARED
                    .UsingWpfGeared()
#endif
                    ;

                

                   
                 

            });
        }
    }
}
