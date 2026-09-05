using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DDY.Models;
using DDY.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DDY.ViewModels
{
    [QueryProperty(nameof(Cartas), "Carta")]
    public partial class DetalleViewModel : ObservableObject
    {
        [ObservableProperty]
        private CartaPokemon cartas;

        [RelayCommand]
        async Task Editar()
        {
            if (Cartas is null)
                return;

            await Shell.Current.GoToAsync(nameof(CartaFormPage), true, new Dictionary<string, object>
            {
                { "Carta", Cartas }
            });
        }
    }
}
