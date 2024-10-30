using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

/// <summary>
/// UIComponentGenerator is a class used to dynamically generate user interface components.
/// This class provides convenient methods to generate common UI elements such as tables, radio button lists, checkbox lists, 
/// and drop-down menus, helping to improve the efficiency and flexibility of interface development.
/// </summary>
public static class UIComponentGenerator
{
    public static void PopulateDropDownList(DropDownList ddl, List<string> selection, List<string> value)
    {
        if (selection.Count != value.Count)
        {
            throw new ArgumentException("Column names and values must have the same length.");
        }
        else
        {
            ddl.Items.Clear();

            for (int i = 0; i < selection.Count; i++)
            {
                ddl.Items.Add(new ListItem(selection[i], value[i]));
            }
        }

    }
}