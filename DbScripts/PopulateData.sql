INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('100', 'System', 'Administrator', 'System Administrator', 1, NULL, NULL, 'admin', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('200', 'Andrea', 'Munoz', 'Executive VP of Operations', 1, 'andrea@example.com', '555-123-0001', 'Andrea.Munoz', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('201', 'Nicholas', 'Griffin', 'Chief Technical Architect', 1, 'nicholas@example.com', '555-123-0002', 'Nicholas.Griffin', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('202', 'Virginia', 'Wright', 'Director of Finance', 1, 'virginia@example.com', '555-123-0003', 'Virginia.Wright', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('203', 'Simon', 'Harmon', 'Director of Human Resources', 1, 'simon@example.com', '555-123-0004', 'Simon.Harmon', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('204', 'Nathaniel', 'Henderson', 'Director of Engineering', 1, 'nathaniel@example.com', '555-123-0005', 'Nathaniel.Henderson','$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('205', 'Mario', 'Farmer', 'Director of Engineering', 3, 'marion@example.com', '555-123-0006', 'Marion.Farmer', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('206', 'Marily', 'Barber', 'Director of Engineering', 2, 'marilyn@example.com', '555-123-0007', 'Marilyn.Barber', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('207', 'Leticia', 'Klei', 'Director of Engineering', 1, 'leticia@example.com', '555-123-0008', 'Leticia.Klei', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('208', 'Tiffany', 'Cai', 'Chairman & CEO', 1, 'tiffany@example.com', '555-123-0009', 'Tiffany.Cai', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');
INSERT INTO Employee (Id, FirstName, LastName, Title, Office, Email, PhoneNumber, Username, HashedPassword) VALUES ('209', 'Shawna', 'Rya', 'President', 1, 'shawna@example.com', '555-123-0010', 'Shawna.Rya', '$2a$10$gcEzk9UTkSPeAszrKkhB6ex9n9l0yBi4bxLQ/xb2meck/zxaFy.SC');

INSERT INTO EmployeeRole (Id, EmployeeId, RoleId) VALUES ('1', '100', '1');

INSERT INTO Role (Id, Name) VALUES ('1', 'System Administrator');
INSERT INTO Role (Id, Name) VALUES ('2', 'Manager');
INSERT INTO Role (Id, Name) VALUES ('3', 'Human Resources');

INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('50', '1', 1);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('51', '1', 2);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('52', '1', 3);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('53', '1', 4);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('54', '3', 2);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('55', '3', 3);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('56', '3', 4);
INSERT INTO RolePermission (Id, RoleId, Permission) VALUES ('57', '2', 4);