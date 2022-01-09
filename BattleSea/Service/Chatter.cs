using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BattleSea.Service
{
    public class Chatter
    {
        public static Chatter SelfRef { get; set; }

        public Chatter()
        {
            SelfRef = this;
        }

        private void GameWriteNotificationFunc(RichTextBox obj, string notification)
        {
            string text = " Игра: " + notification + "\n";
            obj.Text = obj.Text + text;
            obj.SelectionStart = obj.Text.Length -1;
            obj.ScrollToCaret();
        }

        public void GameWriteNotification(RichTextBox obj, string notification)
        {
            if (obj.InvokeRequired)
            {
                obj.Invoke(new Action(delegate { GameWriteNotificationFunc(obj, notification); }));
            }
            else GameWriteNotificationFunc(obj, notification);
        }

        public void GameUpdateNotification(RichTextBox obj, string not)
        {
            if (obj.InvokeRequired)
                obj.Invoke(new Action(delegate { GameUpdateNotificationFunc(obj, not); }));
            else GameUpdateNotificationFunc(obj, not);
        }

        public void GameUpdateNotificationFunc(RichTextBox obj, string not)
        {
            string text = " Игра: " + not + "\n";
            obj.Text = obj.Text + text;
            obj.SelectionStart = obj.Text.Length;
            obj.ScrollToCaret();
        }

        public void UpdateStatusLbl1(ToolStripStatusLabel obj, string not)
        {
            obj.Text = not + "...";
        }

        public void UpdateGrouBoxText(GroupBox obj, string not)
        {
            obj.Text = not;
        }

        public void Message(string message)
        {
            MessageBox.Show(message);
        }
    }
}
