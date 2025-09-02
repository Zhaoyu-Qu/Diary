## Diary API

This project is a simple demonstration of how to implement a RESTful API using ASP.NET Core. It serves as a diary application, allowing users to create, read, update, and delete diary entries.

### Dependencies
- [.NET SDK 9.0 or later](https://dotnet.microsoft.com/download) (required for building and running with the .NET CLI)
- [Docker](https://www.docker.com/get-started) (required for building and running the Docker container)
- No manual installation of SQLite is needed; the application will create and manage the database file automatically.

### Features
- Exposes a RESTful API at `http://localhost:5001` for CRUD operations on diary items.
- Separation of concerns: the controller layer handles HTTP requests, while the service (model) layer manages business logic and data access.
- The service layer communicates with a SQLite database, which is automatically created on application startup if it does not already exist.

### Usage

#### Run with .NET CLI
1. Navigate to the `Diary.Api` directory:
	```
	cd Diary.Api
	```
2. Start the application:
	```
	dotnet run
	```
3. The API will be available at `http://localhost:5001/swagger/index.html`.

#### Run with Docker
1. Build the Docker image from the project root:
	```
	docker build -t diary-api .
	```
2. Run the Docker container:
	```
	docker run -p 5001:5001 diary-api
	```
3. The API will be available at `http://localhost:5001/swagger/index.html`.