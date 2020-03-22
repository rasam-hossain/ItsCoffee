using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using ItsCoffee.Core.Entities;

namespace ItsCoffee.Core.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IDbConnection _db;

        public CouponRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public void AddCoupon(Order order)
        {
            // TO Do
            // Validate the order
            _db.Open();
            using (var transaction = _db.BeginTransaction())
            {
                var sql = "INSERT INTO 'Coupon' (CouponId, CouponCode, CouponType, DiscountAmount) " +
                          "VALUES (@CouponId, @CouponCode, @CouponType, @DiscountAmount);";

                _db.Execute(sql, new
                {
                    //OrderId = order.OrderId,
                    CouponID = order.Coupon.CouponID,
                    CouponCode = order.Coupon.CouponCode,
                    CouponType = order.Coupon.CouponType,
                    DiscountAmount = order.Coupon.DiscountAmount
                });
                transaction.Commit();
            }
            _db.Close();
        }

        public void UpdateCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public void RemoveCoupon(Coupon coupon)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Coupon> GetAllCoupons()
        {
            return _db.Query<Coupon>("SELECT * FROM Coupon;").ToList();
        }

        public Coupon GetCoupon(string couponCode)
        {
            var coupon = _db.Query<Coupon>("SELECT * FROM Coupon where CouponCode = @value;",
                    new
                    {
                        value = couponCode
                    })
                .ToList()
                .FirstOrDefault();
            return coupon;
        }

    }
}
