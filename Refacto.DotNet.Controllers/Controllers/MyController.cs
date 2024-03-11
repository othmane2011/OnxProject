using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Dtos.Product;
using Refacto.DotNet.Controllers.Services.Impl;

namespace Refacto.DotNet.Controllers.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ProductService _ps;
        private readonly AppDbContext _ctx;

        public OrdersController(ProductService ps, AppDbContext ctx)
        {
            _ps = ps;
            _ctx = ctx;
        }

        [HttpPost("{orderId}/processOrder")]
        [ProducesResponseType(200)]
        public ActionResult<ProcessOrderResponse> ProcessOrder(long orderId)
        {
            Entities.Order? order = _ctx.Orders
                .Include(o => o.Items)
                .SingleOrDefault(o => o.Id == orderId);
            Console.WriteLine(order);
            List<long> ids = new() { orderId };
            /// check if order is not null
            if (order != null)
            {
                ICollection<Entities.Product>? products = order.Items;

                /// check if products collection is not null
                if (products != null)
                {
                    foreach (Entities.Product p in products)
                    {
                        if (p.Type == "NORMAL")
                        {
                            /// MOVED TO ProductService
                            //if (p.Available > 0)
                            //{
                            //    p.Available -= 1;
                            //    _ctx.Entry(p).State = EntityState.Modified;
                            //    _ = _ctx.SaveChanges();

                            //}
                            //else
                            //{
                            //    int leadTime = p.LeadTime;
                            //    if (leadTime > 0)
                            //    {
                            //        _ps.NotifyDelay(leadTime, p);
                            //    }
                            //}
                            _ps.HandleNormalProduct(p);
                        }
                        else if (p.Type == "SEASONAL")
                        {
                            /// MOVED TO ProductService
                            //if (DateTime.Now.Date > p.SeasonStartDate && DateTime.Now.Date < p.SeasonEndDate && p.Available > 0)
                            //{
                            //    p.Available -= 1;
                            //    _ = _ctx.SaveChanges();
                            //}
                            //else
                            //{
                            //    _ps.HandleSeasonalProduct(p);
                            //}
                            _ps.HandleSeasonalProduct(p);

                        }
                        else if (p.Type == "EXPIRABLE")
                        {
                            /// DUPLICATED CODE
                            //if (p.Available > 0 && p.ExpiryDate > DateTime.Now.Date)
                            //{
                            //    p.Available -= 1;
                            //    _ = _ctx.SaveChanges();
                            //}
                            //else
                            //{
                            //    _ps.HandleExpiredProduct(p);
                            //}

                            _ps.HandleExpiredProduct(p);
                        }
                        else if (p.Type == "FLASHSALE")
                        {
                            _ps.HandleFlashableProduct(p);
                        }
                    }
                }
                else
                {
                    /// throw NullReferenceException for products
                    throw new NullReferenceException();
                }
                return new ProcessOrderResponse(order.Id);
            }
            else
            {
                /// throw NullReferenceException for orders
                throw new NullReferenceException();
            }
        }
    }
}
