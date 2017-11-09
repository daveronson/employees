# employees
A pure .NET Core 1.1 version of Jimmy Bogard's EmployeeDirectory app  

Prerequisites  
Install SQLite tools and set the path so you can run sqlite3: https://www.sqlite.org/download.html  
  
Mac Instructions:  
Open a terminal and navigate to employees directory  
$ dotnet restore  
$ chmod 755 updatedb.mac  
$ chmod 755 web.mac  
$ ./updatedb.mac  
$ ./web.mac  

Windows Instructions:  
Open a cmd prompt and navigate to employees directory  
> dotnet restore  
> dotnet ef migrations add Initial  
> dotnet ef database update  
> sqlite3 employeeDirectory.db < DbScripts/PopulateData.sql  
> dotnet run  
