using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Permissions;
using System.Drawing;
using MainPowerLog;
using MainAchievementsLog;
using MainAssetLog;
using MainFaceDownCardLog;
using MainZoneLog;
using MainOutputLog;

namespace Log {
  public class Parse {
    public static void Log(string logFile) {
      CleanUpLogs();
      var watch = new FileSystemWatcher();
      watch.Path = @"FileSystem/Logs";
      watch.NotifyFilter = NotifyFilters.LastWrite;
      watch.EnableRaisingEvents = true;
      if(logFile == "power") {
        watch.Filter = "Power.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
      if(logFile == "achievements") {
        watch.Filter = "Achievements.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
      if(logFile == "asset") {
        watch.Filter = "Asset.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
      if(logFile == "faceDownCard") {
        watch.Filter = "FaceDownCard.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
      if(logFile == "zone") {
        watch.Filter = "Zone.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
      if(logFile == "outputLog") {
        watch.Filter = "Output.log";
        watch.Changed += new FileSystemEventHandler(powerLogChangeEvent);
      }
    }

    public static CleanUpLogs() {
      File.Delete(@"Processed\\Power.log");
      File.Delete(@"Processes\\Achievement.log");
      File.Delete(@"Processes\\Asset.log");
      File.Delete(@"Processes\\FaceDownCard.log");
      File.Delete(@"Processes\\Zone.log");
      File.Delete(@"Processes\\OutputLog.log");
    }

    public static int outputLogLinesSkip = 0;
    public static void outputLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/Output.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < outputLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          outputLogLinesSkip = outputLogLinesSkip + 1;
          String response = OutputLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\Output.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }

    public static int zoneLogLinesSkip = 0;
    public static void zoneLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/Zone.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < zoneLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          zoneLogLinesSkip = zoneLogLinesSkip + 1;
          String response = ZoneLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\Zone.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }

    public static int faceDownCardLogLinesSkip = 0;
    public static void faceDownCardLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/FaceDownCard.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < faceDownCardLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          faceDownCardLogLinesSkip = faceDownCardLogLinesSkip + 1;
          String response = FaceDownCardLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\FaceDownCard.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }

    public static int assetLogLinesSkip = 0;
    public static void assetLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/Asset.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < assetLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          assetLogLinesSkip = assetLogLinesSkip + 1;
          String response = AssetLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\Asset.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }

    public static int achievementLogLinesSkip = 0;
    public static void achievementLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/Achievement.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < achievementLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          achievementLogLinesSkip = achievementLogLinesSkip + 1;
          String response = AchievementLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\Achievement.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }

    public static int powerLogLinesSkip = 0;
    public static void powerLogChangeEvent(object sender, FileSystemEventArgs e) {
      var fs = new FileStream(@"FileSystem/Logs/Power.log", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
      using (var sr = new StreamReader(fs)) {
        for (var i = 0; i < powerLogLinesSkip; i++)
          sr.ReadLine();
        List<string> lines = new List<string>();
        while (sr.Peek() >= 0) {
          powerLogLinesSkip = powerLogLinesSkip + 1;
          String response = PowerLog.LineParse(sr.ReadLine());
          lines.Add(response);
        }
        string[] terms = lines.ToArray();
        string joinedJson = string.Join("\n", terms).ToString();
        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"Processed\\Power.log", true)) {
          file.WriteLine(joinedJson);
        }
      }
    }
  }
}



