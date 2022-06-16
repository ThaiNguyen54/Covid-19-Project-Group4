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
	Gender varchar(20),
	Age int
)

Create table VACCINE_REGISTRATION_FORM
(
	FormID int Identity not null primary key,
	CitizenID int not null,
	VaccineID int not null,
	InjectionDate date not null,
	RegisterDate date not null,
	InjectionNumber varchar(20) not null
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
	Email varchar(100),
	Age int
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
	Email varchar(100),
	Age int
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
	Primary key (VaccineID, EntryID)
)

Create table WAREHOUSE_VACCINE
(
	VaccineID int not null,
	WarehouseID int not null,
	Stock int not null, 
	Primary key (VaccineID, WarehouseID)
)

Create table WAREHOUSE
(
	WarehouseID int not null primary key,
	Location varchar(100)
)

--Add foreign key for register form
alter table [dbo].[VACCINE_REGISTRATION_FORM] add constraint Register_Citizen foreign key (CitizenID) references [dbo].[CITIZEN](CitizenID)
alter table [dbo].[VACCINE_REGISTRATION_FORM] add constraint Register_Vaccine foreign key (VaccineID) references [dbo].[VACCINE](VaccineID)


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
alter table [dbo].[WAREHOUSE_VACCINE] add constraint Warehouse_And_Vaccine foreign key (VaccineID) references [dbo].[VACCINE](VaccineID)
alter table [dbo].[WAREHOUSE_VACCINE] add constraint Warehouse_And_warehouse foreign key (WarehouseID) references [dbo].[WAREHOUSE](WarehouseID)



-- Insert Vaccine Record Trigger --------------------------------------

create or alter trigger Check_Requirement_SecondVaccine_register_form
on VACCINE_REGISTRATION_FORM
after insert
as 
	declare @cid int, @date_inject date, @first_inject_date date, @vID int, @FirstVaccineName varchar(10), @firstVaccineID int, @injectNumber int, @Second_inject_date date
	declare @Second_vaccination_date int, @Third_vaccination_date int, @preferred_second_date date, @preferred_third_date date, @current_date date, @error varchar(200)
begin

	select @injectNumber = InjectionNumber
	from inserted

	if (@injectNumber = 2)
	begin

		select @cid = CitizenID, @date_inject = InjectionDate
		from inserted

		select @first_inject_date = FirstDate
		from VACCINE_RECORD
		where CitizenID = @cid

		select @FirstVaccineName = FirstVaccine
		from VACCINE_RECORD

		select @firstVaccineID = VaccineID
		from VACCINE
		where VaccineName = @FirstVaccineName

		select @vID = VaccineID
		from inserted

		select @Second_vaccination_date = datediff(day,@first_inject_date, @date_inject)
		if(@Second_vaccination_date < 60)
		begin
			select @preferred_second_date = DATEADD(dayofyear, 60, @first_inject_date)
			select @error = concat(concat(concat('Please wait ', convert(varchar,datediff(day, SYSDATETIME(),@preferred_second_date))), ' day(s) to get the second vaccination, '), concat('you can get the second vaccination on ', convert(varchar, @preferred_second_date)))
			RAISERROR(@error, 16, 1)
			RollBack Transaction
		end

		if (@vID != @firstVaccineID)
		begin
			select @error = 'You have to choose the same type of vaccine with the first injection.'
			RAISERROR(@error, 16, 1)
			rollback transaction
		end
	end
	
	if (@injectNumber = 3)
	begin
		select @cid = CitizenID, @date_inject = InjectionDate
		from inserted

		select @Second_inject_date = SecondDate
		from VACCINE_RECORD
		where CitizenID = @cid

		select @Third_vaccination_date = DATEDIFF(day, @Second_inject_date, @date_inject)
		if(@Third_vaccination_date < 90)
		begin
			select @preferred_third_date = DATEADD(dayofyear, 90, @Second_inject_date)
			select @error = concat(concat(concat('Please wait ', convert(varchar,datediff(day, SYSDATETIME(),@preferred_third_date))), ' day(s) to get the second vaccination, '), concat('you can get the second vaccination on ', convert(varchar, @preferred_third_date)))
			RAISERROR(@error, 16, 1)
			rollback transaction
		end
	end
