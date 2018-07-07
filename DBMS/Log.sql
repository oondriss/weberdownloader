CREATE TABLE [dbo].[Log]
(
	[Date] DATETIME NOT NULL , 
    [Thread] NVARCHAR(255) NULL, 
    [Level] NVARCHAR(50) NULL, 
    [Logger] NVARCHAR(255) NULL, 
    [Message] NVARCHAR(4000) NULL, 
    [Exception] NVARCHAR(2000) NULL, 
    CONSTRAINT [PK_Table] PRIMARY KEY ([Date])
)
