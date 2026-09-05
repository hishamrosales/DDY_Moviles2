using DDY.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace DDY.Views;

public partial class ListaCartas : ContentPage
{
    public ListaCartas()
    {
        InitializeComponent();
        BindingContext = MauiProgram.Services.GetService<ListaViewModel>();
    }
}