using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace MainAssetLog {
  public class AssetLog {
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

      /* -------- CachedAsset.UnloadAssetObject --------- */
      if (line.Contains("CachedAsset.UnloadAssetObject() - unloading") == true) {
        string timeObject = JSONTimeObject(timeSetup[0], timeSetup[1], timeSetup[2].Split('.')[0], timeSetup[2].Split('.')[0]);
        string typeObject = GenericJsonSingle("type", 0);
        string functionObject = GenericJsonSingle("function", "CachedAsset.UnloadAssetObject()")
        string name = GenericJsonSingle("name", actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"persistent="},StringSplitOptions.None)[0].Split(new string[]{"family="},StringSplitOptions.None)[0].Trim())
        string persistent = GenericJsonSingle("persistent", GetBetween(actions, "persistent="))
        string family = GenericJsonSingle("family", GetBetween(actions, "family="))
        endObject = "{"
          +timeObject+", "
          +functionObject+", "
          +name+", "
          +persistent+", "
          +family+"}";
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


















