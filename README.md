# Welcome to the Space Images Project!

This project is a full-stack web application that displays various space images from NASA [APOD API](https://github.com/nasa/apod-api) in an hourly basis. It is built using .NET Web API for the backend and ReactJS for the frontend.

## Purpose:

The purpose of this project is to:
- Create a visually appealing and informative website displaying space images.

## Technologies Used:

- Backend:
  - .NET Web API
- Frontend:
  - ReactJS
- Database:
  - Redis
- Others: 
  - Docker
  - Github Actions

## Features:

- Displays various space images in an hourly basis.
- Provides information about each space image, such as its name, description, and source.

## Getting Started:

- Clone the repository:
```
git clone https://github.com/adhika16/space-images-project.git
```
- Install dependencies:
```
cd space-images-project
dotnet restore
npm install
```
- Run the frontend application:
```
cd reactapp
npm run dev
```
- Run the backend application:
```
cd webapi
dotnet build
dotnet run
```
- Open the application in your browser:
```
https://localhost:5173/space-images-project/
```

## Contributing:

We encourage contributions to this project. If you have any suggestions or improvements, please feel free to submit a pull request.

## License:

This project is licensed under the MIT License.

## Additional Information:

- For more information about the technologies used in this project, please refer to the [techstack.md](techstack.md).
- For more information about the project's architecture and design, please refer to the project documentation (_**coming soon**_).

Thank you for visiting!
