using OfflineFormApp.Repositories;

namespace OfflineFormApp.Views;

public partial class InspectionReport : ContentPage
{
	public InspectionReport()
	{
		InitializeComponent();
        LoadReports();

    }
    private void LoadReports()
    {
        var reports = ReportRepository.GetAllReports();
        ReportsCollectionView.ItemsSource = reports;
    }

}