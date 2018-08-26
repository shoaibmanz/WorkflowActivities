using System;
using System.Activities.Presentation.Model;
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

        public ModelItem OwnerActivity
        {
            get;
            set;
        }

        
        public RemoveValuesNewColumnsDialog(ModelItem ownerActivity, ObservableCollection<Pair<ArgumentType, string>> Columns)
        {
            this.Columns = Columns;
            this.SaveChanges = false;
            this.OwnerActivity = OwnerActivity;

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
}
