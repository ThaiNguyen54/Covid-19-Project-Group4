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
	Password varchar(100),
	Phone varchar(100),
	Email varchar(100),
	Gender varchar(20)
)

Create table VACCINE_REGISTRATION_FORM
(
	FormID int not null primary key,
	CitizenID int not null,
	VaccineID int not null,
	InjectionDate date not null,
	RegisterDate date not null,
)

Create table VACCINE_RECORD
(
	RecordID int not null primary key,
	CitizenID int not null,
	Number_Injection int not null,
	FirstDate date,
	FirstVaccine varchar(100),
	SecondDate date,
	SecondVaccine varchar(100),
	ThirdDate date,
	ThirdVaccine varchar(100),
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
	Phone varchar(100),
	Password varchar(100),
	Email varchar(100)
)

create table SUPPLIER
(
	SupID int not null primary key,
	SupName varchar(100),
	SupAddress varchar(100),
	Phone varchar(100)
)

Create table VACCINE_ENTRY
(
	EntryID int not null primary key,
	ManagerID int not null,
	SupID int not null,
	EntryDate date not null,
	Amount_Vaccine int not null
)

Create table MANAGER
(
	ManagerID int not null primary key,
	FName varchar(100),
	LName varchar(100),
	BDate date,
	Address varchar(100),
	Phone varchar(100),
	Password varchar(100),
	Email varchar(100)
)

Create table VACCINE
(
	VaccineID int not null primary key,
	WarehouseID int not null,
	VaccineName varchar(100) not null
)

Create table ENTRY_DETAIL
(
	VaccineID int not null,
	EntryID int not null,
	Quantity int not null,
	Primary key (VaccinID, EntryID)
)

Create table WAREHOUSE_VACCINE
(
	VaccineID int not null,
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
alter table [dbo].[VACCINE_REGISTRATION_FORM] add constraint Register_Citizen foreign key (CitizenID) references [dbo].[CITIZEN](CitizenID)
alter table [dbo].[VACCINE_REGISTRATION_FORM] add constraint Register_Vaccin foreign key (VaccineID) references [dbo].[VACCINE](VaccineID)


--Add foreign key for record 
alter table [dbo].[VACCINE_RECORD] add constraint Record_Nurse foreign key (First_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCINE_RECORD] add constraint Record_SeconNurse foreign key (Second_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCINE_RECORD] add constraint Record_ThirdNurse foreign key (Third_NurseID) references [dbo].[Nurse](NurseID)
alter table [dbo].[VACCINE_RECORD] add constraint Record_Citizen foreign key (CitizenID) references [dbo].[CITIZEN](citizenID)

--Add foreign key for Nurse
alter table [dbo].[Nurse] add constraint Nurse_Manager foreign key (ManagerID) references [dbo].[MANAGER](ManagerID)

--Add foreign key for Vaccin Entry
alter table [dbo].[VACCINE_ENTRY] add constraint Entry_Supplier foreign key (SupID) references [dbo].[SUPPLIER](SupID)
alter table [dbo].[VACCINE_ENTRY] add constraint Entry_Manager foreign key (managerID) references [dbo].[MANAGER](ManagerID)

--Add foreign key for Entry Detail
alter table [dbo].[ENTRY_DETAIL] add constraint Detail_Entry foreign key (EntryID) references [dbo].[VACCINE_ENTRY](EntryID)
alter table [dbo].[ENTRY_DETAIL] add constraint Detail_Vaccin foreign key (VaccineID) references [dbo].[VACCINE](VaccineID)

--Add foreing key for Warehouse_Vaccin
alter table [dbo].[WAREHOUSE_VACCINE] add constraint Warehouse_And_Vaccine foreign key (VaccineID) references [dbo].[VACCINE](VaccinID)
alter table [dbo].[WAREHOUSE_VACCINE] add constraint Warehouse_And_warehouse foreign key (WarehouseID) references [dbo].[WAREHOUSE](WarehouseID)



-- Insert Vaccine Record Trigger

create or alter trigger insert_register_form
on VACCINE_REGISTRATION_FORM
after insert
as 
	declare @cid int, @date_ineject date, @first_inject_date date
begin
	select @cid = CitizenID, @date_ineject = InjectionDate
	from inserted

	select @first_inject_date = FirstDate
	from VACCINE_RECORD

	if(datediff(day,@first_inject_date, @date_ineject) < 60)
	begin
		print'not enough days'
		rollback transaction
	end
end
