# Azure Function Logic Apps And Blob Storage For .NET Developer

In this project, we will study 4 of the essential services that every .NET developer should be aware of. Using these services some tasks can be simplified drastically :
- Azure Blob Storage
- Azure Functions
- Azure Logic Apps
- Azure Cache for Redis


## Azure Blob Storage 

### Package Manager Command in **AzureBlobStorage**
```
Install-Package Azure.Storage.Blobs
```

### Create Storage Account

<img src="/pictures/containers.png" title="containers"  width="800">
<img src="/pictures/connection_strings.png" title="connection strings"  width="600">

### Create Container

<img src="/pictures/before.png" title="before"  width="800">
<img src="/pictures/create_container.png" title="create container"  width="800">
<img src="/pictures/create_container2.png" title="create container"  width="800">
<img src="/pictures/create_container3.png" title="create container"  width="800">
<img src="/pictures/after.png" title="after"  width="800">

### Add Blob

<img src="/pictures/blob_before.png" title="before"  width="800">
<img src="/pictures/add_blob.png" title="add blob"  width="800">
<img src="/pictures/added_blob.png" title="add blob"  width="800">





### Migration scripts for the database in **Products.API**
```
Add-Migration AddProductToDb
Update-Database
```
