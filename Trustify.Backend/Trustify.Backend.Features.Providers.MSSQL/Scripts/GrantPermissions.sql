IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Trustify')
CREATE USER [Trustify] FOR LOGIN [Trustify] WITH DEFAULT_SCHEMA=[dbo];
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'Rol_Trustify' AND type = 'R')
CREATE ROLE [Rol_Trustify];
ALTER AUTHORIZATION ON ROLE::[Rol_Trustify] TO [dbo];
ALTER ROLE [Rol_Trustify] ADD MEMBER [Trustify];
GRANT CONNECT TO [Trustify] AS [dbo];
GRANT VIEW ANY COLUMN ENCRYPTION KEY DEFINITION TO [public] AS [dbo];
GRANT VIEW ANY COLUMN MASTER KEY DEFINITION TO [public] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[Roles] TO  SCHEMA OWNER;
GRANT CONTROL ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[Roles] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[Roles] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[ImageContents] TO  SCHEMA OWNER; 
GRANT CONTROL ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[ImageContents] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[ImageContents] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[ImageContentSection] TO  SCHEMA OWNER ;
GRANT CONTROL ON [dbo].[ImageContentSection] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[ImageContentSection] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[ImageContentSection] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[ImageContentSection] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[ImageContentSection] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[RoleSection] TO  SCHEMA OWNER; 
GRANT CONTROL ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[RoleSection] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[RoleSection] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[SectionTextualContent] TO  SCHEMA OWNER;
GRANT CONTROL ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[SectionTextualContent] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[TextualContents] TO  SCHEMA OWNER;
GRANT CONTROL ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[TextualContents] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[TextualContents] TO [Trustify] AS [dbo];

ALTER AUTHORIZATION ON [dbo].[Sections] TO  SCHEMA OWNER; 
GRANT CONTROL ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT DELETE ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT INSERT ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT REFERENCES ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT SELECT ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT UPDATE ON [dbo].[Sections] TO [Trustify] AS [dbo];
GRANT VIEW CHANGE TRACKING ON [dbo].[Sections] TO [Trustify] AS [dbo];

ALTER DATABASE [Trustify] SET  READ_WRITE ;