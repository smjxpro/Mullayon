CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" TEXT NOT NULL CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY,
    "ProductVersion" TEXT NOT NULL
);

BEGIN TRANSACTION;

CREATE TABLE "AspNetRoles" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetRoles" PRIMARY KEY,
    "Discriminator" TEXT NOT NULL,
    "Name" TEXT NULL,
    "NormalizedName" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL
);

CREATE TABLE "AspNetUsers" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_AspNetUsers" PRIMARY KEY,
    "UserName" TEXT NULL,
    "NormalizedUserName" TEXT NULL,
    "Email" TEXT NULL,
    "NormalizedEmail" TEXT NULL,
    "EmailConfirmed" INTEGER NOT NULL,
    "PasswordHash" TEXT NULL,
    "SecurityStamp" TEXT NULL,
    "ConcurrencyStamp" TEXT NULL,
    "PhoneNumber" TEXT NULL,
    "PhoneNumberConfirmed" INTEGER NOT NULL,
    "TwoFactorEnabled" INTEGER NOT NULL,
    "LockoutEnd" TEXT NULL,
    "LockoutEnabled" INTEGER NOT NULL,
    "AccessFailedCount" INTEGER NOT NULL
);

CREATE TABLE "Tags" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Tags" PRIMARY KEY,
    "Name" TEXT NOT NULL
);

CREATE TABLE "AspNetRoleClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY AUTOINCREMENT,
    "RoleId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserClaims" (
    "Id" INTEGER NOT NULL CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY AUTOINCREMENT,
    "UserId" TEXT NOT NULL,
    "ClaimType" TEXT NULL,
    "ClaimValue" TEXT NULL,
    CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserLogins" (
    "LoginProvider" TEXT NOT NULL,
    "ProviderKey" TEXT NOT NULL,
    "ProviderDisplayName" TEXT NULL,
    "UserId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey"),
    CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserRoles" (
    "UserId" TEXT NOT NULL,
    "RoleId" TEXT NOT NULL,
    CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId"),
    CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "AspNetUserTokens" (
    "UserId" TEXT NOT NULL,
    "LoginProvider" TEXT NOT NULL,
    "Name" TEXT NOT NULL,
    "Value" TEXT NULL,
    CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name"),
    CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Categories" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Categories" PRIMARY KEY,
    "Name" TEXT NOT NULL,
    "ImageId" TEXT NOT NULL,
    "Description" TEXT NOT NULL,
    CONSTRAINT "FK_Categories_Images_ImageId" FOREIGN KEY ("ImageId") REFERENCES "Images" ("Id") ON DELETE CASCADE
);

CREATE TABLE "CategoryPost" (
    "CategoriesId" TEXT NOT NULL,
    "PostsId" TEXT NOT NULL,
    CONSTRAINT "PK_CategoryPost" PRIMARY KEY ("CategoriesId", "PostsId"),
    CONSTRAINT "FK_CategoryPost_Categories_CategoriesId" FOREIGN KEY ("CategoriesId") REFERENCES "Categories" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_CategoryPost_Posts_PostsId" FOREIGN KEY ("PostsId") REFERENCES "Posts" ("Id") ON DELETE CASCADE
);

CREATE TABLE "Images" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Images" PRIMARY KEY,
    "Url" TEXT NOT NULL,
    "PostId" TEXT NULL,
    CONSTRAINT "FK_Images_Posts_PostId" FOREIGN KEY ("PostId") REFERENCES "Posts" ("Id")
);

CREATE TABLE "Posts" (
    "Id" TEXT NOT NULL CONSTRAINT "PK_Posts" PRIMARY KEY,
    "Title" TEXT NOT NULL,
    "Content" TEXT NOT NULL,
    "CreatedAt" TEXT NOT NULL,
    "UpdatedAt" TEXT NOT NULL,
    "FeaturedImageId" TEXT NOT NULL,
    "AuthorId" TEXT NULL,
    "Stars" INTEGER NOT NULL,
    CONSTRAINT "FK_Posts_AspNetUsers_AuthorId" FOREIGN KEY ("AuthorId") REFERENCES "AspNetUsers" ("Id"),
    CONSTRAINT "FK_Posts_Images_FeaturedImageId" FOREIGN KEY ("FeaturedImageId") REFERENCES "Images" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PostTag" (
    "PostsId" TEXT NOT NULL,
    "TagsId" TEXT NOT NULL,
    CONSTRAINT "PK_PostTag" PRIMARY KEY ("PostsId", "TagsId"),
    CONSTRAINT "FK_PostTag_Posts_PostsId" FOREIGN KEY ("PostsId") REFERENCES "Posts" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PostTag_Tags_TagsId" FOREIGN KEY ("TagsId") REFERENCES "Tags" ("Id") ON DELETE CASCADE
);

INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName")
VALUES ('2988e309-7890-4db1-9746-e0426ff4f1cc', '0ca72a16-70b9-4525-93bf-67c0fdc08876', 'ApplicationRole', 'Admin', 'ADMIN');

INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName")
VALUES ('839c31b5-4adc-47e2-a069-f055742c613a', '80a278a2-16f9-44cb-a4ac-078bea29fed6', 'ApplicationRole', 'User', 'USER');

CREATE INDEX "IX_AspNetRoleClaims_RoleId" ON "AspNetRoleClaims" ("RoleId");

CREATE UNIQUE INDEX "RoleNameIndex" ON "AspNetRoles" ("NormalizedName");

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");

CREATE INDEX "EmailIndex" ON "AspNetUsers" ("NormalizedEmail");

CREATE UNIQUE INDEX "UserNameIndex" ON "AspNetUsers" ("NormalizedUserName");

CREATE INDEX "IX_Categories_ImageId" ON "Categories" ("ImageId");

CREATE INDEX "IX_CategoryPost_PostsId" ON "CategoryPost" ("PostsId");

CREATE INDEX "IX_Images_PostId" ON "Images" ("PostId");

CREATE INDEX "IX_Posts_AuthorId" ON "Posts" ("AuthorId");

CREATE INDEX "IX_Posts_FeaturedImageId" ON "Posts" ("FeaturedImageId");

CREATE INDEX "IX_PostTag_TagsId" ON "PostTag" ("TagsId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221103150355_AddedEntities', '6.0.10');

COMMIT;

BEGIN TRANSACTION;

DELETE FROM "AspNetRoles"
WHERE "Id" = '2988e309-7890-4db1-9746-e0426ff4f1cc';
SELECT changes();


DELETE FROM "AspNetRoles"
WHERE "Id" = '839c31b5-4adc-47e2-a069-f055742c613a';
SELECT changes();


ALTER TABLE "Posts" ADD "PublishStatus" INTEGER NOT NULL DEFAULT 0;

ALTER TABLE "Posts" ADD "SubmissionStatus" INTEGER NOT NULL DEFAULT 0;

INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName")
VALUES ('3ae682fe-96fc-44e4-bb2d-c9b6b82c5e42', '49924ef0-fa27-4b17-a096-f6a585b088ec', 'ApplicationRole', 'Admin', 'ADMIN');

INSERT INTO "AspNetRoles" ("Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName")
VALUES ('b0af6ffb-7f1a-49a8-98f1-54c20b38b555', '6fa603b3-193a-480e-a106-e2ff0f1aeba4', 'ApplicationRole', 'User', 'USER');

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20221103160140_AddedStatusToPost', '6.0.10');

COMMIT;

