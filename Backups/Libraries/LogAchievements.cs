using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace MainAchievementsLog {
  public class AchievementsLog {
    public static string GetBetween (main, first, last = " ") {
      return main.Split(new string[]{first},StringSplitOptions.None)[1].Split(new string[]{last},StringSplitOptions.None)[0].Trim()
    }

    public static string JSONTimeObject (hour, minute, seconds, milliseconds) {
      return "\"time\": {\"hour\":\""+hour+"\" ,\"minute\":\""+minute+"\" ,\"seconds\":\""+seconds+"\" ,\"milliseconds\":\""+milliseconds+"\"}";
    }

    public static string GenericJsonSingle(id, value) {
      if (value == true | value == false)
        return "\""+id+"\":"+value;
      else if (int.TryParse(value, out n))
        return "\""+id+"\":"+value;
      else
        return "\""+id+"\":\""+value+"\"";
    }

    public static string LineParse(string line) {
      line = System.Text.RegularExpressions.Regex.Replace(line,@"\s+"," ");
	  var timeSetup = line.Split(' ')[1].Split(':');
      var actions = line.Split(new string[]{": ["}, StringSplitOptions.None)[1];
      String endObject = "";

      /* -------- Notify Of CardGained --------- */
      if (line.Contains("NotifyOfCardGained: [") == true) {
        string timeObject = JSONTimeObject(timeSetup[0], timeSetup[1], timeSetup[2].Split('.')[0], timeSetup[2].Split('.')[0]);
        string typeObject = GenericJsonSingle("type", 0);
        string name = GenericJsonSingle("name", actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"type="},StringSplitOptions.None)[0].Split(new string[]{"cardID="},StringSplitOptions.None)[0].Trim())
        string cardId = GenericJsonSingle("cardId", GetBetween(actions, "cardId="))
        string type = GenericJsonSingle("type", GetBetween(actions, "type="))
        string cardFlair = GenericJsonSingle("cardFlair", GetBetween(actions, "CardFlair: "))
        endObject = "{"
          +timeObject+", "
          +typeObject+", "
          +name+", "
          +cardId+", "
          +type+", "
          +cardFlair+"}";
      }

      /* -------- Processing achievement --------- */
      if (line.Contains("Processing achievement: [") == true) {
        string timeObject = JSONTimeObject(timeSetup[0], timeSetup[1], timeSetup[2].Split('.')[0], timeSetup[2].Split('.')[0]);
        string typeObject = GenericJsonSingle("type", 1);
        string id = GenericJsonSingle("id", GetBetween(actions, "ID="))
        string achieveGroup = GenericJsonSingle("achieveGroup", GetBetween(actions, "AchieveGroup="))
        string name = GenericJsonSingle("name", GetBetween(actions, "Name='", "'"))
        string maxProgress = GenericJsonSingle("maxProgress", GetBetween(actions, "MaxProgress="))
        string progress = GenericJsonSingle("progress", GetBetween(actions, "Progress="))
        string ackProgress = GenericJsonSingle("ackProgress", GetBetween(actions, "AckProgress="))
        string isActive = GenericJsonSingle("isActive", GetBetween(actions, "IsActive="))
        string dateGive = GenericJsonSingle("dateGive", GetBetween(actions, "DateGiven="))
        string dateCompleted = GenericJsonSingle("dateCompleted", GetBetween(actions, "DateCompleted="))
        string description = GenericJsonSingle("description", GetBetween(actions, "Description='", "'"))
        string trigger = GenericJsonSingle("trigger", GetBetween(actions, "Trigger="))
        endObject = "{"
          +timeObject+", "
          +typeObject+", "
          +id+", "
          +achieveGroup+", "
          +name+", "
          +maxProgress+", "
          +progress+", "
          +ackProgress+", "
          +isActive+", "
          +dateGive+", "
          +dateCompleted+", "
          +description+", "
          +trigger+"}";
      }

      /* -------- ELSE --------- */
      else {
        System.Console.WriteLine("No function for this action yet: " + actions);
        endObject = "";
      }
      return endObject;
    }
  }
}


















