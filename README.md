# Sweet Treats

#### By **Samantha Callie**

#### A database to track your favorite sweet and savory treats

## Technologies Used

* HTML
* BootStrap
* CSS
* C#
* ASP.NET Core
* Razor
* EF Core

## Description

This site allows users to create an account and add sweets to a database. They can also add flavors and associate the two. It has Create, Read, Update, and Delete functionality.

## Setup/Installation Requirements

1. Press the green <> Code button and select Download ZIP
2. Unzip file
3. Open your terminal (e.g., Terminal or GitBash) and navigate to this project's production directory called "SweetTreats".
4. Within that directory, create a file called `appsettings.json`
5. In `appsettings.json`, paste the following code, replacing the `uid` and `pwd` values with your own username and password for MySQL.
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=sweet_treats;uid=YOUR_USERNAME;pwd=YOUR_PASSWORD;"
  }
}
```
6. To create the database, execute `dotnet ef database update` in the command line (still in the production directory)
7. Now run the command `dotnet run` to compile and execute the application. Then navigate to https://localhost:5001.
8. Optionally, you can run `dotnet run --launch-profile "production"` to run in production mode, as opposed to development mode.

## Known Bugs

* If the search function on the index pages returns no results, the message will appear as if no objects existed at all

## License

MIT License

Copyright (c) 2024 Samantha Callie

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.