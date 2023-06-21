
Para migrar la base se debe ingresar al PackageManagerConsole. En la ventana seleccionamos src\Infraestructure\Persistence y 
escribimos el siguiente comando: add-migration MarvelDb -o Migrations (se debe configurar el appsettings para que apunte al local 
"DefaultConnection": "Data Source=ServerName";) y cuando termine de generarse se da la siguiente instruccion "update-database".

Con esto ya se genera las tablas para poder trabajar con las api.

SELECT TOP (1000) [Id]
      ,[IdHero]
      ,[IdTeam]
      ,[Created]
  FROM [MarvelDB].[dbo].[TeamsHero]

SELECT TOP (1000) [Id]
      ,[Name]
      ,[Created]
  FROM [MarvelDB].[dbo].[Teams]

SELECT TOP (1000) [Id]
      ,[Name]
      ,[Description]
      ,[UrlImage]
      ,[ModifiedDate]
      ,[Intelligence]
      ,[Agility]
      ,[Force]
      ,[Captured]
      ,[MarvelID]
      ,[Created]
  FROM [MarvelDB].[dbo].[Character]
