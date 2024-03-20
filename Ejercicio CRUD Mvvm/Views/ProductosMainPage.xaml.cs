using Ejercicio_CRUD_Mvvm.ViewModels;

namespace Ejercicio_CRUD_Mvvm.Views;

public partial class ProductosMainPage : ContentPage
{
	private ProductosMainPageViewModel viewModel;
	public ProductosMainPage()
	{
		InitializeComponent();
		viewModel = new ProductosMainPageViewModel();
		this.BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		viewModel.GetAll();
    }
}