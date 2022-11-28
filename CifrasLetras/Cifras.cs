using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis.CSharp.Scripting;


namespace CifrasLetras
{
    public partial class CifrasyLetras : ObservableObject
	{
        
        List<int> Cifras = new(){ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 25, 50, 75, 100 };

        [ObservableProperty]
        public int numAdivinar;


        [ObservableProperty]
        public ObservableCollection<int> numeros = new();


        //Operaciones
        [ObservableProperty]
        public string operaciones;

        partial void OnOperacionesChanged(string value)
        {
            Task.Run(async () =>
                Resultado = await CSharpScript.EvaluateAsync<int>(value)
            );
        }

 


        //Resultado
        [ObservableProperty]
        public int resultado;

        [ObservableProperty]
        public Command resultadoCommand;

        

        System.Timers.Timer timer;

		




        private async void NuevoJuegoCifras()
        {
            turno = Turno.Letras;

            Operaciones = String.Empty;
            Resultado = 0;

            // Generar Numeros
            NumAdivinar = aleatorio.Next(100, 1000);

            //Añadir 6 cifras a la lista de numeros
            Numeros.Clear();
            for (int i = 0; i < 6; i++)
            {
                Numeros.Add(Cifras[aleatorio.Next(Cifras.Count)]);
            }

            //Cargar la interfaz de cifras
            await ((Shell)Application.Current.MainPage).GoToAsync("//Cifras");

            
        }

        public IAsyncRelayCommand comprobarResultadoCommand => new AsyncRelayCommand(new Func<Task>(comprobarResultado));


        /// <summary>
        /// Cuando pulsan Intro en el cuadro de operaciones se comprueba que el resultado se aproxime
        /// lo suficiente a NumAdivinar
        /// </summary>
        /// <returns></returns>
        private async Task comprobarResultado()
        {
            try
            {
                //Parar cuenta atras
                timer.Stop();

                if ( NumAdivinar == Resultado){
                    Puntuacion += 9;
                    await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "Enhorabuena el resultado es exacto", "OK");
                }
                else if(Math.Abs(NumAdivinar - Resultado) < 100){
                    Puntuacion += 6;
                    await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "El resultado es correcto ", "OK");
                }
                else {
                    await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "No te has acercado lo suficiente", "OK");
                }

                
                NuevoJuego();
            }
            catch (Microsoft.CodeAnalysis.Scripting.CompilationErrorException)
            {
                await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "La Operacion Introducida es invalida", "OK");
            }

            

        }



    }
}

