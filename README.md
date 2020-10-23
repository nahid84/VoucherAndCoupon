# VoucherAndCoupon

Improvements in the code still can be made are (README): -
1.	Add dependency injection so that classes get it instance injected when needed. This simplifies unit testing as well.

2.	VoucherController has more responsibility than it should do. I would recommend having a VoucherService which can be called by controller. Controller should be a thin layer anyway by design. Actual working logic should be inside Service. VoucherService has been created in the code as example.

3.	Exception handling must be added. It helps to track when something expected happened during execution and gives control what to show user in those cases. Helps also in logging.

4.	Logging can be added.


5.	Input validation can be added through filter or middleware.

6.	The API’s are not secured. So, security is a must in case of public facing API. Azure API Management by OAuth 2.0 authorization with Azure AD can be considered.


7.	Repository layer must follow interface inheritance and actual data should be provided by a provider.

8.	Integration test from API interface can be added. This ensures API’s got called right.


9.	New Unit tests must be added for new functionality implemented.

10.	In some place’s comments are expected. This helps in faster readability.


11.	CouponController has been added to support the frontend what type of coupons are supported.
