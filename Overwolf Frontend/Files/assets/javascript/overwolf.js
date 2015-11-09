overwolf.window = {};
overwolf.window.fullscreen = function fullscreen(timeout){
  setTimeout(function(){
    overwolf.windows.getCurrentWindow(function(result){
      if (result.status == "success"){
        overwolf.windows.changeSize(result.window.id, window.screen.availWidth, window.screen.availHeight);
        overwolf.windows.changePosition(result.window.id, 0, 0);
      }
    });
  }, timeout);
};



overwolf.window.draggable = function fullscreen(select){
  $(function() {
    $(select).draggable();
  });
};


overwolf.variable = [];
overwolf.variable.set = function variable(id, value){
  overwolf.variable[id] = value;
};
overwolf.variable.get = function variable(id){
  return overwolf.variable[id];
};
