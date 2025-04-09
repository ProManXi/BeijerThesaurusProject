## WebApp - Thesaurus Dictionary

This is a web application built using .NET 8, HTML, CSS and JQuery.

### Functional Requirements

• Add Word

• Get Synonyms of a given word

• View All Words

Create a simple UI experience that can fully interact with the thesaurus.

### Non-Functional Requirements

• Performance

• Seperation of Concerns

• Custom Logging

• Avoid Duplicity of words

• Testability using UTCs and Integration Tests

• Validations

• Maintainability

### Core Entities

• Word

• Synonyms

### API Endpoints

- **POST /addword**  
    Adds a new word with its synonyms after performing validation checks.
- **GET /getsyn?word={word}**  
    Retrieves synonyms for a given word.
- **GET /**  
    Returns a list of all words currently stored in the system.

### High Level Design
![High-Level Design](hld.png)


## Trade Offs

### 1\. Fast Startup & No Persistence vs Slow Startup & Persistence  // I chose ( Slow Startup & Persistence)

    I went with Slow Startup and Data persistence. (Hybrid model with saving data in a File and during startup,
    load them into Cache but once in cache, it will be fast and persistence)

### 2\. Reverse Dictionary Logic vs Something to avoid Duplicacy  // I chose (Graphs)

    I went with Graphs as I came to know they work on Nodes and Edges and using them we can arrange data in a
    BiDirectional way  (if A has relatonship with B, then if someone searches B, its all relationships will
    come up). It avoids duplicacy.

### 3\. Weighted & Directed vs Unweighted & Undirected // I chose (Unweighted & Undirected)

    We can say happy is 2 times weighted to joyful, 3 times weighted to escatic and 1 time wieghted to glad, 
    but for this project, I can go with unweighted as it can work as an extension for future releases!
    
### 4\. React app vs HTML pages // I chose (HTML)

    I went with HTML so that I can focus more on functionality of Graph and Cache. It will be difficult to release two different      tech stacks for a small project.

### 5\. SQL vs NoSQL vs FileSystem // I chose (FileSystem)

     We do not need any ACID properties/ relationships between numerous tables, so we can avoid Relational Databases.
     It will also be an overhead to setup DB and make unnecessary connections for this small project.



## Steps to Run the Application

### 1\. Clone the Repository

git clone [https://github.com/ProManXi/BeijerThesaurusProject.git]

cd BeijerThesaurusProject

### 2\. Open the Project

You can open the project in any of the following:

- **Visual Studio 2022 or later** (recommended)
- **Visual Studio Code** (with C# extension installed)

### 3\. Build the Application

dotnet build

### 4\. Startup Project

Set ThesaurusWebAPI as Startup project

dotnet restore

### 5\. Run the Application

dotnet run

By default, the API will be available at:

<http://localhost:5186>

### 6\. Test API Endpoints

You can test the following endpoints using tools like **Postman**, **curl**, or a **frontend app**:

- POST /addword – Add a word and its synonyms
- GET /getsynonyms – Get synonyms for a given word
- GET / – Get all words in the system

### 7\. Run Unit Tests and Integration Tests

If you have unit tests set up in a test project:

dotnet test
