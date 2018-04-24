CREATE TABLE [dbo].[GuestCode]
(
	GuestCodeId INT NOT NULL IDENTITY(1,1),
	GuestId INT NULL,
	GuestCode VARCHAR(50) NOT NULL,
	UseLimit INT NOT NULL,
	CONSTRAINT PK_GuestCode PRIMARY KEY(GuestCodeId),
	CONSTRAINT FK_GuestCode_Guest
		FOREIGN KEY(GuestId)
		REFERENCES dbo.Guest(GuestId)
)
