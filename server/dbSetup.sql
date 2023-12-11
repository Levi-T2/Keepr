-- Account Commands

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

SELECT * FROM accounts;

-- Keep Commands

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        name VARCHAR(300) NOT NULL,
        description VARCHAR(3000) NOT NULL,
        img VARCHAR(2000) NOT NULL,
        views INT NOT NULL DEFAULT 0,
        kept INT NOT NULL DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8;

SELECT * FROM keeps;

DROP TABLE keeps;

DELETE FROM keeps LIMIT 10;

-- Vault Commands

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
        name VARCHAR(300) NOT NULL,
        description VARCHAR(3000) NOT NULL,
        img VARCHAR(2000) NOT NULL,
        isPrivate BOOLEAN NOT NULL DEFAULT false,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8;

SELECT * FROM vaults;

DROP TABLE vaults;

DELETE FROM vaults LIMIT 10;

-- VaultKeep Commands

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT PRIMARY KEY AUTO_INCREMENT NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8;

SELECT * FROM vaultKeeps;

DROP TABLE vaultKeeps;