# How to refresh the Data Grid with a timer

This example illustrates how you can wrap your data source collection in `RefreshOnTimerCollection` in order to avoid immediate updates in the Data Grid when your source is changed. This can help you improve performance in your application if the Data Grid is bound to a large source (100K items and less) with a significant number of updates.

The `RefreshOnTimerCollection` class exposes a constructor that accepts two parameters: the time interval for the timer and your source collection.

```cs
...
data = new ObservableCollection<MarketData>(...);
Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data); 
...
```

Please note that the interval value should be sufficient to complete the refresh operation in the Data Grid. Otherwise, the Data Grid may not completely process the current set of changes when it receives the next set to process.
