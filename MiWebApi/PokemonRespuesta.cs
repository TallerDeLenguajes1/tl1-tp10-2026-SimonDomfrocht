using espacioPokemon;
public class PokemonRespuesta
{
    public int count { get; set; }
    public string? next { get; set; }
    public string? previous { get; set; }
    public List<Pokemon> results { get; set; } = new List<Pokemon>();
}