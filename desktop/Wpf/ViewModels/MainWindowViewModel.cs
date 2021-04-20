using ClassLibrary.Api;
using ClassLibrary.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Ookii.Dialogs.Wpf;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Wpf.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        ArquivoApi api = new ArquivoApi();

        private Arquivo _selectedItem;
        public Arquivo SelectedItem
        {
            get { return _selectedItem; }
            set => SetProperty(ref _selectedItem, value);
        }


        private ObservableCollection<Arquivo> _arquivos;
        public ObservableCollection<Arquivo> Arquivos
        {
            get { return _arquivos; }
            set => SetProperty(ref _arquivos, value);
        }

        public ICommand DownloadArquivoCommand { get; set; }
        public ICommand DeleteArquivoCommand { get; set; }
        public ICommand GetArquivosCommand { get; set; }

        public MainWindowViewModel()
        {
            DownloadArquivoCommand = new Command(DownloadArquivo);
            DeleteArquivoCommand = new Command(DeleteArquivo);
            GetArquivosCommand = new Command(GetArquivoAsync);

            GetArquivoAsync();
        }

        async void GetArquivoAsync()
        {
            var arquivos = await api.GetArquivos();
            Arquivos = new ObservableCollection<Arquivo>(arquivos);
        }

        void DownloadArquivo()
        {
            if (SelectedItem is null) return;

            VistaSaveFileDialog vsf = new VistaSaveFileDialog()
            {
                Title = "Salvar Arquivo...",
                CheckFileExists = true,
                Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            vsf.FileName = SelectedItem.nome;
            if (vsf.ShowDialog() == true)
            {
                using (StreamWriter sw = new StreamWriter($"{vsf.FileName}.txt"))
                {
                    sw.Write(SelectedItem.conteudo);
                }
            }

        }

        async void DeleteArquivo()
        {
            await api.DeleteArquivo(SelectedItem);
            GetArquivoAsync();
        }
    }
}
