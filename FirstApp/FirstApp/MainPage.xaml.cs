using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.I2c;
using Windows.Devices.Gpio;
using Rinsen.IoT.OneWire;
using System.Diagnostics;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FirstApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static List<DS18B20> temperatureSensors = new List<DS18B20>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Page_Loaded( object sender, RoutedEventArgs e )
        {

        }

        private void Timer_Tick( object sender, object e )
        {
            foreach ( var temperatureSensor in temperatureSensors )
            {
                lbDebug.Text += string.Format( "Address:{0} Temperature:{1} Time:{2}", temperatureSensor.OneWireAddressString, temperatureSensor.GetTemperature(), DateTime.Now );
                lbDebug.Text += "<Linebreak/>";
            }
        }

        private void Button_Click( object sender, RoutedEventArgs e )
        {

            Task.Run( () =>
            {
                var oneWireDeviceHandler = new OneWireDeviceHandler( true, false );
                var devices = oneWireDeviceHandler.GetDevices<DS18B20>();

                foreach ( var device in devices )
                {
                    temperatureSensors.Add( device );

                    var result = device.GetTemperature();

                    Debug.WriteLine( DateTime.Now + " " + device.OneWireAddressString +
                        ": " + result );
                }


                DispatcherTimer timer = new DispatcherTimer { Interval = new TimeSpan( 0, 0, 1 ) };
                timer.Tick += Timer_Tick;
            } );









        }
    }
}
