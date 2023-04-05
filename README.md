# ToDoApi
<div align="center">
<img src="https://user-images.githubusercontent.com/118696036/230163264-5032c5ba-ea4e-4e58-a90e-b3838fe1730c.png" />
<img src="https://user-images.githubusercontent.com/118696036/230163275-590be216-c2fb-47b6-8bc6-ef5bf9b31a3b.png" />
<img src="https://user-images.githubusercontent.com/118696036/230163293-b795061e-50e4-4e2b-b2f6-36137dbbfc7d.png" />
<img src="https://user-images.githubusercontent.com/118696036/230163306-f974b951-59b6-4332-8b30-63449d8ea475.png" />
<img src="https://user-images.githubusercontent.com/118696036/230163332-b0306a29-f00e-4c11-93d4-acae7d5dd01b.png" />
</div>

## 🌐 Status
<p>Finished project ✅</p>

#
## 🧰 Prerequisites

- .NET 6.0 or +

- Connection string to SQLServer in ToDoApi/appsettings.json named as Default

#
## <img src="https://icon-library.com/images/database-icon-png/database-icon-png-13.jpg" width="20" /> Database

_Create a database in SQLServer that contains the table created from the following script:_

```sql
CREATE DATABASE ToDoApiDapper

Use ToDoApiDapper

CREATE TABLE Categories(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Name VARCHAR(80) NOT NULL,
    Description VARCHAR(255) NOT NULL,
);

CREATE TABLE People(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Name VARCHAR(80) NOT NULL,
);

CREATE TABLE ToDos(
    Id INT PRIMARY KEY IDENTITY NOT NULL,
    Name VARCHAR(80) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    CreatedDate DATETIME NOT NULL,
    PersonId INT NOT NULL,
    CategoryId INT NOT NULL,

    CONSTRAINT [FK_ToDos_Person] FOREIGN KEY(PersonId)
        REFERENCES People(Id),
    CONSTRAINT [FK_ToDos_Category] FOREIGN KEY(CategoryId)
        REFERENCES Categories(Id)
);
```
### Relationships
```yaml
+--------------+        +-------------+        +--------------+
|   Categories | 1    * |    Todos    | *    1 |     People   |
+--------------+        +-------------+        +--------------+
|     Id       |<-------|      Id     |------->|      Id      |
|     Name     |        |     Name    |        |     Name     |
|              |        | Description |        |              |
|              |        | CreatedDate |        |              |
+--------------+        | PersonId    |        +--------------+
                        | CategoryId  |
                        +-------------+
```

#
## 🔧 Installation

`$ git clone https://github.com/lcsmota/ToDoApi.git`

`$ cd ToDoApi/`

`$ dotnet restore`

`$ dotnet run`

