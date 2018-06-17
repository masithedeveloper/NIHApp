CREATE TABLE [dbo].[Notification] (
    [NotId]          BIGINT        NOT NULL,
    [NotMessage]     VARCHAR (MAX) NULL,
    [NotIsSent]      BIT           NOT NULL,
    [NotPersonId]    BIGINT        NOT NULL,
    [NotTimeCreated] DATETIME      NOT NULL,
    [NotEventId]     BIGINT        NOT NULL
);



