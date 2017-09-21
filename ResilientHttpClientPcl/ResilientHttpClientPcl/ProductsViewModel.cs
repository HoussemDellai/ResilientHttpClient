using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ResilientHttpClientPcl
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        private DataService _dataService;
        private List<string> _products;

        public List<string> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ICommand GetProductsCommand
        {
            get
            {
                return new Command(async () =>
                {
                    Products = await _dataService.GetProductsAsync();
                });
            }
        }

        public ProductsViewModel(IHttpClient httpClient)
        {
            _dataService = new DataService(httpClient);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
