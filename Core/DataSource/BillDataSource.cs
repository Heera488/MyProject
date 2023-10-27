using DTO.Data;
using DTO.Request;
using DTO.Response;
using FMS.Helpers;
using MFAUDIT.Persistence;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.DataSource
{
    public class BillDataSource
    {
        private readonly static Lazy<BillDataSource> m_instance;

        public static BillDataSource Instance
        {
            get
            {
                return BillDataSource.m_instance.Value;
            }
        }

        static BillDataSource()
        {
            BillDataSource.m_instance = new Lazy<BillDataSource>(() => new BillDataSource());

        }


        public itemResponse ItemGet(ItemRequest request)
        {

            itemResponse response = new itemResponse();

            if (request.type == "1")
            {
                string query = "select max(t.id)itmId from ASS_ITEM_MASTER t";
                object id = new OracleHelper().ExecuteScalar<object>(query);

                string qry = "select  max(t.code)itemCode from ASS_ITEM_MASTER t";
                object code = new OracleHelper().ExecuteScalar<object>(qry);

                int newid = Int32.Parse(id.ToString());
                int newcode = Int32.Parse(code.ToString());

                int itmId = newid + 1;
                int itemCode = newcode + 1;

                if (itmId > 0 && itemCode > 0)
                {
                    response.itmId = itmId;
                    response.itemCode = itemCode;
                    response.isDataAvailable = true;
                    response.message = "SUCCESS";
                }
                else
                {
                    response.message = "FAIL";
                }
            }
            else if(request.type == "2")
            {
               string qry = "select to_char(t.id)ID ,to_char(t.name)Item_Name  from ass_item_master t";

                response.item_List = new OracleHelper().GetRecords<ItemData>(qry);
                if (response.item_List!=null)
                {
                    
                    response.isDataAvailable = true;
                    response.message = "SUCCESS";
                }
                else
                {
                    response.message = "FAIL";
                }
            }
            else if (request.type == "3")
            {
                string qry = "select to_char(t.id)ID ,to_char(t.code)Item_Code,to_char(t.name)Item_Name,to_char(t.createdby)Createdby," +
                     "to_char(to_date(t.createdon))Createdon,to_char(t.modifiedby)ModifiedBy,to_char(to_date(t.modifiedon))Modifiedon,to_char(t.quantity)Quantity,to_char(t.price)Price  from ass_item_master t where t.name='"+request.name+"' and t.id='"+request.id+"'";

                response.item_List = new OracleHelper().GetRecords<ItemData>(qry);
                if (response.item_List != null)
                {

                    response.isDataAvailable = true;
                    response.message = "SUCCESS";
                }
                else
                {
                    response.message = "FAIL";
                }
            }
            return response;
        }
        //public BillinsertResponse BillingSubmit(BillRequest request)
        //{
        //    BillinsertResponse response = new BillinsertResponse();


        //    string query = "select count(t.billno) from ass_bill_details t";
        //    decimal a = new OracleHelper().ExecuteScalar<decimal>(query);
        //    int slno = 99;
        //    if (a > 0)
        //    {
        //        string qery = "select max(t.billno) from ass_bill_details t";
        //        object slnomax = new OracleHelper().ExecuteScalar<object>(qery);


        //        slno = Int32.Parse(slnomax.ToString());


        //    }

        //    var slno1 = slno + 1;
        //    //string query1 = "insert into ass_bill_details (billno,customer,item,rate,quantity,amount,purchasedate)values('" + slno1 + "','" + request.customerName + "','" + request.itemName + "','" + request.itemRate + "','" + request.count + "','" + request.Amount + "',sysdate)";
        //    //var b = new OracleHelper().ExecuteNonQuery(query1);
        //    //if ()
        //    //{
        //    //    response.isDataAvailable = true;
        //    //    response.Message = "insert successfully";

        //    //}
        //    //else
        //    //{
        //    //    response.Message = "can't insert Item";
        //    //}
        //    return response;

        //}


        public BillinsertResponse BillingSubmit(BillRequest request)
        {
            BillinsertResponse response = new BillinsertResponse();
            try
            {
                OracleParameter[] parm_c = new OracleParameter[2];


                parm_c[0] = new OracleParameter("InstrM", OracleDbType.Varchar2);
                parm_c[0].Value = request.Rqstlist;
                parm_c[0].Direction = ParameterDirection.Input;

                parm_c[1] = new OracleParameter("msg", OracleDbType.Varchar2, 1000);
                parm_c[1].Direction = ParameterDirection.Output;

                new OracleHelper().ExecuteNonQuery("Item_heera", parm_c);

                response.Message = parm_c[1].Value.ToString();
                var result = response.Message.Split('^');
                if (result[0] == "Success")
                {
                    response.isDataAvailable = true;
       
                    response.Message = "SUCCESS";

                }
                else
                {
                    response.isDataAvailable = false;
                    response.Message = "FAIL";
                }
            }
            catch (Exception ex)
            {

                response.Message = ex.Message;
            }
            return response;
        }
        
        public LoginResponse Login(LoginRequest request)
        {
            LoginResponse LoginResponse = new LoginResponse();

            string qry = "";



            qry = "select count(*) from ASS_EMPLOYEE_MASTER t where t.userid='" + request.userId + "' and t.password='" + request.password + "'";
            decimal i = new OracleHelper().ExecuteScalar<decimal>(qry);

            if (i > 0)
            {

                var token = CreateToken(request.userId);
                qry = "insert into ass_login_token_handler(USERID,TOKEN,STATUS,STARTTIME,ENTTIME) values('" + request.userId + "',' " + token + "',1,sysdate,sysdate + (.000694 * 30))";
                var b = new OracleHelper().ExecuteNonQuery(qry);
                if (i == 1)
                {
                    LoginResponse.loginStatus = 1;
                }
                else
                {
                    LoginResponse.loginStatus = 0;
                }
            }
            else
            {
                LoginResponse.loginStatus = 0;
            }
            return LoginResponse;
        }
        public string CreateToken(long empId)
        {
            var tokenString = "";
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("myEncryptionKey@143#"));

                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name ,empId+"," +DateTime.Now.ToLongTimeString())
                        };
                var tokeOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: claims,
                notBefore: DateTime.Now,
                signingCredentials: signinCredentials
                );
                tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            }
            catch (Exception e) { }

            return tokenString;
        }


    }
}
