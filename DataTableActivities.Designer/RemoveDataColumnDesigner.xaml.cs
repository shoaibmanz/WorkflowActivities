using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataTableActivities.Designer
{
    // Interaction logic for RemoveDataColumnDesigner.xaml
    public partial class RemoveDataColumnDesigner
    {
        public RemoveDataColumnDesigner()
        {
            InitializeComponent();

            cbChoices.SelectionChanged += ComboBox_SelectionChanged;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ModelItem.Properties["ColumnIndex"].SetValue(null);
            ModelItem.Properties["ColumnName"].SetValue(null);
            ModelItem.Properties["ColumnObject"].SetValue(null);
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DesignerAttribute(typeof(RemoveDataColumnDesigner)));
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }

    public class OptionsToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value != null && parameter != null)
            {
                string param = parameter as string;

                if (((int)value).ToString() == param)
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
