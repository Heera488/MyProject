$(document).ready(function () {



    $("#card").hide();


});

var billdetails = [];
var bill = {
    ShowPage: function (value) {
        if (value == 1) {
            $("#frmBillEntry").show();
            $("#Billing").hide();
            $("#Delete").hide();
            $("#Remove").hide();
            bill.GetID(value);

        }
        else if (value == 2) {
            $("#frmBillEntryUpdate").show();
            $("#Billing").hide();
            $("#Delete").hide();
            $("#Remove").hide();
            bill.GetID(value);

        }
        else if (value == 3) {
            $("#frmBillEntryDelete").show();
            $("#Billing").hide();
            $("#Delete").hide();
            $("#Remove").hide();
            bill.GetID(value);

        }
        else {
            location.reload();
        }
    },
    login: function () {
        debugger

        var url = Api_URL + 'api/Bill/Login';

        var data = {

            "userId": $("#userId").val(),
            "password": $("#password").val()

        }



        //$.ajax({
        //    url: apiurl,
        //    method: 'POST',
        //    contentType: 'application/json', 
        //    data: JSON.stringify(logindata), 
        //    success: function (response) {
        //        debugger
        //    },
        //    error: function (error) {
        //        debugger
        //    }
        //});



        _http.post(url, data, function (result) {
            debugger
            if (result.loginStatus == "1") {

                window.location.href = DOMAIN_URL + 'Home/Index';
            }


        }, null)

    },
    GetID: function (value) {
        debugger;
        var data = {
            "type": value,
            "id": "0",
            "name": "0"
        }
        var url = Api_URL + 'api/Bill/GetItem';

        _http.post(url, data, function (result) {
            debugger

            if (result.isDataAvailable == true) {
                console.log(result.itmId);
                console.log(result.itemCode);
                if (value == 1) {
                    $("#ID").val(result.itmId);
                    $("#Item_Code").val(result.itemCode);
                }
                else if (value == 2) {

                    $("#Itemname1").empty();
                    $("#Itemname1").append($("<option selected disabled></option>").val("0").text("--Select Itemname.--"));
                    $.each(result.item_List, function (key, value) {

                        $("#Itemname1").append($("<option></option>").val(value.id).text(value.item_Name));
                    });
                }
                //else {
                //    $("#Itemname2").empty();
                //    $("#Itemname2").append($("<option selected disabled></option>").val("0").text("--Select Itemname.--"));
                //    $.each(result.item_List, function (key, value) {

                //        $("#Itemname2").append($("<option></option>").val(value.item_Id).text(value.item_name));
                //    });
                //}

            }
            else {
                Swal.fire({
                    icon: 'info',
                    text: result.message
                });
            }
        }, null);
    },

   
    itemdtls: function () {
        var data = {
            "type": "3",
            "id": $("#Itemname1 :selected").val(),
            "name": $("#Itemname1 :selected").text()
        }
        var url = Api_URL + 'api/Bill/GetItem';

        _http.post(url, data, function (result) {
            debugger

            if (result.isDataAvailable == true) {
                $("#ID1").val(result.item_List[0].id);
                $("#Quantity1").val(result.item_List[0].quantity);
                $("#Price1").val(result.item_List[0].price);
                $("#Item_Code1").val(result.item_List[0].item_Code);
                $("#Createdby1").val(result.item_List[0].createdby);
                $("#ModifiedBy1").val(result.item_List[0].modifiedBy);



            }
            else {
                Swal.fire({
                    icon: 'info',
                    text: result.message
                });
            }
        }, null);
    },
    Submit: function () {
        debugger
        var rqstlist = 1 + "÷" + $("#ID").val() + "÷" + $("#Item_Code").val() + "÷" + $("#Quantity").val() + "÷" + $("#Price").val() + "÷" + $("#Createdby").val() + "÷" + $("#ModifiedBy").val() + "÷" + $("#Item_Name").val();
        //var rqstlist = 1 + "÷" + $("#ID").val() + "÷" + $("#Item_Code").val() + "÷" + $("#Item_Name").val() + "÷" + $("#Createdby").val() + "÷" + $("#ModifiedBy").val();



        var data = {


            "Rqstlist": rqstlist


        }

        var url = Api_URL + 'api/Bill/BillingSubmit';
        debugger

        _http.post(url, data, function (result) {
            debugger

            alert(result.message);

        })

    },

    Update: function () {
        debugger


        var rqstlist = 2 + "÷" + $("#Itemname1 :selected").val() + "÷" + $("#Item_Code1").val() + "÷" + $("#Quantity1").val() + "÷" + $("#Price1").val() + "÷" + $("#Createdby1").val() + "÷" + $("#ModifiedBy1").val() + "÷" + $("#Itemname1 :selected").text();


        var data = {


            "Rqstlist": rqstlist


        }
        var url = Api_URL + 'api/Bill/BillingSubmit';
        _http.post(url, data, function (result) {
            debugger

            alert(result.message);

        }, null)


    },
    Delete: function () {
        debugger


        var rqstlist = 3 + "÷" + $("#Itemname1 :selected").val() + "÷" + $("#Item_Code1").val() + "÷" + $("#Quantity1").val() + "÷" + $("#Price1").val() + "÷" + $("#Createdby1").val() + "÷" + $("#ModifiedBy1").val() + "÷" + $("#Itemname1 :selected").text();


        var data = {


            "Rqstlist": rqstlist


        }
        var url = Api_URL + 'api/Bill/BillingSubmit';
        _http.post(url, data, function (result) {
            debugger

            alert(result.message);

        }, null)

    },
}

