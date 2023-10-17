# VSEAT

## Goals and features
VSEAT is a project which aims towards unifying restaurant's online order system. It is a web-based application that allows people to order from their favourite restaurants all over Valais.
What's more, delivery men are automatically assigned to orders according to their location. Finally, restaurants can view summary of their sales through VSEAT. 

## Usage
### General
Anonymous user can view restaurant and their available dishes but they cannot place an order.

To log in, one must be registered in the site database. When signing in, one must fill in his first and last name, a valid email address and a password.
The password has to be made observing the following constraints: 
- At least one uppercase character
- At least one lowercase character
- At least one digit
- At least eight characters long

You can either sign up as a customer or as a courier, but not both. Signed in users can log out at anytime.

### Customer
A customer is a registered user. The customer chooses the restaurant he wants to order from as well as the dishes. The customer is then redirected to the checkout page where he needs to fill in his/her delivery address and the desired delivery time. Finally, he is redirected to a confirmation page with all the relevant orders's information, he can cancel an order only if he does it at least three hours before.

The customer can view a summary of all his orders ordered by date. He/she can then see details for a specific order and cancel it if the order is supposed to be deliverd in more than three hours.

### Courier
When logged in, the courier is shown all her/his orders. He/she can see each order's details and set them to delivered/undelivered. Undelivered orders are always on of the page, ordered by their expected delivery time.

### Restaurant
When logged in, the restaurant is shown a brief summary of his sales.

## Authors and Contributors
Authors: Biollaz Benjamin, Gaillard Nathan
Contributors: Duc Alain, Widmer Antoine

## Languages
C# ASP.net 5.0, CSS, HTML, JavaScript, Microsoft SQL, TypeScript

## License
Developped in academic context at HES-SO