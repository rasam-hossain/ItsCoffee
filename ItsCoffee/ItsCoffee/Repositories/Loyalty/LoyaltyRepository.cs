using Dapper;
using ItsCoffee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ItsCoffee.Core.Repositories
{
    public class LoyaltyRepository : ILoyaltyRepository
    {
        private readonly IDbConnection _db;

        public LoyaltyRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public void AddLoyaltyCustomer(LoyaltyCustomer customer)
        {
            var sql = "INSERT INTO LoyaltyCustomer (CustomerId, FirstName, LastName, EmailAddress, RewardStatus, LoyaltyPointsBalance, LifetimeLoyaltyPoints)" +
                      " Values (@CustomerId, @FirstName, @LastName, @EmailAddress, @RewardStatus, @LoyaltyPointsBalance, @LifetimeLoyaltyPoints);";

            _db.Execute(sql,new
            {
                CustomerId = customer.CustomerId,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                RewardStatus = customer.RewardStatus.ToString(),
                LoyaltyPointsBalance = customer.LoyaltyPointsBalance,
                LifetimeLoyaltyPoints = customer.LifetimeLoyaltyPoints
            });
        }

        public void UpdateLoyaltyCustomer(LoyaltyCustomer customer)
        {
            // Todo: Test 2 - Need to update the SQL statement and parameters in order to save first and last names
            var sql = "UPDATE LoyaltyCustomer Set FirstName = @FirstName, LastName = @LastName, EmailAddress = @EmailAddress, RewardStatus = @RewardStatus," +
                      " LoyaltyPointsBalance = @LoyaltyPointsBalance, LifetimeLoyaltyPoints = @LifetimeLoyaltyPoints" +
                      " WHERE CustomerId = @CustomerId;";
            
            _db.Execute(sql, new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                EmailAddress = customer.EmailAddress,
                RewardStatus = customer.RewardStatus.ToString(),
                LoyaltyPointsBalance = customer.LoyaltyPointsBalance,
                LifetimeLoyaltyPoints = customer.LifetimeLoyaltyPoints,
                CustomerId = customer.CustomerId
            });
        }

        public void DeleteLoyaltyCustomer(LoyaltyCustomer customer)
        {
            throw new NotImplementedException();
        }

        public LoyaltyCustomer GetLoyaltyCustomer(Guid customerId)
        {
            return _db.Query<LoyaltyCustomer>("SELECT * FROM LoyaltyCustomer WHERE CustomerId = @value;", 
                new { value = customerId}).ToList().FirstOrDefault();
        }

        public IEnumerable<LoyaltyCustomer> GetAllLoyaltyCustomers()
        {
            return _db.Query<LoyaltyCustomer>("SELECT * FROM LoyaltyCustomer");
        }
    }
}
