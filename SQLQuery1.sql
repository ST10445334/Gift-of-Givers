-- 1. Clean up the broken row
DELETE FROM AspNetUserRoles WHERE UserId IS NULL OR RoleId IS NULL;

-- 2. Ensure Admin role exists
IF NOT EXISTS (SELECT 1 FROM AspNetRoles WHERE Name = 'Admin')
    INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp)
    VALUES (NEWID(), 'Admin', 'ADMIN', NEWID());

-- 3. Ensure admin user has the Admin role
DECLARE @AdminUserId NVARCHAR(450);
DECLARE @AdminRoleId NVARCHAR(450);

SELECT @AdminUserId = Id FROM AspNetUsers WHERE Email = 'admin@giftofgivers.local';
SELECT @AdminRoleId = Id FROM AspNetRoles WHERE Name = 'Admin';

IF @AdminUserId IS NOT NULL AND @AdminRoleId IS NOT NULL
    AND NOT EXISTS (SELECT 1 FROM AspNetUserRoles
                    WHERE UserId = @AdminUserId AND RoleId = @AdminRoleId)
BEGIN
    INSERT INTO AspNetUserRoles (UserId, RoleId)
    VALUES (@AdminUserId, @AdminRoleId);
END

-- 4. Verify — this should show admin@giftofgivers.local | Admin
SELECT u.Email, r.Name AS Role
FROM AspNetUsers u
LEFT JOIN AspNetUserRoles ur ON ur.UserId = u.Id
LEFT JOIN AspNetRoles r ON r.Id = ur.RoleId
WHERE u.Email = 'admin@giftofgivers.local';