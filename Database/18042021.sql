USE [LearningHub]
GO
/****** Object:  Table [dbo].[AdminTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminTbl](
	[AdminID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[AdminName] [varchar](50) NULL,
 CONSTRAINT [PK_AdminTbl] PRIMARY KEY CLUSTERED 
(
	[AdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[APITokenTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[APITokenTbl](
	[APITokenID] [int] IDENTITY(1,1) NOT NULL,
	[APIName] [varchar](50) NULL,
	[APIKey] [varchar](50) NULL,
	[SenderID] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[IsTest] [bit] NULL,
	[IsChannel] [varchar](50) NULL,
	[IsUnicord] [varchar](50) NULL,
	[IsFlash] [varchar](50) NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
	[Route] [varchar](50) NULL,
 CONSTRAINT [PK_APITokenTbl] PRIMARY KEY CLUSTERED 
(
	[APITokenID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AssignmentTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AssignmentTbl](
	[AssignmentID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubjectID] [bigint] NULL,
	[Topic] [varchar](50) NULL,
	[Description] [varchar](max) NULL,
	[DeadLineOn] [datetime] NULL,
	[PublishedOn] [datetime] NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_AssaignmentTbl] PRIMARY KEY CLUSTERED 
(
	[AssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChapterTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChapterTbl](
	[ChapterID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubjectID] [bigint] NULL,
	[ChapterName] [varchar](50) NULL,
 CONSTRAINT [PK_ChapterTbl] PRIMARY KEY CLUSTERED 
(
	[ChapterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassTbl](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[DeptID] [int] NULL,
	[ClassName] [varchar](50) NULL,
 CONSTRAINT [PK_ClassTbl] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CollegeTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollegeTbl](
	[CollegeID] [int] IDENTITY(1,1) NOT NULL,
	[CollegeName] [varchar](200) NULL,
 CONSTRAINT [PK_CollegeTbl] PRIMARY KEY CLUSTERED 
(
	[CollegeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CredentialTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CredentialTbl](
	[CredID] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_CredentialTbl] PRIMARY KEY CLUSTERED 
(
	[CredID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentTbl](
	[DeptID] [int] IDENTITY(1,1) NOT NULL,
	[DeptName] [varchar](50) NULL,
 CONSTRAINT [PK_DepartmentTbl] PRIMARY KEY CLUSTERED 
(
	[DeptID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamQuestionTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamQuestionTbl](
	[ExamQuestionID] [bigint] IDENTITY(1,1) NOT NULL,
	[ExamID] [bigint] NULL,
	[Question] [varchar](500) NULL,
	[OptionA] [varchar](50) NULL,
	[OptionB] [varchar](50) NULL,
	[OptionC] [varchar](50) NULL,
	[OptionD] [varchar](50) NULL,
	[Answer] [varchar](20) NULL,
 CONSTRAINT [PK_ExamQuestionTbl] PRIMARY KEY CLUSTERED 
(
	[ExamQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExamTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExamTbl](
	[ExamID] [bigint] IDENTITY(1,1) NOT NULL,
	[SubjectID] [bigint] NULL,
	[ExamName] [varchar](50) NULL,
	[ConductDate] [datetime] NULL,
 CONSTRAINT [PK_ExamTbl] PRIMARY KEY CLUSTERED 
(
	[ExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageTbl](
	[MsgID] [bigint] IDENTITY(1,1) NOT NULL,
	[MessageText] [varchar](max) NULL,
	[FirstPara] [varchar](50) NULL,
	[SecondPara] [varchar](50) NULL,
	[ThirdPara] [varchar](50) NULL,
	[FourthPara] [varchar](50) NULL,
	[FifthPara] [varchar](50) NULL,
 CONSTRAINT [PK_MessageTvl] PRIMARY KEY CLUSTERED 
(
	[MsgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MesSendTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MesSendTbl](
	[MsgSendID] [bigint] IDENTITY(1,1) NOT NULL,
	[MsgTransID] [bigint] NULL,
	[ParentID] [bigint] NULL,
	[MessageText] [varchar](50) NULL,
 CONSTRAINT [PK_MesSendTbl] PRIMARY KEY CLUSTERED 
(
	[MsgSendID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MsgTransTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MsgTransTbl](
	[MsgTransID] [bigint] IDENTITY(1,1) NOT NULL,
	[ErrCode] [varchar](50) NULL,
	[ErrMsg] [varchar](50) NULL,
	[JobId] [varchar](50) NULL,
 CONSTRAINT [PK_MsgTransTbl] PRIMARY KEY CLUSTERED 
(
	[MsgTransID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParentTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParentTbl](
	[ParentID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[ParentName] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_ParentTbl] PRIMARY KEY CLUSTERED 
(
	[ParentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleTbl](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [varchar](50) NULL,
 CONSTRAINT [PK_RoleTbl] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffAssignClassTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffAssignClassTbl](
	[AllotedClassID] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffID] [bigint] NULL,
	[ClassID] [int] NULL,
 CONSTRAINT [PK_StaffAssignClass] PRIMARY KEY CLUSTERED 
(
	[AllotedClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffAttendanceTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffAttendanceTbl](
	[StaffAttendanceID] [bigint] IDENTITY(1,1) NOT NULL,
	[StaffID] [bigint] NULL,
	[PresentDate] [datetime] NULL,
	[IsLogin] [datetime] NULL,
	[IsLogout] [datetime] NULL,
 CONSTRAINT [PK_StaffAttendance] PRIMARY KEY CLUSTERED 
(
	[StaffAttendanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StaffTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StaffTbl](
	[StaffID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[StaffName] [varchar](50) NULL,
	[IsActive] [bit] NULL,
	[SkypeUserName] [varchar](50) NULL,
	[IsHead] [bit] NULL,
	[DeptID] [int] NULL,
 CONSTRAINT [PK_StaffTbl] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentAssignmentTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentAssignmentTbl](
	[StudentAssignmentID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NULL,
	[AssignmentID] [bigint] NULL,
	[MeterailPath] [varchar](50) NULL,
	[AssignmentMark] [int] NULL,
	[SubmittedDate] [datetime] NULL,
	[IsStaffApprove] [bit] NULL,
	[IsHODApprove] [bit] NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_StudentAssignmentTbl] PRIMARY KEY CLUSTERED 
(
	[StudentAssignmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentClassAttend]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentClassAttend](
	[StudentClassAttID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NULL,
	[SubjectID] [bigint] NULL,
	[PresentDate] [datetime] NULL,
	[OpenTime] [datetime] NULL,
	[CloseTime] [datetime] NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_StudentClassAttend] PRIMARY KEY CLUSTERED 
(
	[StudentClassAttID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentExamResultTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentExamResultTbl](
	[StudentExamResultID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentExamID] [bigint] NULL,
	[ExamQuestionID] [bigint] NULL,
	[SelectedAnswer] [varchar](50) NULL,
	[IsTrue] [bit] NULL,
	[AttendTime] [datetime] NULL,
 CONSTRAINT [PK_StudentExamResultTbl_1] PRIMARY KEY CLUSTERED 
(
	[StudentExamResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentExamTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentExamTbl](
	[StudentExamID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NULL,
	[ExamID] [bigint] NULL,
	[TotalAttendQuestion] [int] NULL,
	[TotalCurrectAnwer] [int] NULL,
	[TotalWrongAnswer] [int] NULL,
 CONSTRAINT [PK_StudentExamResultTbl] PRIMARY KEY CLUSTERED 
(
	[StudentExamID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentsAttentanceTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentsAttentanceTbl](
	[StudentAttentanceID] [bigint] IDENTITY(1,1) NOT NULL,
	[StudentID] [bigint] NULL,
	[PresentDate] [datetime] NULL,
	[IsLogin] [datetime] NULL,
	[IsLogout] [datetime] NULL,
	[LoginMinites] [int] NULL,
	[IsStaffApprove] [bit] NULL,
	[IsHODApprove] [bit] NULL,
 CONSTRAINT [PK_StudentsAttentanceTbl] PRIMARY KEY CLUSTERED 
(
	[StudentAttentanceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTbl](
	[StudentID] [bigint] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NULL,
	[ClassID] [int] NULL,
	[ParentID] [bigint] NULL,
	[StudentName] [varchar](50) NULL,
	[Mobile] [varchar](50) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_StudentTbl] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudyMeterialTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudyMeterialTbl](
	[StudyMeterialID] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterID] [bigint] NULL,
	[MeterialPath] [varchar](500) NULL,
	[StudyMeterialTitle] [varchar](50) NULL,
	[PublishedOn] [datetime] NULL,
	[DisplayOrder] [int] NULL,
	[UpdatedOn] [datetime] NULL,
	[EnteredOn] [datetime] NULL,
 CONSTRAINT [PK_StudyMeterialTbl] PRIMARY KEY CLUSTERED 
(
	[StudyMeterialID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectTbl](
	[SubjectID] [bigint] IDENTITY(1,1) NOT NULL,
	[ClassID] [int] NULL,
	[SubjectName] [varchar](50) NULL,
 CONSTRAINT [PK_SubjectTbl] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SuperAdminTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SuperAdminTbl](
	[SuperAdminID] [int] IDENTITY(1,1) NOT NULL,
	[CredID] [bigint] NOT NULL,
	[SuperAdminName] [varchar](50) NULL,
 CONSTRAINT [PK_SuperAdminTbl] PRIMARY KEY CLUSTERED 
(
	[SuperAdminID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoTbl]    Script Date: 18-04-2021 04:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoTbl](
	[VideoID] [bigint] IDENTITY(1,1) NOT NULL,
	[ChapterID] [bigint] NULL,
	[VideoLink] [varchar](max) NULL,
	[VideoTitle] [varchar](200) NULL,
	[PublishedOn] [datetime] NULL,
	[DisplayOrder] [int] NULL,
	[EnteredOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_VideoTbl] PRIMARY KEY CLUSTERED 
(
	[VideoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdminTbl]  WITH CHECK ADD  CONSTRAINT [FK_AdminTbl_CredentialTbl] FOREIGN KEY([CredID])
REFERENCES [dbo].[CredentialTbl] ([CredID])
GO
ALTER TABLE [dbo].[AdminTbl] CHECK CONSTRAINT [FK_AdminTbl_CredentialTbl]
GO
ALTER TABLE [dbo].[AssignmentTbl]  WITH CHECK ADD  CONSTRAINT [FK_AssaignmentTbl_SubjectTbl] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectTbl] ([SubjectID])
GO
ALTER TABLE [dbo].[AssignmentTbl] CHECK CONSTRAINT [FK_AssaignmentTbl_SubjectTbl]
GO
ALTER TABLE [dbo].[ChapterTbl]  WITH CHECK ADD  CONSTRAINT [FK_ChapterTbl_SubjectTbl] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectTbl] ([SubjectID])
GO
ALTER TABLE [dbo].[ChapterTbl] CHECK CONSTRAINT [FK_ChapterTbl_SubjectTbl]
GO
ALTER TABLE [dbo].[ClassTbl]  WITH CHECK ADD  CONSTRAINT [FK_ClassTbl_DepartmentTbl] FOREIGN KEY([DeptID])
REFERENCES [dbo].[DepartmentTbl] ([DeptID])
GO
ALTER TABLE [dbo].[ClassTbl] CHECK CONSTRAINT [FK_ClassTbl_DepartmentTbl]
GO
ALTER TABLE [dbo].[CredentialTbl]  WITH CHECK ADD  CONSTRAINT [FK_CredentialTbl_RoleTbl] FOREIGN KEY([RoleID])
REFERENCES [dbo].[RoleTbl] ([RoleID])
GO
ALTER TABLE [dbo].[CredentialTbl] CHECK CONSTRAINT [FK_CredentialTbl_RoleTbl]
GO
ALTER TABLE [dbo].[ExamQuestionTbl]  WITH CHECK ADD  CONSTRAINT [FK_ExamQuestionTbl_ExamTbl] FOREIGN KEY([ExamID])
REFERENCES [dbo].[ExamTbl] ([ExamID])
GO
ALTER TABLE [dbo].[ExamQuestionTbl] CHECK CONSTRAINT [FK_ExamQuestionTbl_ExamTbl]
GO
ALTER TABLE [dbo].[ExamTbl]  WITH CHECK ADD  CONSTRAINT [FK_ExamTbl_SubjectTbl] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectTbl] ([SubjectID])
GO
ALTER TABLE [dbo].[ExamTbl] CHECK CONSTRAINT [FK_ExamTbl_SubjectTbl]
GO
ALTER TABLE [dbo].[MesSendTbl]  WITH CHECK ADD  CONSTRAINT [FK_MesSendTbl_MesSendTbl] FOREIGN KEY([ParentID])
REFERENCES [dbo].[ParentTbl] ([ParentID])
GO
ALTER TABLE [dbo].[MesSendTbl] CHECK CONSTRAINT [FK_MesSendTbl_MesSendTbl]
GO
ALTER TABLE [dbo].[MesSendTbl]  WITH CHECK ADD  CONSTRAINT [FK_MesSendTbl_MsgTransTbl] FOREIGN KEY([MsgTransID])
REFERENCES [dbo].[MsgTransTbl] ([MsgTransID])
GO
ALTER TABLE [dbo].[MesSendTbl] CHECK CONSTRAINT [FK_MesSendTbl_MsgTransTbl]
GO
ALTER TABLE [dbo].[ParentTbl]  WITH CHECK ADD  CONSTRAINT [FK_ParentTbl_CredentialTbl] FOREIGN KEY([CredID])
REFERENCES [dbo].[CredentialTbl] ([CredID])
GO
ALTER TABLE [dbo].[ParentTbl] CHECK CONSTRAINT [FK_ParentTbl_CredentialTbl]
GO
ALTER TABLE [dbo].[StaffAssignClassTbl]  WITH CHECK ADD  CONSTRAINT [FK_StaffAssignClassTbl_ClassTbl] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ClassTbl] ([ClassID])
GO
ALTER TABLE [dbo].[StaffAssignClassTbl] CHECK CONSTRAINT [FK_StaffAssignClassTbl_ClassTbl]
GO
ALTER TABLE [dbo].[StaffAssignClassTbl]  WITH CHECK ADD  CONSTRAINT [FK_StaffAssignClassTbl_StaffTbl] FOREIGN KEY([StaffID])
REFERENCES [dbo].[StaffTbl] ([StaffID])
GO
ALTER TABLE [dbo].[StaffAssignClassTbl] CHECK CONSTRAINT [FK_StaffAssignClassTbl_StaffTbl]
GO
ALTER TABLE [dbo].[StaffAttendanceTbl]  WITH CHECK ADD  CONSTRAINT [FK_StaffAttendance_StaffTbl] FOREIGN KEY([StaffID])
REFERENCES [dbo].[StaffTbl] ([StaffID])
GO
ALTER TABLE [dbo].[StaffAttendanceTbl] CHECK CONSTRAINT [FK_StaffAttendance_StaffTbl]
GO
ALTER TABLE [dbo].[StaffTbl]  WITH CHECK ADD  CONSTRAINT [FK_StaffTbl_CredentialTbl] FOREIGN KEY([CredID])
REFERENCES [dbo].[CredentialTbl] ([CredID])
GO
ALTER TABLE [dbo].[StaffTbl] CHECK CONSTRAINT [FK_StaffTbl_CredentialTbl]
GO
ALTER TABLE [dbo].[StaffTbl]  WITH CHECK ADD  CONSTRAINT [FK_StaffTbl_StaffTbl] FOREIGN KEY([DeptID])
REFERENCES [dbo].[DepartmentTbl] ([DeptID])
GO
ALTER TABLE [dbo].[StaffTbl] CHECK CONSTRAINT [FK_StaffTbl_StaffTbl]
GO
ALTER TABLE [dbo].[StudentAssignmentTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentAssignmentTbl_AssignmentTbl] FOREIGN KEY([AssignmentID])
REFERENCES [dbo].[AssignmentTbl] ([AssignmentID])
GO
ALTER TABLE [dbo].[StudentAssignmentTbl] CHECK CONSTRAINT [FK_StudentAssignmentTbl_AssignmentTbl]
GO
ALTER TABLE [dbo].[StudentAssignmentTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentAssignmentTbl_StudentTbl] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentTbl] ([StudentID])
GO
ALTER TABLE [dbo].[StudentAssignmentTbl] CHECK CONSTRAINT [FK_StudentAssignmentTbl_StudentTbl]
GO
ALTER TABLE [dbo].[StudentClassAttend]  WITH CHECK ADD  CONSTRAINT [FK_StudentClassAttend_StudentTbl] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentTbl] ([StudentID])
GO
ALTER TABLE [dbo].[StudentClassAttend] CHECK CONSTRAINT [FK_StudentClassAttend_StudentTbl]
GO
ALTER TABLE [dbo].[StudentClassAttend]  WITH CHECK ADD  CONSTRAINT [FK_StudentClassAttend_SubjectTbl] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectTbl] ([SubjectID])
GO
ALTER TABLE [dbo].[StudentClassAttend] CHECK CONSTRAINT [FK_StudentClassAttend_SubjectTbl]
GO
ALTER TABLE [dbo].[StudentExamResultTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamResultTbl_ExamQuestionTbl] FOREIGN KEY([ExamQuestionID])
REFERENCES [dbo].[ExamQuestionTbl] ([ExamQuestionID])
GO
ALTER TABLE [dbo].[StudentExamResultTbl] CHECK CONSTRAINT [FK_StudentExamResultTbl_ExamQuestionTbl]
GO
ALTER TABLE [dbo].[StudentExamResultTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamResultTbl_StudentExamTbl] FOREIGN KEY([StudentExamID])
REFERENCES [dbo].[StudentExamTbl] ([StudentExamID])
GO
ALTER TABLE [dbo].[StudentExamResultTbl] CHECK CONSTRAINT [FK_StudentExamResultTbl_StudentExamTbl]
GO
ALTER TABLE [dbo].[StudentExamTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamTbl_ExamTbl] FOREIGN KEY([ExamID])
REFERENCES [dbo].[ExamTbl] ([ExamID])
GO
ALTER TABLE [dbo].[StudentExamTbl] CHECK CONSTRAINT [FK_StudentExamTbl_ExamTbl]
GO
ALTER TABLE [dbo].[StudentExamTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentExamTbl_StudentTbl] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentTbl] ([StudentID])
GO
ALTER TABLE [dbo].[StudentExamTbl] CHECK CONSTRAINT [FK_StudentExamTbl_StudentTbl]
GO
ALTER TABLE [dbo].[StudentsAttentanceTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentsAttentanceTbl_StudentTbl] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentTbl] ([StudentID])
GO
ALTER TABLE [dbo].[StudentsAttentanceTbl] CHECK CONSTRAINT [FK_StudentsAttentanceTbl_StudentTbl]
GO
ALTER TABLE [dbo].[StudentTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentTbl_ClassTbl] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ClassTbl] ([ClassID])
GO
ALTER TABLE [dbo].[StudentTbl] CHECK CONSTRAINT [FK_StudentTbl_ClassTbl]
GO
ALTER TABLE [dbo].[StudentTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentTbl_CredentialTbl] FOREIGN KEY([CredID])
REFERENCES [dbo].[CredentialTbl] ([CredID])
GO
ALTER TABLE [dbo].[StudentTbl] CHECK CONSTRAINT [FK_StudentTbl_CredentialTbl]
GO
ALTER TABLE [dbo].[StudentTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudentTbl_ParentTbl] FOREIGN KEY([ParentID])
REFERENCES [dbo].[ParentTbl] ([ParentID])
GO
ALTER TABLE [dbo].[StudentTbl] CHECK CONSTRAINT [FK_StudentTbl_ParentTbl]
GO
ALTER TABLE [dbo].[StudyMeterialTbl]  WITH CHECK ADD  CONSTRAINT [FK_StudyMeterialTbl_ChapterTbl] FOREIGN KEY([ChapterID])
REFERENCES [dbo].[ChapterTbl] ([ChapterID])
GO
ALTER TABLE [dbo].[StudyMeterialTbl] CHECK CONSTRAINT [FK_StudyMeterialTbl_ChapterTbl]
GO
ALTER TABLE [dbo].[SubjectTbl]  WITH CHECK ADD  CONSTRAINT [FK_SubjectTbl_SubjectTbl] FOREIGN KEY([ClassID])
REFERENCES [dbo].[ClassTbl] ([ClassID])
GO
ALTER TABLE [dbo].[SubjectTbl] CHECK CONSTRAINT [FK_SubjectTbl_SubjectTbl]
GO
ALTER TABLE [dbo].[SuperAdminTbl]  WITH CHECK ADD  CONSTRAINT [FK_SuperAdminTbl_CredentialTbl] FOREIGN KEY([CredID])
REFERENCES [dbo].[CredentialTbl] ([CredID])
GO
ALTER TABLE [dbo].[SuperAdminTbl] CHECK CONSTRAINT [FK_SuperAdminTbl_CredentialTbl]
GO
ALTER TABLE [dbo].[VideoTbl]  WITH CHECK ADD  CONSTRAINT [FK_VideoTbl_ChapterTbl] FOREIGN KEY([ChapterID])
REFERENCES [dbo].[ChapterTbl] ([ChapterID])
GO
ALTER TABLE [dbo].[VideoTbl] CHECK CONSTRAINT [FK_VideoTbl_ChapterTbl]
GO
