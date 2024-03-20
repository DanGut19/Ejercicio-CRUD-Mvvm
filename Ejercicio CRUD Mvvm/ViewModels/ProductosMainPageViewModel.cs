using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ejercicio_CRUD_Mvvm.Models;
using Ejercicio_CRUD_Mvvm.Services;
using Ejercicio_CRUD_Mvvm.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_CRUD_Mvvm.ViewModels
{
    public partial class ProductosMainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Productos> productosCollection = new ObservableCollection<Productos>();

        private readonly ProductoService _productoService;

        public ProductosMainPageViewModel()
        {
            _productoService = new ProductoService();
        }

        public void GetAll()
        {
            var getAll = _productoService.GetAll();

            if (getAll.Count > 0)
            {
                ProductosCollection.Clear();
                foreach (var producto in getAll)
                {
                    ProductosCollection.Add(producto);
                }
            }
        }

        [RelayCommand]
        private async Task GoToProductosFormPage()
        {
            await App.Current!.MainPage!.Navigation.PushAsync(new ProductosFormPage());
        }

        [RelayCommand]
        private async Task SelectProductos(Productos productos)
        {
            try
            {

                string res = await App.Current!.MainPage!.DisplayActionSheet("Operación", "Cancelar", null, "Actualizar", "Eliminar");

                switch (res) 
                {
                    case "Actualizar":
                        await App.Current.MainPage.Navigation.PushAsync(new ProductosFormPage(productos));
                        break;
                    case "Eliminar":
                        bool respuesta = await App.Current!.MainPage!.DisplayAlert("Eliminar Cliente", "¿Desea eliminar el cliente?", "Si", "No");

                        if (respuesta)
                        {
                            int del = _productoService.Delete(productos);
                            if (del > 0)
                            {
                                ProductosCollection.Remove(productos);
                            }
                        }
                        break;
                }

            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        private void Error(String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert("ERROR", Mensaje, "Aceptar"));
        }

    }
}
