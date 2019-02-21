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
using PZ2.Models;
using PZ3.Xml;

namespace PZ4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public NetworkModel NetworkModel { get; set; }
        public CustomXmlRW CustomXmlRW { get; set; }
        public bool IsDataLoaded { get; set; }
        // Absolute mins and maxs given in xml
        private double _minimalPointX = 0.0;
        private double _minimalPointY = 0.0;
        private double _maximalPointX = 0.0;
        private double _maximalPointY = 0.0;
        private double _intervalX = 0.0;
        private double _intervalY = 0.0;
        // Assignment mins and maxs
        private double _mapLeft = 19.793909;
        private double _mapBottom = 45.2325;
        private double _mapTop = 45.277031;
        private double _mapRight = 19.894459;

        public MainWindow()
        {
            CustomXmlRW = new CustomXmlRW();
            IsDataLoaded = false;
            ReadXmlAndConvertAndFindMinimalsAndMaximalsTemplate();
            InitializeComponent();
        }

        public void FindMinimalAndMaximalPoints()
        {
            List<double> minimalXList = new List<double>(3);
            List<double> minimalYList = new List<double>(3);
            List<double> maximalXList = new List<double>(3);
            List<double> maximalYList = new List<double>(3);

            // Find minimal x's and y's for each type of grid element
            minimalXList.Add(NetworkModel.Substations.Min(s => s.X));
            minimalXList.Add(NetworkModel.Nodes.Min(n => n.X));
            minimalXList.Add(NetworkModel.Switches.Min(sw => sw.X));
            minimalYList.Add(NetworkModel.Substations.Min(s => s.Y));
            minimalYList.Add(NetworkModel.Nodes.Min(n => n.Y));
            minimalYList.Add(NetworkModel.Switches.Min(sw => sw.Y));

            // Find maximal x's and y's for each type of grid element
            maximalXList.Add(NetworkModel.Substations.Max(s => s.X));
            maximalXList.Add(NetworkModel.Nodes.Max(n => n.X));
            maximalXList.Add(NetworkModel.Switches.Max(sw => sw.X));
            maximalYList.Add(NetworkModel.Substations.Max(s => s.Y));
            maximalYList.Add(NetworkModel.Nodes.Max(n => n.Y));
            maximalYList.Add(NetworkModel.Switches.Max(sw => sw.Y));

            // Set up the referential minimal point
            _minimalPointX = minimalXList.Min();
            _minimalPointY = minimalYList.Min();

            // Set up the referential maximal point
            _maximalPointX = maximalXList.Max();
            _maximalPointY = maximalYList.Max();

            // Save intervals between minimal and maximal point on both axes
            _intervalX = _maximalPointX - _minimalPointX;
            _intervalY = _maximalPointY - _minimalPointY;
        }

        public async Task ReadXmlAndConvertAndFindMinimalsAndMaximalsTemplate()
        {
            Task readXmlTask = Task.Run(() =>
            {
                ReadXml();
            }).ContinueWith(antecedent =>
            {
                ConvertUTMToDecimal();
                IsDataLoaded = true;
            }).ContinueWith(antecedent =>
            {
                FindMinimalAndMaximalPoints();
            });
        }

        public void ReadXml()
        {
            NetworkModel = CustomXmlRW.DeSerializeObject<NetworkModel>(@"../../GridData/Geographic.xml");
        }

        public void ConvertUTMToDecimal()
        {
            foreach (var substation in NetworkModel.Substations)
            {
                UTMToLatLonConverter.ToLatLon(substation.X, substation.Y, 34, out double latitude, out double longitude);
                substation.X = longitude;
                substation.Y = latitude;
            }

            foreach (var node in NetworkModel.Nodes)
            {
                UTMToLatLonConverter.ToLatLon(node.X, node.Y, 34, out double latitude, out double longitude);
                node.X = longitude;
                node.Y = latitude;
            }

            foreach (var sw in NetworkModel.Switches)
            {
                UTMToLatLonConverter.ToLatLon(sw.X, sw.Y, 34, out double latitude, out double longitude);
                sw.X = longitude;
                sw.Y = latitude;
            }

            // Lines are omitted because we are using FirstEnd and SecondEnd properties that give us
            // beginning and the end of the lines
        }


        //public void ConvertLatLongToLocalCoordinates(double longX, double latY, out double x, out double y)
        //{
        //    // Move will store the percentage in decimal number that will with multiplication with the grid
        //    // give us the coordinates on the grid
        //    double moveX = (longX - _minimalPointX) / (_intervalX);
        //    double moveY = (latY - _minimalPointY) / (_intervalY);
        //    x = GridCanvas.Width * moveX;
        //    y = GridCanvas.Height * moveY;
        //}
    }
}
