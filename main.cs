using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Drawing;
using Log;

namespace Form {
  public class DeveloperPanel : System.Windows.Forms.Form {
    private Button button1;
    public DeveloperPanel() {
      button1 = new Button ();
      button1.Text = "Quit";
      button1.Name = "button1";
      button1.Size = new System.Drawing.Size (72, 30);
      button1.Location = new System.Drawing.Point ((ClientRectangle.Width - button1.Size.Width) / 2, ClientRectangle.Height - 35);
      Controls.AddRange(new System.Windows.Forms.Control[] {this.button1});
      button1.Click += new System.EventHandler(OnClickButton1);
    }

    static public void Main() {
      Parse.Log("power");
      Application.Run(new DeveloperPanel());
    }
    
    void OnClickButton1 (object sender, System.EventArgs e) {
      Application.Exit ();
    }
  }
}


