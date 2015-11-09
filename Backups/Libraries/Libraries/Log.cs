using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Drawing;
using MainAssetLog;

namespace Log {
  public class Parse {

    public static void Log() {
      Monitor("Asset.log");
    }

    static int assetLastLineCount = 0;
    static int assetCurrentLineCount = 0;
    public static void monitorAssets(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"O:\\Program Files (x86)\\Hearthstone\\Logs\\Asset.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        assetCurrentLineCount = 0;
        while (sr.Peek() >= 0) {
          if (assetCurrentLineCount > assetLastLineCount){
            string response = AssetLog.LineParse(sr.ReadLine());
            Console.WriteLine(sr.ReadLine());
            assetLastLineCount = assetLastLineCount + 1;
          }
          else {
           sr.ReadLine();
          }
          assetCurrentLineCount = assetCurrentLineCount + 1;
        }
      }
    }

    public static FileSystemWatcher Monitor (string fileName) {
      var watch = new FileSystemWatcher();
      watch.Path = @"O:\\Program Files (x86)\\Hearthstone\\Logs";
      watch.NotifyFilter = NotifyFilters.LastWrite;
      watch.EnableRaisingEvents = true;
      watch.Filter = fileName;
      if (fileName == "Asset.log") {watch.Changed += new FileSystemEventHandler(monitorAssets);}
      return watch;
    }
  }
}



