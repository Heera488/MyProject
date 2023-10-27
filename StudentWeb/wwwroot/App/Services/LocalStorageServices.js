//if (sessionStorage.getItem("BrowserCheck") == null) {
//	window.location.href = DOMAIN_URL + "/pagenotfound";
//}
var userdata = null;
var OpsInternalStorageUser = 0;
var OpsPDCUser = 0;
var ctr;
var retrivalrequestdata = [];
var retrivalrequestbuttonvalue = 1;
if (localStorage.getItem("currentUser") != null) {

	//userdata = JSON.parse(CryptoJS.AES.decrypt(localStorage.getItem("currentUser"), encryptkey).toString(CryptoJS.enc.Utf8));
	userdata = JSON.parse(localStorage.getItem("currentUser"));
	debugger
	var index1 = userdata.userRoles.findIndex(x => x.roleId == 5);
	if (index1!= -1) {
		OpsInternalStorageUser = 1;
	} else {
		OpsInternalStorageUser = 0;
	}
	var index2 = userdata.userRoles.findIndex(x => x.roleId ==6);
	if (index2 != -1) {
		OpsPDCUser = 1;
	} else {
		OpsPDCUser = 0;
	}


}
if (userdata != null) {
	$(".clientName").text(userdata.employeeName);
	$(".branchName").text(userdata.branchName);
	$("#ChangeUserName").val(userdata.empCode);
	// + "-" + userdata.branchName
} else {
	window.location.href = DOMAIN_URL;
}

//var locTwowheelerQuoteDetails = {};


//if (localStorage.getItem("TravelQuoteDetails") !== null && localStorage.getItem("TravelQuoteDetails") !== "null") {

//	locTravelQuoteDetails = JSON.parse(CryptoJS.AES.decrypt(localStorage.getItem("TravelQuoteDetails"), encryptkey).toString(CryptoJS.enc.Utf8));
//}

$(document).ready(function () {
	$('form').on('focus', ':input', function () {
		$(this).attr('autocomplete', 'off');
	});
	$("input").on("keypress", function (e) {
	
		if (e.which === 32 && !this.value.length) {
			
			e.preventDefault();
		}
	});
	$("input").on("change", function (e) {
		
		$(this).val($.trim( $(this).val().replace(/\s+/g, " ")));
	});
	
});
var _logout = {


	LogoutLoadCompleted: function (response) {
		
	},
	Logout: function () {

		LogOutClearLocalStorage()
		window.location.href = DOMAIN_URL;

	},
	ChangePasswordCompleted: function (response) {
		if (response.status === "SUCCESS") {
			alert("Password has been changed");
			location.reload(true);
		}
		else {
			alert("filed to change password");
			
		}
	},
	ChangePassword: function () {

		var request = {
			"username": $("#ChangeUserName").val(),
			"password": $("#password").val(),

		};
		_http.post(Api_URL + "api/UserCreation/ChangePassword", request, _logout.ChangePasswordCompleted, "")

	},
	GetUserNameLoadCompleted: function (response) {
		
		if (response.status === "SUCCESS") {
			$(".confirm").attr("disabled", false)
			$(".empolyeeName").val(response.data.getUserNameProperties[0].user_Name);
		}
		else {
			alert("Employee code does not exist");
			$(".empolyeeName").val("");
			$(ctr).val("");
			$(".confirm").attr("disabled", true)
		}
	},
	GetUserName: function (empolyeeCode) {
		$(".confirm").attr("disabled",true)
		ctr = empolyeeCode;
		var request = {
			"User_ID": empolyeeCode.value

		};
		_http.post(Api_URL + "api/General/GetUserDetails", request, _logout.GetUserNameLoadCompleted, "")

	},

}
$("#frmChangePassword").validate({
	rules: {
		
	},
	messages:
	{

	},
	submitHandler: function (form) {
		debugger
		var form = document.getElementById("frmChangePassword");
		function handleForm(event) { event.preventDefault(); }
		form.addEventListener('submit', handleForm);
		_logout.ChangePassword();
	}
});