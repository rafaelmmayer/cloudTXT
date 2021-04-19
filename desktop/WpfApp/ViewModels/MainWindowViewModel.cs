using MvvmHelpers;
using MvvmHelpers.Commands;
using Ookii.Dialogs.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.Models;

namespace WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
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

        public MainWindowViewModel()
        {
            DownloadArquivoCommand = new Command(DownloadArquivo);
            DeleteArquivoCommand = new Command(DeleteArquivo);

            GetArquivoAsync();
        }

        async void GetArquivoAsync()
        {
            var client = new RestClient("https://projeto-pf.herokuapp.com/");
            var request = new RestRequest("api", DataFormat.Json);
            var response = await client.GetAsync<List<Arquivo>>(request);
            Arquivos = new ObservableCollection<Arquivo>(response);
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
            vsf.FileName = SelectedItem.Nome;
            if(vsf.ShowDialog() == true)
            {
                using(StreamWriter sw = new StreamWriter($"{vsf.FileName}.txt"))
                {
                    sw.Write(SelectedItem.Conteudo);
                }
            }

        }

        async void DeleteArquivo()
        {
            if (SelectedItem is null) return;

            var client = new RestClient("https://projeto-pf.herokuapp.com/");
            var request = new RestRequest($"api/{SelectedItem.Id}", DataFormat.Json);
            await client.DeleteAsync<Arquivo>(request);
            GetArquivoAsync();
        }
    }
}
