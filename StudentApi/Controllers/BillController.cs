using Core.DataSource;
using DTO.Request;
using DTO.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentApi.Controllers
{
    [Route("api/Bill")]
    [ApiController]
    public class BillController : ControllerBase
    {
        [HttpPost("Login")]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {


            if (ModelState.IsValid)
            {

                return BillDataSource.Instance.Login(request);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("GetItem")]
        public ActionResult<itemResponse> GetItem(ItemRequest request)
        {
            if (ModelState.IsValid)
            {

                return BillDataSource.Instance.ItemGet(request);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("BillingSubmit")]
        public ActionResult<BillinsertResponse> BillingSubmit(BillRequest request)
        {
            if (ModelState.IsValid)
            {

                return BillDataSource.Instance.BillingSubmit(request);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        

    }
}
