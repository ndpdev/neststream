create table Thermostat
(
    ThermostatID int not null auto_increment,
    DeviceID varchar(75) not null,
    FriendlyName varchar(50) not null,
    constraint PK_Thermostat primary key (ThermostatID)
);

create table ThermostatData
(
    EntryTime datetime not null,
    ThermostatID int not null,
    Temperature smallint not null, 
    Humidity smallint not null, 
    HvacMode enum('H', 'C', 'X', 'E', 'O', '?') not null,
    HvacState enum('H', 'C', 'O', '?') not null,
    IsOnline enum('Y', 'N') not null,
    FanActive enum('Y', 'N') not null,
    constraint PK_ThermostatData primary key (EntryTime, ThermostatID)
);

