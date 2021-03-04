## HR Salary Calculator
This is a simple web app to help HR compute employee's monthly salary based on some specified employee type. Built with asp.net core 3.1 (C#) and JSON datastore using a custom built and generic DataContext manager which simulates a real db instance with .json files.
Presentation is built with MVC Core, HTML, jQuery and Bootstrap.

- Admin dashboard can only show max 20 records for employees
- The percentage taxed on the salary can be changed in AppSettings which is 12 by default 
- Two types of employees are seeded on startup and their basic take home stored along side, which are 20000 for Regular Employee and 500 for Contractual employee

### The Next Improvement I Will Prioritize
Going forward I will prioritize the following

- Change the application's storage system to a relational database management system.
- Upgrading the security of the application by adding identity server
    - Adding authorization and authentication
	- Assigning users to defined roles aids in the definition of a user's access set. 
- Create APIs (Rest services) and house the core business logic solely on the backend to clearly separate the backend from the frontend.

### Setup
On start of the application all neccessary store files are being created to /DataStore/{schema}.json

### State Management
Caching is carried out with Static objects references to enhance performances. On user logout, all cache items are cleared

### User Access
**Administrator**
Email: admin@gmail.com
Password: 123456
