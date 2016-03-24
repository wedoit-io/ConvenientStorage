How to Database on Azure
========================

...

## Create the SQL database *(and SQL server)*

0. Click "Add" to create a new resource under the resource group.

  ![1 - add resource](https://cloud.githubusercontent.com/assets/314398/13353665/3ca1b0c6-dc96-11e5-9079-ddc7b6f5623b.jpg)

0. Search for "SQL Database", select it and click on "Create".

  ![2 - select sql database](https://cloud.githubusercontent.com/assets/314398/13353666/3ca3ff52-dc96-11e5-89e4-9fe2ca3a223d.jpg)

0. Give your SQL database a name (e.g., `example-database`)

0. Click on "Server" to configure required settings

0. Click on "Create a new server"

0. Give your SQL server a name (e.g.,  `example-database-server`)

0. Give your Server admin login a name (e.g.,  `sqlserver-admin`)

0. Decide a password (e.g.,  `!2e4567B`)

0. Click on "Location" to change it to your preference (e.g., "West Europe")

0. Click on "Ok"

  ![3 - create sql server](https://cloud.githubusercontent.com/assets/314398/13353667/3caa3cbe-dc96-11e5-9882-bf90244bf2f3.jpg)

0. Click on "Pricing tier", select "Basic" and click on "Select"

0. Click on "Create"

  ![4 - select pricing tier](https://cloud.githubusercontent.com/assets/314398/13353668/3cabc494-dc96-11e5-9b70-2595bfb0e9e6.jpg)

... with all this, Azure should take some time (usually under 60 seconds) and create your SQL server along with your SQL database.


## Create a Contained Database User

:information_source: If you don't want to use Visual Studio, that's fine. Same concepts still apply, but you may need to use a different combination of Azure Portal & SQL Server Management Studio to do these stuff.

0. Select `View > Server Explorer` to connect to Your Azure Subscription using Visual Studio.

  ![5 - vs open server explorer](https://cloud.githubusercontent.com/assets/314398/13353669/3caddc66-dc96-11e5-9237-c0c65e3f80ef.jpg)

0. Right click on "Azure" in Server Explorer and click on "Connect to Microsoft Azure Subscription".

  ![6 - connect to azure](https://cloud.githubusercontent.com/assets/314398/13353670/3cb1fecc-dc96-11e5-9242-49583eb4bc8a.jpg)

0. *At this point you will follow on screen instructions to log in with your credentials...*

0. Expand `SQL Databases` under Azure in Server Explorer and you should be able to see the database you created previously. Right click on it and select "Open in SQL Server Object Explorer".

  ![7 - open in sql server object explorer](https://cloud.githubusercontent.com/assets/314398/13353671/3cc279dc-dc96-11e5-87a9-b367ccd28aa8.jpg)

0. If everything goes according to plan, you should be asked to add a firewall rule to access to the database on Azure. *(Ask with your network administrator for IP addresses you should use. Any additional IP addresses must be added using Azure portal later on.)*

  ![8 - add firewall rule](https://cloud.githubusercontent.com/assets/314398/13354395/133c5c7c-dc9b-11e5-859e-195f944d9392.jpg)

0. In "Connect to Server" dialog, insert password for Server admin login previously created **but don't just connect, yet!** Instead, click on "Options".

  ![9 - connect to server as admin](https://cloud.githubusercontent.com/assets/314398/13353673/3cce05a4-dc96-11e5-8d02-3b77c87d9dd3.jpg)

0. On the "Connection Properties" tab, find "Connect to database" field and clear the database name there and click "Connect". *(If you specify a database name here, then you won't be able to connect to the master database. We need to be able to connect to the master database to create a new login later on.)*

  ![10 - clear database name and connect](https://cloud.githubusercontent.com/assets/314398/13353674/3cd148ea-dc96-11e5-88ec-76a6b9e876c5.jpg)

0. Make sure you're connected to the right SQL server (`example-database-server.database.windows.net` in the example below) and to the right SQL database with the right login (`sqlserver-admin` in the example below).

  > :warning: **If the connection name ends with something similar to `sqlserver-admin example-database` (rather than `sqlserver-admin` only), then you're doing it wrong, because it means you're only connected to example-database and you may not have access to the master database.**

  ![11 - check connection](https://cloud.githubusercontent.com/assets/314398/13353675/3cd51056-dc96-11e5-97c9-412db52733f5.jpg)

0. Expand `Security > Logins` under your SQL server, right click and select "Add New Login (SQL Server)..."

  ![12 - add new login](https://cloud.githubusercontent.com/assets/314398/13353676/3cda845a-dc96-11e5-84f9-b7308fce8da9.jpg)

0. Now you should see a query window in Visual Studio with a template to create a new login. Generally it is a good idea to leave the password as it is, because it is *secure*, but take a note of it, because we will need it later. Where it says `[Login]` change it with something like `[dmnmcvsunkmgtk]`:

  ```sql
  CREATE LOGIN [dmnmcvsunkmgtk] WITH PASSWORD = 'pUcc|il2gm;6r5gjnz_nSyyqmsFT7_&#$!~<zt}opeqqt|it'
  ```

  > :information_source: It doesn't really matter what you type in, as long as you take a note of it, because you will use this login to configure your web app later on. We recommend some random letters, since you shouldn't be using this login to manually connect to the database anyway.

  :warning: Before executing it, double check you're connect to **master** database and then right click on the editor and select "Execute".

  ![13 - create login](https://cloud.githubusercontent.com/assets/314398/13353677/3cde0c6a-dc96-11e5-82d3-39702adecacb.jpg)

0. If you select "Refresh" when you right click on `Security > Users`, then you should be able to see newly created login.

  ![14 - check new login](https://cloud.githubusercontent.com/assets/314398/13353679/3cf0aca8-dc96-11e5-9e0f-67c28158abf2.jpg)

0. Expand `Databases > example-database > Security > Users` under your SQL database **(:warning: this is not under "System Databases or master database, etc.")**, right click on it and select "Add New User..."

  ![14 - add new user](https://cloud.githubusercontent.com/assets/314398/13353678/3ce1dc50-dc96-11e5-951f-f81fefbc73c1.jpg)

0. You should see a new query window in Visual Studio with a template to create a new user. Replace it with and make sure you replace both `[dmnmcvsunkmgtk]` with login name you decided previously:

  ```sql
  CREATE USER [dmnmcvsunkmgtk] FROM LOGIN [dmnmcvsunkmgtk] WITH DEFAULT_SCHEMA = dbo
EXEC sp_addrolemember 'db_owner', 'dmnmcvsunkmgtk'
  ```

  > :information_source: This will create a new database user with the same name as login name you decided previously and give this user "database owner (`db_owner`)" role.

  :warning: Before executing it, double check you're connect to **example-database** and then right click on the editor and select "Execute".

  ![15 - creat db_owner user](https://cloud.githubusercontent.com/assets/314398/13353680/3cf26372-dc96-11e5-83cc-71b61f87d1c0.jpg)
