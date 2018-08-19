using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for RemoveValuesNewColumnsDialog.xaml
    /// </summary>
    public partial class RemoveValuesNewColumnsDialog : Window
    {
        public ObservableCollection<Pair<ArgumentType, string>> Columns
        {
            get; set;
        }

        public bool SaveChanges
        {
            get; set;
        }

        public RemoveValuesNewColumnsDialog(ObservableCollection<Pair<ArgumentType, string>> Columns)
        {
            this.Columns = Columns;
            this.SaveChanges = false;
            InitializeComponent();
            
            this.lbInputs.DataContext = this.Columns;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            SaveChanges = false;
            this.Close();

        }

        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
            this.Columns.Add(new Pair<ArgumentType, string>(ArgumentType.ColumnName, ""));            
        }

        private void RemoveColumn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int index = lbInputs.Items.IndexOf(button.DataContext);

            Columns.RemoveAt(index);
        }
    }



    public class ArgumentTypeToComboBoxItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value != null && (ArgumentType)value == ArgumentType.ColumnName)
            {
                return 0;
            }
            return 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if ((int)value == 0)
            {
                return ArgumentType.ColumnName;
            }
            return ArgumentType.ColumnIndex;
        }
    }

    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value != null)
            {
                if ((int) value == 0)
                {
                    return Visibility.Visible;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
