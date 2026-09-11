using DDY.ViewModels;

namespace DDY.Views;

public partial class CartaFormPage : ContentPage
{
    public CartaFormPage(CartaFormViewModels vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}