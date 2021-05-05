
-- ************************************
-- TODO ITEMS TABLE FOR DEMO

CREATE DATABASE todo_db;
GO

USE todo_db;
GO

CREATE TABLE todo_list (
  id int IDENTITY(1,1) primary key,
  title varchar(150) NOT NULL
);
GO

CREATE TABLE todo_item (
  id int IDENTITY(1,1) primary key,
  title varchar(150) not null,
  checked bit DEFAULT 0,
  list_id integer not null,
  foreign key(list_id) references todo_list(id)
);
GO

