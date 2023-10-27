var _http = {

    post: function (url, data, success, AuthorizationToken) {
        debugger
        $("#overlay").show();
        return $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            datatype: 'json',
            traditional: true,
            data: JSON.stringify(data),
            headers: {
                "X-Content-Type-Options": "nosniff",
                "Access-Control-Allow-Headers": false,
                "Authorization": AuthorizationToken
            },
            success: function (resp, textStatus, xhr) {
                debugger
                $("#overlay").hide();
                if (xhr.status === 200) {
                    success(resp);
                }
                setTimeout(function () {
                    $("#overlay").fadeOut(300);
                }, 500);
            },
            error: function (xhr, textStatus, errorThrown) {
                debugger
                $("#overlay").hide();
                switch (xhr.status) {
                    case 400:
                        //Swal.fire(
                        //    'Failed',
                        //    "Bad Request",

                        //    'warning'
                        //);
                        $("#btnSave").removeAttr("disabled");
                        break;
                    
                    case 403:
                        //Swal.fire({
                        //    title: 'Token Expired ',
                        //    text: "Your Token Expired, Please Try Again after Login",
                        //    icon: 'warning',
                        //    confirmButtonText: 'Ok'
                        //}).then((result) => {
                        //    if (result.value) {
                        //        //  _common.Logout();
                        //        LogOutClearLocalStorage();
                        //        sessionStorage.removeItem("LoginStatus");
                        //        setTimeout(function () { window.location.href = DOMAIN_URL; }, 6000);
                        //    }
                        //});
                        break;
                    case 503:
                        //Swal.fire(
                        //    'Failed',
                        //    "Service Unavailable, Please Try Again Later",

                        //    'warning'
                        //);
                        //$("#btnSave").removeAttr("disabled");
                        break;
                    default:
                        //Swal.fire(
                        //    'Failed',
                        //    "Please Check Your Internet Connection And Try Again",
                        //    'warning'
                        //);
                        /*$("#btnSave").removeAttr("disabled");*/
                        break;
                }
                setTimeout(function () {
                    $("#overlay").fadeOut(300);
                }, 500);
            }
        });
    },
    get: function (url, success, AuthorizationToken) {
        return $.ajax({
            url: url,
            type: 'GET',
            contentType: 'application/json',
            datatype: 'json',
            traditional: true,
            headers: {
                "X-Content-Type-Options": "nosniff",
                "Access-Control-Allow-Headers": false,       
                "Authorization": AuthorizationToken
            },
            success: function (resp, textStatus, xhr) {
                if (xhr.status === 200) {
                    success(resp);
                }
                setTimeout(function () {
                    $("#overlay").fadeOut(300);
                }, 500);
            },
            error: function (xhr, textStatus, errorThrown) {

                switch (xhr.status) {
                    case 403:
                        //Swal.fire({
                        //    title: 'Token Expired ',
                        //    text: "Your Token Expired, Please Try Again after Login",
                        //    icon: 'warning',
                        //    confirmButtonText: 'Ok',
                        //}).then((result) => {
                        //    if (result.value) {
                        //        // _common.Logout();
                        //        LogOutClearLocalStorage();
                        //        sessionStorage.removeItem("LoginStatus");
                        //        setTimeout(function () { window.location.href = DOMAIN_URL; }, 6000);
                        //    }
                        //});
                        break;
                    case 503:
                        //Swal.fire(
                        //    'Failed',
                        //    "Service Unavailable, Please Try Again Later",
                        //    'warning'
                        //);
                        //$("#btnSave").removeAttr("disabled");
                        break;
                    default:
                        //Swal.fire(
                        //    'Failed',
                        //    "Please Check Your Internet Connection And Try Again",
                        //    'warning'
                        //);
                        //$("#btnSave").removeAttr("disabled");
                        break;
                }
                setTimeout(function () {
                    $("#overlay").fadeOut(300);
                }, 500);
            }
        });
    },
    //CustomentPaymentpost: function (url, data, success, AuthorizationToken) {

    //    return $.ajax({
    //        url: url,
    //        type: 'POST',
    //        contentType: 'application/json',
    //        datatype: 'json',
    //        traditional: true,
    //        data: JSON.stringify(data),
    //        headers: {
    //            "Authorization": AuthorizationToken
    //        },
    //        success: function (resp, textStatus, xhr) {
    //            if (xhr.status === 200) {
    //                success(resp);
    //            }
    //            setTimeout(function () {
    //                $("#overlay").fadeOut(300);
    //            }, 500);
    //        },
    //        error: function (xhr, textStatus, errorThrown) {
    //            //$('.page-loader-wrapper').hide();
    //            debugger
    //            if (xhr.status === 403) {
    //                swal("", "Your token expired please try after login", "error");

    //                setTimeout(function () { window.location.href = DOMAIN_URL; }, 6000);

    //            } else {
    //                //alert("some error occurred please try again");
    //                //failure(xhr);
    //            }
    //            setTimeout(function () {
    //                $("#overlay").fadeOut(300);
    //            }, 500);
    //        }

    //    });
    //},
    //postWithHeadersData: function (url, data, success, AuthorizationToken) {

    //    return $.ajax({
    //        url: url,
    //        type: 'POST',
    //        contentType: 'application/json',
    //        datatype: 'json',
    //        traditional: true,
    //        headers: {
    //            "Authorization": AuthorizationToken,
    //            "PropertyData": data
    //        },
    //        success: function (resp, textStatus, xhr) {
    //            debugger
    //            if (xhr.status === 200) {

    //                success(code.decryptMessage(resp));
    //            }
    //            setTimeout(function () {
    //                $("#overlay").fadeOut(300);
    //            }, 500);
    //        },
    //        error: function (xhr, textStatus, errorThrown) {
    //            //$('.page-loader-wrapper').hide();
    //            debugger
    //            if (xhr.status === 403) {
    //                swal("", "Your token expired please try after login", "error");

    //                setTimeout(function () { window.location.href = DOMAIN_URL; }, 6000);

    //            } else {
    //                //alert("some error occurred please try again");
    //                //failure(xhr);
    //            }
    //        }
    //    });
    //},


};