BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "ChatData" (
	"ID"	INTEGER NOT NULL UNIQUE,
	"Request"	TEXT NOT NULL,
	"Response"	TEXT NOT NULL,
	PRIMARY KEY("ID" AUTOINCREMENT)
);
INSERT INTO "ChatData" VALUES (1,'hello','hi');
COMMIT;
