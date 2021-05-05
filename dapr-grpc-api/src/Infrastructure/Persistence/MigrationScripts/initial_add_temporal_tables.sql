/*Temporal tables for SQL Server example */

ALTER TABLE dbo.Entity
    ADD StartTime datetime2(0)
    GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT
      DF_Entity_SysStart DEFAULT SYSUTCDATETIME(),
    EndTime datetime2(0)
    GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT
      DF_Entity_SysEnd DEFAULT CONVERT(datetime2 (0),
      '9999-12-31 23:59:59'),
    PERIOD FOR SYSTEM_TIME (StartTime, EndTime)
  ALTER TABLE dbo.Entity
    SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.EntityHistory));

ALTER TABLE dbo.CatalogItems
    ADD StartTime datetime2(0)
    GENERATED ALWAYS AS ROW START HIDDEN CONSTRAINT
      DF_CatalogItems_SysStart DEFAULT SYSUTCDATETIME(),
    EndTime datetime2(0)
    GENERATED ALWAYS AS ROW END HIDDEN CONSTRAINT
      DF_CatalogItems_SysEnd DEFAULT CONVERT(datetime2 (0),
      '9999-12-31 23:59:59'),
    PERIOD FOR SYSTEM_TIME (StartTime, EndTime)
  ALTER TABLE dbo.CatalogItems
    SET (SYSTEM_VERSIONING = ON (HISTORY_TABLE = dbo.CatalogItemsHistory));
