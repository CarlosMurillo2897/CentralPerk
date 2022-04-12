-- INSERT into Products Table.
INSERT INTO "Products" ("CreatedOn", "UpdatedOn", "Name", "Description", "Price", "IsTaxable", "IsArchived") 
VALUES
(NOW(), NOW(), '10 LB Dark Roast', '10 LB bag of dark roast coffee beans', 100, true, false),
(NOW(), NOW(), '30 LB Dark Roast', '30 LB bag of dark roast coffee beans', 280, true, false),
(NOW(), NOW(), '50 LB Dark Roast', '50 LB bag of dark roast coffee beans', 450, true, false),
(NOW(), NOW(), '10 LB Light Roast', '10 LB bag of light roast coffee beans', 80, true, false),
(NOW(), NOW(), '30 LB Light Roast', '30 LB bag of light roast coffee beans', 260, true, false),
(NOW(), NOW(), '50 LB Light Roast', '50 LB bag of light roast coffee beans', 430, true, false);