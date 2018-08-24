using System.Activities;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public class RemoveDataColumn : CodeActivity
    {
        [Browsable(false)]
        public int Selected
        {
            get;
            set;
        }

        [Browsable(false)]
        public string[] Options
        {
            get;
            set;
        } = { "Column Name", "Column Index", "Column Object" };



        public RemoveDataColumn()
        {
            this.Selected = 0;
        }


        [RequiredArgument]
        [Category("Input")]
        public InOutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        [OverloadGroup("ColumnIndex")]
        [RequiredArgument]
        [Category("Input")]
        public InArgument<int> ColumnIndex
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

        [Category("Input")]
        public InArgument<DataColumn> ColumnObject
        {
            get;
            set;
        }


        protected override void Execute(CodeActivityContext context)
        {
            
            DataTable dataTable = this.DataTable.Get(context);

            switch (this.Selected)
            {
                case 0:
                    dataTable.Columns.Remove(this.ColumnName.Get(context));
                    break;

                case 1:
                    dataTable.Columns.RemoveAt(this.ColumnIndex.Get(context));
                    break;
                case 2:
                    dataTable.Columns.Remove(this.ColumnObject.Get(context));
                    break;

            }

            this.DataTable.Set(context, dataTable);
        }
    }
}
