-- Intranet Database
DROP DATABASE `Intranet`;
CREATE DATABASE IF NOT EXISTS `Intranet`;

USE `Intranet`;

CREATE TABLE IF NOT EXISTS `MenuItems` (
    `Id`        INT          NOT NULL AUTO_INCREMENT PRIMARY KEY,
    `ParentId`  INT          NULL,
    `Title`     LONGTEXT     NOT NULL,
    `Url`       LONGTEXT     NULL,
    `Icon`      LONGTEXT     NULL,
    `SortOrder` INT          NOT NULL DEFAULT 0,
    `IsActive`  TINYINT(1)   NOT NULL DEFAULT 1,
    CONSTRAINT `FK_MenuItems_MenuItems_ParentId` FOREIGN KEY (`ParentId`)
        REFERENCES `MenuItems` (`Id`)
        ON DELETE RESTRICT,
    INDEX `IX_MenuItems_ParentId` (`ParentId`),
    INDEX `IX_MenuItems_SortOrder` (`SortOrder`)
);


-- Seed menu
INSERT IGNORE INTO `MenuItems` (`Id`, `ParentId`, `Title`, `Url`, `Icon`, `SortOrder`, `IsActive`)
VALUES (1, NULL, 'Dashboard', '/', 'bi-house-door', 1, 1);

INSERT IGNORE INTO `MenuItems` (`Id`, `ParentId`, `Title`, `Url`, `Icon`, `SortOrder`, `IsActive`)
VALUES (2, NULL, 'Documents', NULL, 'bi-folder', 2, 1);

INSERT IGNORE INTO `MenuItems` (`Id`, `ParentId`, `Title`, `Url`, `Icon`, `SortOrder`, `IsActive`)
VALUES (3, 2, 'Policies', '/documents/policies', 'bi-file-earmark-text', 1, 1);

INSERT IGNORE INTO `MenuItems` (`Id`, `ParentId`, `Title`, `Url`, `Icon`, `SortOrder`, `IsActive`)
VALUES (4, 2, 'Templates', '/documents/templates', 'bi-file-earmark', 2, 1);


-- Print menu
SELECT * FROM `MenuItems`;
