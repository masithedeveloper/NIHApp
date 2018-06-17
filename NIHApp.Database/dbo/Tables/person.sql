CREATE TABLE [dbo].[Person] (
    [PerId]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [PerFirstname]     VARCHAR (255) NULL,
    [PerLastname]      VARCHAR (255) NULL,
    [PerFullname]      VARCHAR (255) NULL,
    [PerTitle]         BIT           NULL,
    [PerPassword]      VARCHAR (255) NULL,
    [PerHashPassword]  VARCHAR (255) NULL,
    [PerDob]           DATETIME      NULL,
    [PerIdNumber]      VARCHAR (255) NULL,
    [PerType]          BIGINT        NOT NULL,
    [PerEmail]         VARCHAR (255) NULL,
    [PerTransportId]   BIGINT        NULL,
    [PerCellPhone]     VARCHAR (255) NULL,
    [PerEmailVerified] BIGINT        CONSTRAINT [DF_Person_PerEmailVerified] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_person] PRIMARY KEY CLUSTERED ([PerId] ASC)
);











