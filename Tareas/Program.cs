using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using espacioTarea;

//instancio el cliente
HttpClient cliente = new HttpClient();

HttpResponseMessage respuesta = await cliente.GetAsync("https://jsonplaceholder.typicode.com/todos/"); //hago solicitud de get
respuesta.EnsureSuccessStatusCode(); //compruebo si se logro la solicitud

string jsonCrudo = await respuesta.Content.ReadAsStringAsync(); 
List<Tarea> listaTareas = JsonSerializer.Deserialize<List<Tarea>>(jsonCrudo); //guardo el json y lo deserializo

Console.WriteLine("\n\nTAREAS PENDIENTES: ");
foreach (var tarea in listaTareas)
{   
    if (!tarea.completed) //filtrando
    {   
        Console.WriteLine("Id: " + tarea.id + " Titulo: " + tarea.title + " ---->  Pendiente");
    }
}

Console.WriteLine("\n\nTAREAS REALIZADAS: ");
foreach (var tarea in listaTareas)
{   
    if (tarea.completed)
    {   
        Console.WriteLine("Id: " + tarea.id + " Titulo: " + tarea.title + " ---->  Realizada");
    }
}

var options = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
};//todo esto es para hacer que el archivo JSON se guarde de forma ordenada y no todo en una sola linea

string jsonFinal = JsonSerializer.Serialize(listaTareas,options); //serializo el json
string rutaArchivo = "tareas.json";

File.WriteAllText(rutaArchivo,jsonFinal); //lo guardo en un archivo json
Console.WriteLine($"\n\nArchivo JSON guardado en {Path.GetFullPath(rutaArchivo)}");

