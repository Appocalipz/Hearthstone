using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace MainZoneLog {
  public class ZoneLog {
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

      /* -------- ELSE --------- */
      // else {
        System.Console.WriteLine("No function for this action yet: " + actions);
        endObject = "";
      // }
      return endObject;
    }
  }
}


















