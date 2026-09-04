using DDY.ViewModels;

namespace DDY.Views;

public partial class CartaFormPage : ContentPage
{
    public CartaFormPage(CartaFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}