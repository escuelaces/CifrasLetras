using CommunityToolkit.Maui.Markup;

namespace CifrasLetras;


public partial class MainPage : ContentPage
{


	public MainPage()
	{
        Content = new VerticalStackLayout()
        {
            Children = {
                //numAdivinar
                new Label().Font(size:50).Center()
                    .Bind(Label.TextProperty, nameof(CifrasyLetras.NumAdivinar)),

                //Numeros
                new CollectionView() {
                    ItemTemplate = new DataTemplate(() =>
                         new Label().Font(size:50)
                            .Bind(Label.TextProperty).Paddings(50)
                    ),

                    ItemsLayout = LinearItemsLayout.Horizontal,
                    BackgroundColor = Colors.Grey,


                }.Size(600,60)
                 .Bind(CollectionView.ItemsSourceProperty, nameof(CifrasyLetras.Numeros)),


                new HorizontalStackLayout
                {
                    Children =
                    {
                        //Operaciones
                        new Entry().Size(400,50).Font(size:50).CenterHorizontal()
                            .Bind(Entry.TextProperty, nameof(CifrasyLetras.Operaciones), BindingMode.TwoWay)
                            .Bind(Entry.ReturnCommandProperty, nameof(CifrasyLetras.comprobarResultadoCommand)),
                    
                        //Resultado                
                        new Label().Size(200,50).Font(size:50).CenterHorizontal()
                            .Bind(Label.TextProperty, nameof(CifrasyLetras.Resultado),
                                  convert:(int resultado) => resultado.ToString(),
                                  convertBack:(string resultado)=> int.Parse(resultado)) 

                    }

                },


                //Puntuacion                
                new Entry().Font(size:50).Center()
                    .Margins(top:50,bottom:50)
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.Puntuacion)),
                
          

                //Cuenta Atras                
                new Entry().Font(size:50).Center()
                    .Bind(Entry.TextProperty, nameof(CifrasyLetras.CuentaAtras))
                    
                    
            }

        }.Center();
    }


}


