using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VIewMetadata.Model
{
    public class PDFContainer
    {
        readonly static string arupMetadataKey = "ARUP_EMBEDDED_DATA";
        public string SheetName { get; set; }

        public string Revision { get; set; }

        public string Status { get; set; }

        public string IssueDate { get; set; }

        public string FilePath { get; set; }

        public string ColorSet { get; set; }

        public static FileInfo[] GetDirectoryContent(string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);

            return dinfo.GetFiles(FileType);

            
        }

        public static PDFContainer CreatePDFContainer(string pdfPath)
        {

            PdfDocument document = PdfReader.Open(pdfPath);

            PDFContainer container = new PDFContainer();

            container.FilePath = pdfPath;

            var properties = document.Info.Elements;


            foreach (var property in properties)
            {     
                    if (property.Key == "/Sheet Name")
                        container.SheetName = RemoveFirstLast(property.Value);

                    if (property.Key == "/Revision")
                        container.Revision = RemoveFirstLast(property.Value);

                    if (property.Key == "/Status")
                        container.Status = RemoveFirstLast(property.Value);

                    if (property.Key == "/Issue Date")
                        container.IssueDate = RemoveFirstLast(property.Value);
            }
            document.Close();

            if (container.Status == "Construction")
                container.ColorSet = "#F5DD93";
            else if (container.Status == "Certification")
                container.ColorSet = "#ffb3ba";
            else
                container.ColorSet = "#baffc9";
            return container;
        }




        private static string RemoveFirstLast(PdfSharp.Pdf.PdfItem input)
        {
            return input.ToString().Replace("(", "").Replace(")", "").Replace(@"\", ""); 
        }
    }
}

