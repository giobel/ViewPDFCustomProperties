using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace VIewMetadata.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand LoadFilesCommand { get; }
        public RelayCommand OpenFileCommand { get; }
        public string FilePath { get; set; }
        public string WindowTitle { get; private set; }
        public ObservableCollection<Model.PDFContainer> PDFList { get; set; }
        
        public Model.PDFContainer SelectedPDF { get; set; }

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                FilePath = "Enter file path";
                WindowTitle = "design mode title";
                
            }
            else
            {
                PDFList = new ObservableCollection<Model.PDFContainer>();
                WindowTitle = "PDF properties viewer";
                FilePath = "Enter file path";
                LoadFilesCommand = new RelayCommand(() => LoadFiles());
                OpenFileCommand = new RelayCommand(() => OpenFile());

            }
        }

        private void OpenFile()
        {
            Process.Start( SelectedPDF.FilePath);
        }

        private void LoadFiles()
        {
            PDFList.Clear();
            try
            {
                FileInfo[] pdfs = Model.PDFContainer.GetDirectoryContent(FilePath, "*.pdf");
                foreach (FileInfo file in pdfs)
                {
                    Model.PDFContainer currentPdf = Model.PDFContainer.CreatePDFContainer(file.FullName);
                    if (currentPdf.SheetName != null)
                    {
                        PDFList.Add(currentPdf);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }




            
        }
    }
}