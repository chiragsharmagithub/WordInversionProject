Word Inversion App Setup & Run
What You Have
Pure ASP.NET Core 8 application. Just invert, get all, search.

Before You Start
You need: 

.NET 8 SDK (verify: dotnet --version)
SQLite comes built-in.

Setup Instructions
Step 1: Get Your Project Files
Copy all files to your local directory. 
Step 2: Install NuGet Packages
Open terminal in your project directory and run:

bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
Step 3: Create Database
Run migrations:

bash
dotnet ef migrations add InitialCreate
dotnet ef database update
This creates WordInversion.db file. SQLite will handle everything.

Step 4: Run The App
bash
dotnet run
App runs on https://localhost:7001. Open in browser.

What To Expect
You'll see:

Input box to invert sentence

Search box to find records

Table showing all records

Clean, simple UI

Type sentence → click invert → see result. Done.

Search word → click search → see matching records. Done.
