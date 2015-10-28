overwolf.hearthstone={}
overwolf.hearthstone.player={}

overwolf.hearthstone.addcard = function card(cardName){
  $(".main-body").append($("<img>").attr("src","assets/images/cards/" + cardName).addClass("active-card card faceup-card"))
}

overwolf.hearthstone.player.addHDCount = function card(player, hand, deck){
  handDeckContainer = $("<div></div>").addClass("hand-deck-container").appendTo(".player-"+ player +"-counters")
  hand = $("<div></div>").html("Hand: " + hand).addClass("player-"+player+"-hand-count").appendTo(handDeckContainer)
  deck = $("<div></div>").html("Deck: " + deck).addClass("player-"+player+"-deck-count").appendTo(handDeckContainer)
}

overwolf.hearthstone.player.addDeckCard = function card(player, diamonds, title, image){
  minItemContainer = $("<div></div>").addClass("container-min-" + player).appendTo(".player-"+player+"-mincards")
  frame = $("<img>").attr("src","assets/images/frame/frame.png").addClass("frame-min").appendTo(minItemContainer)
  frameRight = $("<img>").attr("src","assets/images/frame/frame-right.png").addClass("frame-right-min").appendTo(minItemContainer)
  number = $("<div></div>").html(diamonds).addClass("number-min").appendTo(minItemContainer)
  title = $("<div></div>").html(title).addClass("title-min").appendTo(minItemContainer)
  image = $("<img>").attr("src","assets/images/card-stripes/" + image).addClass("card-min").appendTo(minItemContainer)
  frameRight.animate({
    opacity: 0
  }, 5000, function() {});
  if (player == 1){
    minItemContainer.animate({
      left: 0
    }, 2000, function() {});
  }
  else if (player == 2){
    minItemContainer.animate({
      right: 0
    }, 2000, function() {});
  }
}

overwolf.hearthstone.player.cards = 0;

overwolf.hearthstone.player.addCard = function card(pos, cardName){
  $(".player-" + pos + "-hand")
	  .append($("<img>")
	  .attr("src","assets/images/cards/" + cardName)
	  .addClass("active-card card faceup-card")
	  .css("position","absolute")
	  .css("left", (overwolf.hearthstone.player.cards * 180) + "px")
	)
  overwolf.hearthstone.player.cards = overwolf.hearthstone.player.cards + 1;
  if(overwolf.hearthstone.player.cards < 4) { 
  	$(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
  		.css("margin-left", -((20 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
  }
  if(overwolf.hearthstone.player.cards == 4) { 
    $(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((0 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
    $($(".player-1-hand").find(".card")[0]).css("transform", "rotate(-14deg)")
    $($(".player-1-hand").find(".card")[0]).css("left","130px").css("bottom","-300px")

    $($(".player-1-hand").find(".card")[1]).css("transform", "rotate(-8deg)")
    $($(".player-1-hand").find(".card")[1]).css("left","230px")

    $($(".player-1-hand").find(".card")[2]).css("transform", "rotate(8deg)")
    $($(".player-1-hand").find(".card")[2]).css("left","330px")

    $($(".player-1-hand").find(".card")[3]).css("transform", "rotate(14deg)")
    $($(".player-1-hand").find(".card")[3]).css("left","430px").css("bottom","-300px")
  }
  if(overwolf.hearthstone.player.cards == 5) { 
    $(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-200 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
    $($(".player-1-hand").find(".card")[0]).css("transform", "rotate(-14deg)")
    $($(".player-1-hand").find(".card")[0]).css("left","130px").css("bottom","-300px")

    $($(".player-1-hand").find(".card")[1]).css("transform", "rotate(-8deg)")
    $($(".player-1-hand").find(".card")[1]).css("left","210px")

    $($(".player-1-hand").find(".card")[2]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[2]).css("left","290px")

    $($(".player-1-hand").find(".card")[3]).css("transform", "rotate(8deg)")
    $($(".player-1-hand").find(".card")[3]).css("left","370px").css("bottom","-290px")

    $($(".player-1-hand").find(".card")[4]).css("transform", "rotate(14deg)")
    $($(".player-1-hand").find(".card")[4]).css("left","450px").css("bottom","-300px")
  }
  if(overwolf.hearthstone.player.cards == 6) { 
    $(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-230 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
    $($(".player-1-hand").find(".card")[0]).css("transform", "rotate(-14deg)")
    $($(".player-1-hand").find(".card")[0]).css("left","130px").css("bottom","-300px")

    $($(".player-1-hand").find(".card")[1]).css("transform", "rotate(-8deg)")
    $($(".player-1-hand").find(".card")[1]).css("left","210px").css("bottom","-275px")

    $($(".player-1-hand").find(".card")[2]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[2]).css("left","290px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[3]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[3]).css("left","370px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[4]).css("transform", "rotate(8deg)")
    $($(".player-1-hand").find(".card")[4]).css("left","440px").css("bottom","-285px")

    $($(".player-1-hand").find(".card")[5]).css("transform", "rotate(14deg)")
    $($(".player-1-hand").find(".card")[5]).css("left","510px").css("bottom","-305px")
  }
  if(overwolf.hearthstone.player.cards == 6) { 
    $(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-230 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
    $($(".player-1-hand").find(".card")[0]).css("transform", "rotate(-14deg)")
    $($(".player-1-hand").find(".card")[0]).css("left","130px").css("bottom","-300px")

    $($(".player-1-hand").find(".card")[1]).css("transform", "rotate(-8deg)")
    $($(".player-1-hand").find(".card")[1]).css("left","210px").css("bottom","-275px")

    $($(".player-1-hand").find(".card")[2]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[2]).css("left","290px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[3]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[3]).css("left","370px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[4]).css("transform", "rotate(8deg)")
    $($(".player-1-hand").find(".card")[4]).css("left","440px").css("bottom","-285px")

    $($(".player-1-hand").find(".card")[5]).css("transform", "rotate(14deg)")
    $($(".player-1-hand").find(".card")[5]).css("left","510px").css("bottom","-305px")
  }
  if(overwolf.hearthstone.player.cards == 7) { 
    $(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-390 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
    $($(".player-1-hand").find(".card")[0]).css("transform", "rotate(-14deg)")
    $($(".player-1-hand").find(".card")[0]).css("left","130px").css("bottom","-300px")

    $($(".player-1-hand").find(".card")[1]).css("transform", "rotate(-8deg)")
    $($(".player-1-hand").find(".card")[1]).css("left","210px").css("bottom","-275px")

    $($(".player-1-hand").find(".card")[2]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[2]).css("left","290px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[3]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[3]).css("left","370px").css("bottom","-265px")

    $($(".player-1-hand").find(".card")[4]).css("transform", "rotate(0deg)")
    $($(".player-1-hand").find(".card")[4]).css("left","450px").css("bottom","-275px")

    $($(".player-1-hand").find(".card")[5]).css("transform", "rotate(8deg)")
    $($(".player-1-hand").find(".card")[5]).css("left","510px").css("bottom","-285px")

    $($(".player-1-hand").find(".card")[6]).css("transform", "rotate(14deg)")
    $($(".player-1-hand").find(".card")[6]).css("left","590px").css("bottom","-305px")
  }
}
