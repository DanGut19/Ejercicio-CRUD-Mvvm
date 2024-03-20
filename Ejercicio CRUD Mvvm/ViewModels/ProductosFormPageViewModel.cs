using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ejercicio_CRUD_Mvvm.Models;
using Ejercicio_CRUD_Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio_CRUD_Mvvm.ViewModels
{
    public partial class ProductosFormPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private int id;

        [ObservableProperty]
        private int nombre;

        [ObservableProperty]
        private int categoria;

        [ObservableProperty]
        private int descripcion;

        [ObservableProperty]
        private int fechaFabricacion;

        [ObservableProperty]
        private int fechaVencimiento;

        private readonly ProductoService _productoService;

        public ProductosFormPageViewModel()
        {
            _productoService = new ProductoService();
        }

        public ProductosFormPageViewModel(Productos productos)
        {
            Id = productos.Id;
            Nombre = productos.nombre;
            Categoria = productos.categoria;
            Descripcion = productos.descripcion;
            FechaFabricacion = productos.fechaFabricacion;
            FechaVencimiento = productos.fechaVencimiento;
        }

        [RelayCommand]
        private async Task CreateUpdate()
        {
            try
            {
                Productos productos = new Productos
                {
                    Id = Id,
                    Nombre = Nombre,
                    Categoria = Categoria,
                    Descripcion = Descripcion,
                    FechaFabricacion = FechaFabricacion,
                    FechaVencimiento = FechaVencimiento
                };

                if (Validar(productos))
                {
                    if (Id == 0)
                    {
                        _productoService.Insert(productos);
                    }
                    else
                    {
                        _productoService.Update(productos);
                    }
                    await App.Current!.MainPage!.Navigation.PopAsync();
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private bool Validar(Productos productos)
        {
            try
            {
                if (productos.Nombre == null || productos.Nombre == "")
                {
                    Warning("Escriba el nombre.");
                    return false;
                }
                else if (productos.Categoria == null || productos.Categoria == "")
                {
                    Warning("Escriba la categoria.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
                return false;
            }
        }

        private void Error(String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert("ERROR", Mensaje, "Aceptar"));
        }

        private void Warning(String Mensaje)
        {
            MainThread.BeginInvokeOnMainThread(async () => await App.Current!.MainPage!.DisplayAlert("ADVERTENCIA", Mensaje, "Aceptar"));
        }
    }
}
