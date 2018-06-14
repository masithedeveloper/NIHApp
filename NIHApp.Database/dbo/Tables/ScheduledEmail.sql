CREATE TABLE [dbo].[ScheduledEmail] (
    [SchEmailId]           BIGINT          IDENTITY (1, 1) NOT NULL,
    [SchPersonId]          BIGINT          NOT NULL,
    [SchSendAt]            DATETIME        CONSTRAINT [DF__scheduled__sch_s__571DF1D5] DEFAULT (getdate()) NOT NULL,
    [SchFromEmailAddress]  NVARCHAR (500)  CONSTRAINT [DF__scheduled__sch_f__5812160E] DEFAULT ('') NOT NULL,
    [SchToEmailAddress]    NVARCHAR (2500) CONSTRAINT [DF__scheduled__sch_t__59063A47] DEFAULT ('') NOT NULL,
    [SchCcEmailAddress]    NVARCHAR (2500) CONSTRAINT [DF__scheduled__sch_c__59FA5E80] DEFAULT ('') NOT NULL,
    [SchBccEmailAddress]   NVARCHAR (2500) CONSTRAINT [DF__scheduled__sch_b__5AEE82B9] DEFAULT ('') NOT NULL,
    [SchSubject]           NVARCHAR (4000) CONSTRAINT [DF__scheduled__sch_s__5BE2A6F2] DEFAULT ('') NOT NULL,
    [SchContent]           NTEXT           CONSTRAINT [DF__scheduled__sch_c__5CD6CB2B] DEFAULT ('') NOT NULL,
    [SchType]              SMALLINT        CONSTRAINT [DF__scheduled__sch_t__5DCAEF64] DEFAULT ((0)) NOT NULL,
    [SchIsHtml]            BIT             CONSTRAINT [DF__scheduled__sch_i__5EBF139D] DEFAULT ((0)) NOT NULL,
    [SchEmailed]           BIT             CONSTRAINT [DF__scheduled__sch_e__5FB337D6] DEFAULT ((0)) NOT NULL,
    [SchReady]             BIT             CONSTRAINT [DF__scheduled__sch_r__60A75C0F] DEFAULT ((0)) NOT NULL,
    [SchFailureCount]      INT             CONSTRAINT [DF__scheduled__sch_f__619B8048] DEFAULT ((0)) NOT NULL,
    [SchLastFailureReason] TEXT            NULL,
    [SchCreateDate]        DATETIME        CONSTRAINT [DF__scheduled__sch_c__628FA481] DEFAULT (getdate()) NOT NULL,
    [SchModifiedDate]      DATETIME        CONSTRAINT [DF__scheduled__sch_m__6383C8BA] DEFAULT (getdate()) NOT NULL,
    [SchFromName]          NVARCHAR (500)  CONSTRAINT [DF__scheduled__sch_f__6477ECF3] DEFAULT ('') NULL,
    CONSTRAINT [PK__schedule__F0657C946E98C7F5] PRIMARY KEY CLUSTERED ([SchEmailId] ASC),
    CONSTRAINT [FK_ScheduledEmail_person] FOREIGN KEY ([SchPersonId]) REFERENCES [dbo].[Person] ([PerId])
);



