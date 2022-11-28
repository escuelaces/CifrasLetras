using System;
using System.Net.Http.Headers;
using System.Text.Json;

namespace RAE;

public class Diccionario
{
    HttpClient httpClient = new();

    public Diccionario()
    {
        httpClient.BaseAddress = new Uri("https://dle.rae.es/data/");
        httpClient.DefaultRequestHeaders.Authorization = new("Basic", "cDY4MkpnaFMzOmFHZlVkQ2lFNDM0");
        httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json")
        );
    }

    /// <summary>
    /// Busca una palabra en el diccionario
    /// </summary>
    /// <param name="Palabra"></param>
    /// <returns>true si la palabra esta en el diccionario,
    /// false en caso contrario</returns>
    public async Task<bool> buscarPalabraAsync(string Palabra)
    {
        var response = await httpClient.GetStringAsync($"search?w={Palabra}");

        var Respuesta = JsonSerializer.Deserialize<Respuesta>(response);
        return Respuesta.res.Find((palabra) => {
            var word =  new String(palabra.header.TakeWhile((caracter) => caracter >= 'a' & caracter <= 'z').ToArray());
            return word.Equals(Palabra.ToLower());
        }) != null;
        

    }
}

public class Palabra
{
	public String header { get; set; }
	public String id { get; set; }
	public int grp { get; set; }
}


public class Respuesta
{
	public int approx { get; set; }
    public List<Palabra> res { get; set; }
}


