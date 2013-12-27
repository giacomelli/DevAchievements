/*
Simple OpenID Plugin
http://code.google.com/p/openid-selector/

This code is licensed under the New BSD License.
*/

var providers_large = {
	google : {
		name : 'Google',
		url : 'Google'
	},
	facebook : {
		name : 'Facebook',
		url : 'Facebook'
	},
	yahoo : {
		name : 'Yahoo',
		url : 'Yahoo'
	}
};

var providers_small = {
	twitter : {
		name : 'Twitter',
		url : 'Twitter'
	},
	openid : {
		name : 'OpenID',
		label : 'Enter your OpenID.',
		url : null 
	}
};

openid.locale = 'en';
openid.sprite = 'en'; 
openid.signin_text = 'Sign-In';
openid.image_title = 'log in with {provider}';
openid.img_path = "Scripts/openid-selector/images/";

