using DDY.ViewModels;

namespace DDY.Views;

public partial class DetalleCarta : ContentPage
{
	public DetalleCarta(DetalleViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}