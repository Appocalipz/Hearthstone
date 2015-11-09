using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Drawing;
using System.Threading;
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
    }

    static public void Main() {
      File.Copy("O:\\Program Files (x86)\\Hearthstone\\DBF\\CARD.xml", "W:\\Hearthstone\\Resources\\CARD.xml", true);

      Thread thread = new Thread(Parse.Log);
      thread.Start();

      Application.Run(new DeveloperPanel());
    }

  }
}


