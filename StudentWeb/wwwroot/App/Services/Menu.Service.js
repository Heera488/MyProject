var _menu = {
	LoadMenu: function () {
		debugger
		$("#sidebarnav").load(DOMAIN_URL + "Home/_Menu", { userMenus: userdata.userMenuList }, function () {
			$('#sidebarnav').metisMenu('dispose'); //to stop and destroy metisMenu
			$('#sidebarnav').metisMenu();
		});
	}
}


$(document).ready(function () {

	_menu.LoadMenu();


});