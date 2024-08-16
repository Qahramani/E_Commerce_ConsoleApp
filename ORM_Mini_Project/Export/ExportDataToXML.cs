namespace ORM_Mini_Project.XML;
using ClosedXML.Excel;
public static  class ExportDataToXML
{
    public static bool Export<T>(List<T> list, string file, string sheetName)
    {
        bool exported = false;
        using(IXLWorkbook workbook = new XLWorkbook())
        {
            workbook.AddWorksheet(sheetName).FirstCell().InsertTable<T>(list, false);
            workbook.SaveAs(file);
            exported = true;
        }
        return exported;

    }
}
