using System;
using System.Text;
using Avalonia.Controls;

namespace XAMLGen.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void TextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        try
        {
            var rawText = InputCodeTB.Text;
            var properties = rawText.Split("public",
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var objName = properties[0].Split(' ')[3].Replace("\r\n", "").Trim('{');
            var finalXaml = new StringBuilder();
            var insertCode = new StringBuilder($"public void {objName}InsertData({objName} data) \r\n{{\r\n");
            var fetchCode = new StringBuilder($"public bool {objName}FetchData({objName} data) \r\n{{\r\n");
            foreach (var property in properties)
            {
                var parts = property.Split(" ");
                var type = parts[0];
                var name = parts[1];
                finalXaml.AppendLine($"<TextBlock Margin=\"5\" Text=\"{name}\"></TextBlock>");
                switch (type)
                {
                    case "string":
                    case "int":
                    case "decimal":
                        finalXaml.AppendLine($"<TextBox Margin=\"5\" Name=\"{name}TB\"></TextBox>");
                        insertCode.AppendLine($"\t{name}TB.Text = data.{name}.ToString();");
                        break;
                    case "System.DateTime":
                        finalXaml.AppendLine(
                            $"<DatePicker  HorizontalAlignment=\"Left\" Margin=\"5\" Width=\"200\" Name=\"{name}DP\"></DatePicker>\n");
                        insertCode.AppendLine($"\t{name}TB.SelectedDate = data.{name};");
                        fetchCode.AppendLine($"\tdata.{name} = {name}TB.SelectedDate;");
                        break;
                }

                switch (type)
                {
                    case "string":
                        fetchCode.AppendLine($"\tdata.{name} = {name}TB.Text;");
                        break;
                    case "int":
                        fetchCode.AppendLine($$"""
                                                   if (!int.TryParse({{name}}TB.Text, out data.{{name}}))
                                                   {
                                                       MessageBox.Show("Проверьте правильность введенных данных.");
                                                       return false;
                                                   }
                                               """);
                        break;
                    case "decimal":
                        fetchCode.AppendLine($$"""
                                                   if (!decimal.TryParse({{name}}TB.Text, out data.{{name}}))
                                                   {
                                                       MessageBox.Show("Проверьте правильность введенных данных.");
                                                       return false;
                                                   }
                                               """);
                        break;
                }
            }

            insertCode.AppendLine("}");
            insertCode.AppendLine();
            fetchCode.AppendLine("return true;\n}");
            insertCode.Append(fetchCode.ToString());
            XAMLTB.Text = finalXaml.ToString();
            IOTB.Text = insertCode.ToString();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}