# Fix NullReferenceException in StoreStockService

# Steps
- [x] 1. Analyze code and identify root cause
- [x] 2. Fix `GetStoreStocks` in `StoreStockService.cs` — add `.Include(x => x.Item)`
- [ ] 3. Fix `GetItemInAllStores` in `GetStoreStockController.cs` — fix route, call correct service, return correct variable
- [ ] 4. Build project to verify

