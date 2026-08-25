using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace SamanthaDJ.ServiceWPF.Commands {

    public class NotifyCommand : BaseCommand {
        private readonly NotifyIcon _notifyIcon;

        public NotifyCommand(NotifyIcon notifyIcon) {
            _notifyIcon = notifyIcon;
        }

        public override void Execute(object parameter) {
            _notifyIcon.ShowBalloonTip(3000, "SingletonSean", "Be sure to subscribe.", ToolTipIcon.Info);
        }
    }
}
