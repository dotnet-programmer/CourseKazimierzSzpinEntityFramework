using Blog.UI.Linq;
using Blog.UI.EntityFrameworkCore;

Linq linq = new();
linq.Basics();
linq.CollectionJoins();
linq.DataAggregationOperations();
linq.DataGrouping();
linq.DataPagination();
linq.Homework();

EFCoreQueries efCoreQueries = new();
await efCoreQueries.Basics();
efCoreQueries.DatabaseQueryPreview();
efCoreQueries.EagerLoading();
efCoreQueries.ExplicitLoading();
efCoreQueries.ChangeTracking();
efCoreQueries.RawSql();

EFCoreCommands eFCoreCommands = new();
await eFCoreCommands.Add();
await eFCoreCommands.AdvancedDataAdding();
await eFCoreCommands.Update();
await eFCoreCommands.AdvancedDataUpdating();
await eFCoreCommands.Delete();
await eFCoreCommands.AdvancedDataDeletion();
await eFCoreCommands.Transactions();
await eFCoreCommands.ConcurrencyConflicts();
await eFCoreCommands.Views();
await eFCoreCommands.Procedures();