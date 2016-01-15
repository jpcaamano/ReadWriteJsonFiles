using ReadWriteJSONFiles.Controladores;
using ReadWriteJSONFiles.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteJSONFiles
{
    /// <summary>
    /// Este proyecto simula la generación de facturas, el ingreso de clientes y de productos, el almacenamiento de informacion se basa en archivos JSON
    /// los cuales son leidos al inicio de la aplicación. y conforme se hacen cambios en la listas que contienen los datos, se van escribiendo.
    /// 
    /// Entre los principales temas que se tocan en este proyecto están:
    /// Implementacion de clases, Implementacion de clases estáticas, Lectura de archivos, Escritura de archivos, Estructuras JSON
    /// 
    /// Indicaciones: 
    /// En la carpeta -Controladores- en cada una de las clases en los métodos "Escribir Datos" o "Leer datos" estan las rutas de los archivos, para mi caso
    /// he usado "C:\Test\..." ussted puede modificarlo, para adaptarlo a sus necesidades, solo asegurese que la ruta exista, caso contrario no guardará
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region Variables
            string respuesta;
            Cliente clienteFactura = new Cliente();
            string clienteID = string.Empty;
            string respuestaFactura = string.Empty;
            string respuestaDetalle = string.Empty;
            Factura factura = new Factura();
            string productoID = string.Empty;
            Producto productoDetalle = new Producto();
            Detalle detalle = new Detalle();
            double subtotal = 0;
            double descuento = 0;
            double impuesto = 0;
            double total = 0;
            Cliente clienteActualizar = new Cliente();
            Producto productoActualizar = new Producto();
            Cliente cliente = new Cliente();
            Producto producto = new Producto();
            #endregion

            //Inicio de listas
            List<Factura> listaFacturas = new List<Factura>();
            List<Cliente> listaClientes = new List<Cliente>();
            List<Producto> listaProductos = new List<Producto>();
             //llenar las listas
            listaClientes = ClienteController.JSONListadoDeClientes();
            listaProductos = ProductoController.JSONListadoDeProductos();
            listaFacturas = FacturaController.JSONListadoDeFacturas();

            int opcion = 0;
            do
            {
                do
                {
                    Console.Clear();

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
                        #region Proceso de Emision de facturas
                        Console.WriteLine("EMITIR FACTURA");

                        //Busqueda de cliente
                        do
                        {
                            Console.WriteLine("Ingrese la cedula del cliente");
                            clienteID = Console.ReadLine();
                            clienteFactura = ClienteController.BuscarClientePorID(listaClientes, clienteID);
                            if (clienteFactura == null)
                            {
                                Console.WriteLine("No existe cliente con esa cédula");
                                //Si el cliente no existe permite ir al menu principal
                                Console.WriteLine("Desea salir para ingresar al cliente S/N:");
                                respuesta = Console.ReadLine();
                                if (respuesta == "S")
                                {
                                    break;
                                }
                            }
                            //Se asegura que haya un cliente hasta continuar
                        } while (clienteFactura == null);
                        //Fin bsuqueda de Cliente

                        Console.WriteLine("\n Nombres: {0} {1}", clienteFactura.Nombres, clienteFactura.Apellidos);
                        Console.WriteLine("Teléfono: {0} Dirección: {1}", clienteFactura.Telefono, clienteFactura.Direccion);

                        //Agrega datos de la cabecera de factura
                        factura.ID = listaFacturas.Count + 1;
                        factura.Fecha = DateTime.Now;
                        factura.Cliente = clienteFactura;
                        factura.ListaDetalles = new List<Detalle>();

                        //Inicia el ingreso del detalle
                        do
                        {
                            //Generacion de detalle
                            do
                            {
                                productoDetalle = new Producto();

                                //Busqueda de producto, se repite ahsta que se encuentre uno
                                do
                                {
                                    Console.WriteLine("Ingrese el producto a facturar");
                                    productoID = Console.ReadLine();

                                    productoDetalle = ProductoController.BuscarProductoPorID(listaProductos, productoID);

                                    if (productoDetalle == null)
                                    {
                                        Console.WriteLine("El producto no existe, reingrese el ID");
                                    }
                                    //Asegura que ingrese un producto existente
                                } while (productoDetalle == null);
                                //Fin Busqueda de Producto

                                //Ingreso de infromacion relacionada al detalle
                                detalle = new Detalle();
                                detalle.ID = factura.ListaDetalles.Count + 1;
                                detalle.ID = int.Parse(factura.ID.ToString() + detalle.ID.ToString());
                                detalle.Producto = productoDetalle;
                                Console.WriteLine(detalle.Producto.Nombre);
                                Console.WriteLine("Ingrese la cantidad del producto");
                                detalle.Cantidad = double.Parse(Console.ReadLine());
                                Console.WriteLine("Ingrese el descuento");
                                detalle.Descuento = double.Parse(Console.ReadLine());

                                //calculo de valores del detalle
                                subtotal = detalle.Cantidad * detalle.Producto.Precio;
                                descuento = (subtotal) * (detalle.Descuento / 100);
                                impuesto = (subtotal - descuento) * (detalle.Producto.Impuesto / 100);
                                total = subtotal - descuento + impuesto;
                                Console.WriteLine("Código | Nombre | Precio | Cantidad | Subtotal | Descuento | Impuesto | Total }");
                                Console.WriteLine("{0}    | {1} | {2} | {3} | {4} | {5} | {6} | {7}", detalle.Producto.ID.ToString(), detalle.Producto.Nombre.ToString(), detalle.Producto.Precio.ToString(), detalle.Cantidad.ToString(), subtotal.ToString(), descuento.ToString(), impuesto.ToString(), total.ToString());
                                //Validacion de que el ingreso de la respuesta este correcto
                                do
                                {
                                    Console.WriteLine("Esta correcto lo ingresado: S/N");
                                    respuestaDetalle = Console.ReadLine();
                                } while ((respuestaDetalle != "N") && (respuestaDetalle != "S"));
                                //Fin validacion

                            } while (respuestaDetalle == "N");
                            //Fin generacion Detaller
                            //Agrega el detaller
                            factura.ListaDetalles.Add(detalle);
                            //Validacion de que el ingreso de la respuesta este correcto
                            do
                            {
                                Console.WriteLine("Desea ingresar otro Item: S/N");
                                respuestaFactura = Console.ReadLine();
                            } while ((respuestaFactura != "N") && (respuestaFactura != "S"));
                            //Fin validacion
                        }
                        while (respuestaFactura == "S");
                        //Fin de ingreso de detalle
                        //agrega la factura actual a la lista de facturs
                        listaFacturas.Add(factura);
                        //Detalle de la Facturas
                        Console.WriteLine("\nCABECERA DE FACTURA");
                        Console.WriteLine("Nombres: {0} {1}", factura.Cliente.Nombres, factura.Cliente.Apellidos);
                        Console.WriteLine("Teléfono: {0} Dirección: {1}", factura.Cliente.Telefono, factura.Cliente.Direccion);

                        Console.WriteLine("\nDETALLE DE FACTURA");
                        Console.WriteLine("Código | Nombre | Precio | Cantidad | Subtotal | Descuento | Impuesto | Total }");
                        foreach (Detalle detalleItem in factura.ListaDetalles)
                        {
                            subtotal = detalleItem.Cantidad * detalleItem.Producto.Precio;
                            descuento = (subtotal) * (detalleItem.Descuento / 100);
                            impuesto = (subtotal - descuento) * (detalleItem.Producto.Impuesto / 100);
                            total = subtotal - descuento + impuesto;

                            Console.WriteLine("{0} | {1} | {2} | {3} | {4} | {5} | {6} | {7}", detalleItem.Producto.ID.ToString(), detalleItem.Producto.Nombre.ToString(), detalleItem.Producto.Precio.ToString(), detalleItem.Cantidad.ToString(), subtotal.ToString(), descuento.ToString(), impuesto.ToString(), total.ToString());


                        }
                        //envia a escribir las facturas
                        FacturaController.ListadoDeFacturasJSON(listaFacturas);
                        Console.WriteLine("Presiones cualquier tecla para continuar");
                        Console.ReadKey();
                        #endregion

                        break;
                    case 2:
                        #region Proceso para agregar Clientes
                        Console.WriteLine("Agregar Cliente");
                        Console.WriteLine("Cédula: ");
                        cliente.ID = Console.ReadLine();
                        Console.WriteLine("Apellidos: ");
                        cliente.Apellidos = Console.ReadLine();
                        Console.WriteLine("Nombres: ");
                        cliente.Nombres = Console.ReadLine();
                        Console.WriteLine("Dirección: ");
                        cliente.Direccion = Console.ReadLine();
                        Console.WriteLine("Teléfono: ");
                        cliente.Telefono = Console.ReadLine();

                        Console.WriteLine("Los datos ingresados están correctos?? S/N");
                        respuesta = Console.ReadLine();
                        if (respuesta == "S")
                        {
                            if (listaClientes == null)
                            {
                                listaClientes = new List<Cliente>();
                            }
                            listaClientes.Add(cliente);
                            ClienteController.ListadoDeClientesJSON(listaClientes);
                        }

                        Console.WriteLine("Presiones cualquier tecla para continuar");
                        Console.ReadKey();
                        #endregion

                        break;
                    case 3:
                        #region Proceso para editar Clientes
                        Console.WriteLine("Editar Cliente");
                        //Busqueda de cliente
                        do
                        {
                            Console.WriteLine("Ingrese la cedula del cliente");
                            clienteID = Console.ReadLine();
                            cliente = ClienteController.BuscarClientePorID(listaClientes, clienteID);
                            if (cliente == null)
                            {
                                Console.WriteLine("No existe cliente con esa cédula");
                                //Si el cliente no existe permite ir al menu principal
                                Console.WriteLine("Desea salir para ingresar al cliente S/N:");
                                respuesta = Console.ReadLine();
                                if (respuesta == "S")
                                {
                                    break;
                                }
                            }
                            //Se asegura que haya un cliente hasta continuar
                        } while (cliente == null);
                        //Fin bsuqueda de Cliente

                        Console.WriteLine("\n Nombres: {0} {1}", cliente.Nombres, cliente.Apellidos);
                        Console.WriteLine("Teléfono: {0} Dirección: {1}", cliente.Telefono, cliente.Direccion);

                        do
                        {
                            Console.WriteLine("Si desea actualizar el cliente presione -A- si desea eliminarlo presiones -E-");
                            respuesta = Console.ReadLine();
                        } while ((respuesta != "A") && (respuesta != "E"));
                        //Si desea actualizar
                        if (respuesta == "A")
                        {

                            Console.WriteLine("Datos para actualizar");
                            Console.WriteLine("Cédula: ");
                            clienteActualizar.ID = Console.ReadLine();
                            Console.WriteLine("Apellidos: ");
                            clienteActualizar.Apellidos = Console.ReadLine();
                            Console.WriteLine("Nombres: ");
                            clienteActualizar.Nombres = Console.ReadLine();
                            Console.WriteLine("Dirección: ");
                            clienteActualizar.Direccion = Console.ReadLine();
                            Console.WriteLine("Teléfono: ");
                            clienteActualizar.Telefono = Console.ReadLine();

                            Console.WriteLine("Los datos ingresados están correctos?? S/N");
                            respuesta = Console.ReadLine();
                            if (respuesta == "S")
                            {

                                var clienteARemover = listaClientes.SingleOrDefault(c => c.ID == cliente.ID);
                                if (clienteARemover != null)
                                    listaClientes.Remove(clienteARemover);

                                listaClientes.Add(clienteActualizar);
                                ClienteController.ListadoDeClientesJSON(listaClientes);
                                Console.WriteLine("Dato Actualizado con éxito");
                            }
                            else
                            {
                                Console.WriteLine("No se hicieron cambios en los datos");
                            }


                        }
                        //Si desea eliminar
                        if (respuesta == "E")
                        {
                            Console.WriteLine("Desea eliminar al cliente?? S/N");
                            respuesta = Console.ReadLine();
                            if (respuesta == "S")
                            {

                                var clienteARemover = listaClientes.SingleOrDefault(c => c.ID == cliente.ID);
                                if (clienteARemover != null)
                                    listaClientes.Remove(clienteARemover);

                                ClienteController.ListadoDeClientesJSON(listaClientes);
                                Console.WriteLine("Dato eliminado con éxito");
                            }
                            else
                            {
                                Console.WriteLine("No se hicieron cambios en los datos");
                            }
                        }

                        Console.WriteLine("Presione cualquier tecla para continuar");
                        Console.ReadKey();
                        #endregion

                        break;

                    case 4:
                        #region Proceso para agregar producto
                        Console.WriteLine("Agregar Producto");
                        Console.WriteLine("ID: ");
                        producto.ID = Console.ReadLine();
                        Console.WriteLine("Nombre: ");
                        producto.Nombre = Console.ReadLine();
                        Console.WriteLine("Precio unitario: ");
                        producto.Precio = double.Parse(Console.ReadLine());
                        Console.WriteLine("Impuesto grabado: ");
                        producto.Impuesto = double.Parse(Console.ReadLine());

                        Console.WriteLine("Los datos ingresados están correctos?? S/N");
                        respuesta = Console.ReadLine();
                        if (respuesta == "S")
                        {
                            if (listaProductos == null)
                            {
                                listaProductos = new List<Producto>();
                            }
                            listaProductos.Add(producto);
                            ProductoController.ListadoDeProductosJSON(listaProductos);
                        }
                        Console.WriteLine("Presione cualquier tecla para continuar");
                        Console.ReadKey();
                        #endregion

                        break;

                    case 5:
                        #region Proceso para editar producto
                        Console.WriteLine("Editar Producto");
                        //Busqueda de producto, se repite ahsta que se encuentre uno
                        do
                        {
                            Console.WriteLine("Ingrese el producto a editar");
                            productoID = Console.ReadLine();

                            producto = ProductoController.BuscarProductoPorID(listaProductos, productoID);

                            if (producto == null)
                            {
                                Console.WriteLine("El producto no existe, reingrese el ID");
                            }
                            //Asegura que ingrese un producto existente
                        } while (producto == null);
                        //Fin Busqueda de Producto

                        Console.WriteLine("ID: {0}  Nombre: {1}  Precio: {2}  Impuesto: {3}",producto.ID,producto.Nombre,producto.Precio,producto.Impuesto);

                        do
                        {
                            Console.WriteLine("Si desea actualizar el producto presione -A- si desea eliminarlo presione -E-");
                            respuesta = Console.ReadLine();
                        } while ((respuesta != "A") && (respuesta != "E"));
                        //Si desea actualizar
                        if (respuesta == "A")
                        {

                            Console.WriteLine("Datos para actualizar");
                            Console.WriteLine("ID: ");
                            productoActualizar.ID = Console.ReadLine();
                            Console.WriteLine("Nombre: ");
                            productoActualizar.Nombre = Console.ReadLine();
                            Console.WriteLine("Precio unitario: ");
                            productoActualizar.Precio = double.Parse(Console.ReadLine());
                            Console.WriteLine("Impuesto grabado: ");
                            productoActualizar.Impuesto = double.Parse(Console.ReadLine());

                            Console.WriteLine("Los datos ingresados están correctos?? S/N");
                            respuesta = Console.ReadLine();
                            if (respuesta == "S")
                            {

                                var productoARemover = listaProductos.SingleOrDefault(p => p.ID == producto.ID);
                                if (productoARemover != null)
                                    listaProductos.Remove(productoARemover);

                                listaProductos.Add(productoActualizar);
                                ProductoController.ListadoDeProductosJSON(listaProductos);
                                Console.WriteLine("Datos Actualizados con éxito");
                            }
                            else
                            {
                                Console.WriteLine("No se hicieron cambios en los datos");
                            }


                        }
                        //Si desea eliminar
                        if (respuesta == "E")
                        {
                            Console.WriteLine("Desea eliminar al producto?? S/N");
                            respuesta = Console.ReadLine();
                            if (respuesta == "S")
                            {

                                var productoARemover = listaProductos.SingleOrDefault(p => p.ID == producto.ID);
                                if (productoARemover != null)
                                    listaProductos.Remove(productoARemover);

                                ProductoController.ListadoDeProductosJSON(listaProductos);
                                Console.WriteLine("Datos Actualizados con éxito");
                            }
                            else
                            {
                                Console.WriteLine("No se hicieron cambios en los datos");
                            }
                        }


                        Console.WriteLine("Presione cualquier tecla para continuar");
                        Console.ReadKey();
                        #endregion

                        break;

                    case 6:
                        Console.WriteLine("Presione cualquier tecla para continuar");
                        Console.ReadKey();
                        break;
                }
                Console.Clear();
            } while (opcion != 6);
         
        }
    }
}
