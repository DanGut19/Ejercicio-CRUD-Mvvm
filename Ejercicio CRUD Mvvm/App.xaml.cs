using Ejercicio_CRUD_Mvvm.Views;

namespace Ejercicio_CRUD_Mvvm
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new ProductosMainPage();
        }
    }
}
