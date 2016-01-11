using ReadWriteJSONFiles.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Factura> listaFacturas = new List<Factura>();
            List<Factura> listaClientes = new List<Factura>();
            List<Factura> listaProductos = new List<Factura>();

            int opcion = 0;
            do
            {
                do
                {
                    Console.WriteLine("Seleccione la Opcion");
                    Console.WriteLine("1.- Emitir Factura");
                    Console.WriteLine("2.- Agregar Cliente");
                    Console.WriteLine("3.- Editar Cliente");
                    Console.WriteLine("4.- Agregar Producto");
                    Console.WriteLine("5.- Editar Producto");
                    Console.WriteLine("6.- Salir");

                    
                    opcion = int.Parse(Console.ReadLine());

                    Console.Clear();

                } while ((opcion > 6) || (opcion < 1));

                switch (opcion)
                {
                    case 1:
                        Console.WriteLine("Emitir Factura");
                        Console.ReadKey();

                        break;
                    case 2:
                        Console.WriteLine("Agregar Cliente");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("Editar Cliente");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Agregar Producto");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.WriteLine("Editar Producto");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (opcion != 6);
         
        }
        static int ObtenerClientesDeArchivo()
        {

        }
    }
}
