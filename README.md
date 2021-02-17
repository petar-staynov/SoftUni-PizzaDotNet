# SoftUni-PizzaDotNet
Demo project for Pizza restaurant business web app based on ASP.NET Core 3.1  
Live demo available here: https://pizzadotnetweb20201130231618.azurewebsites.net/

## Roles
* Administrator
* User (registered)
* Visitor (guest)

## Functionality
### Site guest (**Visitor**) 
* can view the menu and its products 
* sort products by name/price/rating  
  
### Users (**Registered**)
* same as guest
* can add items to their cart
* rate products
* place orders
* receive coupon codes
* use coupon codes and view their available ones
* view their orders and cancel ones which are still processing
* change their email, password and address

### Administrator (created from site owner)
* access the Admin Panel
* manage user accounts
* manage products and menu categories


## Template authors
- [Nikolay Kostov](https://github.com/NikolayIT)
- [Vladislav Karamfilov](https://github.com/vladislav-karamfilov)

## Used Frameworks/Libraries
* ASP.Net Core 3.1
* ASP.NET Core Default Identity (with additional Facebook and Google authentication)
* Entity Framework Core
* AutoMapper
* Google Cloud Storage
* XUnit
* Razor Views
* Razor Pages
* Bootstrap 4

## Used techniques
* MVC
* Repository pattern
* Service pattern
* Web Api controllers + AJAX
* MVVM (for custom Identity pages)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Author

- [Petar Staynov](https://github.com/petar-staynov)
