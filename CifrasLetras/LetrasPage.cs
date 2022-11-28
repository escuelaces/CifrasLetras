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

                // Letras
                new CollectionView() {
                    ItemTemplate = new DataTemplate(() =>
                         new Label().Font(size:50)
                            .Bind(Label.TextProperty).Paddings(50)
                    ),

                    ItemsLayout = LinearItemsLayout.Horizontal,

                }
                .Size(800,60)
                .Bind(CollectionView.ItemsSourceProperty, nameof(CifrasyLetras.Letras)),


                //Palabra
                new Entry().Size(500,50).Font(size:50).CenterHorizontal()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.Palabra), BindingMode.TwoWay)
                    .Bind(Entry.ReturnCommandProperty, nameof(CifrasyLetras.palabraCommand)),

                //Cuenta Atras                
                new Entry().Font(size:20).Center()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.CuentaAtras))

            }
		}.Center();
	}
}
