using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace PZ2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Gmap_Load(object sender, EventArgs e)
        {
            // If there is some problem with map loading, use this code
            // if it's not shown even with this, that means we messed up something
            //GMapProvider.WebProxy = WebRequest.GetSystemWebProxy();
            //GMapProvider.WebProxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            gmap.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            gmap.MinZoom = 2;
            gmap.MaxZoom = 17;
            // Whole world zoom
            gmap.Zoom = 12;
            // Lets the map use the mousewheel to zoom
            gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            // Lets the user drag the map
            gmap.CanDragMap = true;
            // Lets the user drag the map with the left mouse button
            gmap.DragButton = (System.Windows.Forms.MouseButtons)MouseButton.Left;
            // Centers the map on Novi Sad
            double blX = 45.2325;
            double blY = 19.793909;
            double trX = 45.277031;
            double trY = 19.894459;
            gmap.Position = new PointLatLng((blX + trX) / 2, (blY + trY) / 2);
        }
    }
}
