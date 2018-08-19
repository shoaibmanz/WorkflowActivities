using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;
using System.IO;
using System.Data;
using System.ComponentModel;

namespace DataTableActivities
{

    public sealed class BuildDataTable : CodeActivity
    {
        [RequiredArgument, Browsable(false)]
        public string TableInfo
        {
            get;
            set;
        }

        [Category("Output")]
        public OutArgument<DataTable> DataTable
        {
            get;
            set;
        }

        public BuildDataTable()
        {
            DataTable expr_0B = new DataTable();
            expr_0B.TableName = "TableName";
            expr_0B.Columns.Add(new DataColumn("Column1", typeof(string))
            {
                MaxLength = 100
            });
            expr_0B.Columns.Add(new DataColumn("Column2", typeof(int)));
            DataRow dataRow = expr_0B.NewRow();
            dataRow["Column1"] = "text";
            dataRow["Column2"] = 1;
            expr_0B.Rows.Add(dataRow);
            StringWriter stringWriter = new StringWriter();
            expr_0B.WriteXml(stringWriter, XmlWriteMode.WriteSchema);
            this.TableInfo = stringWriter.ToString();
        }

        protected override void Execute(CodeActivityContext context)
        {
            StringReader reader = new StringReader(this.TableInfo);
            DataTable dataTable = new DataTable();
            dataTable.ReadXml(reader);
            this.DataTable.Set(context, dataTable);
        }
    }
}
