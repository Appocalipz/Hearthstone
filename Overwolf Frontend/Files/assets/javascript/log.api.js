
overwolf.hearthstone.log = {};
overwolf.hearthstone.log.parse = {};
overwolf.hearthstone.log.watch = function parse(logfile, returnFunction, processFunction){
  window.setInterval(function(){
    overwolf.io.getTextFile(overwolf.hearthstone.config.path() + "" + logfile, function (content) {
      overwolf.hearthstone.log.useLog(content, logfile, returnFunction, processFunction);
    });
  }, 1500);
};

overwolf.hearthstone.log.useLog = function parse(logContent, logfile, returnFunction, processFunction){
  if (typeof overwolf.variable.get(logfile) == "undefined" && logContent != false){
    overwolf.variable.set(logfile, logContent.split("\n"));
    returnFunction(overwolf.variable.get(logfile), processFunction);
  }
  else if (logContent != false) {
    if (logContent.split("\n").length > overwolf.variable.get(logfile).length) {
      var newLines = logContent.split("\n").slice(overwolf.variable.get(logfile).length-1);
      overwolf.variable.set(logfile, logContent.split("\n"));
      returnFunction(newLines, processFunction);
    }
  }
  else {
    console.log("Log file '"+logfile+"' parse failed.")
  }
};

overwolf.hearthstone.log.parse.asset = function parse(content, processFunction){
  var parseContent = $.map( content, function( value ) {
    if(value.indexOf("AssetLoader.LoadObject()") > -1) {

    }
    else if(value.indexOf("CachedAsset.UnloadAssetObject()") > -1) {
      var innerMap = $.map( value.split("CachedAsset.UnloadAssetObject() - unloading ")[1].split(" "), function( innerValue ) {
        return [innerValue.split("=")];
      });
      return [innerMap];
    }
    else
      return value;
  });
  processFunction(parseContent);
};




