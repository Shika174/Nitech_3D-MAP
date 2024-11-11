mergeInto(LibraryManager.library, {
  GetCurrentPosition: function () {
    navigator.geolocation.getCurrentPosition(
      function(position) {
          xy = position.coords.latitude + "," + position.coords.longitude;   
	        SendMessage('PositionTracer', 'ReceiveLocation', xy); 
          SendMessage('TextController', 'ReceiveLocation', xy);
      });
  },
});