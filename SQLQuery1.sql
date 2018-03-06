CREATE TABLE [dbo].[Customer] (
    [customer_ref]     INT           NOT NULL,
    [customer_name]    VARCHAR (50)  NOT NULL,
    [customer_address] VARCHAR (100) NOT NULL,
    PRIMARY KEY CLUSTERED ([customer_ref] ASC)
);

CREATE TABLE [dbo].[Booking] (
    [booking_ref]    INT  NOT NULL,
    [arrival_date]   DATE NOT NULL,
    [departure_date] DATE NOT NULL,
    [chalet_ID]      INT  NOT NULL,
    [customer_ref]   INT  NOT NULL,
    [breakfast]      BIT  NOT NULL,
    [evening_meal]   BIT  NOT NULL,
    PRIMARY KEY CLUSTERED ([booking_ref] ASC),
    CONSTRAINT [FK_Booking_ToTable] FOREIGN KEY ([customer_ref]) REFERENCES [dbo].[Customer] ([customer_ref])
);

CREATE TABLE [dbo].[Guest] (
    [guest_id] INT NOT NULL,
    [guest_age]       INT          NOT NULL,
    [guest_name]      VARCHAR (50) NOT NULL,
    [booking_ref]     INT          NOT NULL,
    [passport_number] VARCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([guest_id] ASC),
    CONSTRAINT [FK_Guest_ToTable] FOREIGN KEY ([booking_ref]) REFERENCES [dbo].[Booking] ([booking_ref])
);

CREATE TABLE [dbo].[Car] (
    [booking_ref] INT  NOT NULL,
    [start_date]  DATE NOT NULL,
    [end_date]    DATE NOT NULL,
    [driver_name] VARCHAR(50) NOT NULL, 
    PRIMARY KEY CLUSTERED ([booking_ref] ASC),
    CONSTRAINT [FK_Car_ToTable] FOREIGN KEY ([booking_ref]) REFERENCES [dbo].[Booking] ([booking_ref])
);

