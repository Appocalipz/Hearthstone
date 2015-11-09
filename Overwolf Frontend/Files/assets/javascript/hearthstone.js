overwolf.hearthstone={};
overwolf.hearthstone.player={};
overwolf.hearthstone.config={};
overwolf.hearthstone.download={};
overwolf.hearthstone.cards={};
overwolf.hearthstone.click={};
overwolf.hearthstone.key={};

overwolf.hearthstone.init = function card(cardName){
  var mainBody = $("<div></div>").addClass("main-body");
  $("<div></div>").addClass("player-1-hand hand").appendTo(mainBody);
  $("<div></div>").addClass("player-2-hand hand").appendTo(mainBody);
  $("<div></div>").addClass("player-1-mincards").appendTo(mainBody);
  $("<div></div>").addClass("player-2-mincards").appendTo(mainBody);
  $("<div></div>").addClass("player-1-counters").appendTo(mainBody);
  $("<div></div>").addClass("player-2-counters").appendTo(mainBody);
  $("body").append(mainBody)
};

overwolf.hearthstone.cards.click = function click(){
  $(".big-card").click(function() {
    if (typeof inDeckCards[0][$(this).attr("cardid")] == "undefined") {
      inDeckCards[0][$(this).attr("cardid")] = 1;
      overwolf.hearthstone.player.addDeckCard(2, $(this).attr("cardid"), $(this).attr("cost"), $(this).attr("name"), $(this).attr("name").replace(/\ /g, "-").replace(/\'/g, "-").toLowerCase()+".png");
    }
    else if (inDeckCards[0][$(this).attr("cardid")] < 2) {
      inDeckCards[0][$(this).attr("cardid")] = inDeckCards[0][$(this).attr("cardid")] + 1;
      overwolf.hearthstone.player.addDeckCard(2, $(this).attr("cardid"), $(this).attr("cost"), $(this).attr("name"), $(this).attr("name").replace(/\ /g, "-").replace(/\'/g, "-").toLowerCase()+".png");
    }
    else {
      console.log("Deck already contains 2 of these");
    }

  });
};

overwolf.hearthstone.cards.loadAll = function all(path, classset){
  overwolf.io.getTextFile(path, function (cardxml) {
    var parsedCardXml = JSON.parse(cardxml);
    $.map( parsedCardXml, function( xmlTopLevelValue ) {
      $.map( xmlTopLevelValue, function( xmlMidLevelValue ) {
        if (typeof xmlMidLevelValue.img != "undefined") {
          if (pageCardsCount < pageCardCount && xmlMidLevelValue.health != 30) {
            pageCardsCount = pageCardsCount + 1;
            $(classset).append($("<img src='" + xmlMidLevelValue.img + "'>").addClass("big-card")
                .attr("collectible",(typeof xmlMidLevelValue.collectible != "undefined" ? xmlMidLevelValue.collectible : ""))
                .attr("faction",(typeof xmlMidLevelValue.faction != "undefined" ? xmlMidLevelValue.faction : ""))
                .attr("flavor",(typeof xmlMidLevelValue.flavor != "undefined" ? xmlMidLevelValue.flavor : ""))
                .attr("howToGet",(typeof xmlMidLevelValue.howToGet != "undefined" ? xmlMidLevelValue.howToGet : ""))
                .attr("howToGetGold",(typeof xmlMidLevelValue.howToGetGold != "undefined" ? xmlMidLevelValue.howToGetGold : ""))
                .attr("playerClass",(typeof xmlMidLevelValue.playerClass != "undefined" ? xmlMidLevelValue.playerClass : ""))
                .attr("cardId",(typeof xmlMidLevelValue.cardId != "undefined" ? xmlMidLevelValue.cardId : ""))
                .attr("cardSet",(typeof xmlMidLevelValue.cardSet != "undefined" ? xmlMidLevelValue.cardSet : ""))
                .attr("cost",(typeof xmlMidLevelValue.cost != "undefined" ? xmlMidLevelValue.cost : ""))
                .attr("health",(typeof xmlMidLevelValue.health != "undefined" ? xmlMidLevelValue.health : ""))
                .attr("img",(typeof xmlMidLevelValue.img != "undefined" ? xmlMidLevelValue.img : ""))
                .attr("imgGold",(typeof xmlMidLevelValue.imgGold != "undefined" ? xmlMidLevelValue.imgGold : ""))
                .attr("locale",(typeof xmlMidLevelValue.locale != "undefined" ? xmlMidLevelValue.locale : ""))
                .attr("name",(typeof xmlMidLevelValue.name != "undefined" ? xmlMidLevelValue.name : ""))
                .attr("rarity",(typeof xmlMidLevelValue.rarity != "undefined" ? xmlMidLevelValue.rarity : ""))
                .attr("text",(typeof xmlMidLevelValue.text != "undefined" ? xmlMidLevelValue.text : ""))
                .attr("type",(typeof xmlMidLevelValue.type != "undefined" ? xmlMidLevelValue.type : ""))
            );
            pageCards.push(xmlMidLevelValue);
            activeCards = pageCards;
          }
          else if (xmlMidLevelValue.health != 30) {
            totalCardCount = totalCardCount + 1;
            pageCards.push(xmlMidLevelValue);
            activeCards = pageCards;
          }
        }
      });
    });
    overwolf.hearthstone.cards.click();
  });
};


overwolf.hearthstone.key.up = function nextPage(classset02, searchBox){
  $(searchBox).keyup(function() {
    currentPage = 1;
    activeCards = [];
    var currentCardCount = 0;
    var maxCardCount = pageCardCount;
    $(classset02).html("");
    $.map( pageCards, function( card ) {
      if (currentCardCount != maxCardCount && card.name.indexOf($(searchBox).val()) >= 0) {
        currentCardCount = currentCardCount + 1;
        $(classset02).append($("<img src='" + card.img + "'>").addClass("big-card")
            .attr("collectible",(typeof card.collectible != "undefined" ? card.collectible : ""))
            .attr("faction",(typeof card.faction != "undefined" ? card.faction : ""))
            .attr("flavor",(typeof card.flavor != "undefined" ? card.flavor : ""))
            .attr("howToGet",(typeof card.howToGet != "undefined" ? card.howToGet : ""))
            .attr("howToGetGold",(typeof card.howToGetGold != "undefined" ? card.howToGetGold : ""))
            .attr("playerClass",(typeof card.playerClass != "undefined" ? card.playerClass : ""))
            .attr("cardId",(typeof card.cardId != "undefined" ? card.cardId : ""))
            .attr("cardSet",(typeof card.cardSet != "undefined" ? card.cardSet : ""))
            .attr("cost",(typeof card.cost != "undefined" ? card.cost : ""))
            .attr("health",(typeof card.health != "undefined" ? card.health : ""))
            .attr("img",(typeof card.img != "undefined" ? card.img : ""))
            .attr("imgGold",(typeof card.imgGold != "undefined" ? card.imgGold : ""))
            .attr("locale",(typeof card.locale != "undefined" ? card.locale : ""))
            .attr("name",(typeof card.name != "undefined" ? card.name : ""))
            .attr("rarity",(typeof card.rarity != "undefined" ? card.rarity : ""))
            .attr("text",(typeof card.text != "undefined" ? card.text : ""))
            .attr("type",(typeof card.type != "undefined" ? card.type : "")));
      }
      if(typeof card.name == "undefined") return false;
      else {
        if(card.name.indexOf($(searchBox).val()) >= 0){
          activeCards.push(card);
          return true;
        }
        else return false;
      }
    });
    overwolf.hearthstone.cards.click();
  });
};

overwolf.hearthstone.click.nextPage = function nextPage(clickElement, classset){
  $(clickElement).click(function() {
    if(activeCards.length > (pageCardCount * currentPage)) {
      currentPage = currentPage + 1;
      $(classset).html("");
      $.map( activeCards, function( card, i ) {
        if (i >= ((currentPage * pageCardCount) - pageCardCount) && i < (currentPage * pageCardCount))
          $(classset).append($("<img src='" + card.img + "'>").addClass("big-card").addClass("big-card")
              .attr("collectible",(typeof card.collectible != "undefined" ? card.collectible : ""))
              .attr("faction",(typeof card.faction != "undefined" ? card.faction : ""))
              .attr("flavor",(typeof card.flavor != "undefined" ? card.flavor : ""))
              .attr("howToGet",(typeof card.howToGet != "undefined" ? card.howToGet : ""))
              .attr("howToGetGold",(typeof card.howToGetGold != "undefined" ? card.howToGetGold : ""))
              .attr("playerClass",(typeof card.playerClass != "undefined" ? card.playerClass : ""))
              .attr("cardId",(typeof card.cardId != "undefined" ? card.cardId : ""))
              .attr("cardSet",(typeof card.cardSet != "undefined" ? card.cardSet : ""))
              .attr("cost",(typeof card.cost != "undefined" ? card.cost : ""))
              .attr("health",(typeof card.health != "undefined" ? card.health : ""))
              .attr("img",(typeof card.img != "undefined" ? card.img : ""))
              .attr("imgGold",(typeof card.imgGold != "undefined" ? card.imgGold : ""))
              .attr("locale",(typeof card.locale != "undefined" ? card.locale : ""))
              .attr("name",(typeof card.name != "undefined" ? card.name : ""))
              .attr("rarity",(typeof card.rarity != "undefined" ? card.rarity : ""))
              .attr("text",(typeof card.text != "undefined" ? card.text : ""))
              .attr("type",(typeof card.type != "undefined" ? card.type : "")));
      });
      overwolf.hearthstone.cards.click();
    }
  });
};

overwolf.hearthstone.click.previousPage = function nextPage(clickElement, classset){
  $(clickElement).click(function() {
    if(currentPage > 1 && activeCards.length > (pageCardCount * currentPage)) {
      currentPage = currentPage - 1;
      $(classset).html("");
      $.map( activeCards, function( card, i ) {
        if (i >= ((currentPage * pageCardCount) - pageCardCount) && i < (currentPage * pageCardCount))
          $(classset).append($("<img src='" + card.img + "'>").addClass("big-card").addClass("big-card")
              .attr("collectible",(typeof card.collectible != "undefined" ? card.collectible : ""))
              .attr("faction",(typeof card.faction != "undefined" ? card.faction : ""))
              .attr("flavor",(typeof card.flavor != "undefined" ? card.flavor : ""))
              .attr("howToGet",(typeof card.howToGet != "undefined" ? card.howToGet : ""))
              .attr("howToGetGold",(typeof card.howToGetGold != "undefined" ? card.howToGetGold : ""))
              .attr("playerClass",(typeof card.playerClass != "undefined" ? card.playerClass : ""))
              .attr("cardId",(typeof card.cardId != "undefined" ? card.cardId : ""))
              .attr("cardSet",(typeof card.cardSet != "undefined" ? card.cardSet : ""))
              .attr("cost",(typeof card.cost != "undefined" ? card.cost : ""))
              .attr("health",(typeof card.health != "undefined" ? card.health : ""))
              .attr("img",(typeof card.img != "undefined" ? card.img : ""))
              .attr("imgGold",(typeof card.imgGold != "undefined" ? card.imgGold : ""))
              .attr("locale",(typeof card.locale != "undefined" ? card.locale : ""))
              .attr("name",(typeof card.name != "undefined" ? card.name : ""))
              .attr("rarity",(typeof card.rarity != "undefined" ? card.rarity : ""))
              .attr("text",(typeof card.text != "undefined" ? card.text : ""))
              .attr("type",(typeof card.type != "undefined" ? card.type : "")));
      });
      overwolf.hearthstone.cards.click();
    }
  });
};


overwolf.hearthstone.config.path = function path(){
  return "Temp/Logs/";
};

overwolf.hearthstone.addcard = function card(cardName){
  $(".main-body").append($("<img>").attr("src","Files/assets/images/cards/" + cardName).addClass("active-card card faceup-card"));
};

overwolf.hearthstone.download.img = function card(filename, url){
};

overwolf.hearthstone.download.imggold = function card(filename, url){
  $(".main-body").append($("<img>").attr("src","Files/assets/images/cards/" + cardName).addClass("active-card card faceup-card"));
};

overwolf.hearthstone.player.addHDCount = function card(player, hand, deck){
  handDeckContainer = $("<div></div>").addClass("hand-deck-container").appendTo(".player-"+ player +"-counters");
  hand = $("<div></div>").html("Hand: " + hand).addClass("player-"+player+"-hand-count").appendTo(handDeckContainer);
  deck = $("<div></div>").html("Deck: " + deck).addClass("player-"+player+"-deck-count").appendTo(handDeckContainer);
};

overwolf.hearthstone.player.addDeckCard = function card(player, id, diamonds, title, image){
  if(totalCardsInLinkedDeck != 30) {
    if (typeof $(".card-id-" + id).html() == "undefined") {
      totalCardsInLinkedDeck = totalCardsInLinkedDeck + 1;
      minItemContainer = $("<div></div>").attr("cardCount", 1).addClass("container-min-" + player + " card-id-" + id).attr("cardid",id).appendTo(".player-" + player + "-mincards").click(function() {
        if ($(this).attr("cardCount") == "2") {
          $(this).attr("cardCount","1");
          $(this).find(".cards-in-deck-2").remove();
          totalCardsInLinkedDeck = totalCardsInLinkedDeck - 1;
          inDeckCards[0][$(this).attr("cardid")] = 1;
        }
        else {
          $(this).remove();
          totalCardsInLinkedDeck = totalCardsInLinkedDeck - 1;
          inDeckCards[0][$(this).attr("cardid")] = 0;
          console.log("remove")
        }
      });
      frame = $("<img>").attr("src", "Files/assets/images/frame/frame.png").addClass("frame-min").appendTo(minItemContainer);
      frameRight = $("<img>").attr("src", "Files/assets/images/frame/frame-right.png").addClass("frame-right-min").appendTo(minItemContainer);
      number = $("<div></div>").html(diamonds).addClass("number-min").appendTo(minItemContainer);
      title = $("<div></div>").html(title).addClass("title-min").appendTo(minItemContainer);
      image = $("<img>").attr("src", "Files/assets/images/card-stripes/" + image).addClass("card-min").appendTo(minItemContainer);
      frameRight.animate({
        opacity: 0
      }, 5000, function () {
      });
      if (player == 1) {
        minItemContainer.animate({
          left: 0
        }, 2000, function () {
        });
      }
      else if (player == 2) {
        minItemContainer.animate({
          right: 0
        }, 2000, function () {
        });
      }
    }
    else {
      if ($(".card-id-" + id).attr("cardCount") != "2") {
        totalCardsInLinkedDeck = totalCardsInLinkedDeck + 1;
        $(".card-id-" + id).attr("cardCount", 2);
        $(".card-id-" + id).append("<img src='Files/assets/images/main/frame_2.png' class='cards-in-deck-2'>");
      }
    }
  }
};

overwolf.hearthstone.player.cards = 0;

overwolf.hearthstone.player.addCard = function card(pos, cardName){
  $(".player-" + pos + "-hand")
	  .append($("<img>")
	  .attr("src","Files/assets/images/cards/" + cardName)
	  .addClass("active-card card faceup-card")
	  .css("position","absolute")
	  .css("left", (overwolf.hearthstone.player.cards * 180) + "px")
	);
  overwolf.hearthstone.player.cards = overwolf.hearthstone.player.cards + 1;
  if(overwolf.hearthstone.player.cards < 4) { 
  	$(".player-1-hand").css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
  		.css("margin-left", -((20 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px")
  }
  if(overwolf.hearthstone.player.cards == 4) {
    var element =  $(".player-1-hand");
    element.css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px").css("margin-left", -(((190 * overwolf.hearthstone.player.cards)) / 2) + "px");
    $(element.find(".card")[0]).css("transform", "rotate(-14deg)");
    $(element.find(".card")[0]).css("left","130px").css("bottom","-300px");
    $(element.find(".card")[1]).css("transform", "rotate(-8deg)");
    $(element.find(".card")[1]).css("left","230px");
    $(element.find(".card")[2]).css("transform", "rotate(8deg)");
    $(element.find(".card")[2]).css("left","330px");
    $(element.find(".card")[3]).css("transform", "rotate(14deg)");
    $(element.find(".card")[3]).css("left","430px").css("bottom","-300px");
  }
  if(overwolf.hearthstone.player.cards == 5) {
    var element =  $(".player-1-hand");
    element.css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-200 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px");
    $(element.find(".card")[0]).css("transform", "rotate(-14deg)");
    $(element.find(".card")[0]).css("left","130px").css("bottom","-300px");
    $(element.find(".card")[1]).css("transform", "rotate(-8deg)");
    $(element.find(".card")[1]).css("left","210px");
    $(element.find(".card")[2]).css("transform", "rotate(0deg)");
    $(element.find(".card")[2]).css("left","290px");
    $(element.find(".card")[3]).css("transform", "rotate(8deg)");
    $(element.find(".card")[3]).css("left","370px").css("bottom","-290px");
    $(element.find(".card")[4]).css("transform", "rotate(14deg)");
    $(element.find(".card")[4]).css("left","450px").css("bottom","-300px");
  }
  if(overwolf.hearthstone.player.cards == 6) {
    var element =  $(".player-1-hand");
    element.css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-230 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px");
    $(element.find(".card")[0]).css("transform", "rotate(-14deg)");
    $(element.find(".card")[0]).css("left","130px").css("bottom","-300px");
    $(element.find(".card")[1]).css("transform", "rotate(-8deg)");
    $(element.find(".card")[1]).css("left","210px").css("bottom","-275px");
    $(element.find(".card")[2]).css("transform", "rotate(0deg)");
    $(element.find(".card")[2]).css("left","290px").css("bottom","-265px");
    $(element.find(".card")[3]).css("transform", "rotate(0deg)");
    $(element.find(".card")[3]).css("left","370px").css("bottom","-265px");
    $(element.find(".card")[4]).css("transform", "rotate(8deg)");
    $(element.find(".card")[4]).css("left","440px").css("bottom","-285px");
    $(element.find(".card")[5]).css("transform", "rotate(14deg)");
    $(element.find(".card")[5]).css("left","510px").css("bottom","-305px");
  }
  if(overwolf.hearthstone.player.cards == 6) {
    var element =  $(".player-1-hand");
    element.css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-230 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px");
    $(element.find(".card")[0]).css("transform", "rotate(-14deg)");
    $(element.find(".card")[0]).css("left","130px").css("bottom","-300px");
    $(element.find(".card")[1]).css("transform", "rotate(-8deg)");
    $(element.find(".card")[1]).css("left","210px").css("bottom","-275px");
    $(element.find(".card")[2]).css("transform", "rotate(0deg)");
    $(element.find(".card")[2]).css("left","290px").css("bottom","-265px");
    $(element.find(".card")[3]).css("transform", "rotate(0deg)");
    $(element.find(".card")[3]).css("left","370px").css("bottom","-265px");
    $(element.find(".card")[4]).css("transform", "rotate(8deg)");
    $(element.find(".card")[4]).css("left","440px").css("bottom","-285px");
    $(element.find(".card")[5]).css("transform", "rotate(14deg)");
    $(element.find(".card")[5]).css("left","510px").css("bottom","-305px");
  }
  if(overwolf.hearthstone.player.cards == 7) {
    var element =  $(".player-1-hand");
    element.css("width", (20 + (190 * overwolf.hearthstone.player.cards)) + "px")
      .css("margin-left", -((-390 + (190 * overwolf.hearthstone.player.cards)) / 2) + "px");
    $(element.find(".card")[0]).css("transform", "rotate(-14deg)");
    $(element.find(".card")[0]).css("left","130px").css("bottom","-300px");
    $(element.find(".card")[1]).css("transform", "rotate(-8deg)");
    $(element.find(".card")[1]).css("left","210px").css("bottom","-275px");
    $(element.find(".card")[2]).css("transform", "rotate(0deg)");
    $(element.find(".card")[2]).css("left","290px").css("bottom","-265px");
    $(element.find(".card")[3]).css("transform", "rotate(0deg)");
    $(element.find(".card")[3]).css("left","370px").css("bottom","-265px");
    $(element.find(".card")[4]).css("transform", "rotate(0deg)");
    $(element.find(".card")[4]).css("left","450px").css("bottom","-275px");
    $(element.find(".card")[5]).css("transform", "rotate(8deg)");
    $(element.find(".card")[5]).css("left","510px").css("bottom","-285px");
    $(element.find(".card")[6]).css("transform", "rotate(14deg)");
    $(element.find(".card")[6]).css("left","590px").css("bottom","-305px");
  }
}
