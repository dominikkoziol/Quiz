BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "Categories" (
	"Id"	TEXT NOT NULL,
	"Name"	TEXT,
	CONSTRAINT "PK_Categories" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Questions" (
	"Id"	TEXT NOT NULL,
	"QuestionContent"	TEXT,
	"AnswerA"	TEXT,
	"AnswerB"	TEXT,
	"AnswerC"	TEXT,
	"AnswerD"	TEXT,
	"CorrectAnswer"	INTEGER NOT NULL,
	"QuestionLevel"	INTEGER NOT NULL,
	"CategoryId"	TEXT,
	CONSTRAINT "FK_Questions_Categories_CategoryId" FOREIGN KEY("CategoryId") REFERENCES "Categories"("Id") ON DELETE RESTRICT,
	CONSTRAINT "PK_Questions" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "Results" (
	"Id"	TEXT NOT NULL,
	"UserName"	TEXT,
	"Points"	INTEGER NOT NULL,
	"CategoryId"	TEXT NOT NULL,
	CONSTRAINT "FK_Results_Categories_CategoryId" FOREIGN KEY("CategoryId") REFERENCES "Categories"("Id") ON DELETE CASCADE,
	CONSTRAINT "PK_Results" PRIMARY KEY("Id")
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20200705093459_Initial','3.1.5');
INSERT INTO "Categories" VALUES ('1','Co wiesz o JS?');
INSERT INTO "Questions" VALUES ('1','Jak w JS deklarujemy zmienne?','var','http','https','www',1,0,'1');
INSERT INTO "Questions" VALUES ('2','W jakim znaczniku umieszczamy kod JS?','scripting','js','script','jdoc',3,0,'1');
INSERT INTO "Questions" VALUES ('3','Co jest prawidłową składnią aby edytować niżej podany element? \n <p id=''demo''>This is a demonstration.</p>',' #demo.innerHTML = ''Hello World!'';',' document.getElement(''p'').innerHTML = ''Hello World!'';',' document.getElementById(''demo'').innerHTML = ''Hello World!'';',' document.getElementByName(''p'').innerHTML = ''Hello World!'';',3,1,'1');
INSERT INTO "Questions" VALUES ('4','Gdzie umieszcza się kod JavaScript w html?','W sekcji <head>','W sekcji <body>','W sekcji <?php','W sekcji head i body',4,0,'1');
INSERT INTO "Questions" VALUES ('5','Jak prawidłowo zalinkować zewnętrzny plik z kodem JS?','<script src=></script>','<script href=></script>','<script route=></script>','<script path=></script>',1,0,'1');
INSERT INTO "Questions" VALUES ('6','Czy zewnętrzny plik JS musi zawierać znacznik script?','Prawda','Fałsz',NULL,NULL,2,0,'1');
INSERT INTO "Questions" VALUES ('7','Ja wyświetlisz w domyślnym oknie dialogowym przeglądarki napisa ''Witaj Świecie''?','alert(''Witaj Świecie'')','messageBox(''Witaj Świecie'')','openDialog(''Witaj Świecie'')','console.log(''Witaj Świecie'')',1,1,'1');
CREATE INDEX IF NOT EXISTS "IX_Questions_CategoryId" ON "Questions" (
	"CategoryId"
);
CREATE INDEX IF NOT EXISTS "IX_Results_CategoryId" ON "Results" (
	"CategoryId"
);
COMMIT;
