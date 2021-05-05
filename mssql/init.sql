
-- ************************************
-- COMMANDER ITEMS TABLE FOR DEMO

CREATE DATABASE commander_db;
GO

USE commander_db;
GO

CREATE TABLE Commands (
  id int IDENTITY(1,1) primary key,
  howto varchar(255) NOT NULL,
  command varchar(255) NOT NULL,
  platform varchar(255) NOT NULL
);
GO

INSERT INTO Commands (howto,command,platform)
  VALUES ('how to value 1','command value 1', 'platform value 1');

GO