end

-----------------------------------------------------


-- Trigger to check age of citizen --------------

create or alter trigger Check_Age_Citizen
on Citizen
after insert
as
	declare @cid int, @birth_date date, @current_date date
begin
	select @cid = CitizenID, @birth_date = BDate
	from inserted

	select @current_date = SYSDATETIME()
	if (datediff(year, @birth_date, @current_date) < 18)
	begin
		print'Citizen must be older than or equal to 18 years old'
		rollback transaction
	end
end

-----------------------------------------------------

--Trigger to compute stock of vaccine------------------------

create or alter trigger Vaccine_Stock
on VACCINE_RECORD
after insert
as
	declare @FirstVaccineName varchar(10), @vID int
begin
	select @FirstVaccineName = FirstVaccine
	from inserted

	select @vID = VaccineID
	from VACCINE
	where VaccineName = @FirstVaccineName

	update WAREHOUSE_VACCINE
	set Stock = Stock - 1
	where VaccineID = @vID
end


create or alter trigger SecondVaccine_Stock
on VACCINE_RECORD
after update
as
	declare @SecondVaccineName varchar(10), @SecondvID int, @ThirdVaccineName varchar(100), @ThirdvID int, @cID int, @firstVaccineName varchar(10)
	declare @second_injection_date date, @third_injection_date date, @first_injection_date date
	declare @second_vaccine_Name varchar(10), @first_vaccine_Name varchar(10)
begin
	select @SecondVaccineName = SecondVaccine, @ThirdVaccineName = ThirdVaccine, @cID = CitizenID
	from inserted

	select @SecondvID = VaccineID
	from VACCINE
	where VaccineName = @SecondVaccineName

	select @ThirdvID = VaccineID
	from VACCINE
	where VaccineName = @ThirdVaccineName

	select @first_injection_date = FirstDate
	from Vaccine_Record

	if (UPDATE(SecondVaccine))
	begin
		select @second_injection_date = SecondDate, @second_vaccine_Name = SecondVaccine
		from inserted

		select @first_vaccine_Name = FirstVaccine
		from VACCINE_RECORD
		if (@first_vaccine_Name != @second_vaccine_Name)
		begin
			print'Different type of vaccine'
			rollback transaction
		end
		else 
		begin
			if(DATEDIFF(day, @first_injection_date, @second_injection_date) < 60)
			begin 
				print'Not enough days'
				rollback transaction
			end
			else
			begin
				update WAREHOUSE_VACCINE
				set Stock = Stock - 1
				where VaccineID = @SecondvID
			end
		end
	end

	
	if (UPDATE(ThirdVaccine))
	begin
		select @third_injection_date = ThirdDate
		from inserted

		select @second_injection_date = SecondDate
		from VACCINE_RECORD
		if(DATEDIFF(day, @second_injection_date, @third_injection_date) < 90)
		begin
			print'Not enough days'
			rollback transaction
		end
		else 
		begin
			update WAREHOUSE_VACCINE
			set Stock = Stock - 1
			where VaccineID = @ThirdvID
		end
	end

	select @firstVaccineName = FirstVaccine, @SecondVaccineName = SecondVaccine, @ThirdVaccineName = ThirdVaccine
	from VACCINE_RECORD
	where CitizenID = @cID

	if(@SecondVaccineName is not null and @ThirdVaccineName is null)
	begin
		update VACCINE_RECORD
		set Number_Injection = 2
		where CitizenID = @cID
	end
	else if (@SecondVaccineName is not null and @ThirdVaccineName is not null)
	begin
		update VACCINE_RECORD
		set Number_Injection = 3
		where CitizenID = @cID
	end
	
end

-----------------------------------------------------
