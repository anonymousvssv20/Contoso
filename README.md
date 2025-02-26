# Contoso University

This is a sample application for **Contoso University**, demonstrating a simple educational management system built using **ASP.NET Core** with **Razor Pages**, **Entity Framework Core**, and **SQL Server**. It provides functionalities such as managing courses, students, instructors, and enrollments.

## Features

- **Course Management**: View and manage courses, their credits, and instructors.
- **Instructor Management**: View details about instructors and their courses.
- **Enrollment Management**: Track student enrollments across courses.
- **Data Visualization**: Interactive charts displaying student and course data.

## Prerequisites

- .NET 8.0 SDK
- SQL Server or SQLite (local database)
- Visual Studio 2022 or Visual Studio Code

## Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/anonymousvssv19/Contoso.git

Navigate to the project folder and restore dependencies:


Update the database connection string in appsettings.json.

Run the migrations to set up the database:


dotnet ef database update
Run the application:

dotnet run
Open a browser and navigate to http://localhost:5000.

Contributing
Feel free to fork the repository, make changes, and submit a pull request. For major changes, please open an issue first to discuss what you would like to change.

License
This project is licensed under the MIT License - see the LICENSE file for details.

Acknowledgments
Microsoft for ASP.NET Core and Entity Framework
Chart.js for data visualization
