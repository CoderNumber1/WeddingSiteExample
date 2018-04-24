CREATE TABLE [dbo].[PendingQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReplyEmail] [varchar](50) NULL,
	[Question] [varchar](2000) NOT NULL,
	WillAnswer bit not null default(1),
 CONSTRAINT [PK_PendingQuestion] PRIMARY KEY([Id])
)