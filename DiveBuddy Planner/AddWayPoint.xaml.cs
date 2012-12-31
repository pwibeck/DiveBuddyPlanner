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
using DiveLibrary;
using System.Collections.ObjectModel;

namespace DiveBuddy_Planner
{
    public partial class AddLevel : PhoneApplicationPage
    {
        bool enabled = false;
        private int depth, time, oxygen;

        public AddLevel()
        {
            InitializeComponent();

            enabled = true;
            this.depth = (int)SelectedDepth.Value;
            this.time = (int)SelectedTime.Value;
            this.oxygen = (int)SelectedOxygen.Value;
        }

        private void time_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (enabled)
            {
                this.timeView.Text = (int)Math.Round(e.NewValue) + " min";
                this.time = (int)Math.Round(e.NewValue);
            }
        }

        private void depth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (enabled)
            {
                this.depthView.Text = (int)Math.Round(e.NewValue) + " m";
                this.depth = (int)Math.Round(e.NewValue);
            }
        }

        private void oxygen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {   
            if (enabled)
            {
                this.oxygenView.Text = "Oxygen" + (int)Math.Round(e.NewValue) + " %";
                this.oxygen = (int)Math.Round(e.NewValue);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            WayPoint wayPoint = new WayPoint(this.depth, this.time, new Gas((double)this.oxygen /100.0, 1.0 - ((double)this.oxygen / 100.0), 0));
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            if (data == null)
            {
                data = new DivePlanData();
            }

            data.WayPoints.Add(wayPoint);

            SerializerHelper.Serialize<DivePlanData>("DivePlanData.xml", data);
            this.NavigationService.GoBack();
        }
    }
}