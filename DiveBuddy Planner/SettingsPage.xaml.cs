using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Xml.Serialization;
using System.IO.IsolatedStorage;
using System.IO;

namespace DiveBuddy_Planner
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private string settingsFileName = "Settings.xml";
        private Settings settings;
        private bool PageLoaded = false;

        public SettingsPage()
        {
            InitializeComponent();

            for (double i = 0.1; i < 1.0; i += 0.1)
			{
			    this.HighGradient.Items.Add(Math.Round(i, 1).ToString());
                this.LowGradient.Items.Add(Math.Round(i, 1).ToString());

                this.TimeForGasSwitch.Items.Add(((i - 0.1) * 10).ToString());
                this.MinDecoDepth.Items.Add(((i - 0.1) * 10).ToString());
			}

            this.settings = SerializerHelper.DeSeralize<Settings>(settingsFileName);
            if (this.settings == null)
            {
                this.settings = new Settings();
            }

            if (this.settings.Algoritm == Settings.AlgoritmType.ZH16B)
            {
                this.Algoritm.SelectedIndex = 0;
            }
            else
            {
                this.Algoritm.SelectedIndex = 1;
            }

            this.HighGradient.SelectedIndex = (int)(this.settings.HighGraident * 10.0) - 1;
            this.LowGradient.SelectedIndex = (int)(this.settings.LowGraident * 10.0) - 1;

            this.TimeForGasSwitch.SelectedIndex = (int)(this.settings.TimeToSwitchGas);
            this.MinDecoDepth.SelectedIndex = (int)(this.settings.MiniDepthForDeco);

            Loaded += new RoutedEventHandler(SettingsPage_Loaded);
        }

        void SettingsPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.PageLoaded = true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            SerializerHelper.Serialize<Settings>(settingsFileName, this.settings);
            this.NavigationService.GoBack();
        }

        private void AlgortimSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageLoaded)
            {
                if (string.Compare(e.AddedItems[0].ToString(), "ZH16B", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    this.settings.Algoritm = Settings.AlgoritmType.ZH16B;
                }
                else
                {
                    this.settings.Algoritm = Settings.AlgoritmType.ZH17B;
                }
            }
        }

        private void HighGradientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageLoaded)
            {
                this.settings.HighGraident = double.Parse(e.AddedItems[0].ToString());
            }

        }

        private void LowGradientSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageLoaded)
            {
                this.settings.LowGraident = double.Parse(e.AddedItems[0].ToString());
            }
        }

        private void TimeForGasSwitchSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageLoaded)
            {
                this.settings.TimeToSwitchGas = double.Parse(e.AddedItems[0].ToString());
            }
        }

        private void MinDecoDepthSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageLoaded)
            {
                this.settings.MiniDepthForDeco = double.Parse(e.AddedItems[0].ToString());
            }
        }  
    }
}