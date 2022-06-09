In order to run api:

Open cmd with the UsersApi path
type "dotnet run".
Access http://localhost:20619/api/users for get users api

### Deployment
I have created the docker file.
### Build and run the Docker image
Open a command prompt and navigate to your project folder.
Use the following commands to build and run your Docker image:
 docker build -t UsersApi .
 docker run -d -p 8080:80 --name myapp UsersApi

View the web page running from a container
Go to localhost:8080 to access your app in a web browser.

Source:
https://docs.docker.com/samples/dotnetcore/  

Other resources
https://docs.microsoft.com/aspnet/core/host-and-deploy/?view=aspnetcore-6.0. 
https://medium.com/net-core/deploy-an-asp-net-core-app-to-google-cloud-d5ff3ff99b2d  
