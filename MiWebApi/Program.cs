using System.Text.Json;
using System.Text.Json.Serialization;
using espacioPokemon;

HttpClient cliente = new HttpClient();

string url = "https://pokeapi.co/api/v2/pokemon?limit=10";

HttpResponseMessage respuesta = await cliente.GetAsync(url);
respuesta.EnsureSuccessStatusCode();

string jsonCrudo = await respuesta.Content.ReadAsStringAsync();

PokemonRespuesta? datos = JsonSerializer.Deserialize<PokemonRespuesta>(jsonCrudo);

if (datos == null)
{
    Console.WriteLine("No se pudieron cargar los datos.");
    return;
}

Console.WriteLine("\nLISTA DE POKEMON:\n");

foreach (var pokemon in datos.results)
{
    Console.WriteLine("Nombre: " + pokemon.name);
    Console.WriteLine("URL: " + pokemon.url);
    Console.WriteLine("----------------------------");
}

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};

string jsonFinal = JsonSerializer.Serialize(datos, options);

File.WriteAllText("pokemon.json", jsonFinal);

Console.WriteLine("Datos guardados en pokemon.json");