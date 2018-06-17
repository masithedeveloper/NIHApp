CREATE TABLE [dbo].[Event] (
    [EvtID]           BIGINT         NOT NULL,
    [EvtParentId]     BIGINT         NOT NULL,
    [EvtDriverId]     BIGINT         NOT NULL,
    [EvtPickUpTime]   DATETIME       NOT NULL,
    [EvtDropOffTime]  DATETIME       NOT NULL,
    [EvtTripFromHome] BIT            NOT NULL,
    [EvtType]         VARBINARY (50) NOT NULL,
    [EvtLongitude]    VARCHAR (255)  NULL,
    [EvtLatitude]     VARCHAR (255)  NULL,
    [EvtDateCreated]  DATETIME       NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([EvtID] ASC)
);



