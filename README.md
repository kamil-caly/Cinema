# 🎥 Cinema Ticket Booking System

## Overview

The Cinema Ticket Booking System is a complete platform for reserving movie tickets, managing ticket validity, and accessing cinema schedules. It supports multiple user roles with specific functionalities, such as customers, ticket clerks, and administrators.

---

## Features

### General Functionality
- Browse the cinema schedule without logging in, including movie descriptions, posters, session dates, and times. ✔
- Purchase tickets for a specific session, including selecting a date, time, and seats using an intuitive seat layout view. ✔
- Generate a unique reservation code upon successful booking as proof of purchase. ✔

### Users
- View purchased tickets and session details. ✔
- Reserve seats for upcoming sessions. ✔

### Ticket Clerks
- Verify ticket validity using a reservation code. ✔
- Check ticket details such as hall number, reserved seats, and ticket status (valid, used, invalid, not exists). ✔
- Change the ticket status based on session timing. ✔

### Administrators
- Full access to all functionalities, including user and system management. ✔

### Unauthenticated Users
- Browse movie schedules and details. ✔

---

## Technologies Used

### Frontend
- **React**: Used for building the user interface.
- **React Router**: Handles client-side routing for the single-page application (SPA) behavior.
- **Tailwind CSS**: Provides a utility-first CSS framework for styling.
- **React Toastify**: Implements toast notifications for user feedback.
- **TypeScript**: Adds static typing for better development experience and error handling.

### Backend
- **.NET Core**: Implements backend API following the Clean Architecture pattern.
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations for better scalability and maintainability.
- **MS SQL Server** and **MySQL**: Manages database operations.

### DevOps
- **Azure**: Hosts the backend, frontend and database infrastructure.
- **CI/CD with Azure Pipelines**: Automates deployment processes for both backend and frontend.
- **Automated Tests**: Includes unit tests for backend: domain, application, and infrastructure layers, as well as controller tests.

---

## License
This project is open source and available under the [GPL-3.0 License](LICENSE).
