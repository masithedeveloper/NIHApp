USE [NHIAppDB]
GO

/****** Object:  Table [dbo].[Person]    Script Date: 2018/06/09 12:13:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Person](
	[PerId] [bigint] IDENTITY(1,1) NOT NULL,
	[PerFirstname] [varchar](255) NULL,
	[PerLastname] [varchar](255) NULL,
	[PerFullname] [varchar](255) NULL,
	[PerTitle] [bit] NULL,
	[PerPassword] [varchar](255) NULL,
	[PerHashPassword] [varchar](255) NULL,
	[PerDob] [datetime] NULL,
	[PerIdNumber] [varchar](255) NULL,
	[PerType] [bigint] NOT NULL,
	[PerEmail] [varchar](255) NULL,
 CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED 
(
	[PerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

USE [NHIAppDB]
GO

/****** Object:  Table [dbo].[Device]    Script Date: 2018/06/09 14:27:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Device](
	[DevId] [bigint] IDENTITY(1,1) NOT NULL,
	[DevPersonId] [bigint] NOT NULL,
	[DevFirebaseToken] [varchar](255) NULL,
	[DevPlatform] [varchar](50) NULL,
	[DevOSVersion] [varchar](50) NULL,
	[DevCreateDate] [datetime] NULL,
	[DevModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Device_1] PRIMARY KEY CLUSTERED 
(
	[DevId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Device]  WITH CHECK ADD  CONSTRAINT [FK_devicePerson] FOREIGN KEY([DevPersonId])
REFERENCES [dbo].[Person] ([PerId])
GO

ALTER TABLE [dbo].[Device] CHECK CONSTRAINT [FK_devicePerson]
GO

USE [NHIAppDB]
GO

/****** Object:  Table [dbo].[ScheduledEmail]    Script Date: 2018/06/09 14:28:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ScheduledEmail](
	[SchEmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SchPersonId] [bigint] NOT NULL,
	[SchSendAt] [datetime] NOT NULL,
	[SchFromEmailAddress] [nvarchar](500) NOT NULL,
	[SchToEmailAddress] [nvarchar](2500) NOT NULL,
	[SchCcEmailAddress] [nvarchar](2500) NOT NULL,
	[SchBccEmailAddress] [nvarchar](2500) NOT NULL,
	[SchSubject] [nvarchar](4000) NOT NULL,
	[SchContent] [ntext] NOT NULL,
	[SchType] [smallint] NOT NULL,
	[SchIsHtml] [bit] NOT NULL,
	[SchEmailed] [bit] NOT NULL,
	[SchReady] [bit] NOT NULL,
	[SchFailureCount] [int] NOT NULL,
	[SchLastFailureReason] [text] NULL,
	[SchCreateDate] [datetime] NOT NULL,
	[SchModifiedDate] [datetime] NOT NULL,
	[SchFromName] [nvarchar](500) NULL,
 CONSTRAINT [PK__schedule__F0657C946E98C7F5] PRIMARY KEY CLUSTERED 
(
	[SchEmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_s__571DF1D5]  DEFAULT (getdate()) FOR [SchSendAt]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_f__5812160E]  DEFAULT ('') FOR [SchFromEmailAddress]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_t__59063A47]  DEFAULT ('') FOR [SchToEmailAddress]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_c__59FA5E80]  DEFAULT ('') FOR [SchCcEmailAddress]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_b__5AEE82B9]  DEFAULT ('') FOR [SchBccEmailAddress]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_s__5BE2A6F2]  DEFAULT ('') FOR [SchSubject]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_c__5CD6CB2B]  DEFAULT ('') FOR [SchContent]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_t__5DCAEF64]  DEFAULT ((0)) FOR [SchType]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_i__5EBF139D]  DEFAULT ((0)) FOR [SchIsHtml]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_e__5FB337D6]  DEFAULT ((0)) FOR [SchEmailed]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_r__60A75C0F]  DEFAULT ((0)) FOR [SchReady]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_f__619B8048]  DEFAULT ((0)) FOR [SchFailureCount]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_c__628FA481]  DEFAULT (getdate()) FOR [SchCreateDate]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_m__6383C8BA]  DEFAULT (getdate()) FOR [SchModifiedDate]
GO

ALTER TABLE [dbo].[ScheduledEmail] ADD  CONSTRAINT [DF__scheduled__sch_f__6477ECF3]  DEFAULT ('') FOR [SchFromName]
GO

ALTER TABLE [dbo].[ScheduledEmail]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledEmail_person] FOREIGN KEY([SchPersonId])
REFERENCES [dbo].[Person] ([PerId])
GO

ALTER TABLE [dbo].[ScheduledEmail] CHECK CONSTRAINT [FK_ScheduledEmail_person]
GO

USE [NHIAppDB]
GO

/****** Object:  Table [dbo].[Session]    Script Date: 2018/06/09 14:29:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Session](
	[SesId] [bigint] IDENTITY(1,1) NOT NULL,
	[SesKey] [varchar](255) NOT NULL,
	[SesPersonId] [bigint] NOT NULL,
	[SesCreatedDate] [datetime] NOT NULL,
	[SesValidDate] [datetime] NOT NULL,
	[SesIsActive] [bit] NOT NULL,
	[SesDeviceActive] [bit] NULL,
	[SesTimeLimitInhours] [datetime] NULL,
	[SesModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED 
(
	[SesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Session]  WITH CHECK ADD  CONSTRAINT [FK_session_session] FOREIGN KEY([SesPersonId])
REFERENCES [dbo].[Person] ([PerId])
GO

ALTER TABLE [dbo].[Session] CHECK CONSTRAINT [FK_session_session]
GO







