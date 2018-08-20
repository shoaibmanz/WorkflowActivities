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
        private Type _dateType;

        private bool _autoIncrement;
        
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

        public long AutoIncrementStep
        {
            get;
            set;
        }

        public long AutoIncrementSeed
        {
            get;
            set;
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

            this.ColumnName = dataColumn.ColumnName;
            this.DateType = dataColumn.DataType;
            this.AllowDBNull = dataColumn.AllowDBNull;
            this.AutoIncrement = dataColumn.AutoIncrement;
            this.Unique = dataColumn.Unique;
            this.DefaultValue = dataColumn.DefaultValue;
            this.MaxLength = dataColumn.MaxLength;
            this.AutoIncrementSeed = dataColumn.AutoIncrementSeed;
            this.AutoIncrementStep = dataColumn.AutoIncrementStep;

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
                    throw new ArgumentException("Please enter a column name!");
                }
                dataColumn.ColumnName = this.ColumnName;
                dataColumn.DataType = this.DateType;
                dataColumn.AllowDBNull = this.AllowDBNull;
                dataColumn.Unique = this.Unique;
                
                if (this.CanAutoIncrement)
                {
                    dataColumn.AutoIncrement = this.AutoIncrement;
                    dataColumn.AutoIncrementSeed = this.AutoIncrementSeed;
                    dataColumn.AutoIncrementStep = this.AutoIncrementStep;
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
                    throw new InvalidOperationException("Invalid type for given set of options.");
                }

                this.SaveChanges = true;
                this.Close();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

    }
}
