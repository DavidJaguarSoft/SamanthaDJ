using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WForms = System.Windows.Forms;

namespace SamanthaDJ.ServiceWPF {

    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application {

        //private readonly WForms.NotifyIcon notifyIcon;

        public App() {
            //notifyIcon = new WForms.NotifyIcon();
        }

        protected override void OnStartup(StartupEventArgs e) {

            //MainWindow = new MainWindow();
            //MainWindow.Show();

            // Start update check in background if URL configured
            //try {
            //    var url = SamanthaDJ.ServiceWPF.Tools.Global.UpdateInfoUrl;
            //    if (!string.IsNullOrWhiteSpace(url)) {
            //        _ = SamanthaDJ.ServiceWPF.Tools.Updater.CheckAndInstallIfAvailableAsync(url, true);
            //    }
            //} catch { }

            #region System tray icon

            //WForms.NotifyIcon notifyIcon = new WForms.NotifyIcon();
            //string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "djs.ico");
            //notifyIcon.Icon = new System.Drawing.Icon(iconPath);
            //var uri = new Uri("pack://application:,,,/Resources/djs.ico", UriKind.Absolute);
            //var streamInfo = Application.GetResourceStream(uri);
            //System.Drawing.Icon myIcon = null;
            //using (var s = streamInfo.Stream) {
            //    //notifyIcon.Icon = new System.Drawing.Icon(s);
            //    myIcon = new System.Drawing.Icon(s);
            //}
            //notifyIcon.Icon = myIcon;
            //notifyIcon.Text = "Samantha Service";
            //notifyIcon.Click += NotifyIcon_Click;
            //notifyIcon.ContextMenuStrip = new WForms.ContextMenuStrip();
            //notifyIcon.ContextMenuStrip.Items.Add("Status", Image.FromFile("Resources/djs.ico"), OnStatusClicked);
            //notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripLabel("Status: Running"));
            //notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripButton("Status: Running"));
            //notifyIcon.ContextMenuStrip.Items.Add(new WForms.ToolStripDropDownButton("Status: Running", null,
            //    new WForms.ToolStripLabel("Label 1"),
            //    new WForms.ToolStripLabel("Label 2")));
            //notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            //notifyIcon.Visible = true;

            #endregion System tray icon

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {
            //notifyIcon.Dispose();
            base.OnExit(e);
        }

        //private void NotifyIcon_Click(object sender, EventArgs e) {

        //    MainWindow.WindowState = WindowState.Normal;
        //    MainWindow.Activate();

        //    if (MainWindow == null) {
        //        MainWindow = new MainWindow();
        //    }
        //    if (MainWindow.IsVisible) {
        //        MainWindow.Hide();
        //    } else {
        //        MainWindow.Show();
        //        MainWindow.WindowState = WindowState.Normal;
        //        MainWindow.Activate();
        //    }
        //}

        //private void NotifyIcon_BalloonTipClicked(object sender, EventArgs e) {
        //    MessageBox.Show("Application is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        //}

        //private void OnStatusClicked(object sender, EventArgs e) {
        //    MessageBox.Show("Application is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        //}

    }
}
