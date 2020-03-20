﻿using ItsCoffee.Core.Entities;
using ItsCoffee.Core.Repositories;
using ItsCoffee.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Xunit.Abstractions;

namespace ItsCoffee.Core.Tests
{
    public class CouponTests : TestsBase
    {
        [Fact]
        public void Add_Coupon_To_DB_And_Retrieve_It_Back()
        {
            var order = GetValidOrder();
            var couponService = new CouponService(new CouponRepository(_testDbConnection));
            order.AddCouponToOrder(new Coupon(Guid.NewGuid(), "TENOFF", CouponType.Percentage, 10));
            
            

            //Act
            couponService.AddCoupon(order);
            var returnedCoupon = couponService.GetCoupon("TENOFF");


            //Assert
            Assert.Equal("TENOFF", returnedCoupon.CouponCode);
            Assert.Equal("10", returnedCoupon.DiscountAmount.ToString());
        }

        private readonly ITestOutputHelper _output;
        public CouponTests(ITestOutputHelper output)
        {
            _output = output;
        }

        private void OutputCollection(IEnumerable<string> collection)
        {
            foreach (var item in collection)
            {
                _output.WriteLine(item);
            }
        }
    }
}
