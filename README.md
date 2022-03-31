<div align="center" style="text-align:center">
<div style="disply:flex; justify-content: center;">

<img src=".readme/images/dotNetCore.png" alt="Dot NET Core pic" width="75" height="auto">
<img src=".readme/images/api.png" alt="API pic" width="75" height="auto">

</div>

# .NET API Boilerplate

</div>

This boilerplate was created to serve as a base project for a quick start of an API in .NET, with a structured architecture opened for scalability and easy to maintenance.

In this documentation are the specifications of each page and important files, as well as the conventions to be followed.

## Technical specification 
Below are the diagrams related to the project and how it was designed, with the architecture diagram showing the significant nugget packages included.

There is also, for a better perception of the project, a tree of folders with the types of framework version used in each one.

<div align="center" style="text-align:center">
<img src=".readme/images/diagrams.png" alt="Onion Architecture Diagram" width="400" height="auto">
</div>
<br/>

```
Boilerplate
└───1 - Entities
│       │   Domain.Entities
│       │   DTO.Entities
└───2 - Repositories
│       │   Data.Repositories
└───3 - Services
│       │   Core.Services
└───4.1 - Infranstructure
│       │   Infrastructure.Core
│       │   Infrastructure.IoC
└───4.2 - Presentation
│       │   WebAPI
└───4.3 - Tests 
        │   WebAPI.Tests
```

Notes:
* .NET Standard 2.0 &#8594; Layer 1 to 4.1
* .NET 5 &#8594; Layer 4.2 to 4.3
   
## Architecture
The architecture implemented in this project is onion architecture, and you can find its documentation in this [link](https://www.codeguru.com/csharp/understanding-onion-architecture).

## Features
Below is a list of basic features that this API offers:
1. Structured architecture based on layers
2. API request examples
3. Log events using Serilog
4. FluentValidation
5. Unit test examples in xUnit

# Getting Started
To setup the project locally a few steps need to be followed.

1. Clone the repo 
    ```
    git clone https://github.com/adilsonmsoares/API_Boilerplate
    ```
2. Open the cloned project
   
   If you need to install all packages, use the following command on the package manager console:
   
    ```
    Update-Package –reinstall
    ```
3. Build and run the Project

# Contribute
To contribute to this project, first of all, new developers need to understand the architecture used and read the convention determined for this project in order to proceed with the developement.

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are greatly appreciated.

1. Create your Feature Branch (git checkout -b feature/AmazingFeature)
2. Commit your Changes (git commit -m 'Add some AmazingFeature')
3. Push to the Branch (git push origin feature/AmazingFeature)
4. Open a Pull Request