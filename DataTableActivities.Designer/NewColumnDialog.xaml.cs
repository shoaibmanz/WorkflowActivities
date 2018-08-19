using System;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for NewColumnDialog.xaml
    /// </summary>
    public partial class NewColumnDialog : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string ColumnName
        {
            get
            {
                return this.dataColumn.ColumnName;
            }
            set
            {
                this.dataColumn.ColumnName = value;
            }
        }

        public Type DateType
        {
            get
            {
                return this.dataColumn.DataType;
            }
            set
            {
                this.dataColumn.DataType = value;
                this.OnNotifyPropertyChanged("DateType");
                this.OnNotifyPropertyChanged("IsStringColumn");
                this.OnNotifyPropertyChanged("CanAutoIncrement");
            }
        }

        public bool AllowDBNull
        {
            get
            {
                return this.dataColumn.AllowDBNull;
            }
            set
            {
                this.dataColumn.AllowDBNull = value;
            }
        }

        public bool AutoIncrement
        {
            get
            {
                return this.dataColumn.AutoIncrement;
            }
            set
            {
                this.dataColumn.AutoIncrement = value;
                this.OnNotifyPropertyChanged("AutoIncrement");
            }
        }

        public bool CanAutoIncrement
        {
            get
            {
                return this.DateType.Equals(typeof(short)) || this.DateType.Equals(typeof(int)) || this.DateType.Equals(typeof(long));
            }
        }

        public bool Unique
        {
            get
            {
                return this.dataColumn.Unique;
            }
            set
            {
                this.dataColumn.Unique = value;
            }
        }

        public object DefaultValue
        {
            get
            {
                return this.dataColumn.DefaultValue;
            }
            set
            {
                if (this.dataColumn.DataType.Equals(value.GetType()))
                {
                    MessageBox.Show("Error: Invalid default value type with this column type");
                }
                else
                {
                    this.dataColumn.DefaultValue = value;
                }
            }
        }

        public int MaxLength
        {
            get
            {
                return this.dataColumn.MaxLength;
            }
            set
            {

            }
        }

        public bool IsStringColumn
        {
            get
            {
                return this.DateType.Equals(typeof(string));
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public DataColumn dataColumn
        {
            get;
            set;
        }

        public bool SaveChanges
        {
            get;
            set;
        }


        public NewColumnDialog(ModelItem ownerActivity, DataColumn dataColumn)
        {

            this.dataColumn = dataColumn;
            this.MaxLength = 0;
            this.DateType = typeof(string);
            this.AllowDBNull = true;
            this.InitializeComponent();            
            this.TypePresenter.Context = ownerActivity.GetEditingContext();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.SaveChanges = false;
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.ColumnName))
                {
                    throw new ArgumentException("Please enter a column name");
                }
                else if (this.DefaultValue != null && !string.IsNullOrWhiteSpace(this.DefaultValue.ToString()))
                {
                    dataColumn.DefaultValue = Convert.ChangeType(this.DefaultValue, this.DateType);
                }

                if (dataColumn.DataType != this.DateType)
                {
                    throw new InvalidOperationException("Column options incompatible with desired date type");
                }

                this.SaveChanges = true;
                this.Close();
            }
            catch (Exception arg_E3_0)
            {
                System.Windows.MessageBox.Show(arg_E3_0.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

    }
}
