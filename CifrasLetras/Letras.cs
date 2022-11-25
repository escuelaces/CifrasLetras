using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm;


namespace CifrasLetras;

public class Letras : ContentPage
{
	public Letras()
	{
		Content = new VerticalStackLayout
		{
			Children = {
				new Label{ Text = "Letras"}.Center()
				
			}
		};
	}
}
