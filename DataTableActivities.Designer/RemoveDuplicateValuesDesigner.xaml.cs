using System;
using System.Activities.Presentation.Metadata;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Linq;
using DataTableActivities;

namespace DataTableActivities.Designer
{
    // Interaction logic for RemoveDuplicateValuesDesigner.xaml
    public partial class RemoveDuplicateValuesDesigner
    {
        public RemoveDuplicateValuesDesigner()
        {
            InitializeComponent();
        }

        private void AddColumnsButton_Click(object sender, RoutedEventArgs e)
        {
           
            ObservableCollection<Pair<ArgumentType, string>> Columns = new ObservableCollection<Pair<ArgumentType, string>>((List<Pair<ArgumentType, string>>)ModelItem.Properties["Columns"].ComputedValue);

            var NewWindow = new RemoveValuesNewColumnsDialog(Columns);

            NewWindow.ShowDialog();

            if (NewWindow.SaveChanges)
            {
                ModelItem.Properties["Columns"].SetValue(Columns.ToList());
            }
        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RemoveDuplicateValues), new DesignerAttribute(typeof(RemoveDuplicateValuesDesigner)));
            builder.AddCustomAttributes(typeof(RemoveDuplicateValues), new DescriptionAttribute("Converts Data Table to a readable String"));
        }

    }
}
