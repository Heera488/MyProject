
var DOMAIN_URL = "http://localhost:5248/";
var HOME_URL = "http://localhost:64426/home/index";

var Api_URL = "http://localhost:5143/";




//$(".login-sidebar").css("background-image", "url(" + DOMAIN_URL + "Public/assets/images/cover.jpg)");
$("#login_roundSrc").attr("src", DOMAIN_URL + "Public/assets/images/user.png");
var encryptkey = "fmsappkey"; 
 

function LogOutClearLocalStorage() {
    debugger
    localStorage.removeItem('currentUser');
    window.location.href = DOMAIN_URL;
}

function ClearLocalStorage() {


    //localStorage.removeItem('TwowheelerQuoteDetails');
   
}
function ClearLocalStorageAfterProceed() {


    //localStorage.removeItem('TwowheelerQuoteDetails');
   
}



