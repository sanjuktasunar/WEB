
GO
CREATE TABLE [dbo].[MemberLeads]
(
	LeadId int not null Identity(1,1) Constraint MemberLeads_pk Primary Key,
	Name nvarchar(150) not null,
	ContactNumber nvarchar(20) not null,
	EmailAddress nvarchar(200) null,
	Address nvarchar(200) not null,
	Occupation nvarchar(200) null,
	VisitedDateAD Datetime not null,
	VisitedDateBS nvarchar(10) not null,
	CreatedBy int not null Constraint MemberLeads_Users_CreatedBy References Users(UserId),
	CreatedDate datetime not null,
	UpdatedBy int null Constraint MemberLeads_Users_UpdatedBy References Users(UserId),
	UpdatedDate datetime null,
);
GO

GO
CREATE UNIQUE INDEX [MemberLeads_ContactNumber_ui] ON
[dbo].[MemberLeads](ContactNumber)
GO
GO
CREATE UNIQUE INDEX [MemberLeads_EmailAddress_ui] ON
[dbo].[MemberLeads](EmailAddress) WHERE EmailAddress IS NOT NULL
GO


GO
CREATE TABLE [dbo].[MemberLeadFollowup]
(
	FollowupId int not null Identity(1,1) Constraint MemberLeadFollowup_pk Primary Key,
	LeadId int not null Constraint MemberLeadFollowup_MemberLead_LeadId_fk References MemberLeads(LeadId),
	Remarks nvarchar(1000) not null,
	FollowupDateBS nvarchar(10) not null,
	FollowupDateAD datetime not null
);
GO