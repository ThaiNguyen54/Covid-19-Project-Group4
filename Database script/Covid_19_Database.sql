Create database Covid_19

USE Covid_19
GO
EXEC sp_changedbowner 'sa'


Create table CITIZEN
(
	CitizenID int not null primary key,
	FName varchar(100),
	LName varchar(100),
	BDate date,
	Address varchar(100),
	Age int,
	Password varchar(100),
	Phone varchar(100)
)

Create table Vaccin_Registration_Form
(
	FormID int not null primary key,
	CitizenID int not null,
	VaccinID int not null,
	InjectionDate date not null,
	RegisterDate date not null,
)

Create table VACCIN_RECORD
(
	RecordID int not null primary key,
	CitizenID int not null,
	Number_Injection int not null,
	FirstDate date,
	FirstVaccin varchar(100),
	SecondDate date,
	SecondVaccin varchar(100),
	ThirdDate date,
	ThirdVaccin varchar(100),
	First_NurseID int,
	Second_NurseID int,
	Third_NurseID int
)

Create table Nurse
(
	NurseID int not null primary key,
	ManagerID int,
	FName varchar(100),
	LName varchar(100),
	BDate date,
	Address varchar(100),
	Age int,
	Phone varchar(100),
)

create table SUPPLIER
(
	SupID int not null primary key,
	SupName varchar(100),
	SupAddress varchar(100),
	Phone varchar(100)
)

Create table VACCIN_ENTRY
(
	EntryID int not null primary key,
	ManagerID int not null,
	SupID int not null,
	EntryDate date not null,
	Amount_Vaccin int not null
)

Create table MANAGER
(
	ManagerID int not null primary key,
	FName varchar(100),
	LName varchar(100),
	BDate date,
	Address varchar(100),
	Age int,
	Phone varchar(100)
)

Create table VACCIN
(
	VaccinID int not null primary key,
	WarehouseID int not null,
	VaccinName varchar(100) not null
)

Create table ENTRY_DETAIL
(
	VaccinID int not null,
	EntryID int not null,
	Quantity int not null,
	Primary key (VaccinID, EntryID)
)

Create table WAREHOUSE_VACCIN
(
	VaccinID int not null,
	WarehouseID int not null,
	Stock int not null, 
	Primary key (VaccinID, WarehouseID)
)

Create table WAREHOUSE
(
	WarehouseID int not null primary key,
	Location varchar(100)
)

--Add foreign key for register form
alter table [dbo].[Vaccin_Registration_Form] add constraint Register_Citizen foreign key (CitizenID) references [dbo].[CITIZEN](CitizenID)
alter table [dbo].[Vaccin_Registration_Form] add constraint Register_Vaccin foreign key (VaccinID) references [dbo].[VACCIN](VaccinID)


--Add foreign key for record 
alter table [dbo].[VACCIN_RECORD] add constraint Record_Nurse foreign key (First_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCIN_RECORD] add constraint Record_SeconNurse foreign key (Second_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCIN_RECORD] add constraint Record_ThirdNurse foreign key (Third_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCIN_RECORD] add constraint Record_Citizen foreign key (CitizenID) references [dbo].[CITIZEN](citizenID)

--Add foreign key for Nurse
alter table [dbo].[Nurse] add constraint Nurse_Manager foreign key (ManagerID) references [dbo].[MANAGER](ManagerID)

--Add foreign key for Vaccin Entry
alter table [dbo].[VACCIN_ENTRY] add constraint Entry_Supplier foreign key (SupID) references [dbo].[SUPPLIER](SupID)
alter table [dbo].[VACCIN_ENTRY] add constraint Entry_Manager foreign key (managerID) references [dbo].[MANAGER](ManagerID)

--Add foreign key for Entry Detail
alter table [dbo].[ENTRY_DETAIL] add constraint Detail_Entry foreign key (EntryID) references [dbo].[VACCIN_ENTRY](EntryID)
alter table [dbo].[ENTRY_DETAIL] add constraint Detail_Vaccin foreign key (VaccinID) references [dbo].[VACCIN](VaccinID)

--Add foreing key for Warehouse_Vaccin
alter table [dbo].[WAREHOUSE_VACCIN] add constraint Warehouse_And_Vaccin foreign key (VaccinID) references [dbo].[VACCIN](VaccinID)
alter table [dbo].[WAREHOUSE_VACCIN] add constraint Warehouse_And_warehouse foreign key (WarehouseID) references [dbo].[WAREHOUSE](WarehouseID)
