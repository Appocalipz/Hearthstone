using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace MainFaceDownCardLog {
  public class FaceDownCardLog {
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

    public static String LineParse(string line) {
      line = System.Text.RegularExpressions.Regex.Replace(line,@"\s+"," ");
	  var timeSetup = line.Split(' ')[1].Split(':');
      var actions = line.Split(new string[]{" - "}, StringSplitOptions.None)[1];
      String endObject = "";

      /* -------- Card.SetDoNotSort() --------- */
      if (line.Contains("Card.SetDoNotSort() -") == true) {
        string timeObject = JSONTimeObject(timeSetup[0], timeSetup[1], timeSetup[2].Split('.')[0], timeSetup[2].Split('.')[0]);
        string typeObject = GenericJsonSingle("type", 0);
        string functionObject = GenericJsonSingle("function", line.Split(' ')[2]);
        string id = GenericJsonSingle("id", GetBetween(actions, "id="))
        string cardId = GenericJsonSingle("cardId", GetBetween(actions, "cardId="))
        string type = GenericJsonSingle("type", GetBetween(actions, "type="))
        string zone = GenericJsonSingle("zone", GetBetween(actions, "zone="))
        string zonePos = GenericJsonSingle("zonePos", GetBetween(actions, "zonePos="))
        string player = GenericJsonSingle("player", GetBetween(actions, "player=", "]"))
        string on = GenericJsonSingle("on", GetBetween(actions, "on="))
        endObject = "{"
          +timeObject+", "
          +functionObject+", "
          +typeObject+", "
          +id+", "
          +cardId+", "
          +type+", "
          +zone+", "
          +zonePos+", "
          +player+", "
          +on+", "
          +endActionObject+"}";
      }

      /* -------- Card.MarkAsGrabbedByEnemyActionHandler() --------- */
      if (line.Contains("Card.MarkAsGrabbedByEnemyActionHandler() -") == true) {
        string timeObject = JSONTimeObject(timeSetup[0], timeSetup[1], timeSetup[2].Split('.')[0], timeSetup[2].Split('.')[0]);
        string typeObject = GenericJsonSingle("type", 0);
        string functionObject = GenericJsonSingle("function", line.Split(' ')[2]);
        string id = GenericJsonSingle("id", GetBetween(actions, "id="))
        string cardId = GenericJsonSingle("cardId", GetBetween(actions, "cardId="))
        string type = GenericJsonSingle("type", GetBetween(actions, "type="))
        string zone = GenericJsonSingle("zone", GetBetween(actions, "zone="))
        string zonePos = GenericJsonSingle("zonePos", GetBetween(actions, "zonePos="))
        string player = GenericJsonSingle("player", GetBetween(actions, "player=", "]"))
        string enable = GenericJsonSingle("enable", GetBetween(actions, "enable="))
        endObject = "{"
          +timeObject+", "
          +functionObject+", "
          +typeObject+", "
          +id+", "
          +cardId+", "
          +type+", "
          +zone+", "
          +zonePos+", "
          +player+", "
          +enable+", "
          +endActionObject+"}";
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


















