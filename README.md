<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/389632775/21.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1018626)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# Data Grid for WPF - How to Refresh the Data Grid on a Timer

This example illustrates how to process [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) updates on a timer. You can implement `RefreshOnTimerCollection` and wrap your data source in it. The collection blocks notifications about changes from the source and copies data in the specified time interval. Changes in `RefreshOnTimerCollection` trigger GridControl updates. You can use this technique if your data source contains less than 100K items and data changes frequently (for example, each millisecond).

The `RefreshOnTimerCollection` class exposes a constructor that accepts two parameters: the time interval and the data source collection.

```cs
// ...
data = new ObservableCollection<MarketData>(...);
Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data); 
// ...
```
If the data source collection is too large, the [GridControl](https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.GridControl) may process updates incorrectly and visible UI lags can appear. In this case, you can increase the interval. 

<!-- default file list -->
## Files to Look At

- [RefreshOnTimerCollection.cs](./CS/RefreshOnTimer/RefreshOnTimerCollection.cs) ([RefreshOnTimerCollection.vb](./VB/RefreshOnTimer/RefreshOnTimerCollection.vb))
- [ViewModel.cs](./CS/RefreshOnTimer/ViewModel.cs) ([ViewModel.vb](./VB/RefreshOnTimer/ViewModel.vb))

<!-- default file list end -->

## Documentation

- [Automatic Refresh on a Timer](https://docs.devexpress.com/WPF/115836/controls-and-libraries/data-grid/performance-improvement/frequent-data-updates?v=21.2#automatic-refresh-on-a-timer)
