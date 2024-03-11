﻿using Microsoft.EntityFrameworkCore;
using Refacto.DotNet.Controllers.Database.Context;
using Refacto.DotNet.Controllers.Entities;

namespace Refacto.DotNet.Controllers.Services.Impl
{
    public class ProductService
    {
        private readonly INotificationService _ns;
        private readonly AppDbContext _ctx;

        public ProductService(INotificationService ns, AppDbContext ctx)
        {
            _ns = ns;
            _ctx = ctx;
        }

        public void NotifyDelay(int leadTime, Product p)
        {
            p.LeadTime = leadTime;
            _ = _ctx.SaveChanges();
            _ns.SendDelayNotification(leadTime, p.Name);
        }

        public void HandleNormalProduct(Product p)
        {
            if (p.Available > 0)
            {
                p.Available -= 1;
                _ctx.Entry(p).State = EntityState.Modified;
                _ = _ctx.SaveChanges();

            }
            else
            {
                int leadTime = p.LeadTime;
                if (leadTime > 0)
                {
                    NotifyDelay(leadTime, p);
                }
            }
        }

        public void HandleSeasonalProduct(Product p)
        {
            if (DateTime.Now.Date > p.SeasonStartDate && DateTime.Now.Date < p.SeasonEndDate && p.Available > 0)
            {
                p.Available -= 1;
                _ = _ctx.SaveChanges();
            }
            else
            {
                if (DateTime.Now.AddDays(p.LeadTime) > p.SeasonEndDate)
                {
                    _ns.SendOutOfStockNotification(p.Name);
                    p.Available = 0;
                    _ = _ctx.SaveChanges();
                }
                else if (p.SeasonStartDate > DateTime.Now)
                {
                    _ns.SendOutOfStockNotification(p.Name);
                    _ = _ctx.SaveChanges();
                }
                else
                {
                    NotifyDelay(p.LeadTime, p);
                }
            }
        }

        public void HandleExpiredProduct(Product p)
        {
            if (p.Available > 0 && p.ExpiryDate > DateTime.Now)
            {
                p.Available -= 1;
                _ = _ctx.SaveChanges();
            }
            else
            {
                _ns.SendExpirationNotification(p.Name, (DateTime)p.ExpiryDate);
                p.Available = 0;
                _ = _ctx.SaveChanges();
            }
        }
        public void HandleFlashableProduct(Product p)
        {
            if (DateTime.Now.Date > p.SeasonStartDate && DateTime.Now.Date <= p.SeasonEndDate && p.Available > 0)
            {
                p.Available -= 1;
                _ = _ctx.SaveChanges();
            }
            else if ((DateTime.Now.Date > p.SeasonEndDate) || (DateTime.Now.Date <= p.SeasonEndDate && p.Available <= 0))
            {
                /// the date has expired or no more stock
                _ns.SendExpirationNotification(p.Name, (DateTime)p.ExpiryDate);
                p.Available = 0;
                _ = _ctx.SaveChanges();
            }
            else
            {
                /// the date has expired and still in stock
                _ns.SendExpirationNotification(p.Name, (DateTime)p.ExpiryDate);
                p.Available = 0;

            }
        }
    }
}
