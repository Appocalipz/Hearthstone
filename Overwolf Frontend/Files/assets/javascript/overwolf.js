overwolf.fullscreen = function fullscreen(){
  overwolf.windows.getCurrentWindow(function(result){
    if (result.status == "success"){
      overwolf.windows.changeSize(result.window.id, window.screen.availWidth, window.screen.availHeight);
      overwolf.windows.changePosition(result.window.id, 0, 0);
    }
  });
}



