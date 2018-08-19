using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public class AddDataColumn<T> : CodeActivity
    {
        [RequiredArgument, Category("Input")]
        public InArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [OverloadGroup("Column"), RequiredArgument, Category("Input")]
        public InArgument<DataColumn> Column
        {
            get;
            set;
        }

        [OverloadGroup("ColumnName"), RequiredArgument, Category("Input")]
        public InArgument<string> ColumnName
        {
            get;
            set;
        }

        [Category("Options")]
        public bool AutoIncrement
        {
            get;
            set;
        }

        [Category("Options")]
        public bool Unique
        {
            get;
            set;
        }

        [Category("Options")]
        public InArgument<T> DefaultValue
        {
            get;
            set;
        }

        [Category("Options")]
        public bool AllowDBNull
        {
            get;
            set;
        }

        [Category("Options")]
        public int MaxLength
        {
            get;
            set;
        }

        public AddDataColumn()
        {
            this.AllowDBNull = true;
            this.MaxLength = 100;
        }

        protected override void Execute(CodeActivityContext context)
        {
            DataTable dataTable = this.DataTable.Get(context);
            DataColumn dataColumn = this.Column.Get(context);
            if (dataColumn != null)
            {
                dataTable.Columns.Add(this.Column.Get(context));
                return;
            }
            dataColumn = new DataColumn(this.ColumnName.Get(context), typeof(T));
            dataColumn.AutoIncrement = this.AutoIncrement;
            dataColumn.AllowDBNull = this.AllowDBNull;
            if (typeof(T).Equals(typeof(string)))
            {
                dataColumn.MaxLength = this.MaxLength;
            }
            if (!this.AutoIncrement)
            {
                dataColumn.DefaultValue = this.DefaultValue.Get(context);
            }
            dataColumn.Unique = this.Unique;
            dataTable.Columns.Add(dataColumn);
        }
    }
}
