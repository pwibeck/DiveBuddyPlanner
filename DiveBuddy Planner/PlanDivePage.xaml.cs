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

namespace DiveBuddy_Planner
{
    public partial class PlanDivePage : PhoneApplicationPage
    {
        public PlanDivePage()
        {
            InitializeComponent();
        }

        private void AddWayPointButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddWayPoint.xaml", UriKind.Relative));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            if (data == null)
            {
                data = new DivePlanData();
            }

            this.WayPoints.Children.Clear();
            foreach (WayPoint wayPoint in data.WayPoints)
            {
                TextBlock txt = new TextBlock();
                txt.Text = wayPoint.ToString();                
                this.WayPoints.Children.Add(txt);                
            }

            this.DecoGas.Children.Clear();
            foreach (Gas gas in data.DecoGas)
            {
                var decoDepth = DiveLibrary.Calculations.MaximumOperatingDepth(gas.OxygenProcent, 1.6);
                TextBlock txt = new TextBlock();
                txt.Text = gas.ToString() + "MOD " + decoDepth;
                this.DecoGas.Children.Add(txt);
            }
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            bool navigate = false;
            if (data != null)
            {
                if (data.WayPoints.Count > 0)
                {
                    navigate = true;
                }
            }

            if (navigate)
            {
                this.NavigationService.Navigate(new Uri("/DiveProfileResult.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Can no't do the calculation since no waypoint have been defined");
            }
        }

        private void ClearWayPointsButton_Click(object sender, RoutedEventArgs e)
        {
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            data.WayPoints.Clear();
            SerializerHelper.Serialize<DivePlanData>("DivePlanData.xml", data);
            this.WayPoints.Children.Clear();
        }

        private void AddDecoGasButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/AddDecoGas.xaml", UriKind.Relative));
        }

        private void ClearDecoGasButton_Click(object sender, RoutedEventArgs e)
        {
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            data.DecoGas.Clear();
            SerializerHelper.Serialize<DivePlanData>("DivePlanData.xml", data);
            this.DecoGas.Children.Clear();
        }
    }
}