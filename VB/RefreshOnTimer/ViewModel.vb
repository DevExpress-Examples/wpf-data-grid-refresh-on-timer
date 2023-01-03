Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Threading

Namespace RefreshOnTimer

    Friend Class ViewModel
        Inherits ViewModelBase

#Region "Stocks"
        Private Shared ReadOnly Names As String() = {"ANR", "FE", "GT", "PRGO", "APD", "PPL", "AES", "AVB", "IBM", "GAS", "EFX", "GPC", "ICE", "IVZ", "KO", "CCE", "SO", "STI", "BWA", "HRL", "WFM", "LM", "TROW", "K", "EXPE", "PCAR", "TRIP", "WHR", "WMT", "NU", "HST", "CVH", "LMT", "MAR", "CVC", "RF", "VMC", "PHM", "MU", "IRM", "AMT", "BXP", "STT", "PBCT", "FISV", "BLL", "MTB", "DIS", "LH", "AKAM", "CPB", "MYL", "LIFE", "LEG", "SCG", "CNX", "COL", "MCHP", "GR", "DUK", "BAC", "NUE", "UNM", "DLTR", "ABC", "TEG", "RRD", "EQR", "EXC", "BA", "CME", "NTRS", "VTR", "FITB", "PG", "KR", "M", "SNI", "ETN", "CLF", "PH", "KEY", "SHW", "HD", "AFL", "TSS", "CMI", "HBAN", "AEP", "BIG", "LTD", "ESRX", "GLW", "WPI", "MON", "AAPL", "DF", "T", "CMA", "THC", "LUV", "TXN", "TIE", "PX"}

        Private Shared ReadOnly AdditionalNames As String() = {"ZM", "RE", "BSX", "PPD", "LB", "OLN", "ENPH", "NVKR", "GNRC"}

#End Region
        Private timer1 As Timer

        Private timer2 As Timer

        Private timer3 As Timer

        Private random As Random = New Random()

        Private additionalData As Stack(Of MarketData)

        Private syncRoot As Object

        Private data As ObservableCollection(Of MarketData)

        Public Property Source As RefreshOnTimerCollection

        Public Sub New()
            data = New ObservableCollection(Of MarketData)(Names.[Select](Function(name) New MarketData(name)).ToList())
            syncRoot = CType(data, ICollection).SyncRoot
            additionalData = New Stack(Of MarketData)(AdditionalNames.[Select](Function(name) New MarketData(name)))
            Source = New RefreshOnTimerCollection(TimeSpan.FromSeconds(1), data)
            timer1 = New Timer(AddressOf UpdateRows, Nothing, 0, 1)
            timer2 = New Timer(AddressOf TryAddNewRow, Nothing, 10, 1)
            timer3 = New Timer(AddressOf TryRemoveRow, Nothing, 20, 1)
        End Sub

        <Command>
        Public Sub DisposeViewModel()
            timer1.Dispose()
            timer2.Dispose()
            timer3.Dispose()
            Source.Dispose()
        End Sub

        Private Sub UpdateRows(ByVal state As Object)
            SyncLock syncRoot
                For i As Integer = 0 To 2 - 1
                    Dim row As Integer = random.Next(0, data.Count)
                    data(row).Update()
                Next

            End SyncLock
        End Sub

        Private Sub TryAddNewRow(ByVal state As Object)
            SyncLock syncRoot
                If random.Next() Mod 2 = 0 AndAlso additionalData.Count > 0 Then
                    data.Add(additionalData.Pop())
                End If

            End SyncLock
        End Sub

        Private Sub TryRemoveRow(ByVal state As Object)
            SyncLock syncRoot
                If random.Next() Mod 2 = 0 AndAlso additionalData.Count < AdditionalNames.Length Then
                    Dim dataItem = data.First(Function(x) AdditionalNames.Contains(x.Ticker))
                    data.Remove(dataItem)
                    additionalData.Push(dataItem)
                End If

            End SyncLock
        End Sub
    End Class
End Namespace
