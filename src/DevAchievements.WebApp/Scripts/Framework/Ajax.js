$(function() {
	app.ajax = {	
		post: function(controller, action, requestData) {
			return $.post("/" + controller + "/" + action, requestData, function(responseData) {
  				console.log(responseData)
			}, "json");
		}
	};
});

$(function() {

});
