using CommunityToolkit.Maui.Markup;

namespace CifrasLetras;


public partial class MainPage : ContentPage
{


	public MainPage()
	{
		BindingContext = new CifrasyLetras();
        Content = new VerticalStackLayout()
        {
            Children = {
                //numAdivinar
                new Label().Font(size:20).Size(65,50).CenterHorizontal()
                    .Bind(Label.TextProperty, nameof(CifrasyLetras.NumAdivinar)),

                //Numeros
                new CollectionView() {
                    ItemTemplate = new DataTemplate(() => 
                         new Label().Font(size:20)
                            .Bind(Label.TextProperty).Margin(10,0)
                    ),

                    ItemsLayout = LinearItemsLayout.Horizontal,

                }.Size(250,50)
                 .Bind(CollectionView.ItemsSourceProperty, nameof(CifrasyLetras.Numeros)),


                //Operaciones
                new Entry().Font(size:20).Size(250, 50).CenterHorizontal()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.Operaciones), BindingMode.OneWayToSource)
                    .Bind(Entry.ReturnCommandProperty, nameof(CifrasyLetras.comprobarResultadoCommand)),
                    


                //Resultado                
                new Entry().Font(size:20).Size(75, 50).CenterHorizontal()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.Resultado), BindingMode.OneWayToSource)
                    .Bind(Entry.ReturnCommandProperty, nameof(CifrasyLetras.comprobarResultadoCommand)),


                //Cueenta Atras                
                new Entry().Font(size:20).Size(50).CenterHorizontal()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.CuentaAtras))
                    
                    

            }

        }.Center();
    }


}


