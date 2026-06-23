using espacioUsuarios;
using System.Text.Json;

HttpClient cliente = new HttpClient();

HttpResponseMessage respuesta = await cliente.GetAsync("https://jsonplaceholder.typicode.com/users"); 
respuesta.EnsureSuccessStatusCode();

string jsonCrudo = await respuesta.Content.ReadAsStringAsync(); 
List<Root>? listaUsuarios = JsonSerializer.Deserialize<List<Root>>(jsonCrudo); //guardo el json y lo deserializo

if (listaUsuarios == null)
{
    Console.WriteLine("No se pudieron cargar los usuarios.");
    return;
}


Console.WriteLine("\nPRIMEROS 5 USUARIOS: \n");
int contador = 0;
foreach (var usuario in listaUsuarios)
{   
    
    Console.WriteLine(" Id: " + usuario.id + " | Nombre: " + usuario.name + " | Correo: " + usuario.email + " | Domicilio: " + usuario.address.street);
    contador++;
    if (contador == 5)
    {
        break;
    }
}
