using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DataTableActivities.Designer
{
    /// <summary>
    /// Interaction logic for BuildDataTableDialog.xaml
    /// </summary>
    public partial class BuildDataTableDialog : Window
    {
        private DataSet _dataSet;

        private ModelItem _activityModel;

        public DataTable DataTable
        {
            get;
            private set;
        }

        public bool SaveTable
        {
            get;
            private set;
        }

        public BuildDataTableDialog(DataTable dataTable, ModelItem ownerActivity)
        {
            this.InitializeComponent();
            this.DataTable = dataTable;
            this._activityModel = ownerActivity;
            this._dataSet = new DataSet();
            this._dataSet.EnforceConstraints = false;
            this._dataSet.Tables.Add(this.DataTable);
            this.TableDataGrid.DataContext = this._dataSet.Tables[0];
            this.TableDataGrid.ItemsSource = this.DataTable.DefaultView;
        }

        private void RemoveColumnButton_Click(object sender, RoutedEventArgs e)
        {
            string text = (sender as Button).DataContext as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return;
            }
            DataColumn dataColumn = this.DataTable.Columns[text];
            if (dataColumn == null)
            {
                this.ShowError("Column not found");
                return;
            }
            bool unique = dataColumn.Unique;
            try
            {
                if (unique)
                {
                    dataColumn.Unique = false;
                }
                this.DataTable.Columns.Remove(text);
                dataColumn.Unique = unique;
                this.UpdateItemsSource();
            }
            catch (Exception ex)
            {
                dataColumn.Unique = unique;
                this.ShowError(ex.Message);
            }
        }

        private void AddColumnButtonClick(object sender, RoutedEventArgs e)
        {
            //new NewColumnDialog(this._activityModel, this.DataTable)
            //{
            //    WindowStartupLocation = WindowStartupLocation.CenterScreen
            //}.ShowDialog();
            this.UpdateItemsSource();
        }

        private void RemoveRowButton_Click(object sender, RoutedEventArgs e)
        {
            this.OKButton.Focus();
            DataRowView dataRowView = (sender as Button).DataContext as DataRowView;
            if (dataRowView != null)
            {
                try
                {
                    this.DataTable.Rows.Remove(dataRowView.Row);
                    this.UpdateItemsSource();
                }
                catch (Exception ex)
                {
                    this.ShowError(ex.Message);
                }
            }
        }

        private void UpdateItemsSource()
        {
            this.TableDataGrid.ItemsSource = null;
            this.TableDataGrid.ItemsSource = this.DataTable.DefaultView;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveTable = false;
            base.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this._dataSet.EnforceConstraints = true;
                this.SaveTable = true;
                base.Close();
            }
            catch (Exception ex)
            {
                this.ShowError(ex.Message);
            }
        }

        private void ShowError(string message)
        {
            System.Windows.MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
    }

    public class DataTableColumnToPropertiesConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count<object>() < 2 || values[0] == null || values[0].GetType() != typeof(string))
            {
                return Binding.DoNothing;
            }
            try
            {
                string text = values[0] as string;
                DataTable dataTable = values[1] as DataTable;
                if (dataTable.Columns.Contains(text))
                {
                    DataColumn dataColumn = dataTable.Columns[text];
                    string str = (dataColumn.DefaultValue != null) ? dataColumn.DefaultValue.ToString() : string.Empty;
                    return "Column Name : " + text + Environment.NewLine + "Data Type : " + dataColumn.DataType.Name + Environment.NewLine + "Allow Null : " + dataColumn.AllowDBNull.ToString() + Environment.NewLine + "Auto Increment : " + dataColumn.AutoIncrement.ToString() + Environment.NewLine + "Default Value : " + str + Environment.NewLine + "Unique : " + dataColumn.Unique.ToString() + Environment.NewLine + "Maxlength : " + dataColumn.MaxLength.ToString();
                }
            }
            catch
            {
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[]
            {
                Binding.DoNothing,
                Binding.DoNothing
            };
        }
    }

    public class DataTableColumnToTypeConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null || values.Count<object>() < 2 || values[0] == null || values[0].GetType() != typeof(string))
            {
                return Binding.DoNothing;
            }
            try
            {
                string name = values[0] as string;
                DataTable dataTable = values[1] as DataTable;
                if (dataTable.Columns.Contains(name))
                {
                    DataColumn dataColumn = dataTable.Columns[name];
                    return "(" + dataColumn.DataType.Name + ")";
                }
            }
            catch
            {
            }
            return Binding.DoNothing;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[]
            {
                Binding.DoNothing,
                Binding.DoNothing
            };
        }
    }
}
