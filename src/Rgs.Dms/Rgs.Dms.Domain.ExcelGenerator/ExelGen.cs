using ClosedXML.Excel;
using System.Data;

namespace Rgs.Dms.Domain.ExcelGenerator;
public static class ExelGen
{
    public static async Task<XLWorkbook> BuildExcelFile(int id)
    {
        //Creating the workbook
        var t = Task.Run(() =>
        {
            var wb = new XLWorkbook();
            var ws = wb.AddWorksheet("Sheet1");
            ws.FirstCell().SetValue(id);

            return wb;
        });

        return await t;
    }
}
