CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240323220032_Initial') THEN
    CREATE TABLE "Nations" (
        "Id" uuid NOT NULL,
        "Name_Value" text,
        "Position_Value" integer NOT NULL,
        "Status_IsActive" boolean NOT NULL,
        CONSTRAINT "PK_Nations" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240323220032_Initial') THEN
    CREATE UNIQUE INDEX "IX_Nations_Name_Value" ON "Nations" ("Name_Value");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240323220032_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240323220032_Initial', '8.0.3');
    END IF;
END $EF$;
COMMIT;

