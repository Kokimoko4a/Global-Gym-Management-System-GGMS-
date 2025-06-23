# Global Gym Management System (GGMS)

## Overview

The Global Gym Management System (GGMS) is a comprehensive web application developed using ASP.NET Core and Entity Framework Core. It provides an organized interface to manage gym clients, memberships, and trainers efficiently.

## Features

- Client Management
- Membership Tracking
- Trainer Scheduling
- Payment Processing
- Reporting and Analytics

## Architecture

GGMS follows a layered architecture, promoting separation of concerns and maintainability:

- **Presentation Layer**: Handles user interactions.
- **Business Logic Layer**: Contains core business rules.
- **Data Access Layer**: Manages data retrieval and storage.
- **Common Layer**: Provides shared utilities and models.

## Getting Started

### Prerequisites

- .NET 6.0 SDK or higher
- SQL Server or compatible database

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/Kokimoko4a/Global-Gym-Management-System-GGMS-
Navigate to the project directory:

bash
Copy
Edit
cd GGMS
Restore dependencies:

bash
Copy
Edit
dotnet restore
Apply database migrations:

bash
Copy
Edit
dotnet ef database update
Run the application:

bash
Copy
Edit
dotnet run
Usage
Access the application through your web browser at https://localhost:5001. Use the provided admin credentials to log in and manage gym operations.

Contributing
Contributions are welcome! Please fork the repository, create a new branch, and submit a pull request with your proposed changes.

License
This project is licensed under the MIT License.
 
