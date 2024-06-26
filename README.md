# WeddingGem

WeddingGem is a comprehensive e-commerce platform specializing in wedding-related services and products, including cars, wedding halls, hotels, and more. The project consists of two primary components:

1. **WeddingGem API** - A RESTful API for user interactions.
2. **WeddingGem Dashboard** - An MVC-based dashboard for admin and vendor management.

## Features
![WeddingGem](https://drive.google.com/uc?export=view&id=1494ftuazOhrTcl4ID09J_ML5lRIS07JB)


### WeddingGem API

- **Product Management**: 
  - Add, retrieve, update, and delete products.
  - Controller: `ProductsController`.

- **User Authentication and Authorization**: 
  - Secure login and registration.
  - Role-based access control.
  - Controller: `AuthController`.

- **Payments**: 
  - Integration with Stripe for payment processing.
  - Controller: `PaymentsController`.

- **Error Handling**: 
  - Centralized error handling to catch and manage errors effectively.
  - Controller: `ErrorsController`.

- **Bidding System**: 
  - Users can create bids for specific services within a budget.
  - Vendors can view and accept bids.
  - Controller: `BidsController`.

### WeddingGem Dashboard

- **Vendor Management**: 
  - Vendors can publish products, which are immediately available to users through the API.
  - Vendors can edit product details such as price and pictures.
  - Vendors can view and accept bids.

- **Admin Features**: 
  - Admins can manage users and roles, including editing user roles and deleting users.
  - Admins have access to all vendor features.

## Technologies Used

- **Backend**: 
  - .NET Core for API development.
  - ASP.NET Core MVC for the dashboard.

- **Frontend**: 
  - Angular for the user interface.

- **Database**: 
  - SQL Server for data storage.

- **Payment Gateway**: 
  - Stripe for handling payments.
 
  ### Backend (API and MVC)
