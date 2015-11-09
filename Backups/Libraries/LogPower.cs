using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text.RegularExpressions;

namespace MainPowerLog {
  public class PowerLog {
    public static String Primary = "";
    public static String LineParse(string line) {
      line = System.Text.RegularExpressions.Regex.Replace(line,@"\s+"," ");
	  var timeSetup = line.Split(' ')[1].Split(':');
      var actions = line.Split(new string[]{" - "}, StringSplitOptions.None)[1];
      String endObject = "";

      /* -------- End Action --------- */
      if (actions.Contains("End Action") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":0";
        String endActionObject = "\"endAction\":\""+ actions.Split('=')[1] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+endActionObject+"}";
      }

      /* -------- m_currentTaskList --------- */
      else if (actions.Contains("m_currentTaskList=") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":1";
        String endActionObject = "\"mCurrentTaskList\":\""+ actions.Split('=')[1] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+endActionObject+"}";
      }

      /* -------- TAG_CHANGE --------- */
      else if (actions.Contains("TAG_CHANGE") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":2";
        if (actions.Contains("[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[] { "Entity=[" }, StringSplitOptions.None)[1].Split(new string[] { "]" }, StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          String tag = actions.Split(new string[]{"tag="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0];
          String value = actions.Split(new string[]{"value="},StringSplitOptions.None)[1];
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"entity\":" + tempEndObject + ", \"tag\":\""+tag+"\", \"value\":\""+value+"\"}";
        }
        else {
          String entity = actions.Split(new string[]{"Entity="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0];
          String tag = actions.Split(new string[]{"tag="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0];
          String value = actions.Split(new string[]{"value="},StringSplitOptions.None)[1];
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"entity\":" + entity + ", \"tag\":\""+tag+"\", \"value\":\""+value+"\"}";
        }
      }

      /* -------- TAG_CHANGE --------- */
      else if (actions.Contains("ACTION_START") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":3";
        if (actions.Contains("Entity=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"Entity=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          endObject = endObject +  "\"entity\":" + tempEndObject;
        }
        else if (actions.Contains("Entity=") == true) {
          String entity = actions.Split(new string[]{"Entity="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0];
          endObject = endObject +  "\"entity\":\"" + entity + "\"";
        }
        if (actions.Contains("Target=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"Target=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          if (endObject == "" | endObject == null) endObject = endObject +  "\"target\":" + tempEndObject;
          else endObject = endObject +  ", \"target\":" + tempEndObject;
        }
        else {
          if(actions.Contains("Target=[") == false){
            String target = "\"target\":\"" + actions.Split(new string[]{"Target="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            if (endObject == "" | endObject == null) endObject = endObject +  "" + target;
            else endObject = endObject +  ", " + target;
          }
        }
        if(actions.Contains("BlockType=") == true){
          String blockType = "\"blockType\":\"" + actions.Split(new string[]{"BlockType="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
          if (endObject == "" | endObject == null) endObject = endObject +  "" + blockType;
          else endObject = endObject +  ", " + blockType;
        }
        if(actions.Contains("Index=") == true){
          String index = "\"index\":\"" + actions.Split(new string[]{"Index="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
          if (endObject == "" | endObject == null) endObject = endObject +  "" + index;
          else endObject = endObject +  ", " + index;
        }
        if (endObject == "" | endObject == null) endObject = "{" + endObject +  "" + timeObject + "," + functionObject + "," + typeObject + "}";
        else endObject = "{" + endObject +  ", " + timeObject + ", " + functionObject + ", " + typeObject + "}";
      }

      /* -------- tag --------- */
      else if (actions.Contains("tag=") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":4";
        String tagObject = "\"tag\":\""+ actions.Split(new string[]{"tag="},StringSplitOptions.None)[1].Split(' ')[0] + "\"";
        String valueObject = "\"value\":\""+ actions.Split(new string[]{"value="},StringSplitOptions.None)[1].Split(' ')[0] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+tagObject+", "+valueObject+"}";
      }

      /* -------- FULL_ENTITY --------- */
      else if (actions == "FULL_ENTITY") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":5";
        String full_entity = "\"fullEntity\":\"FULL_ENTITY\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+full_entity+"}";
      }

      /* -------- SHOW_ENTITY --------- */
      else if (actions == "SHOW_ENTITY") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":6";
        String showEntity = "\"showEntity\":\"SHOW_ENTITY\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+showEntity+"}";
      }

      /* -------- META_DATA --------- */
      else if (actions == "META_DATA") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":7";
        String metaData = "\"metaData\":\"META_DATA\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+metaData+"}";
      }

      /* -------- ACTION_END --------- */
      else if (actions == "ACTION_END") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":8";
        String actionEnd = "\"actionEnd\":\"ACTION_END\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+actionEnd+"}";
      }

      /* -------- ACTION_END --------- */
      else if (actions.Contains("Source Action=") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":9";
        String endActionObject = "\"sourceAction\":\""+ actions.Split('=')[1] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+endActionObject+"}";
      }

      /* -------- actionStart has unhandled --------- */
      else if (actions.Contains("actionStart has unhandled BlockType PLAY") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":10";
        if (actions.Contains("[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"sourceEntity\":" + tempEndObject + "\"}";
        }
        else {
          endObject = "";
        }
      }

      /* -------- option 0 --------- */
      else if (actions.Contains("option 0 ") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":11";
        String type = "\"type\":\""+ actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        String mainEntity = "\"mainEntity\":\""+ actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+type+", "+mainEntity+"}";
      }

      /* -------- ID, ParentID, PreviousID, TaskCount --------- */
      else if (actions.Contains("ID") == true & actions.Contains("ParentID") == true & actions.Contains("PreviousID") == true & actions.Contains("TaskCount") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":12";
        String id = "\"ID\":\""+ actions.Split(new string[]{"ID="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        String parentid = "\"ParentID\":\""+ actions.Split(new string[]{"ParentID="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        String previousid = "\"PreviousID\":\""+ actions.Split(new string[]{"PreviousID="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        String taskcount = "\"TaskCount\":\""+ actions.Split(new string[]{"TaskCount="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+id+", "+parentid+", "+previousid+", "+taskcount+"}";
      }

      /* -------- options --------- */
      else if (actions.Contains("option 1 ") == true | actions.Contains("option 2 ") == true | actions.Contains("option 3 ") == true | actions.Contains("option 4 ") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":14";
        if (actions.Contains("mainEntity=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"mainEntity=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          if (actions.Contains("type=") == true) {
            String type = actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0];
            endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\", \"type\":" + type + "\"}";
          }
          else {
            endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\"}";
          }
        }
        else {
          endObject = "";
        }
      }

      /* -------- Targets --------- */
      else if (actions.Contains("target 0 ") == true | actions.Contains("target 1 ") == true | actions.Contains("target 2 ") == true | actions.Contains("target 3 ") == true | actions.Contains("target 4 ") == true) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":15";
        if (actions.Contains("entity=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"entity=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\"}";
        }
        else {
          endObject = "";
        }
      }

      /* -------- Count --------- */
      else if (actions.Contains("Count=")) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":17";
        String count = "\"count\":\""+ actions.Split(new string[]{"Count="},StringSplitOptions.None)[1] + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+count+"}";
      }

      /* -------- SelectedOption --------- */
      else if (actions.Contains("selectedOption=") & actions.Contains("selectedSubOption=")) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":18";
        String selectedOption = "\"selectedOption\":\"" + actions.Split(new string[]{"selectedOption="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String selectedSubOption = "\"selectedSubOption\":\"" + actions.Split(new string[]{"selectedSubOption="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String selectedTarget = "\"selectedTarget\":\"" + actions.Split(new string[]{"selectedTarget="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String selectedPosition = "\"selectedPosition\":\"" + actions.Split(new string[]{"selectedPosition="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+selectedOption+", "+selectedSubOption+", "+selectedTarget+", "+selectedPosition+"}";
      }

      /* -------- Options --------- */
      else if (actions.Contains("option ") == true | actions.Contains("type") == true | actions.Contains("mainEntity") == true ) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":20";
        if (actions.Contains("mainEntity=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"mainEntity=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\"}";
        }
        else {
          endObject = "";
        }
      }

      /* -------- Options --------- */
      else if (actions.Contains("option ") == true | actions.Contains("type") == true | actions.Contains("entity") == true ) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":21";
        if (actions.Contains("entity=[") == true & actions.Contains("]") == true) {
          String entity = actions.Split(new string[]{"entity=["},StringSplitOptions.None)[1].Split(new string[]{"]"},StringSplitOptions.None)[0];
          String tempEndObject = "";
          if (actions.Contains("name=") == true) {
            String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
          }
          else {
            String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String type = "\"type\":\"" + actions.Split(new string[]{"type="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
            String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
            tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+type+", "+player+"}";
          }
          endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\"}";
        }
        else {
          endObject = "";
        }
      }

      /* -------- Info --------- */
      else if (actions.Contains("Info[") & actions.Contains("] = [")) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":22";
        String name = "\"name\":\"" + actions.Split(new string[]{"name="},StringSplitOptions.None)[1].Split(new string[]{"player="},StringSplitOptions.None)[0].Split(new string[]{"cardId="},StringSplitOptions.None)[0].Split(new string[]{"zonePos="},StringSplitOptions.None)[0].Split(new string[]{"zone="},StringSplitOptions.None)[0].Split(new string[]{"id="},StringSplitOptions.None)[0].Trim() + "\"";
        String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String zone = "\"zone\":\"" + actions.Split(new string[]{"zone="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String zonePos = "\"zonePos\":\"" + actions.Split(new string[]{"zonePos="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String cardId = "\"cardId\":\"" + actions.Split(new string[]{"cardId="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim() + "\"";
        String player = "\"player\":\"" + actions.Split(new string[]{"player="},StringSplitOptions.None)[1].Split(new string[]{" "},StringSplitOptions.None)[0].Trim().Replace("]","") + "\"";
        String tempEndObject = "{"+id+", "+cardId+", "+zone+", "+zonePos+", "+name+", "+player+"}";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", \"mainEntity\":" + tempEndObject + "\"}";
      }

      /* -------- ID LAST ONE --------- */
      else if (actions.Contains("id=")) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":23";
        String id = "\"id\":\"" + actions.Split(new string[]{"id="},StringSplitOptions.None)[1].Trim() + "\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+id+"}";
      }

      /* -------- HIDE_ENTITY --------- */
      else if (actions == "HIDE_ENTITY") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":24";
        String metaData = "\"hideEntity\":\"HIDE_ENTITY\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+metaData+"}";
      }

      /* -------- CREATE_GAME --------- */
      else if (actions == "CREATE_GAME") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":25";
        String metaData = "\"createGame\":\"CREATE_GAME\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+metaData+"}";
      }

      /* -------- PowerLog --------- */
      else if (actions == "PowerLog.LogWatch") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":26";
        String metaData = "\"powerLogLogWatch\":\"PowerLog.LogWatch\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+metaData+"}";
      }

      /* -------- Main Startup --------- */
      else if (actions.Contains("Main Startup")) {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":26";
        String startup = "\"mainStartUp\":\"Main Startup\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+startup+"}";
      }

      /* -------- Source --------- */
      else if (actions == "Source=GameEntity") {
        String timeObject = "\"time\": {\"hour\":\""+timeSetup[0]+"\" ,\"minute\":\""+timeSetup[1]+"\" ,\"seconds\":\""+timeSetup[2].Split('.')[0]+"\" ,\"milliseconds\":\""+timeSetup[2].Split('.')[0]+"\"}";
        String functionObject = "\"function\":\"" + line.Split(' ')[2] + "\"";
        String typeObject = "\"type\":26";
        String gameEntity = "\"source\":\"GameEntity\"";
        endObject = "{"+timeObject+", "+functionObject+", "+typeObject+", "+gameEntity+"}";
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


















