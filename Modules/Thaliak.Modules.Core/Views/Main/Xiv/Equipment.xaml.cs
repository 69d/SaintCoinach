﻿using System;
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

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;

namespace Thaliak.Modules.Core.Views.Main.Xiv {
    /// <summary>
    /// Interaction logic for Equipment.xaml
    /// </summary>
    [Export("EquipmentView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class Equipment : UserControl {
        public Equipment() {
            InitializeComponent();
        }

        [Import]
        TitledNavigationTarget ViewModel {
            get { return (TitledNavigationTarget)DataContext; }
            set { this.DataContext = value; }
        }

        private void View_Click(object sender, RoutedEventArgs e) {
            var eq = ViewModel.NavigationObject as SaintCoinach.Xiv.Items.Equipment;

            int matVersion;
            var mdl = eq.GetModel(out matVersion);
            if (mdl != null) {
                var subMdl = mdl.GetSubModel(0);
                var component = new SaintCoinach.Graphics.Model(subMdl);
                
                var evt = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<Events.GraphicsViewRequestEvent>();
                evt.Publish(new Events.GraphicsViewRequestArguments {
                    Component = component,
                    Title = eq.Name
                });
            }
        }
    }
}
