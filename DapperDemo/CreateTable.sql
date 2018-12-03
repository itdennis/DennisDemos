CREATE TABLE [dbo].[content](
 [id] [int] IDENTITY(1,1) NOT NULL,
 [title] [nvarchar](50) NOT NULL,
 [content] [nvarchar](max) NOT NULL,
 [status] [int] NOT NULL,
 [add_time] [datetime] NOT NULL,
 [modify_time] [datetime] NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
 [id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[content] ADD  CONSTRAINT [DF_Content_status]  DEFAULT ((1)) FOR [status]
GO

ALTER TABLE [dbo].[content] ADD  CONSTRAINT [DF_content_add_time]  DEFAULT (getdate()) FOR [add_time]
GO

CREATE TABLE [dbo].[comment](
 [id] [int] IDENTITY(1,1) NOT NULL,
 [content_id] [int] NOT NULL,
 [content] [nvarchar](512) NOT NULL,
 [add_time] [datetime] NOT NULL,
 CONSTRAINT [PK_comment] PRIMARY KEY CLUSTERED 
(
 [id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[comment] ADD  CONSTRAINT [DF_comment_add_time]  DEFAULT (getdate()) FOR [add_time]
GO