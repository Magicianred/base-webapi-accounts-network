
-- TO DO - SE E' PRESENTE LO SCRIPT - STOP EXECUTION
INSERT INTO `DbScriptMigration` (`MigrationId`, `MigrationName`, `MigrationDate`)
SELECT * FROM (SELECT UUID(),'001_create_posts_table',NOW()) AS tmp
WHERE NOT EXISTS (
    SELECT `MigrationName` FROM `DbScriptMigration` WHERE `MigrationName` = '001_create_table_auth'
) LIMIT 1;

CREATE TABLE IF NOT EXISTS `Accounts` (
				`Id` INT NOT NULL AUTO_INCREMENT,
				`Email` varchar(255) NOT NULL,
				`Password` varchar(255) NOT NULL,
				`CreateDate` datetime NOT NULL,
			PRIMARY KEY (`Id`)
			) ENGINE=InnoDB;

CREATE TABLE IF NOT EXISTS `Roles` (
				`Id` INT NOT NULL AUTO_INCREMENT,
				`Name` varchar(100) NOT NULL,
				`Description` varchar(250) NOT NULL,
				`CreateDate` datetime NOT NULL,
			PRIMARY KEY (`Id`)
			) ENGINE=InnoDB;
            
CREATE TABLE IF NOT EXISTS `AccountRoles` (
				`AccountId` INT NOT NULL,
				`RoleId` INT NOT NULL,
                PRIMARY KEY (AccountId, RoleId),
                UNIQUE INDEX (RoleId, AccountId),
                foreign key (AccountId) references Accounts(Id),
                foreign key (RoleId) references Roles(Id)
			) ENGINE=InnoDB;



