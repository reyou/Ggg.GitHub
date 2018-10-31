***
* https://sadi02.wordpress.com/2008/09/15/how-to-store-log-in-database-using-log4net/  
* https://www.connectionstrings.com/sql-server/  
***

```
use Log4netLogs

CREATE TABLE [dbo].[Log] (

[Id] [int] IDENTITY (1, 1) NOT NULL,

[Date] [datetime] NOT NULL,

[Thread] [varchar] (255) NOT NULL,

[Level] [varchar] (50) NOT NULL,

[Logger] [varchar] (255) NOT NULL,

[Message] [varchar] (4000) NOT NULL,

[Exception] [varchar] (2000) NULL

)

```
***
```
use Log4netLogs

select top 100 * from [dbo].[Log] order by date desc

```
***