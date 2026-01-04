# AI-Powered Healthcare Appointment System

A full-stack .NET 9 and Next.js application that uses AI to recommend doctors based on patient symptoms. Built with Clean Architecture, CQRS, and modern best practices.

## 🚀 Tech Stack
- **Backend:** .NET 9 Web API, EF Core, MediatR (CQRS), FluentValidation, SQLite / PostgreSQL.
- **Frontend:** Next.js 14 (App Router), TailwindCSS, TypeScript.
- **AI Integration:** Groq API (Llama 3) via OpenAI SDK.
- **Containerization:** Docker & Docker Compose.
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API).

## ✨ Key Features
- **JWT Authentication:** Secure Login and Registration.
- **AI Doctor Recommendation:** Natural language processing to match patient symptoms to specific doctor specializations in the database.
- **Appointment Management:** Book, view, and cancel appointments.
- **Robust Validation:** Prevents booking in the past or invalid data entry.
- **Auto-Migrations:** Database is automatically created and seeded on startup.

---

## 🛠️ How to Run (Standard)

### 1. Backend Setup (.NET 9)
1. Navigate to the backend folder:
   ```bash
   cd HealthcareAppointment
   ```
2. Update `appsettings.json` in the WebAPI project with your Groq API Key (if needed).
3. Run the application:
   ```bash
   dotnet run --project HealthcareAppointment
   ```
   *The database will seed automatically.*
   *API URL: https://localhost:7226* (Swagger available at `/swagger`)

### 2. Frontend Setup (Next.js)
1. Open a new terminal and navigate to the frontend folder:
   ```bash
   cd healthcare-frontend
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Run the development server:
   ```bash
   npm run dev
   ```
4. Open your browser to: http://localhost:3000

---

## 🐳 How to Run (Docker Bonus)
If you have Docker installed, you can run the entire stack with one command:

1. Navigate to the root folder (where `docker-compose.yml` is).
2. Run:
   ```bash
   docker-compose up --build
   ```
3. Access the app:
   - **Frontend:** http://localhost:3000
   - **Backend/Swagger:** http://localhost:7226/swagger

---

## 🧪 Testing the AI Feature
1. Register a new account and Login.
2. Click **"+ Book New (AI)"** on the dashboard.
3. Enter a symptom description:
   - *"I have a severe migraine and dizziness"* → Recommends **Neurologist**.
   - *"I have a deep cut that won't stop bleeding"* → Recommends **Surgeon**.
   - *"I have a generic fever"* → Recommends **General**.
   - *"I have a mysterious illness nobody understands"* → Recommends **Diagnostician**.

## 📂 Project Architecture
The backend follows strict Clean Architecture:
- **Domain:** Pure entities and constants (No dependencies).
- **Application:** Business logic, CQRS (MediatR), DTOs, Validators.
- **Infrastructure:** Database implementation, AI Service, Auth Service.
- **WebAPI:** Controllers and entry point.