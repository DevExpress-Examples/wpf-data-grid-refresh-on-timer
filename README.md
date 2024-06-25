<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/389632775/20.2.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T1018626)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
[![](https://img.shields.io/badge/💬_Leave_Feedback-feecdd?style=flat-square)](#does-this-example-address-your-development-requirementsobjectives)
<!-- default badges end -->
# How to refresh the GridControl on a timer

This example illustrates how to implement `RefreshOnTimerCollection` that allows you to process GridControl data updates in the specified period of time. Wrap your source collection into `RefreshOnTimerCollection` to improve performance. You can use this technique if your data source contains less than 100K items and data changes frequently (for example, each millisecond).

The `RefreshOnTimerCollection` class exposes a constructor that accepts two parameters: the time interval and the data source collection.

```cs
...
data = new ObservableCollection<MarketData>(...);
Source = new RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data); 
...
```
If the data source collection is too large, the GridControl may process updates incorrectly, and visible UI lags might appear. In this case, you can increase the time interval value. 
<!-- feedback -->
## Does this example address your development requirements/objectives?

[<img src="https://www.devexpress.com/support/examples/i/yes-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-refresh-on-timer&~~~was_helpful=yes) [<img src="https://www.devexpress.com/support/examples/i/no-button.svg"/>](https://www.devexpress.com/support/examples/survey.xml?utm_source=github&utm_campaign=wpf-data-grid-refresh-on-timer&~~~was_helpful=no)

(you will be redirected to DevExpress.com to submit your response)
<!-- feedback end -->