**Server listenning at  [https://localhost:7206/swagger](https://localhost:7206/swagger) or [https://localhost:7206/api/v1/ToDos](https://localhost:7206/api/v1/ToDos), [https://localhost:7206/api/v1/Categories](https://localhost:7206/api/v1/Categories) and [https://localhost:7206/api/v1/People](https://localhost:7206/api/v1/People)**

#
# 📫  Routes for ToDos

### Return all objects (ToDos)
```http
  GET https://localhost:7206/api/v1/ToDos
```
⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230167881-c20b111a-ec90-4bb6-be29-b12393ac99c2.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168549-f0cde2ae-687b-488c-91e8-1bc8e39831d9.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230167903-95ed938d-3b8a-4e0c-953c-d2bc78ecc57a.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168238-9c671c3c-3b37-4a1e-baf9-c045fcd710c7.png" />

#
### Return only one object (ToDo)

```http
  GET https://localhost:7206/api/v1/ToDos/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230168868-a751f065-aefe-48e9-8b3c-acce8243b39f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168549-f0cde2ae-687b-488c-91e8-1bc8e39831d9.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230168878-4b573805-a1cd-446b-8044-8634932c66d2.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168238-9c671c3c-3b37-4a1e-baf9-c045fcd710c7.png" />

#
### Insert a new object (ToDo)

```http
  POST https://localhost:7206/api/v1/ToDos
```
📨  **body:**
```json
{
  "name": "Learning English",
  "description": "Practice listening and conversation",
  "createdDate": "2023-04-05T18:17:42.759Z",
  "personId": 1,
  "categoryId": 1
}
```

🧾  **response:**
```json
{
    "description": "Practice listening and conversation",
    "createdDate": "2023-04-05T18:17:42.759Z",
    "personId": 1,
    "categoryId": 1,
    "id": 4,
    "name": "Learning English"
}
```

⚙️  **Status Code:**
```http
  (201) - Created
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230169867-ca018f39-8c61-4dfa-b4f4-771403723c1c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230169883-a859b856-4a9c-42d0-86a0-7ca0f1cbf39a.png" />
<img src="https://user-images.githubusercontent.com/118696036/230169893-308fbb83-30d9-42a3-8f47-2da5a3ac6dc2.png" />

#
### Update an object (ToDo)

```http
  PUT https://localhost:7206/api/v1/ToDos/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "name": "Learning English",
  "description": "Practice writing",
  "personId": 1,
  "categoryId": 1
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230170684-5282c708-a870-42e4-8cb0-d48eeb048b5e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168549-f0cde2ae-687b-488c-91e8-1bc8e39831d9.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230170693-af748f2b-ee02-42f5-8bb9-e8ef3262b7eb.png" />
<img src="https://user-images.githubusercontent.com/118696036/230170706-ec4999b5-cae0-4903-9178-569a4a4ba7a4.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168238-9c671c3c-3b37-4a1e-baf9-c045fcd710c7.png" />

#
### Delete an object (ToDo)
```http
  DELETE https://localhost:7206/api/v1/ToDos/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to delete|

📨  **body:**

🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230171088-a9978096-7c18-4d57-becc-51a293d2fc98.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168549-f0cde2ae-687b-488c-91e8-1bc8e39831d9.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230171107-c4821f0a-afa9-4870-809b-cc8efc34c85d.png" />
<img src="https://user-images.githubusercontent.com/118696036/230168238-9c671c3c-3b37-4a1e-baf9-c045fcd710c7.png" />

#
#
# 📫  Routes for Categories

### Return all objects (Categories)
```http
  GET https://localhost:7206/api/v1/Categories
```
⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230173765-3bdea980-6c17-4b2d-adb4-11f749be5d3f.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173226-5a4d5e1d-e426-4415-8374-6389adf9612c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230173771-54644120-e3a4-49ef-ac81-bd8386ffedc9.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173449-2ff01c99-1bea-4493-8e83-8597e51ea64b.png" />

#
### Return only one object (Category)

```http
  GET https://localhost:7206/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230174081-5ac98f1e-3c4a-4c5b-9d4d-f59e105f2faf.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173226-5a4d5e1d-e426-4415-8374-6389adf9612c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230174098-6e305660-9a60-48b9-8419-7b84cb62eece.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173449-2ff01c99-1bea-4493-8e83-8597e51ea64b.png" />

#
### Return category with Todos

```http
  GET https://localhost:7206/api/v1/Categories/${id}/multipleresults
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230174584-50ad0624-efd4-4bde-b954-3345cc028918.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230174596-31e660a3-4cad-488c-b940-a163140bf9f4.png" />

#
### Return all categories with Todos

```http
  GET https://localhost:7206/api/v1/Categories/multiplemapping
```

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230174965-31b3974e-4203-43f7-9a63-a8e503edbcd7.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230174981-aac3375e-9c08-4e38-a259-21f78a1edbc1.png" />

#
### Insert a new object (Category)

```http
  POST https://localhost:7206/api/v1/Categories
```
📨  **body:**
```json
{
  "name": "Sprint Workouts",
  "description": "How to sprint 100 meters"
}
```

🧾  **response:**
```json
{
    "description": "How to sprint 100 meters",
    "itens": [],
    "id": 7,
    "name": "Sprint Workouts"
}
```

⚙️  **Status Code:**
```http
  (201) - Created
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230178856-e17715ca-41ea-4e7d-9d54-4883fed321eb.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230178869-e59e3c6b-b009-4ea4-914a-4e70fccca2f5.png" />
<img src="https://user-images.githubusercontent.com/118696036/230178887-ca9d5d71-4191-47b9-b31f-ea0fe7e20fe7.png" />

#
### Update an object (Category)

```http
  PUT https://localhost:7206/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "name": "Sprint Workouts",
  "description": "How to sprint 200 meters"
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230179481-a0215e26-a5b4-4529-b1d5-93816b2e0bcb.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173226-5a4d5e1d-e426-4415-8374-6389adf9612c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230179588-13e61a8a-bcb0-4bea-ac7a-7f72f4f68779.png" />
<img src="https://user-images.githubusercontent.com/118696036/230179602-044ef5b4-f383-479d-ade4-88b4fbf0824e.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173449-2ff01c99-1bea-4493-8e83-8597e51ea64b.png" />

#
### Delete an object (Category)
```http
  DELETE https://localhost:7206/api/v1/Categories/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to delete|

📨  **body:**

🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230179866-557e5785-4707-49bd-b9c9-8f19df03b706.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173226-5a4d5e1d-e426-4415-8374-6389adf9612c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230179876-286838e0-43a7-47e7-ac37-595440db04f4.png" />
<img src="https://user-images.githubusercontent.com/118696036/230173449-2ff01c99-1bea-4493-8e83-8597e51ea64b.png" />

#
#
# 📫  Routes for People

### Return all objects (People)
```http
  GET https://localhost:7206/api/v1/People
```
⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230181628-e9ab7b62-b4f8-4f49-9187-ce2f2e38a8d0.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230181642-797da126-b638-40b4-a6bf-b3dd609246bf.png" />

#
### Return only one object (Person)

```http
  GET https://localhost:7206/api/v1/People/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230181877-653abefb-9759-47fb-900f-f16245004507.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182075-9eb64228-5465-4e05-800c-c10023858432.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230181887-383ea03f-a1e2-47f4-ab94-428162dd01c3.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182087-4d0da9bc-b29e-4b1c-b0d0-ff6bb82b8b68.png" />

#
### Return person with Todos

```http
  GET https://localhost:7206/api/v1/People/${id}/multipleresults
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to view|

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230182587-7b9fd5dd-6453-41ac-a5de-06e360efbb25.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230182599-90c56cd2-0398-4a57-bee7-2f21e2236da4.png" />

#
### Return all People with Todos

```http
  GET https://localhost:7206/api/v1/People/multiplemapping
```

⚙️  **Status Code:**
```http
  (200) - OK
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230182884-1d939881-48a8-4a6d-b25d-4b86f590ec2c.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230182899-e0f96682-4ae6-4785-934d-0b4632773539.png" />

#
### Insert a new object (Person)

```http
  POST https://localhost:7206/api/v1/People
```
📨  **body:**
```json
{
  "name": "John Smith"
}
```

🧾  **response:**
```json
{
    "toDos": [],
    "id": 5,
    "name": "John Smith"
}
```

⚙️  **Status Code:**
```http
  (201) - Created
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230183990-eba2f448-8c67-458c-a46d-4bd1a7b5569a.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230184011-fb54494c-3794-48f2-9ef7-b361225e7802.png" />
<img src="https://user-images.githubusercontent.com/118696036/230184023-8d7cf474-702d-4430-9829-fba3765e81f2.png" />

#
### Update an object (Person)

```http
  PUT https://localhost:7206/api/v1/People/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to update|

📨  **body:**
```json
{
  "name": "John Jackson Smith"
}
```
🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230184576-9584089c-4f79-46fa-b52d-75261e1864da.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182075-9eb64228-5465-4e05-800c-c10023858432.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230184595-5c805e46-3fc3-42cd-a008-f149f840b556.png" />
<img src="https://user-images.githubusercontent.com/118696036/230184604-40ce5b9d-ce59-4d70-a477-29be0d777b37.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182087-4d0da9bc-b29e-4b1c-b0d0-ff6bb82b8b68.png" />

#
### Delete an object (Person)
```http
  DELETE https://localhost:7206/api/v1/People/${id}
```

| Parameter   | Type       | Description                                   |
| :---------- | :--------- | :------------------------------------------ |
| `id`      | `int` | **Mandatory**. The ID of the object you want to delete|

📨  **body:**

🧾  **response:**

⚙️  **Status Code:**
```http
  (204) - No Content
  (404) - Not Found
```

#### 📬  Postman
<img src="https://user-images.githubusercontent.com/118696036/230184910-5f861bee-f41d-4872-bc6a-7658ba9e5490.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182075-9eb64228-5465-4e05-800c-c10023858432.png" />

#### 📝  Swagger
<img src="https://user-images.githubusercontent.com/118696036/230184919-825370ae-95bb-4061-9ab0-b5e391c5c518.png" />
<img src="https://user-images.githubusercontent.com/118696036/230182087-4d0da9bc-b29e-4b1c-b0d0-ff6bb82b8b68.png" />

#
## 🔨 Tools used

<div>
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" width="80" />
<img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/microsoftsqlserver/microsoftsqlserver-plain-wordmark.svg" width=80/>
</div>

# 🖥️ Technologies and practices used
- [x] C# 10
- [x] .NET CORE 6
- [x] SQL SERVER
- [x] Dapper
- [x] Swagger
- [x] DTOs
- [x] Repository Pattern
- [x] Dependency injection
- [x] POO

# 📖 Features
Registration, Listing, Update and Removal
