REVOKE CONTROL ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[Roles] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[Roles] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[Roles] TO  [db_owner];

REVOKE CONTROL ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[RoleSection] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[RoleSection] TO   [db_owner]; 

REVOKE CONTROL ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[ImageContents] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[ImageContents] TO  [db_owner]; 

REVOKE CONTROL ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[Sections] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[Sections] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[Sections] TO  [db_owner];

REVOKE CONTROL ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[TextualContents] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[TextualContents] TO   [db_owner];

REVOKE CONTROL ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[ImageContentSections] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[ImageContentSections] TO   [db_owner]; 

REVOKE CONTROL ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE DELETE ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE INSERT ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE REFERENCES ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE SELECT ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE UPDATE ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
REVOKE VIEW CHANGE TRACKING ON [dbo].[SectionTextualContent] FROM [Trustify] AS [dbo];
ALTER AUTHORIZATION ON [dbo].[SectionTextualContent] TO   [db_owner];

REVOKE VIEW ANY COLUMN MASTER KEY DEFINITION FROM [public] AS [dbo];
REVOKE VIEW ANY COLUMN ENCRYPTION KEY DEFINITION FROM [public] AS [dbo];
REVOKE CONNECT FROM [Trustify] AS [dbo];
ALTER DATABASE [Trustify] SET  READ_WRITE;
ALTER AUTHORIZATION ON [dbo].[Users] TO   [db_owner]; 
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Trustify')
DROP USER [Trustify];
IF EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Rol_Trustify' AND type = 'R')
DROP ROLE [Rol_Trustify];
