# Studen Names - Kalin Paraleev
# Studen Number - 2301321039


## Car Shop Application

This is a full-stack car shop application built with:

- **Backend:** Spring Boot
- **Frontend:** React
- **Database:** PostgreSQL
- **Containerization:** Docker Compose

---

## Prerequisites

Make sure you have the following installed on your machine:

- [Docker](https://docs.docker.com/get-docker/)
- [Docker Compose](https://docs.docker.com/compose/install/)

---

## Getting Started

Follow these steps to run the project locally:

1.  **Clone the repository**

    ```bash
    git clone [https://github.com/RedRoSeE-API/distributed-applications-se.git](https://github.com/RedRoSeE-API/distributed-applications-se.git)
    cd carshop
    ```

2.  **Start the application with Docker Compose**

    ```bash
    docker-compose up
    ```

    This command will build and start all necessary services:

    * Spring Boot backend
    * React frontend
    * PostgreSQL database

### Accessing the Application

* **Frontend UI:** `http://localhost:80`
* **Backend API:** `http://localhost:8080`

### Stopping the Application

To stop the running containers, press `Ctrl+C` in the terminal and then run:

```bash
docker-compose down
```

### Additional Notes

* Make sure ports `80`, `8080`, and `5432` are free on your machine before starting.
* Data in the PostgreSQL container is persisted in a Docker volume.
