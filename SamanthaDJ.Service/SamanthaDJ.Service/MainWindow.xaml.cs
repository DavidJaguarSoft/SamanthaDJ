using Newtonsoft.Json;
using SamanthaDJ.Service.UserControl;
using SamanthaDJ.ServiceWPF.Models;
using SamanthaDJ.ServiceWPF.Tools;
using SamanthaDJ.ServiceWPF.UserControl;
using SamanthaDJ.ServiceWPF.ViewModels;
using SamanthaDJ.Socket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using WForms = System.Windows.Forms;

namespace SamanthaDJ.ServiceWPF {

    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        #region Attributes

        private readonly WForms.NotifyIcon _notifyIcon;

        #endregion Attributes

        #region Variables

        HomeUC homeUC = new HomeUC();
        ConfigurationUC configurationUC = new ConfigurationUC();

        #endregion Variables

        #region Constructors

        public MainWindow() {
            InitializeComponent();

            _notifyIcon = new WForms.NotifyIcon();
            InitialiceNotify();
        }

        #endregion Constructors

        #region Form Events

        private async void Window_Loaded(object sender, RoutedEventArgs e) {
            homeUC.Event_WindowMinimize += new EventHandler(EventWindowMinimizeFunction);
            homeUC.Event_GoParametersConfiguration += new EventHandler(EventUCParametersConfigurationFunction);
            configurationUC.event_UCClose += new EventHandler(EventUCConfigurationFunction);
            this.GridMain.Children.Add(homeUC);

            //Automatic update check on startup
            //try {
            //    var info = await Updater.CheckForUpdateAsync(Global.UpdateInfoUrl, Global.SamanthaDJServiceVersion);
            //    if (info != null) {
            //        var res = System.Windows.MessageBox.Show($"Update {info.Version} available. Install now?", "Update available", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //        if (res == MessageBoxResult.Yes) {
            //            await Updater.DownloadAndRunInstallerAsync(info.InstallerUrl);
            //            // Optionally exit to allow installer to replace files
            //            System.Windows.Application.Current.Shutdown();
            //        }
            //    }
            //} catch { }
        }

        private void EventWindowMinimizeFunction(object sender, EventArgs e) {
            this.WindowState = WindowState.Minimized;
            this.Hide();
        }

        private void EventUCParametersConfigurationFunction(object sender, EventArgs e) {
            this.GridMain.Children.Add(configurationUC);
        }

        private void EventUCConfigurationFunction(object sender, EventArgs e) {
            this.GridMain.Children.Remove(configurationUC);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            e.Cancel = true;
            this.Hide();
            //this.WindowState = WindowState.Minimized;
            //_notifyIcon.Dispose();
        }

        private void Window_Closed(object sender, EventArgs e) {
        }

        #endregion Form Events

        #region NotifyIcon

        private void InitialiceNotify() {

            #region System tray icon

            //WForms.NotifyIcon notifyIcon = new WForms.NotifyIcon();
            //string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "djs.ico");
            //notifyIcon.Icon = new System.Drawing.Icon(iconPath);

            var uri = new Uri("pack://application:,,,/Resources/djs.ico", UriKind.Absolute);
            var streamInfo = System.Windows.Application.GetResourceStream(uri);
            System.Drawing.Icon myIcon = null;
            using (var s = streamInfo.Stream) {
                //notifyIcon.Icon = new System.Drawing.Icon(s);
                myIcon = new System.Drawing.Icon(s);
            }
            _notifyIcon.Icon = myIcon;
            _notifyIcon.Text = "Samantha Service";
            _notifyIcon.Click += NotifyIcon_Click;

            _notifyIcon.ContextMenuStrip = new WForms.ContextMenuStrip();
            //_notifyIcon.ContextMenuStrip.Items.Add("Status", System.Drawing.Image.FromFile("Resources/djs.ico"), OnStatusClicked);
            _notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripLabel("Status: Running"));
            _notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripButton("Status: Running"));
            _notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripDropDownButton("Status: Running", null,
                new WForms.ToolStripLabel("Label 1"),
                new WForms.ToolStripLabel("Label 2")));

            // Add update menu
            //var miCheckUpdates = new WForms.ToolStripMenuItem("Check for updates...");
            //miCheckUpdates.Click += async (s, ev) => {
            //    try {
            //        var info = await Updater.CheckAndInstallIfAvailableAsync(Global.UpdateInfoUrl, Global.SamanthaDJServiceVersion);
            //        if (info != null) {
            //            var ans = System.Windows.MessageBox.Show($"Update {info.Version} available. Install now?", "Update available", MessageBoxButton.YesNo, MessageBoxImage.Question);
            //            if (ans == MessageBoxResult.Yes) {
            //                await Updater.DownloadAndRunInstallerAsync(info.InstallerUrl);
            //                System.Windows.Application.Current.Shutdown();
            //            }
            //        } else {
            //            System.Windows.MessageBox.Show("No updates available.", "Update", MessageBoxButton.OK, MessageBoxImage.Information);
            //        }
            //    } catch {
            //        System.Windows.MessageBox.Show("Update check failed.", "Update", MessageBoxButton.OK, MessageBoxImage.Error);
            //    }
            //};
            //_notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripSeparator());
            //_notifyIcon.ContextMenuStrip.Items.Add(miCheckUpdates);

            _notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;

            _notifyIcon.Visible = true;

            #endregion System tray icon
        }

        private void NotifyIcon_Click(object sender, EventArgs e) {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate();
        }

        private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e) {
            System.Windows.MessageBox.Show("NotifyIcon_BalloonTipClicked is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OnStatusClicked(object sender, EventArgs e) {
            System.Windows.MessageBox.Show("OnStatusClicked is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion NotifyIcon
    }
}
