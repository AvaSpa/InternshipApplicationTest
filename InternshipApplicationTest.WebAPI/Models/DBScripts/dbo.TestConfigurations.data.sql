SET IDENTITY_INSERT [dbo].[TestConfigurations] ON
INSERT INTO [dbo].[TestConfigurations] ([Id], [QuestionNumber], [MinimumScore], [TimeLimit]) VALUES (1, 10, 8, N'00:10:00')
SET IDENTITY_INSERT [dbo].[TestConfigurations] OFF
