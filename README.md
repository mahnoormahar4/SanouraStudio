# Sanoura Studio

Course project (Technology Entrepreneurship, BS CS). A simple e-commerce website for a handmade jewellery brand:
product catalogue, custom bracelet builder, cart, cash-on-delivery checkout, order tracking and a small admin panel.

## Tech
- ASP.NET Core MVC (.NET 10), C#, Razor views
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
Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```
git clone https://github.com/mahnoormahar4/SanouraStudio.git
cd SanouraStudio
dotnet run
```
Then open the address shown in the terminal (usually `http://localhost:5000`).
Admin panel: `/Admin` (password is in `appsettings.json`, change it).

## Order flow
Received -> Making -> Packed -> Shipped -> Delivered. Customers track with their order ID (e.g. SAN-12345). Payment is cash on delivery only.

## Status
Initial version. Planned: improved design, product images, admin product management, gift section.