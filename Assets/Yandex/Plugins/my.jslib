mergeInto(LibraryManager.library, {

  Hello: function () {
    window.alert("Hello, world!");
    console.log("my message")
  },

  GetPlayerName: function () {
    console.log(player.getName())
    myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
  },

  ShowAdv : function () {
        ysdk.adv.showFullscreenAdv({
        callbacks: {
            onClose: function(wasShown) {
            // some action after close
            },
            onError: function(error) {
            // some action on error
            }
        }
    })

  },

});