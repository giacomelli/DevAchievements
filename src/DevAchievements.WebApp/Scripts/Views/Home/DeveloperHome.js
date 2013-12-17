$(function() {
	developerHome = {	
		init: function() {
			this.initChartsEvents();
		},
		initChartsEvents: function() {
			$('.achievement').mouseover(function() {
				developerHome.getAchievementStatChart($(this));
			});
		},
		getAchievementStatChart: function(achievement) {
			var devKey = $('#developerKey').val();
			var achievementKey = achievement.prop('id');
	
			$.proxies.developer.getAchievementHistory(devKey, achievementKey)
				.done(function(history) {
					var data = new google.visualization.DataTable();
			        data.addColumn('datetime', 'Date');
			        data.addColumn('number', 'Value');

			        for(var i = 0; i < history.length; i++)
			        {
			        	data.addRow([history[i].DateTime.fromJsonDateTimeToDate(), parseInt(history[i].Value)]);
			        }
			 
			        var options = 
			        { 
			        	'width':400, 
			        	'height':100,
			        	'legend':'none',
			        	'is3D': true,
			        	'axisTitlesPosition': 'none',
			        	'hAxis': { 'textPosition': 'none', 'gridLines': { 'color': '#ffffff' } },
			        	'vAxis': { 'textPosition': 'none', 'gridLines': { 'count': '#ffffff' } },
			        	'pointSize': 2
			        };

			        var container = document.getElementById(achievement.data('issuer') + '_chart');
			      	var chart = new google.visualization.LineChart(container);
			        chart.draw(data, options);
				});
		}
	};
});

$(function() {
	developerHome.init();    

	$('.achievement-value').each(function(i, e) {
		var element = $(e);
		var change = parseInt(element.text());

		if(change == 0)
		{
			element.addClass('achievement-value-stable');
		} 
		else if(change > 0)
		{
			element.addClass('achievement-value-up');
		} 
		else if(change < 0)
		{
			element.addClass('achievement-value-down');
		} 

		element.popover({
			title: element.prop('title'),
			trigger: 'hover',
			placement: 'top'
		});
	});  
});