using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Drawing;

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
      InitTimer();
      Application.Run(new DeveloperPanel());
    }

    static Timer timer1;
    static public void InitTimer(){
      timer1 = new Timer();
      timer1.Tick += new EventHandler(timer1_Tick);
      timer1.Interval = 1000;
      timer1.Start();
    }
    static void timer1_Tick(object sender, EventArgs e){
      Console.WriteLine("Asset Log File Copied");
      try {
       File.Copy("O:/Program Files (x86)/Hearthstone/Logs/Asset.log", "W:/Hearthstone/Overwolf Frontend/Temp/Logs/Asset.log", true);
      }
      catch (IOException)
      {
         Console.WriteLine("IOException source");
      }
    }


  }
}


