using System.ComponentModel;
using System.IO;

namespace ThesisManagement.ViewModels
{
    public class PdfViewerVM : ViewModelBase
    {
        private Stream docStream;

        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(nameof(DocumentStream));
            }
        }

        public PdfViewerVM()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string appDirectory = currentDirectory;

            for (int i = 0; i < 3; i++)
            {
                appDirectory = Directory.GetParent(appDirectory).FullName;
            }
            string filePath = Path.Combine(appDirectory, "Data", "PDF_Succinctly.pdf");
            docStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
        }
    }
}
