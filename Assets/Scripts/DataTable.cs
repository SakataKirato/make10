using System;
using System.Data; // DataTableを使うために追加

public class Calculator
{
    public static double EvaluateExpression(string expression)
    {
        DataTable table = new DataTable();
        table.Columns.Add("expression", typeof(string), expression);
        DataRow row = table.NewRow();
        table.Rows.Add(row);
        return double.Parse((string)row["expression"]);
    }
}
