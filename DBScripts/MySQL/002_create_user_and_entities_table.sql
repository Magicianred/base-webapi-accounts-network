
-- TO DO - SE E' PRESENTE LO SCRIPT - STOP EXECUTION
INSERT INTO `DbScriptMigration` (`MigrationId`, `MigrationName`, `MigrationDate`)
SELECT * FROM (SELECT UUID(),'002_create_user_and_entities_table',NOW()) AS tmp
WHERE NOT EXISTS (
    SELECT `MigrationName` FROM `DbScriptMigration` WHERE `MigrationName` = '002_create_user_and_entities_table'
) LIMIT 1;

CREATE TABLE IF NOT EXISTS `Users` (
				`Id` INT NOT NULL AUTO_INCREMENT,
				`Username` varchar(50) NOT NULL,
				`Name` varchar(100) NULL,
				`Surname` varchar(100) NULL,
				`Properties` varchar(2000) NULL,
				`CreateDate` datetime NOT NULL,
				`LastAccess` datetime NOT NULL,
			PRIMARY KEY (`Id`)
			) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `EntityTypes` (
				`Id` INT NOT NULL AUTO_INCREMENT,
				`Name` varchar(100) NOT NULL,
				`Description` varchar(250) NOT NULL,
			PRIMARY KEY (`Id`)
			) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `Entities` (
				`Id` INT NOT NULL AUTO_INCREMENT,
				`TypeId` INT NOT NULL,
				`Title` varchar(100) NULL,
				`Description` varchar(250) NULL,
				`Properties` varchar(2000) NULL,
				`CreateDate` datetime NOT NULL,
			PRIMARY KEY (`Id`),
                foreign key (TypeId) references EntityTypes(Id)
			) ENGINE=InnoDB;
            
CREATE TABLE IF NOT EXISTS `UserAccounts` (
				`UserId` INT NOT NULL,
				`AccountId` INT NOT NULL,
                PRIMARY KEY (AccountId, UserId),
                UNIQUE INDEX (UserId, AccountId),
                foreign key (AccountId) references Accounts(Id),
                foreign key (UserId) references Users(Id)
			) ENGINE=InnoDB;
            
CREATE TABLE IF NOT EXISTS `UserEntities` (
				`UserId` INT NOT NULL,
				`EntityId` INT NOT NULL,
                PRIMARY KEY (EntityId, UserId),
                UNIQUE INDEX (UserId, EntityId),
                foreign key (EntityId) references Entities(Id),
                foreign key (UserId) references Users(Id)
			) ENGINE=InnoDB;



