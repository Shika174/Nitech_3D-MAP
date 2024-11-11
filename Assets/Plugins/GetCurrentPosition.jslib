mergeInto(LibraryManager.library, {
  GetCurrentPosition: function () {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function(position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            var location = latitude + "," + longitude;
            console.log("位置情報を取得しました: " + location);
            SendMessage('PositionTracer', 'ReceiveLocation', location);
        }, function(error) {
            console.error("位置情報の取得に失敗しました: " + error.message);
        });
    } else {
        console.error("このブラウザはGeolocation APIをサポートしていません");
    }
  },
});