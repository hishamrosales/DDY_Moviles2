using DDY.Models;
using DDY.ViewModels;

namespace DDY.Views;

public partial class ListaCartas : ContentPage
{
    public ListaCartas(ListaViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;
    }
}