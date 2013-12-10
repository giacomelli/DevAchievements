$(function() {
	create = {	
		
	};
});

$(function() {
	var issuers = $('[id$=__IssuerName]');
	$('[id$=__Username]').each(function(i, e) {
		$(this).blur(function() {
			var field = $(this);

			if(field.val().length > 0) {
				var container = field.parent();
				container.addClass('has-warning');
				container.removeClass('has-success has-error');
				field.next().remove();
				field.after($('<div class="alert alert-warning">Checking user...</div>'));
		
				$.proxies.developer.existsDeveloperAccountAtIssuer($(issuers[i]).val(), $(this).val())
					.done(function(exists) {
						container.removeClass('has-warning');
						field.next().remove();

						if(exists) {
							container.addClass('has-success');
						} 
						else {
							container.addClass('has-error');
							field.after($('<div class="alert alert-danger" class=`>User not found.</div>'));
						}
				});
			}
		});
	});

	$('#Email').keyup(function() {
		$('#gravatar').empty().append($.gravatar($(this).val()));
	});
	$('#Email').keyup();

});
