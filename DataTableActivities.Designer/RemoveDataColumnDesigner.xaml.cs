﻿using System;
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

        }

        public static void RegisterMetadata(AttributeTableBuilder builder)
        {
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DesignerAttribute(typeof(RemoveDataColumnDesigner)));
            builder.AddCustomAttributes(typeof(RemoveDataColumn), new DescriptionAttribute("Removes a Column from a Data Table"));
        }
    }


}
