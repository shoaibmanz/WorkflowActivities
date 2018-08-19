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
    public partial class NewColumnDialog : Window, INotifyPropertyChanged
    {
        private Type _dateType;

        private bool _autoIncrement;

        private DataTable _dataTable;

        public event PropertyChangedEventHandler PropertyChanged;

        public string ColumnName
        {
            get;
            set;
        }

        public Type DateType
        {
            get
            {
                return this._dateType;
            }
            set
            {
                this._dateType = value;
                this.OnNotifyPropertyChanged("DateType");
                this.OnNotifyPropertyChanged("IsStringColumn");
                this.OnNotifyPropertyChanged("CanAutoIncrement");
            }
        }

        public bool AllowDBNull
        {
            get;
            set;
        }

        public bool AutoIncrement
        {
            get
            {
                return this._autoIncrement;
            }
            set
            {
                this._autoIncrement = value;
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
            get;
            set;
        }

        public object DefaultValue
        {
            get;
            set;
        }

        public int MaxLength
        {
            get;
            set;
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

        public NewColumnDialog(ModelItem ownerActivity, DataTable dataTable)
        {
            this._dataTable = dataTable;
            this.MaxLength = -1;
            this.DateType = typeof(string);
            this.AllowDBNull = true;
            this.InitializeComponent();
            this.TypePresenter.Context = ownerActivity.GetEditingContext();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.ColumnName))
                {
                    throw new ArgumentException("Column Name is not set");
                }
                DataColumn dataColumn = new DataColumn(this.ColumnName, this.DateType);
                dataColumn.AllowDBNull = this.AllowDBNull;
                dataColumn.Unique = this.Unique;
                if (this.CanAutoIncrement)
                {
                    dataColumn.AutoIncrement = this.AutoIncrement;
                }
                else if (this.DefaultValue != null && !string.IsNullOrWhiteSpace(this.DefaultValue.ToString()))
                {
                    dataColumn.DefaultValue = Convert.ChangeType(this.DefaultValue, this.DateType);
                }
                if (this.DateType.Equals(typeof(string)))
                {
                    dataColumn.MaxLength = this.MaxLength;
                }
                if (dataColumn.DataType != this.DateType)
                {
                    throw new InvalidOperationException("Column options incompatible with desired date type");
                }
                this._dataTable.Columns.Add(dataColumn);
                base.Close();
            }
            catch (Exception arg_E3_0)
            {
                System.Windows.MessageBox.Show(arg_E3_0.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

    }
}
