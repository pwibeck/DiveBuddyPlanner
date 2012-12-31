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
    public partial class DiveProfileResult : PhoneApplicationPage
    {
        public DiveProfileResult()
        {
            InitializeComponent();            
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            DivePlanData data = SerializerHelper.DeSeralize<DivePlanData>("DivePlanData.xml");
            if (data == null)
            {
                throw new ArgumentNullException("DivePlanData can't be empty");
            }

            Settings settings = SerializerHelper.DeSeralize<Settings>("Settings.xml");
            if (settings == null)
            {
                settings = new Settings();
            }

            DiveLibrary.DiveProfile profile = new DiveLibrary.DiveProfile();
            Preference pref = new Preference();
            pref.HighGradient = settings.HighGraident;
            pref.LowGradient = settings.LowGraident;
            pref.MiniDepthForDeco = settings.MiniDepthForDeco;
            pref.TimeToSwitchGas = settings.TimeToSwitchGas;
            ZH_L16 algoritm = null;
            switch (settings.Algoritm)
            {
                case Settings.AlgoritmType.ZH16B:
                    algoritm = new ZH_L16B();
                    break;
                case Settings.AlgoritmType.ZH17B:
                    algoritm = new ZH_L17B();
                    break;
            }

            foreach (Gas gas in data.DecoGas)
            {
                double decoDepth = DiveLibrary.Calculations.MaximumOperatingDepth(gas.OxygenProcent, 1.6);
                // To ensure we swith gas at deco depths
                decoDepth -= (decoDepth % pref.DepthBetweenDecoStops);
                data.WayPoints.Add(new WayPoint((double)decoDepth, 0, gas));                
            }

            if (data.DecoGas.Count == 0)
            {
                data.WayPoints.Add(new WayPoint(0, 0, data.WayPoints.Last<WayPoint>().Gas));
            }
            else
            {
                data.WayPoints.Add(new WayPoint(0, 0, data.DecoGas.Last<Gas>()));
            }

            Collection<DiveSegment> segments = profile.Calc(data.WayPoints, algoritm, pref);

            StackPanel statePanel = new StackPanel();
            StackPanel depthPanel = new StackPanel();
            StackPanel gasPanel = new StackPanel();
            StackPanel startTimePanel = new StackPanel();
            StackPanel endTimePanel = new StackPanel();
            StackPanel cnsPanel = new StackPanel();
            StackPanel otuPanel = new StackPanel();
            double cnsTotal = 0;
            double otuTotal = 0;
            foreach (DiveSegment segment in segments)
            {
                AddTextToPanel(statePanel, segment.State.ToString());
                AddTextToPanel(depthPanel, segment.Depth.ToString());                
                AddTextToPanel(startTimePanel, segment.StartTime.ToString());
                AddTextToPanel(endTimePanel, segment.EndTime.ToString());
                AddTextToPanel(cnsPanel, segment.Cns.ToString() +"%");
                AddTextToPanel(otuPanel, segment.Otu.ToString());
                AddTextToPanel(gasPanel, segment.CurrentGas.OxygenProcent + "/" + segment.CurrentGas.NitrogenProcent + "/" + segment.CurrentGas.HeliumProcent);
                cnsTotal += segment.Cns;
                otuTotal += segment.Otu;
            }

            AddTextToPanel(statePanel, "Total");
            AddTextToPanel(cnsPanel, cnsTotal.ToString() + "%");
            AddTextToPanel(otuPanel, otuTotal.ToString());

            AddStackPanelToGrid(statePanel, 1, 0);
            AddStackPanelToGrid(depthPanel, 1, 1);            
            AddStackPanelToGrid(startTimePanel, 1, 2);
            AddStackPanelToGrid(endTimePanel, 1, 3);
            AddStackPanelToGrid(cnsPanel, 1, 4);
            AddStackPanelToGrid(otuPanel, 1, 5);
            AddStackPanelToGrid(gasPanel, 1, 6);

            base.OnNavigatedTo(e);
        }

        private static void AddTextToPanel(StackPanel panel, string txt)
        {
            TextBlock state = new TextBlock();
            state.Text = txt;
            state.TextAlignment = TextAlignment.Center;
            panel.Children.Add(state);
        }

        private void AddStackPanelToGrid(StackPanel panel, int row, int col)
        {
            Grid.SetRow(panel, row);
            Grid.SetColumn(panel, col);
            this.ResultGrid.Children.Add(panel);
        }
    }
}