CREATE TABLE [dbo].[FAQ](
	[FAQId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [varchar](4000) NOT NULL,
	[Answer] [varchar](4000) NOT NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY 
(
	[FAQId] 
)
)
