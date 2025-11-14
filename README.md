# Word Inversion App

A ASP.NET Core 8 application for inverting words in sentences, searching records, and viewing history.

## Features

- Invert word order in sentences
- Search through inverted records
- View all records in a clean table interface
- Simple, minimalist UI

## Prerequisites

Before you begin, ensure you have:

- **.NET 8 SDK** (verify installation: `dotnet --version`)
- **SQLite** (comes built-in with the project)

## Setup Instructions

### Step 1: Clone the Repository

Copy all project files to your local directory.

### Step 2: Install NuGet Packages

Open a terminal in your project directory and run:

dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools


### Step 3: Create the Database

Run migrations to create the SQLite database:

dotnet ef migrations add InitialCreate
dotnet ef database update

This creates the `WordInversion.db` file. SQLite handles everything automatically.

### Step 4: Run the Application

Start the application:

dotnet run



The app runs on [**https://localhost:7001**](https://localhost:7001). Open this URL in your browser.

## Usage

The application provides three main functions:

1. **Invert Sentence**: Type a sentence in the input box → Click invert → See the result
2. **Search Records**: Enter a word in the search box → Click search → View matching records
3. **View All Records**: All inverted sentences are displayed in a table

## What to Expect

- Clean, simple user interface
- Input box for sentence inversion
- Search functionality
- Table displaying all records with timestamps

## Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- SQLite Database
