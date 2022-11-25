using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.CodeAnalysis.CSharp.Scripting;


namespace CifrasLetras
{
    public partial class CifrasyLetras : ObservableObject
	{
        Random aleatorio = new Random();
        List<int> Cifras = new(){ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 25, 50, 75, 100 };

        [ObservableProperty]
        public int numAdivinar;


        [ObservableProperty]
        public ObservableCollection<int> numeros = new();

        [ObservableProperty]
        public string operaciones;

        [ObservableProperty]
        public string resultado;

        [ObservableProperty]
        public Command resultadoCommand;

        [ObservableProperty]
        int cuentaAtras;


        System.Timers.Timer timer;

		public CifrasyLetras()
        {
            ///Timer
			timer = new(1000);
            timer.Elapsed += async (sender, args) =>
            {
                if (--CuentaAtras == 0)
                {
                    (sender as System.Timers.Timer).Stop();
                    await Application.Current.MainPage.Dispatcher.DispatchAsync(async () => {
                        await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "Se acabo el tiempo", "OK");
                        NuevoJuegoLetras();
                    });
                }

            };
            

             NuevoJuegoCifras();

        }

        async void NuevoJuegoLetras()
        {
            await ((Shell)Application.Current.MainPage).GoToAsync("//Letras");
        }


        private void NuevoJuegoCifras()
        {
            CuentaAtras = 60;

            // Generar Numeros
            NumAdivinar = aleatorio.Next(100, 1000);

            //Añadir 6 cifras a la lista de numeros
            Numeros.Clear();
            for (int i = 0; i < 6; i++)
            {
                Numeros.Add(Cifras[aleatorio.Next(Cifras.Count)]);
            }

            timer.Start();
        }

        public IAsyncRelayCommand comprobarResultadoCommand => new AsyncRelayCommand(new Func<Task>(comprobarResultado));
        private async Task comprobarResultado()
        {
            try
            {
                var resultOps = await CSharpScript.EvaluateAsync<int>(operaciones);

                if (int.Parse(resultado) == resultOps)
                {
                    await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "Enhorabuena el resultado es correcto", "OK");
                    await ((Shell)Application.Current.MainPage).GoToAsync("//Letras");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Cifras y Letras", $"El resultado:{resultado} es incorrento, el correcto es :{resultOps}", "OK");
                    await ((Shell)Application.Current.MainPage).GoToAsync("//Letras");
                }

            }
            catch (Microsoft.CodeAnalysis.Scripting.CompilationErrorException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "La Operacion Introducida es invalida", "OK");
                await ((Shell)Application.Current.MainPage).GoToAsync("//Letras");
            }
            catch (FormatException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "La resultado introducido es invalido", "OK");
                await ((Shell)Application.Current.MainPage).GoToAsync("//Letras");
            }
            catch (ArgumentNullException ex)
            {
                await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "Introduzca resultado ", "OK");

            }

        }



    }
}

