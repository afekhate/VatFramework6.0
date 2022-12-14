
use VatFrameworkDb

DELETE FROM [dbo].[RolePermissions]
GO

DELETE FROM [dbo].[Permissions]
GO
DELETE FROM [dbo].[Icon]
GO
DELETE FROM [dbo].[EmailTemplates]
GO
DELETE FROM [dbo].[ApplicationUserPasswordHistorys]
GO
DELETE FROM [dbo].[ApplicationUserClaims]
GO

DELETE FROM [dbo].[AspNetUserTokens]
GO
DELETE FROM [dbo].[AspNetUserLogins]
GO
DELETE FROM [dbo].[AspNetUserClaims]
GO
DELETE FROM [dbo].[AspNetRoleClaims]
GO
DELETE FROM [dbo].[AspNetUserRoles]
GO
DELETE FROM [dbo].[AspNetUsers]
GO
DELETE FROM [dbo].[AspNetRoles]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'2c0935dc-e157-44cc-900b-57c51a9ac505', N'Reviewer', NULL, N'a64b0c58-401d-4bd2-940c-8fc317635fb0', N'Reviewer', N'Reviewer', 0, CAST(N'2020-03-27T01:51:58.0286228' AS DateTime2), N'', NULL, CAST(N'2020-04-04T19:48:47.7736890' AS DateTime2), 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'3951b0f5-cf16-4007-be43-b260171f8797', N'Super Administrator', NULL, N'dc4522b9-7cdb-40d6-aeed-efea75b0fdd1', N'Super Administrator', N'Super Administrator', 0, CAST(N'2020-03-27T01:47:08.7928494' AS DateTime2), N'', NULL, NULL, 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'5507d0a9-4dbc-41f6-83ae-832939b1bf4d', N'Content Manager', NULL, N'54cc2318-9edc-44cc-bf3f-936d4c9da880', N'Content Manager', N'Content Manager', 0, CAST(N'2020-03-27T01:49:31.2691062' AS DateTime2), N'', NULL, NULL, 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'68a1511c-0b83-4fa8-ba16-08fdb8d2e876', N'Accountant ', NULL, N'8f9d5de5-08b4-4f1a-b77e-06ea5dde6c80', N'Accountant ', N'Accountant ', 0, CAST(N'2020-03-27T01:51:30.4142844' AS DateTime2), N'', NULL, NULL, 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'7b87345676944fe89a2bb55174ecc79d', N'Supervisor', NULL, N'0582e7c1-86df-4c8a-8a71-e8809475990c', N'Supervisor', N'Supervisor', 0, CAST(N'2020-04-04T16:32:12.1433342' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, CAST(N'2020-04-04T17:14:21.0087503' AS DateTime2), 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'd763fd27-6c70-4b0c-b2f8-161bd360fee8', N'Manager', NULL, N'62a74f58-847c-4b45-ad3b-2e39ef02ac76', N'Manager', N'Manager', 0, CAST(N'2020-03-27T01:50:00.6248230' AS DateTime2), N'', NULL, NULL, 1)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp], [RoleName], [RoleDescription], [IsSysAdmin], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive]) VALUES (N'f893c609-4974-4078-a5a7-2efdbafebd54', N'Administrator', NULL, N'b3152af4-8492-4796-8b8a-07678372c43e', N'Administrator', N'Administrator', 0, CAST(N'2020-03-27T01:47:37.1178781' AS DateTime2), N'', NULL, NULL, 1)

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [MiddleName], [LastName], [DOB], [MobileNumber], [ExpirationTime], [LastLoginDate], [PwdExpiryDate], [PwdChangedDate], [ForcePwdChange], [LastModified], [ModifiedBy], [CreatedDate], [CreatedBy], [IsDeleted], [IsActive], [RoleId], [ConfirmationLinkExpireDate]) VALUES (N'37f08491-865c-4536-a12f-a75ba608eee4', N'bolajiworld', N'BOLAJIWORLD', N'bolajiworld@gmail.com', N'BOLAJIWORLD@GMAIL.COM', 1, N'ANQY66fAJNoDlN00fXtBJaxt/9KSGSww02jVhWEWY84EQq0GTAQ39gTvS/993ChqFA==', N'9b8eaa03-54f2-4f6c-9f78-2ce4f1c4957e', N'56e88e76-1719-42ed-b7e1-e110b0cba1f7', N'09087665456', 1, 0, CAST(N'2020-03-28T22:26:01.3189686+01:00' AS DateTimeOffset), 0, 0, N'Iniobong', N'min', N'Jameson', CAST(N'2020-03-28T22:26:01.3177472' AS DateTime2), N'09087665456', CAST(N'2020-03-28T22:26:01.3185063' AS DateTime2), CAST(N'2020-03-28T22:26:01.3190473' AS DateTime2), CAST(N'2020-05-26T02:16:06.2602709' AS DateTime2), CAST(N'2020-03-28T22:26:01.3186305' AS DateTime2), 1, CAST(N'2020-03-28T22:26:01.3189664' AS DateTime2), N'1', CAST(N'2020-03-27T02:16:06.3045301' AS DateTime2), N'1', 0, 1, N'3951b0f5-cf16-4007-be43-b260171f8797', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [MiddleName], [LastName], [DOB], [MobileNumber], [ExpirationTime], [LastLoginDate], [PwdExpiryDate], [PwdChangedDate], [ForcePwdChange], [LastModified], [ModifiedBy], [CreatedDate], [CreatedBy], [IsDeleted], [IsActive], [RoleId], [ConfirmationLinkExpireDate]) VALUES (N'72324729-dba0-4168-ad65-df7118680b28', N'jilir21973', N'JILIR21973', N'jilir21973@gotkmail.com', N'JILIR21973@GOTKMAIL.COM', 1, N'ALHBOcBQR/N6PiOIt/0yKodQ8z4Gt58mTlxzFUPdOLBk+ZBcwzJg8CQnrejP/Wsg0Q==', N'2ZLZVEGG7UDZ7YLEOQGBWO2XIGCW7IHR', N'73a94dfe-2fa3-4525-a789-a89a9a34d57c', N'090876654561', 1, 0, NULL, 1, 0, N'f', N'd', N'd', NULL, N'090876654561', CAST(N'2020-05-29T17:29:01.7745856' AS DateTime2), NULL, CAST(N'2020-05-29T17:29:01.7745856' AS DateTime2), CAST(N'2020-03-31T14:30:33.6099939' AS DateTime2), 1, CAST(N'2020-03-31T14:30:33.6108536' AS DateTime2), NULL, CAST(N'2020-03-30T17:29:01.8413139' AS DateTime2), N'1', 0, 1, N'f893c609-4974-4078-a5a7-2efdbafebd54', CAST(N'2020-03-30T17:59:01.7745904' AS DateTime2))


INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'37f08491-865c-4536-a12f-a75ba608eee4', N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'72324729-dba0-4168-ad65-df7118680b28', N'f893c609-4974-4078-a5a7-2efdbafebd54')

SET IDENTITY_INSERT [dbo].[ApplicationUserPasswordHistorys] ON 

INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10008, CAST(N'2020-03-24T16:17:58.8509917' AS DateTime2), NULL, NULL, NULL, 0, 0, N'e0ea7b36-3680-4780-9516-3d4f6f713871', N'FbY1KscBWbtKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10009, CAST(N'2020-03-25T19:06:12.7458080' AS DateTime2), NULL, NULL, NULL, 0, 0, N'd9dc66f8-fe8b-4a51-a8bf-1fd0ee8a3979', N'InBnA/gIMtdKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10010, CAST(N'2020-03-26T00:50:44.0568685' AS DateTime2), NULL, NULL, NULL, 0, 0, N'0cda612f-dd83-47c2-81d9-5cc917a26211', N'Cs3CyPJXOcFKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10011, CAST(N'2020-03-26T20:28:13.9176645' AS DateTime2), NULL, NULL, NULL, 0, 0, N'ac366ed1-73e8-42de-8433-ec0833fee7d3', N'+zWYlbXqMS9KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10012, CAST(N'2020-03-26T22:38:03.4825287' AS DateTime2), NULL, NULL, NULL, 0, 0, N'5f0549ec-f47c-4572-a64c-8de8f80455db', N'D2xWTqEjo2dKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10013, CAST(N'2020-03-26T23:30:02.1864866' AS DateTime2), NULL, NULL, NULL, 0, 0, N'01d986b9-165b-42bd-b7eb-2e15f4dde68b', N'eCFKYm5vwiJKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10014, CAST(N'2020-03-27T02:16:06.5992709' AS DateTime2), NULL, NULL, NULL, 0, 0, N'37f08491-865c-4536-a12f-a75ba608eee4', N'pGUn7N6XK01KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10015, CAST(N'2020-03-29T21:41:29.3071466' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'bf447f77-f3c6-4761-9568-c2013c745ae0', N'IILdOWHDLIFKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10016, CAST(N'2020-03-29T21:44:01.9716826' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'eeaa34ed-443d-41f0-af08-f1d569819c95', N'1MOwWYOqP85KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10017, CAST(N'2020-03-29T21:45:13.6271738' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'91580616-95df-44f5-8fd5-82706b23ac98', N'j7UDSMNGVk5KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10018, CAST(N'2020-03-29T21:51:28.5493029' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'0', N'w9PjnRSHpsdKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10019, CAST(N'2020-03-29T22:01:38.2729359' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'0', N'HSEash/kRj1KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10020, CAST(N'2020-03-29T22:17:30.2254096' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'3ee6a4cb-f896-4886-a7cb-65aeae3016ab', N'V/kCmanft3RKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10021, CAST(N'2020-03-30T00:34:37.2978935' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'4fda8c25-c4b1-42d9-8d01-2f1e9a203a86', N'yKlhRSVEvJlKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10022, CAST(N'2020-03-30T00:36:59.1485918' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'4fda8c25-c4b1-42d9-8d01-2f1e9a203a86', N'ADzc5HrgHrpKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10023, CAST(N'2020-03-30T14:55:46.1413477' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'edbc567d-2bb2-4b7e-845d-7114a7b18ae0', N'cTG1F+FZPF1KirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10024, CAST(N'2020-03-30T14:57:43.0092709' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'edbc567d-2bb2-4b7e-845d-7114a7b18ae0', N'zPawiliEcplKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10025, CAST(N'2020-03-30T16:37:48.9411699' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'a343e2bf-6bb6-4ca3-bf1d-77ceb2ef0671', N'D1eu6DAooAVKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10026, CAST(N'2020-03-30T16:38:45.2477352' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'a343e2bf-6bb6-4ca3-bf1d-77ceb2ef0671', N'uARjXXbkMehKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10027, CAST(N'2020-03-30T17:13:42.3129001' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'ce012b50-443f-4104-8697-578de2f6f8f0', N'STUf7jTS9pxKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10028, CAST(N'2020-03-30T17:15:07.2377152' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'ce012b50-443f-4104-8697-578de2f6f8f0', N'N0b/7fy2jdFKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10029, CAST(N'2020-03-30T17:29:21.6880140' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'NGrWU1naDdpKirniCuOX1g==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10030, CAST(N'2020-03-30T17:30:00.5238273' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'Soq54grjl9Y=')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10031, CAST(N'2020-03-30T17:46:25.7360867' AS DateTime2), NULL, NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'NGrWU1naDdrcTW3EBp8hSA==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10032, CAST(N'2020-03-30T18:16:57.4670296' AS DateTime2), NULL, NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'NGrWU1naDdqOj9+6Yu7dKg==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10033, CAST(N'2020-03-30T18:38:28.0081388' AS DateTime2), N'72324729-dba0-4168-ad65-df7118680b28', NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'NGrWU1naDdrRtScBNtib4Q==')
INSERT [dbo].[ApplicationUserPasswordHistorys] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [UserId], [HashPassword]) VALUES (10034, CAST(N'2020-03-31T14:30:33.9017835' AS DateTime2), N'37f08491-865c-4536-a12f-a75ba608eee4', NULL, NULL, 0, 0, N'72324729-dba0-4168-ad65-df7118680b28', N'JOW/ybutsD2mqOaVZ4LYwdYI9beSdhLn')
SET IDENTITY_INSERT [dbo].[ApplicationUserPasswordHistorys] OFF
SET IDENTITY_INSERT [dbo].[EmailTemplates] ON 

INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (1, CAST(N'2020-03-22T20:38:58.0592349' AS DateTime2), N'1', N'1', CAST(N'2020-03-22T22:59:52.3082020' AS DateTime2), 1, 0, N'Account Opening', N'<p>This conntent is for account opening</p>', N'this for account opening', N'ACCOUNT')
INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (2, CAST(N'2020-03-29T11:23:45.3314111' AS DateTime2), N'1', N'1', CAST(N'2020-03-29T11:27:16.2181350' AS DateTime2), 1, 0, N'Account Verification', N'<meta name="viewport" content="width=device-width">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
   
    <style>
    /* -------------------------------------
        INLINED WITH htmlemail.io/inline
    ------------------------------------- */
    /* -------------------------------------
        RESPONSIVE AND MOBILE FRIENDLY STYLES
    ------------------------------------- */
    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    /* -------------------------------------
        PRESERVE THESE STYLES IN THE HEAD
    ------------------------------------- */
    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      #MessageViewBody a {
        color: inherit;
        text-decoration: none;
        font-size: inherit;
        font-family: inherit;
        font-weight: inherit;
        line-height: inherit;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
    </style>
  
  
    <table border="0" cellpadding="0" cellspacing="0" class="body" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;">
      <tbody><tr>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
        <td class="container" style="font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;">
          <div class="content" style="box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;">

            <!-- START CENTERED WHITE CONTAINER -->
            <span class="preheader" style="color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;"></span>
            <table class="main" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;">

              <!-- START MAIN CONTENT AREA -->
              <tbody><tr>
                <td class="wrapper" style="font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                    <tbody>
                      <tr>
                        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;"><span style="font-family: sans-serif; font-size: 14px; vertical-align: top;text-align:right"><img src="#LogoUrl" width="178" height="43" alt="#PortalName" style="border:none"></span></td>
                      </tr>
                      <tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Hi #Name,</p>
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Kindly click on the link below to activate your account on #PortalName</p>
                        <table border="0" cellpadding="0" cellspacing="0" class="btn btn-primary" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;">
                          <tbody>
                            <tr>
                              <td align="CENTER" style="font-family: sans-serif; font-size: 14px; vertical-align: top; padding-bottom: 15px;">
                                <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;">
                                  <tbody>
                                    <tr>
                                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #3498db; border-radius: 5px; text-align: center;"> <a href="#Link" target="_blank" style="display: inline-block; color: #ffffff; background-color: #3498db; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;">Activate your Account</a>									   </td>
                                    </tr>
                                  </tbody>
                                </table>
								<p>This activation link expires <b> #Duration</b> from now after which you will not be able to use it again.</p>                              </td>
                            </tr>
                          </tbody>
                        </table>                      </td>
                    </tr>
                  </tbody></table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </tbody></table>

            <!-- START FOOTER -->
            <div class="footer" style="clear: both; Margin-top: 10px; text-align: center; width: 100%;">
              <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                <tbody><tr>
                  <td class="content-block" style="font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;">
                    <span class="apple-link" style="color: #999999; font-size: 12px; text-align:left;">
					This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. #PortalName disclaims any liability for any action taken in reliance on the content of this e-mail. <br>
					<br>
					The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of #PortalName. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.					</span></td>
                </tr>

               
              </tbody></table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
      </tr>
    </tbody></table>', N'Account Verification', N'ACCOUNT_VERIFICATION')
INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (3, CAST(N'2020-03-29T11:31:34.0476213' AS DateTime2), N'1', N'1', CAST(N'2020-03-29T11:27:16.2181350' AS DateTime2), 1, 0, N'Your Account Has Been Created', N'

  
    <meta name="viewport" content="width=device-width">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
   
    <style>
    /* -------------------------------------
        INLINED WITH htmlemail.io/inline
    ------------------------------------- */
    /* -------------------------------------
        RESPONSIVE AND MOBILE FRIENDLY STYLES
    ------------------------------------- */
    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    /* -------------------------------------
        PRESERVE THESE STYLES IN THE HEAD
    ------------------------------------- */
    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      #MessageViewBody a {
        color: inherit;
        text-decoration: none;
        font-size: inherit;
        font-family: inherit;
        font-weight: inherit;
        line-height: inherit;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
    </style>
  
  
    <table border="0" cellpadding="0" cellspacing="0" class="body" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;">
      <tbody><tr>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
        <td class="container" style="font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;">
          <div class="content" style="box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;">

            <!-- START CENTERED WHITE CONTAINER -->
            <span class="preheader" style="color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;"></span>
            <table class="main" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;">

              <!-- START MAIN CONTENT AREA -->
              <tbody><tr>
                <td class="wrapper" style="font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                    <tbody>
                      <tr>
                        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;"><span style="font-family: sans-serif; font-size: 14px; vertical-align: top;text-align:right"><img src="#LogoUrl" width="178" height="43" alt="#PortalName" style="border:none"></span></td>
                      </tr>
                      <tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Hi #Name,</p>
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Your account has been created successfully on #PortalName.  Find below your login details</p>
                        <table border="0" cellpadding="0" cellspacing="0" class="btn btn-primary" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;">
                          <tbody>
                            <tr>
                              <td align="left" style="font-family: sans-serif; font-size: 14px; vertical-align: top;"><p><b> <br>
                                Username :</b> #Username<br>
                                <br>
                                  <b>Password :</b> #Password </p>
                                <p>You will be required to change your password at first login.</p></td>
                            </tr>
                          </tbody>
                        </table>                      </td>
                    </tr>
                  </tbody></table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </tbody></table>

            <!-- START FOOTER -->
            <div class="footer" style="clear: both; Margin-top: 10px; text-align: center; width: 100%;">
              <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                <tbody><tr>
                  <td class="content-block" style="font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;">
                    <span class="apple-link" style="color: #999999; font-size: 12px; text-align:left;">
					This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. #PortalName disclaims any liability for any action taken in reliance on the content of this e-mail. <br>
					<br>
					The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of #PortalName. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.					</span></td>
                </tr>

               
              </tbody></table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
      </tr>
    </tbody></table>
  
', N'Account Creation', N'ACCOUNT_CREATION')
INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (4, CAST(N'2020-03-29T11:37:34.8062842' AS DateTime2), N'1', N'1', CAST(N'2020-03-29T11:27:16.2181350' AS DateTime2), 1, 0, N'Password Changed', N'

  
    <meta name="viewport" content="width=device-width">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
   
    <style>
    /* -------------------------------------
        INLINED WITH htmlemail.io/inline
    ------------------------------------- */
    /* -------------------------------------
        RESPONSIVE AND MOBILE FRIENDLY STYLES
    ------------------------------------- */
    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    /* -------------------------------------
        PRESERVE THESE STYLES IN THE HEAD
    ------------------------------------- */
    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      #MessageViewBody a {
        color: inherit;
        text-decoration: none;
        font-size: inherit;
        font-family: inherit;
        font-weight: inherit;
        line-height: inherit;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
    </style>
  
  
    <table border="0" cellpadding="0" cellspacing="0" class="body" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;">
      <tbody><tr>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
        <td class="container" style="font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;">
          <div class="content" style="box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;">

            <!-- START CENTERED WHITE CONTAINER -->
            <span class="preheader" style="color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;">.</span>
            <table class="main" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;">

              <!-- START MAIN CONTENT AREA -->
              <tbody><tr>
                <td class="wrapper" style="font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                    <tbody><tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;text-align:right">&nbsp;<img src="#LogoUrl" width="178" height="43" alt="#PortalName" style="border:none"></td>
                    </tr>
                    <tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Hi #Name,</p>
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Your password was updated successful on #PortalName.<br>
                          <br> 
                          Find below your login details</p>
                        <table border="0" cellpadding="0" cellspacing="0" class="btn btn-primary" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;">
                          <tbody>
                            <tr>
                              <td align="left" style="font-family: sans-serif; font-size: 14px; vertical-align: top;"><p><b><br>
                                Username :</b> #Username<br>
                                  <b><br>
                                  Password :</b> #Password </p>
                                <p align="justify">Please keep your password safe and do not share your log in details with anyone. 
You may change your password at your convenience. 
                                  <br>
                                  <br>
                                  In the event that you cannot remember your password, kindly follow the instructions provided for password recovery. </p></td>
                            </tr>
                          </tbody>
                        </table>                      </td>
                    </tr>
                  </tbody></table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </tbody></table>

            <!-- START FOOTER -->
            <div class="footer" style="clear: both; Margin-top: 10px; text-align: center; width: 100%;">
              <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                <tbody><tr>
                  <td class="content-block" style="font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;">
                    <span class="apple-link" style="color: #999999; font-size: 12px; text-align:left;">
					This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. #PortalName disclaims any liability for any action taken in reliance on the content of this e-mail. <br>
					<br>
					The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of iponigeria.com. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.					</span></td>
                </tr>

               
              </tbody></table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
      </tr>
    </tbody></table>
  
', N'Password Changed', N'PASSWORD_UPDATE')
INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (5, CAST(N'2020-03-29T11:42:39.5852792' AS DateTime2), N'1', N'1', CAST(N'2020-03-29T11:27:16.2181350' AS DateTime2), 1, 0, N'Forgot Password', N'

  
    <meta name="viewport" content="width=device-width">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
   
    <style>
    /* -------------------------------------
        INLINED WITH htmlemail.io/inline
    ------------------------------------- */
    /* -------------------------------------
        RESPONSIVE AND MOBILE FRIENDLY STYLES
    ------------------------------------- */
    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    /* -------------------------------------
        PRESERVE THESE STYLES IN THE HEAD
    ------------------------------------- */
    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      #MessageViewBody a {
        color: inherit;
        text-decoration: none;
        font-size: inherit;
        font-family: inherit;
        font-weight: inherit;
        line-height: inherit;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
    </style>
  
  
    <table border="0" cellpadding="0" cellspacing="0" class="body" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;">
      <tbody><tr>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
        <td class="container" style="font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;">
          <div class="content" style="box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;">

            <!-- START CENTERED WHITE CONTAINER -->
            <span class="preheader" style="color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;">.</span>
            <table class="main" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;">

              <!-- START MAIN CONTENT AREA -->
              <tbody><tr>
                <td class="wrapper" style="font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                    <tbody><tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;text-align:right">&nbsp;<img src="#LogoUrl" width="178" height="43" alt="#PortalName" style="border:none"></td>
                    </tr>
                    <tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Hi #Name,</p>
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">We received a request to reset your password on #PortalName <br>
                          <br>
                          Use the link below to set up a new password for your account. If you did not request to reset your password, ignore this email and the link will expire on its own</p>
                        <div style="text-align:center"><table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: auto;">
                                  <tbody>
                                    <tr>
                                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top; background-color: #3498db; border-radius: 5px; text-align: center;"> <a href="#Link" target="_blank" style="display: inline-block; color: #ffffff; background-color: #3498db; border: solid 1px #3498db; border-radius: 5px; box-sizing: border-box; cursor: pointer; text-decoration: none; font-size: 14px; font-weight: bold; margin: 0; padding: 12px 25px; text-transform: capitalize; border-color: #3498db;">Activate your Account</a>
								      </td>
                                    </tr>
									
                                  </tbody>
                                </table>    </div>                 </td>
                    </tr>
                  </tbody></table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </tbody></table>

            <!-- START FOOTER -->
            <div class="footer" style="clear: both; Margin-top: 10px; text-align: center; width: 100%;">
              <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                <tbody><tr>
                  <td class="content-block" style="font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;">
                    <span class="apple-link" style="color: #999999; font-size: 12px; text-align:left;">
					This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. Iponigeria.com disclaims any liability for any action taken in reliance on the content of this e-mail. <br>
					<br>
					The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of #PortalName. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.					</span></td>
                </tr>

               
              </tbody></table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
      </tr>
    </tbody></table>
  
', N'Forgot Password', N'FORGOT_PASSWORD')
INSERT [dbo].[EmailTemplates] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [Subject], [MailBody], [Description], [Code]) VALUES (6, CAST(N'2020-03-29T11:37:34.8062842' AS DateTime2), N'1', N'1', CAST(N'2020-03-29T11:27:16.2181350' AS DateTime2), 1, 0, N'Password Changed', N'

  
    <meta name="viewport" content="width=device-width">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
   
    <style>
    /* -------------------------------------
        INLINED WITH htmlemail.io/inline
    ------------------------------------- */
    /* -------------------------------------
        RESPONSIVE AND MOBILE FRIENDLY STYLES
    ------------------------------------- */
    @media only screen and (max-width: 620px) {
      table[class=body] h1 {
        font-size: 28px !important;
        margin-bottom: 10px !important;
      }
      table[class=body] p,
            table[class=body] ul,
            table[class=body] ol,
            table[class=body] td,
            table[class=body] span,
            table[class=body] a {
        font-size: 16px !important;
      }
      table[class=body] .wrapper,
            table[class=body] .article {
        padding: 10px !important;
      }
      table[class=body] .content {
        padding: 0 !important;
      }
      table[class=body] .container {
        padding: 0 !important;
        width: 100% !important;
      }
      table[class=body] .main {
        border-left-width: 0 !important;
        border-radius: 0 !important;
        border-right-width: 0 !important;
      }
      table[class=body] .btn table {
        width: 100% !important;
      }
      table[class=body] .btn a {
        width: 100% !important;
      }
      table[class=body] .img-responsive {
        height: auto !important;
        max-width: 100% !important;
        width: auto !important;
      }
    }

    /* -------------------------------------
        PRESERVE THESE STYLES IN THE HEAD
    ------------------------------------- */
    @media all {
      .ExternalClass {
        width: 100%;
      }
      .ExternalClass,
            .ExternalClass p,
            .ExternalClass span,
            .ExternalClass font,
            .ExternalClass td,
            .ExternalClass div {
        line-height: 100%;
      }
      .apple-link a {
        color: inherit !important;
        font-family: inherit !important;
        font-size: inherit !important;
        font-weight: inherit !important;
        line-height: inherit !important;
        text-decoration: none !important;
      }
      #MessageViewBody a {
        color: inherit;
        text-decoration: none;
        font-size: inherit;
        font-family: inherit;
        font-weight: inherit;
        line-height: inherit;
      }
      .btn-primary table td:hover {
        background-color: #34495e !important;
      }
      .btn-primary a:hover {
        background-color: #34495e !important;
        border-color: #34495e !important;
      }
    }
    </style>
  
  
    <table border="0" cellpadding="0" cellspacing="0" class="body" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background-color: #f6f6f6;">
      <tbody><tr>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
        <td class="container" style="font-family: sans-serif; font-size: 14px; vertical-align: top; display: block; Margin: 0 auto; max-width: 580px; padding: 10px; width: 580px;">
          <div class="content" style="box-sizing: border-box; display: block; Margin: 0 auto; max-width: 580px; padding: 10px;">

            <!-- START CENTERED WHITE CONTAINER -->
            <span class="preheader" style="color: transparent; display: none; height: 0; max-height: 0; max-width: 0; opacity: 0; overflow: hidden; mso-hide: all; visibility: hidden; width: 0;">.</span>
            <table class="main" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; background: #ffffff; border-radius: 3px;">

              <!-- START MAIN CONTENT AREA -->
              <tbody><tr>
                <td class="wrapper" style="font-family: sans-serif; font-size: 14px; vertical-align: top; box-sizing: border-box; padding: 20px;">
                  <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                    <tbody><tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;text-align:right">&nbsp;<img src="#LogoUrl" width="178" height="43" alt="#PortalName" style="border:none"></td>
                    </tr>
                    <tr>
                      <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Hi #Name,</p>
                        <p style="font-family: sans-serif; font-size: 14px; font-weight: normal; margin: 0; Margin-bottom: 15px;">Your password was updated successful on #PortalName.<br>
                          <br> 
                          Find below your login details</p>
                        <table border="0" cellpadding="0" cellspacing="0" class="btn btn-primary" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%; box-sizing: border-box;">
                          <tbody>
                            <tr>
                              <td align="left" style="font-family: sans-serif; font-size: 14px; vertical-align: top;"><p><b><br>
                                Username :</b> #Username<br>
                                  <b><br>
                                  Password :</b> #Password </p>
                                <p align="justify">Please keep your password safe and do not share your log in details with anyone</p></td>
                            </tr>
                          </tbody>
                        </table>                      </td>
                    </tr>
                  </tbody></table>
                </td>
              </tr>

            <!-- END MAIN CONTENT AREA -->
            </tbody></table>

            <!-- START FOOTER -->
            <div class="footer" style="clear: both; Margin-top: 10px; text-align: center; width: 100%;">
              <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: separate; mso-table-lspace: 0pt; mso-table-rspace: 0pt; width: 100%;">
                <tbody><tr>
                  <td class="content-block" style="font-family: sans-serif; vertical-align: top; padding-bottom: 10px; padding-top: 10px; font-size: 12px; color: #999999; text-align: center;">
                    <span class="apple-link" style="color: #999999; font-size: 12px; text-align:left;">
					This e-mail and any attachments are confidential; it must not be read, copied, disclosed or used by any person other than the above named addressees. Unauthorised use, disclosure or copying is strictly prohibited and may be unlawful. #PortalName disclaims any liability for any action taken in reliance on the content of this e-mail. <br>
					<br>
					The comments or statements expressed in this e-mail could be personal opinions and are not necessarily those of #PortalName. If you have received this email in error or think you may have done so, you may not peruse, use, disseminate, distribute or copy this message. Please notify the sender immediately and delete the original e-mail from your system.					</span></td>
                </tr>

               
              </tbody></table>
            </div>
            <!-- END FOOTER -->

          <!-- END CENTERED WHITE CONTAINER -->
          </div>
        </td>
        <td style="font-family: sans-serif; font-size: 14px; vertical-align: top;">&nbsp;</td>
      </tr>
    </tbody></table>
  
', N'Password Changed', N'USER_CHANGE_PASSWORD')
SET IDENTITY_INSERT [dbo].[EmailTemplates] OFF
SET IDENTITY_INSERT [dbo].[Icon] ON 

INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (206, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'File Manager', N'File Manager')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (207, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Content', N'Content')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (210, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Third Level', N'Third Level')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (211, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Widgets', N'Widgets')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (212, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'priority-high', N'priority-high')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (213, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Extra Components', N'Extra Components')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (215, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Account Settings', N'Account Settings')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (216, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Starter kit', N'Starter kit')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (217, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'help', N'help')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (219, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Menu Levels', N'Menu Levels')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (220, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Block-UI', N'Block-UI')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (221, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Treeview', N'Treeview')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (222, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Tour', N'Tour')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (223, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'user', N'user')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (224, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'paper-plane', N'paper-plane')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (229, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Invoice', N'Invoice')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (231, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Content', N'Content')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (232, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Icons', N'Icons')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (234, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Card', N'Card')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (236, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Components', N'Components')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (238, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Form Elements', N'Form Elements')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (239, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Apex', N'Apex')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (241, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Authentication', N'Authentication')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (242, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Miscellaneous', N'Miscellaneous')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (243, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Charts', N'Charts')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (244, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Disabled Menu', N'Disabled Menu')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (248, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'Drag &amp; Drop', N'Drag &amp; Drop')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (249, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'settings', N'settings')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (250, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'warning-alt', N'warning-alt')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (251, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'expand', N'expand')
INSERT [dbo].[Icon] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [IconName], [IconCode]) VALUES (252, CAST(N'2020-03-14T09:45:54.3632518' AS DateTime2), NULL, N'', CAST(N'2020-03-14T14:52:12.0315616' AS DateTime2), 1, 0, N'music', N'music')
SET IDENTITY_INSERT [dbo].[Icon] OFF


SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (1, N'Dashboard', N'dash001', 0, 1, N'', N'37f08491-865c-4536-a12f-a75ba608eee4', CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), N'236', N'Dashboard/Index', 0,'::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (2, N'Parameter Setup', N'PARAM001', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'249', N'ParameterSetup/Index', 0, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (3, N'Role Setup', N'role001', 0, 1, N'', N'37f08491-865c-4536-a12f-a75ba608eee4', CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), CAST(N'2020-04-02T00:13:44.9811977' AS DateTime2), N'251', N'RoleSetup/Index', 0, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (4, N'Role Management', N'ROLEMgt01', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'251', N'RoleManagement/Index', 3, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (5, N'Permission Management', N'permi01', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'251', N'PermissionManagement/Index', 3, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (6, N'Icon Management', N'icon001', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'251', N'IconManagement/Index', 2, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (7, N'Email Templates', N'Email001', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'251', N'EmailSetup/Index', 2, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (8, N'AuditLog', N'AuditLog', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'251', N'AuditLog/Index', 2, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (9, N'Users Setup', N'UserMgt', 0, 1, N'', N'37f08491-865c-4536-a12f-a75ba608eee4', CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), CAST(N'2020-04-02T00:54:47.1681756' AS DateTime2), N'251', N'UserSetup/Index', 0, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (10, N'User Management', N'Usermgt001', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'241', N'UserManagement/Index', 9, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (11, N'Country Management', N'Country001', 0, 1, N'', NULL, CAST(N'2020-04-02T00:48:13.3297037' AS DateTime2), NULL, N'241', N'CountryManagement/Index', 2, '::1')
INSERT [dbo].[Permissions] ([ID], [PermissionName], [PermissionCode], [IsDeleted], [IsActive], [CreatedBy], [ModifiedBy], [CreatedDate], [LastModified], [Icon], [Url], [ParentId],[IPAddress]) VALUES (10011, N'SMS Providers', N'provider01', 0, 1, N'', NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, N'216', N'SMSProviderManagement/Index', 2, '::1')
SET IDENTITY_INSERT [dbo].[Permissions] OFF

SET IDENTITY_INSERT [dbo].[RolePermissions] ON 

INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (207, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 1, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (208, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 2, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (209, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 6, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (210, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 7, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (211, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 8, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (212, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 3, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (213, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 4, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (214, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 5, N'7b87345676944fe89a2bb55174ecc79d')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10215, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 1, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10216, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 2, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10217, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 6, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10218, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 7, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10219, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 8, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10220, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 11, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10221, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 10011, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10222, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 3, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10223, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 4, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10224, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 5, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10225, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 9, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10226, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 10, N'3951b0f5-cf16-4007-be43-b260171f8797')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10227, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 1, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10228, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 2, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10229, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 6, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10230, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 7, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10231, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 8, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10232, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 11, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10233, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 10011, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10234, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 3, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10235, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 4, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10236, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 5, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10237, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 9, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10238, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 10, N'f893c609-4974-4078-a5a7-2efdbafebd54')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10239, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 2, N'2c0935dc-e157-44cc-900b-57c51a9ac505')
INSERT [dbo].[RolePermissions] ([ID], [CreatedDate], [CreatedBy], [ModifiedBy], [LastModified], [IsActive], [IsDeleted], [PermissionId], [RoleId]) VALUES (10240, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 0, 0, 10011, N'2c0935dc-e157-44cc-900b-57c51a9ac505')
SET IDENTITY_INSERT [dbo].[RolePermissions] OFF

