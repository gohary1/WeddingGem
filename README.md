# WeddingGem

![WeddingGem](https://drive.google.com/uc?id=1iohmLkAWv0jGSv2dVsf3GlzELfTwT6Lw)

WeddingGem is an e-commerce website specializing in all wedding-related items such as cars, wedding halls, hotels, and more. The project is divided into two parts: a RESTful API for users and an MVC dashboard for admins and vendors.

## RESTful API

The RESTful API is designed for users to purchase and interact with various wedding-related services and products. The API provides endpoints for the following functionalities:

- **Add Products**: Allows vendors to add new products.
- **Get All Products**: Retrieves all available products.
- **Authentication and Authorization**: Handles user login and permissions.
- **Payments**: Processes payments using Stripe.
- **Error Handling**: Catches and handles errors.
- **Bidding**: Users can place bids for specific needs within a budget, waiting for vendor acceptance.

## MVC Dashboard

The MVC dashboard is designed for admins and vendors to manage products and bids. This project uses the same layers as the API, allowing for consistent data handling.

### Vendor Features

- **Publish Products**: Vendors can publish their products, which are immediately available in the REST API for users.
- **Edit Products**: Vendors can edit product details such as price and pictures.
- **Accept Bids**: Vendors can view and accept bids placed by users.

### Admin Features

- **User Management**: Admins can edit user roles, delete users, and manage roles.
- **Vendor Management**: Admins have all the capabilities of vendors, plus additional administrative functions.

## Images

<div style="display: flex; flex-wrap: wrap;">
    <img src="https://drive.google.com/uc?id=1Bi9rrITB3In-LiUfPSP9gw5b2zYTVtuR" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1VAj-lHeB4EnNYO-mH7CkU1Ur1cCzLMvY" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=120WHaHpWIPnG8erR-LrSk92KKNjCQMR4" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1426bLV4k1EqVFyTAB6KhYsEGowRcdLPG" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1JJfI5j5rCLeR94KwMI2mUjskiTn0ohya" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1GckfMzF84YLNzcAPnlMI8mqbdCBB8nhb" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1rO1ro4xZ1vRVs9IVVV2gDk7XgooSF-2f" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1IITrCfgbVkhY7dJPOitR-HQlYausLevc" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1Cfp_RrEfbU1gY3BD61IKV2SjarL9ypSa" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1w-rQyi0bJawG9KHAHz6j1L1Pyld1s7gs" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1hFfK6r4kXah1P4oty5vMUBs85HA9MaSH" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1FaQcWKWfXiPjKScaQgu7N_uuWrvz-d7n" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1lLhxUzmRkG3JW9B3zaVUvlTpUyA6z6TV" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1X7s_4nvJn2dM8qIBg3x4dGR17ckDDBOS" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1yFL9C9dg0rGuCF4lu1aKbLDllxHScYAI" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1Pb3qewliK9mPynCPqmpfIFX6om-fcQp-" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=14FIuQXmfhSB_nUEj9eoMVpAooks3E37P" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1kwiSsVjZYIpAjf5RQ-ixHaLRiQA05C2s" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1Es8CbvQX_Zber5ihAUPMeDotl_5NzHev" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=12nDrMKPamBvktKQ60gniqm9Y1fX4SSKi" style="width: 400px; margin: 10px;">
    <img src="https://drive.google.com/uc?id=1qdiQYup-yNAQS8ECxhVp6IECzFLB3Yrx" style="width: 400px; margin: 10px;">
</div>

## Getting Started

### Prerequisites

- .NET Core SDK
- SQL Server
- Stripe account for payment processing

### Installation

1. Clone the repository:
    ```bash
    git clone https://github.com/gohary1/WeddingGem.git
    ```

2. Navigate to the project directory:
    ```bash
    cd WeddingGem
    ```

3. Restore dependencies:
    ```bash
    dotnet restore
    ```

4. Update the database connection string in `appsettings.json`.

5. Run the database migrations:
    ```bash
    dotnet ef database update
    ```

6. Build and run the project:
    ```bash
    dotnet run
    ```

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Thanks to the contributors and maintainers of the project.
- Special thanks to the developers of the libraries and frameworks used.
