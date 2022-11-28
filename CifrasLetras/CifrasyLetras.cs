using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CifrasLetras
{
	public partial class CifrasyLetras
	{
        Random aleatorio = new Random();

        Turno turno = Turno.Cifras;
        int NJuegos = 0;

        [ObservableProperty]
        int cuentaAtras;

        [ObservableProperty]
        int puntuacion = 0;

        public CifrasyLetras()
        {
            //Timer
			timer = new(1000);



            ///Cuando acaba la cuenta atras iniciamos un nuevo juego
            timer.Elapsed += async (sender, args) =>
            {
                if (--CuentaAtras == 0)
                {
                    (sender as System.Timers.Timer).Stop();
                    await Application.Current.MainPage.Dispatcher.DispatchAsync(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Cifras y Letras", "Se acabo el tiempo", "OK");
                        NuevoJuego();

                    });
                }

            };


            NuevoJuego();

        }

        private async void NuevoJuego()
        {
            CuentaAtras = 60;

            if (turno == Turno.Cifras)
                if (NJuegos++ <= 4)
                    NuevoJuegoCifras();
                else
                {
                    if (await Application.Current.MainPage.DisplayAlert("Cifras y Letras", $"El Juego acabo tu puntuacion es: {Puntuacion}", "NUEVO JUEGO", "SALIR"))
                    {
                        NJuegos = 1;
                        NuevoJuegoCifras();
                    }
                    else
                    {
                        Application.Current.Quit();
                    }
                }
            else
                NuevoJuegoLetras();

            timer.Start();
        }
    }
}

