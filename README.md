# TextMiningEcomWeb

## Project Overview

This is a full-stack application for text mining and sentiment analysis on e-commerce data. The project consists of three main components:

1. **API:** A Web API built using C# with Swagger support.
2. **Client:** A front-end Angular 16 application.
3. **Common:** A C# library shared across the application.

---

## Folder Structure

- **API:** Contains the Web API built with ASP.NET Core and C#. The API exposes several endpoints to interact with the backend services, such as fetching and processing data for text mining and sentiment analysis. Swagger is integrated for easy API testing and documentation.

- **Common:** A shared C# library used across the application to handle common functionalities, utilities, and models used by both the API and other backend services.

- **Client:** This is the front-end of the application, built using Angular 16. The client interacts with the backend API and presents an interface for users to perform text mining on e-commerce data and view results such as sentiment analysis.

- **.github:** Contains GitHub-specific configurations like actions and workflows.

- **.vs:** Visual Studio-related settings for solution management.

- **.gitignore:** A .gitignore file to exclude unnecessary files from being tracked in the repository.

- **README.md:** This file provides an overview of the project and guidance on how to set up and run the application.

- **TextMiningEcomWeb.sln:** The Visual Studio solution file for the project. Opening this will load the entire solution with all projects included (API, Common, and Client).

---

## Technologies Used

- **Backend:** ASP.NET Core Web API with C#
- **Frontend:** Angular 16
- **Shared Code:** C# libraries in the `Common` folder
- **API Documentation:** Swagger

---

## Setup and Installation

### Prerequisites

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
- [Node.js](https://nodejs.org/) (For Angular Client)
- [Angular CLI](https://angular.io/cli)

### Steps

1. **Clone the repository**

   ```bash
   git clone https://github.com/nhudangstudy/Text-Mining-EComWeb.git
   cd TextMiningEcomWeb
   ```

2. **Backend (API Setup)**
   - Navigate to the API folder:
     ```bash
     cd API
     ```
   - Restore the .NET packages:
     ```bash
     dotnet restore
     ```
   - Run the API:
     ```bash
     dotnet run
     ```
   - The API will be available at `https://localhost:5276` by default, and Swagger can be accessed at `https://localhost:5276/swagger`.

3. **Frontend (Client Setup)**
   - Navigate to the Client folder:
     ```bash
     cd Client
     ```
   - Install the necessary npm packages:
     ```bash
     npm install
     ```
     In case that does not work, you can try this instead:
     ```bash
     npm i --legacy-peer-deps
     ```
   - Run the Angular application:
     ```bash
     ng serve
     ```
   - The application will be available at `http://localhost:4200`.

4. **Common Library**
   - The `Common` folder contains the shared C# code. It will automatically be referenced by the API project.

---

## Usage

After starting both the API and Client, you can access the web interface in your browser at `http://localhost:4200`. The API provides endpoints for text mining and sentiment analysis, and the client allows you to interact with these features.

---

## Documentation
Read more about this project at _**LLL_TextMining_LokeyTech_Word**_

---
