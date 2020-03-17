# Session 02 - Defensive/Clean Coding

#### Current session: Thursday Feb 5, 8:45 – 11:45 am

#### Next Session – Interface & Extension Methods: Tuesday March 10, 1:15 – 4:00 pm

## Unit Testing Exercise – OrderValidatior
The OrderValidator class has been decomposed into individual validation classes located in ItsCoffee.Core/Service/OrderValidation. Write unit tests for each of these new validation rule classes. 


## Clean Coding Exercise – Coupon Service
Add coupon functionality to the ItsCoffee application library. This can be limited to discount percentage coupons. During this exercise, demonstrate defensive/clean coding principles:

1.	Single use methods and classes
2.	Easy to read, clear intent
3.	Clear and descriptive naming
4.	Predictable result
5.	Testability

### Requirements:
- Coupons are applied directly to an order.
- An order may or may not have a coupon applied to it.
- Coupon data needs to be retrievable, so order history is accurate.
- Coupon discounts should affect the loyalty points added with an order. An order with a coupon gives less loyalty points since it reduces the final price of the order.
- Added functionality should be fully covered with unit tests.


### Stretch Requirements:
- Coupons need to be validated before being applied. This includes verifying the coupon exists in the system already and the discount is correct. 
- Different coupon types: 
- Flat rate.
- Percentage.
- ‘buy one get one’ (percentage off every second product with the same ProductId in the order).
- Some coupons are only valid for certain product categories, others for all categories.

Sample solutions to these exercises will be made available by Monday March 9th. 
