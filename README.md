# Bank_Orange

# Classes and system structure.

Person - This is the base class of every person who is interacting with the bank application - it has 3 private fields that holds the persons username, its password and a bool that determines wether the user shall be treated as a admin or not on login.

Admin - inherits from the "Person Class" and sets the username to "Admin" and its password to "Admin". The bool " IsAdmin" is also set to true which makes the application behave differently compared to if it were to be a regular "Customer". An example of this is that the admin can create new user accounts which a customer can not.

Customer - Also inherits from the "Person Class" it also has space for a username, a password and a bool. In this case though the bool will be set to false. - Object of this class will be the are the customers.

AccountDetails - This Class defines what propererties and methods a new savings account will hold upon creation. At this moment it will create an account name, a decimal to hold the amount of money and it will also set a value type to later give functionality tied to currency and formatting related issues.

BankSystem - This Class has more to do with functionality and creation rather than decleration compared to previous classes. In this class we create space for holding all users and their bankaccounts. It also presents the user with the initial experience in dealing with the application. When the application starts, it creates an admin user and goes straight into the "Login" method which is defined in the Banksystem Class. The BankSystem Class then holds several methods, most of them are tied to creating new users and setting their initial values. It also Defines two menues in which the user will choose what functionality they want to use inside the bank, one for admins and the other for customers. 

BankAccount - declares a list with bankaccounts that the customer has to his or hers disposal. Furthermore this class has mostly to do with creating saving accounts and functionality within an account that will be ready for use to the customer. Some of the methods witin this class are 

DisplayAccountInfo - Shows the user all their accounts and what the account balance is at this time.

AddNewBankAccount - Lets the user add new accounts,set their balance and in what currency the money will be saved in.

CurrencyConvert - Lets the user convert currency and sets the correct "Amount".

TrasferMoneyinUser - Lets the user transfer money between its own accounts.



Summary - The application is built using Object oriented programming, we try basing most aspects of the app around that. We use Dictionarys and lists to lists to structure and save the users and their different accounts. The reason we choose Dictionarys instead of regular lists in some cases is to have an easy point of reference that a Dictionary provides with it's key/value structure. 
