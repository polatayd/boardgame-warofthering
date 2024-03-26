CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    CREATE TABLE "Fellowships" (
        "Id" uuid NOT NULL,
        "GameId" uuid NOT NULL,
        "HuntingId" uuid NOT NULL,
        "CorruptionCounter_Value" integer NOT NULL,
        "ProgressCounter_IsHidden" boolean NOT NULL,
        "ProgressCounter_Value" integer NOT NULL,
        CONSTRAINT "PK_Fellowships" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    CREATE TABLE "Huntings" (
        "Id" uuid NOT NULL,
        "FellowshipId" uuid NOT NULL,
        "GameId" uuid NOT NULL,
        "HuntBox_NumberOfCharacterResultDice" integer NOT NULL,
        "HuntBox_NumberOfEyeResultDice" integer NOT NULL,
        "HuntPool" jsonb,
        CONSTRAINT "PK_Huntings" PRIMARY KEY ("Id"),
        CONSTRAINT "FK_Huntings_Fellowships_FellowshipId" FOREIGN KEY ("FellowshipId") REFERENCES "Fellowships" ("Id") ON DELETE CASCADE
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    CREATE UNIQUE INDEX "IX_Fellowships_GameId" ON "Fellowships" ("GameId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    CREATE UNIQUE INDEX "IX_Huntings_FellowshipId" ON "Huntings" ("FellowshipId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    CREATE UNIQUE INDEX "IX_Huntings_GameId" ON "Huntings" ("GameId");
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240326234655_Initial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240326234655_Initial', '8.0.3');
    END IF;
END $EF$;
COMMIT;

