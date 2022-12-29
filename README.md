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

### Shared Access Token

<img src="/pictures/shared_access_token.png" title="shared access token"  width="800">
<img src="/pictures/shared_access_token_account_level.png" title="shared access token account level"  width="800">


## Azure Functions in the Azure Portal

<img src="/pictures/create_function_app.png" title="create function app"  width="800">
<img src="/pictures/create_function_app2.png" title="create function app"  width="800">
<img src="/pictures/create_function_app3.png" title="create function app"  width="800">
<img src="/pictures/create_function_app4.png" title="create function app"  width="800">
<img src="/pictures/create_function_app5.png" title="create function app"  width="800">
<img src="/pictures/create_function_app6.png" title="create function app"  width="800">


## Azure Functions In Visual Studio

<img src="/pictures/function_app_vs.png" title="function app visual studio"  width="800">
<img src="/pictures/function_app_vs2.png" title="function app visual studio"  width="800">
<img src="/pictures/function_app_vs3.png" title="function app visual studio"  width="800">
<img src="/pictures/function_app_vs4.png" title="function app visual studio"  width="800">

Get the connection strings in the **Access Keys** section and paste it for *AzureWebJobsStorage* in the file *local.settings.json*
<img src="/pictures/function_app_vs_connection_string.png" title="function app connection string"  width="800">

### Package Manager Command in **AzureTangyFunc**
```
Install-Package Microsoft.Azure.Extensions.Storage
```

Before call :
<img src="/pictures/empty_queue.png" title="empty queue"  width="800">
<img src="/pictures/queue_call.png" title="queue call"  width="800">
<img src="/pictures/queue_after_call.png" title="queue after call"  width="800">
<img src="/pictures/queue_after_call2.png" title="queue after call"  width="800">