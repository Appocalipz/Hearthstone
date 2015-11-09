overwolf=overwolf;

overwolf.io = {};
overwolf.io.init = function init(){
  if (overwolf.io.plugin() == null) console.log("Plugin couldn't be loaded??");
  else console.log("Plugin was loaded!");
};

overwolf.io.plugin = function plugin() {
  return document.querySelector('#plugin');
};

overwolf.io.programfiles = function programfiles() {
    return overwolf.io.plugin().PROGRAMFILES;
};

overwolf.io.programfilesx86 = function programfilesx86() {
  return overwolf.io.plugin().PROGRAMFILESX86;
};

overwolf.io.commonfiles = function commonfiles() {
  return overwolf.io.plugin().COMMONFILES;
};

overwolf.io.commonfilesx86 = function commonfilesx86() {
  return overwolf.io.plugin().COMMONFILESX86;
};

overwolf.io.commonappdata = function commonappdata() {
  return overwolf.io.plugin().COMMONAPPDATA;
};

overwolf.io.desktop = function desktop() {
  return overwolf.io.plugin().DESKTOP;
};

overwolf.io.windir = function windir() {
  return overwolf.io.plugin().WINDIR;
};

overwolf.io.sysdir = function sysdir() {
  return overwolf.io.plugin().SYSDIR;
};

overwolf.io.sysdirx86 = function sysdirx86() {
  return overwolf.io.plugin().SYSDIRX86;
};

overwolf.io.fileExists = function fileExists(file, exFunction) {
  overwolf.io.plugin().fileExists(
      file,
      function(status) {
        exFunction(status);
      }
  );
};

overwolf.io.isDirectory = function isDirectory(file, exFunction) {
  overwolf.io.plugin().isDirectory(
      file,
      function(status) {
        exFunction(status);
      }
  );
};

overwolf.io.getTextFile = function getTextFile(file, exFunction) {
  $.get(file, function( data ) {
    exFunction(data);
  });
  //overwolf.io.fileExists(file, function (result) {
  //  if (result == true) {
  //    //overwolf.io.plugin().getTextFile(
  //    //  file, false,
  //    //    function(status, data) {
  //    //      if (!status) exFunction(false);
  //    //      else exFunction(data);
  //    //    }
  //    //  );
  //  }
  //  else {
  //    exFunction(false);
  //  }
  //});
};


overwolf.io.getBinaryFile = function getBinaryFile(file, exFunction) {
  overwolf.io.fileExists(file, function (result) {
    if (result == true) {
      overwolf.io.plugin().getBinaryFile(
          file, false,
          function(status, data) {
            if (!status) exFunction(false);
            else exFunction(data);
          }
      );
    }
    else {
      exFunction(false);
    }
  });
};


