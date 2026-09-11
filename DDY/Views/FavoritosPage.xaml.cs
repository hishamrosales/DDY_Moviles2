using DDY.ViewModels;

namespace DDY.Views;

public partial class FavoritosPage : ContentPage
{
    private readonly FavoritosViewModel _vm;

    public FavoritosPage(FavoritosViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _vm.CargarFavoritosCommand.Execute(null);
    }
}