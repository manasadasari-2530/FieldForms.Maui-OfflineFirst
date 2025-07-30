using Newtonsoft.Json.Linq;
using OfflineFormSolution.Models;

namespace OfflineFormApp.Views;

public partial class DynamicFormPage : ContentPage
{
    // ✅ Declare the dictionary to store form inputs
    private Dictionary<string, View> _formInputs = new();

    public DynamicFormPage()
    {
        InitializeComponent();
        LoadForm();
    }

    private async void LoadForm()
    {
        var schema = await FormSchemaService.GetFormSchemaAsync("Vehicle Inspection");

        foreach (var field in schema.Fields)
        {
            Label label = new() { Text = field.Label };
            FormLayout.Children.Add(label);

            View inputControl = field.Type switch
            {
                "entry" => new Entry(),
                "checkbox" => new CheckBox(),
                "date" => new DatePicker(),
                "picker" => await CreatePickerFromApi(field),
                _ => null
            };

            if (inputControl != null)
            {
                _formInputs[field.Binding] = inputControl;
                FormLayout.Children.Add(inputControl);
            }
        }
    }

    private async Task<Picker> CreatePickerFromApi(Field field)
    {
        var picker = new Picker { Title = field.Label };
        var items = await FormSchemaService.FetchPickerItemsAsync(field.DataSource);

        if (items.Count > 0)
        {
            foreach (var item in items)
                picker.Items.Add(item);
        }
        else
        {
            // Show message/disable if no data available
            picker.Items.Add("(Data not available offline. Please connect to download.)");
            picker.SelectedIndex = 0;
            picker.IsEnabled = false;
            picker.TextColor = Colors.Red;
        }
        return picker;
    }

    private async void OnSubmitClicked(object sender, EventArgs e)
    {
        var result = new JObject();

        // Fix: Provide types explicitly for key-value deconstruction
        foreach (KeyValuePair<string, View> kvp in _formInputs)
        {
            var key = kvp.Key;
            var view = kvp.Value;

            object value = view switch
            {
                Entry entry => entry.Text,
                CheckBox cb => cb.IsChecked,
                DatePicker dp => dp.Date.ToString("yyyy-MM-dd"),
                Picker pk => pk.SelectedItem?.ToString(),
                _ => null
            };
            result[key] = JToken.FromObject(value);
        }

        await LocalDbService.SaveReportAsync("Vehicle Inspection", result.ToString());
        await DisplayAlert("Saved", "Report saved locally.", "OK");
    }
}
