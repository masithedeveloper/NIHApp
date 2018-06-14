CREATE TABLE [dbo].[Device] (
    [DevId]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [DevPersonId]      BIGINT        NOT NULL,
    [DevFirebaseToken] VARCHAR (255) NULL,
    [DevPlatform]      VARCHAR (50)  NULL,
    [DevOSVersion]     VARCHAR (50)  NULL,
    [DevCreateDate]    DATETIME      NULL,
    [DevModifiedDate]  DATETIME      NULL,
    CONSTRAINT [PK_Device_1] PRIMARY KEY CLUSTERED ([DevId] ASC),
    CONSTRAINT [FK_devicePerson] FOREIGN KEY ([DevPersonId]) REFERENCES [dbo].[Person] ([PerId])
);









