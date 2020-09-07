using ReactAspx.Models;
using ReactMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReactMvc.Controllers.Api
{
    public class DataController : Controller
    {
        public IList<FoodItem> MenuItems { get; set; } = new List<FoodItem>();
        
        // GET: Data
        [HttpGet]
        public ActionResult GetMenuList()
        {
            using(var context = new AppDbContext())
            {
                foreach (var item in context.FoodItems)
                {
                    MenuItems.Add(item);
                }
            }
            return Json(MenuItems, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AuthorizeNadia]
        public string GetUserId()
        {
            var uid = -1;
            if (Session["UserId"] != null)
                uid = Convert.ToInt32(Session["UserId"].ToString());
            return uid.ToString();
        }

        [HttpPost]
        [AuthorizeNadia]
        public ActionResult PlaceHolder(List<FoodItem> items, int id)
        {
            bool isSuccess = false;
            try
            {
                using (var context = new AppDbContext())
                {
                    var order = new Order
                    {
                        CustomerId = id,
                        OrderDate = DateTime.Now
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();

                    var orderId = order.Id;
                    decimal grandTotal = 0;
                    foreach (var item in items)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderId = orderId,
                            FoodItemId = item.Id,
                            Quantity = item.Quantity,
                            TotalPrice = item.Price * item.Quantity,
                        };
                        context.OrderDetails.Add(orderDetail);
                        grandTotal += orderDetail.TotalPrice;
                    }

                    order.TotalPaid = grandTotal;
                    order.Status = 1;
                    order.OrderDate = DateTime.Now;
                    context.SaveChanges();
                    isSuccess = true;
                }
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            if(isSuccess)
            {
                return Json("Success: true", JsonRequestBehavior.AllowGet);
            }
            return Json("Success: false", JsonRequestBehavior.AllowGet);
        }
    }

    public class AuthorizeNadia : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");

            // Make sure the user logged in.
            if (httpContext.Session["Email"] == null)
            {
                return false;
            }

            // Do you own custom stuff here
            // Check if the user is allowed to Access resources;

            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (this.AuthorizeCore(filterContext.HttpContext) == false)
            {
                filterContext.Result = new RedirectResult("/Account/Login/?ret=" + filterContext.HttpContext.Request.CurrentExecutionFilePath);
            }
        }
    }
}