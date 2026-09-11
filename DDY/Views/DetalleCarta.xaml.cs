using DDY.ViewModels;
using Microsoft.Maui.Controls;
namespace DDY.Views;

public partial class DetalleCarta : ContentPage
{
    public DetalleCarta(DetalleViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}