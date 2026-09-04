using System;
using System.Collections.Generic;
using System.Text;
using DDY.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace DDY.ViewModels
{
    [QueryProperty(nameof(Cartas), "Carta")]
    public partial class DetalleViewModel : ObservableObject
    {
        [ObservableProperty]
        private CartaPokemon cartas;
    }
}
