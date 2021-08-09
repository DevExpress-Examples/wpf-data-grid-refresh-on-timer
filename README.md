# How to refresh the GridControl on a timer

This example illustrates how to use the `RefreshOnTimerCollection`. This collection allows you to process GridControl data updates in the specified period of time. If your data source contains more than 100K items and you need to update data frequently, wrap your source collection into the `RefreshOnTimerCollection` to improve the performance.

The `RefreshOnTimerCollection` class exposes a constructor that accepts two parameters: the time interval and the data source collection.

```cs
...
data = new ObservableCollection<MarketData>(...);
Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data); 
...
```
The time interval should be sufficient to complete the refresh operation in the GridControl. Otherwise, the control may process updates incorrectly, and visible UI lags might appear.
