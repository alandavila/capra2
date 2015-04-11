using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExportLib
{
    public class XportExcel
    {
        Excel.Application xlApp = null;
        Excel.Workbook xlWorkBook = null;
        Excel.Worksheet xlWorkSheet = null;
        Excel.Range xlRange  = null;
        object misValue = System.Reflection.Missing.Value;

        private List<String> _header = new List<String>() 
            {
                "EMPRESA","LUNES","MARTES","MIÉRCOLES","JUEVES","VIERNES","SÁBADO","TAMBOS","   ",   "   "
            };

        public XportExcel() 
        { 
        }
        public void CreateReportSkeleton(DateTime initialdate, DateTime finaldate,List<String> empresas) 
        {
            //place of first schedule entry//header is two rows above, one column to the right
            int iniRow = 3;
            int iniCol = 3;
            //initial day of the week for putting dates on report:
            int day_of_week;
            TimeSpan NumWeeks;
            int reportWeeks = 0;
            int weekCount = 0;

            //start on next monday if initial date lands on Sunday:
            if (initialdate.DayOfWeek == DayOfWeek.Sunday) 
            {
                initialdate = initialdate.AddDays(1);
            }
            //Sunday = 0, Monday =1, ....Saturday = 6
            day_of_week = (int)initialdate.DayOfWeek;
            NumWeeks = finaldate.Subtract(initialdate);
            reportWeeks = NumWeeks.Days / 7;

            //check that the initial and final dates are not the same
            if (initialdate == finaldate) 
            {
                MessageBox.Show("La fecha inicial y final del reporte coinciden...");
                return;
            }

            DateTime queryDate = initialdate;
            //query database to get data for report

            //create Excel application
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            //make sure application could be created
            if (xlApp == null) 
            {
                MessageBox.Show("EXCEL no esta instalado en esta computadora.");
                return;
            }

            //add a workbook to Excel application
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            //add header
            xlWorkSheet.Cells[iniRow -2   , iniCol+1] = "REPORTE DIARIO DE DISPOSICIÓN ESCAMOCHA";
            setColumnsWidth(14);
            //add empresas to spreasheet
            int i = iniRow+2,j = 0;
            foreach (String empresa in empresas) 
            {
                xlWorkSheet.Cells[i, iniCol] = empresa.ToString();
                i++;
            }
            //add header for each week including date for each week day:
            while (weekCount <= reportWeeks)
            {
                foreach (string headerelement in _header)
                {
                    xlWorkSheet.Cells[iniRow, j+iniCol] = headerelement.ToString();
                    if (j >= day_of_week && headerelement != "EMPRESA" && headerelement != "TAMBOS" && headerelement != "   ")
                    {
                        xlWorkSheet.Cells[iniRow + 1, j+iniCol] = queryDate.ToString();
                        //loop over empresasa and query number of tambos per date
                        //
                        //
                        if (headerelement == "SÁBADO")
                        {
                            queryDate = queryDate.AddDays(2);
                        }
                        else
                        {
                            queryDate = queryDate.AddDays(1);
                        }
                    }
                    j++;
                }
                weekCount++;
            }

            //beautify report:
            xlRange = xlWorkSheet.get_Range("c5", "c8");
            xlRange.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlMedium, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlColorIndex.xlColorIndexAutomatic);
            //temporal save here
            SaveReport(@"C:\Reciclados\Reciclados.xls");
        }
        public void SaveReport(string path) 
        {
            xlWorkBook.SaveAs(path, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        private void setColumnsWidth(int width)
        {
            for (int i = 1; i < 100; i++) 
            {
                xlWorkSheet.Columns[i].ColumnWidth = width;
            }
        
        }
        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
