CREATE TABLE [dbo].[Session] (
    [SesId]               BIGINT        IDENTITY (1, 1) NOT NULL,
    [SesKey]              VARCHAR (255) NOT NULL,
    [SesPersonId]         BIGINT        NOT NULL,
    [SesCreatedDate]      DATETIME      NOT NULL,
    [SesValidDate]        DATETIME      NOT NULL,
    [SesIsActive]         BIT           NOT NULL,
    [SesDeviceActive]     BIT           NULL,
    [SesTimeLimitInhours] DATETIME      NULL,
    [SesModifiedDate]     DATETIME      NOT NULL,
    CONSTRAINT [PK_session] PRIMARY KEY CLUSTERED ([SesId] ASC),
    CONSTRAINT [FK_session_session] FOREIGN KEY ([SesPersonId]) REFERENCES [dbo].[Person] ([PerId])
);









