# Sanoura Studio

Course project (Technology Entrepreneurship, BS CS). A simple e-commerce website for a handmade jewellery brand:
product catalogue, custom bracelet builder, cart, cash-on-delivery checkout, order tracking and a small admin panel.

## Tech
- ASP.NET Core MVC (.NET 8), C#, Razor views
- Entity Framework Core + SQLite (file `sanoura.db`, created automatically on first run)
- Plain HTML/CSS/JS for the front end

## Architecture
Browser -> Controllers -> EF Core (`AppDbContext`) -> SQLite. Cart is kept in the session; an order is saved only at checkout.

| Folder | Purpose |
|---|---|
| Models | `Product`, `Order`, `OrderItem`, `CartItem`, session cart helper |
| Data | `AppDbContext` and seed products |
| Controllers | Home, Products, Builder, Cart, Orders, Admin |
| Views | Razor pages for each controller |
| wwwroot | CSS and the builder's live-preview JavaScript |

## Run
```
dotnet run
```
Open the URL printed in the terminal. Admin panel: `/Admin` (password in `appsettings.json`, change it).

## Order flow
Received -> Making -> Packed -> Shipped -> Delivered. Customers track with their order ID (e.g. SAN-12345). Payment is cash on delivery only.
