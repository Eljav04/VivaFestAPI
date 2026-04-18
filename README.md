# 🚀 VivaFest API

Backend system developed for **[VivaFestQuiz](https://github.com/Eljav04/VivaFestQuiz)** and **[VivaFestivalLottery](https://github.com/Eljav04/VivaFestivalLottery)**, designed to handle real-time participant interaction, quiz processing, and lottery operations.

---

## 📌 Overview

VivaFest API is a high-performance ASP.NET backend built to support two core event modules:

* 🎯 Quiz system (VivaFestQuiz)
* 🎰 Lottery system (VivaFestivalLottery)

The system is optimized for **fast response times** and designed to handle **high concurrent load** during live events.

---

## ⚙️ Tech Stack

* ASP.NET Core (.NET)
* Entity Framework Core
* PostgreSQL (migrated from SQLite)
* Docker (containerized deployment)
* Railway (hosting)

---

## 🧱 Architecture

The project follows a clean and modular structure:

```
Controllers/   → API endpoints (Quiz, Lottery)
Services/      → Business logic layer
Interfaces/    → Contracts for services
Data/          → DbContext and database setup
Entities/      → Database models
DTOs/          → Data transfer objects
Extensions/    → Custom service registrations (CORS, modules)
Utility/       → Helpers (connection, data handling)
```

* Clear separation of concerns
* Lightweight service layer
* Optimized database access (EF Core)

---

## 📡 API Endpoints

---

### 🎯 Quiz Module (`/api/quiz`)

#### Admin

* `GET /admin/questions` → Get all questions
* `POST /admin/questions` → Create question
* `PUT /admin/questions/{id}` → Update question
* `DELETE /admin/questions/{id}` → Delete question
* `DELETE /admin/results/reset` → Reset leaderboard
* `POST /admin/quiz/toggle_activity` → Enable/disable quiz

#### Participant

* `GET /questions` → Get active quiz questions
* `POST /submit` → Submit quiz answers

#### Global

* `GET /leaderboard` → Get leaderboard
* `GET /check_activity` → Check if quiz is active

---

### 🎰 Lottery Module (`/api/fortune/lottery-items`)

* `GET /` → Get all participants
* `POST /` → Add participant
* `POST /bulk` → Add multiple participants
* `DELETE /delete-all` → Remove all participants
* `DELETE /{id}` → Remove participant
* `POST /set-winner/{id}` → Mark participant as winner
* `POST /reset` → Reset all winners

---

## 🚀 Performance

* Optimized for **low latency responses**
* Efficient database queries using EF Core
* Designed to handle **simultaneous high-load scenarios**
* Minimal overhead in request processing

---

## 📦 Deployment

* Containerized using Docker
* Deployed on Railway
* Environment-based configuration supported

---

## ⚠️ Notes

* Authentication was intentionally not implemented (event-based usage)
* Backend designed for temporary but high-load scenarios
* Focus placed on **speed, simplicity, and reliability**

---
