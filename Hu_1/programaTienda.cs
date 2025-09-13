public class ProgramaTienda
{
    public static void menuTienda()
    {
        // Productos iniciales
        List<string> productos = new List<string> { "Alfajores", "Galletas", "Revolcones", "Barrilete" };
        List<double> precios = new List<double> { 2500, 400, 300, 400 };
        List<int> stock = new List<int> { 10, 10, 10, 10 };

        //Historial de compras
        List<string> historialProductos = new List<string>();
        List<int> historialCantidades = new List<int>();
        List<double> historialSubtotales = new List<double>();

        while (true)
        {
            //Mostrar productos disponibles
            Console.WriteLine("\n--- Bienvenido a la Tienda de Ana ---");
            Console.WriteLine("Productos disponibles:");
            for (int i = 0; i < productos.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {productos[i]} - Precio: ${precios[i]} - Stock: {stock[i]}");
            }

            // Lógica de compra
            stock = comprarProductos(productos, precios, stock, historialProductos, historialCantidades, historialSubtotales);

            // Preguntar si quiere seguir comprando
            if (!seguirComprando())
            {
                break;
            }
        }

        //Mostrar ticket al final
        double totalPago = historialSubtotales.Sum();
        double descuento = 0;

        if (totalPago > 20000)
        {
            descuento = 0.20;
        }
        else if (totalPago > 10000)
        {
            descuento = 0.10;
        }

        double totalConDescuento = totalPago * (1 - descuento);

        Console.WriteLine("\n--- TICKET DE COMPRA ---");
        for (int i = 0; i < historialProductos.Count; i++)
        {
            Console.WriteLine($"{historialProductos[i]} x{historialCantidades[i]} = ${historialSubtotales[i]}");
        }

        Console.WriteLine($"\nTotal sin descuento: ${totalPago}");
        Console.WriteLine($"Descuento aplicado: {descuento * 100}%");
        Console.WriteLine($"Total con descuento: ${totalConDescuento}");
        Console.WriteLine("\n¡Gracias por comprar en la tienda de Ana!");
    }

    // 🛍 Método para procesar la compra
    public static List<int> comprarProductos(
        List<string> productos,
        List<double> precios,
        List<int> stock,
        List<string> historialProductos,
        List<int> historialCantidades,
        List<double> historialSubtotales)
    {
        while (true)
        {
            Console.WriteLine("\nIngrese el nombre del producto que desea comprar (o escriba 'salir' para terminar):");
            string productoPedido = Console.ReadLine();

            if (productoPedido.ToLower() == "salir")
            {
                break;
            }

            // Validar si el producto existe
            int indexProducto = -1;
            for (int i = 0; i < productos.Count; i++)
            {
                if (productos[i].ToLower() == productoPedido.ToLower())
                {
                    indexProducto = i;
                    break;
                }
            }

            if (indexProducto == -1)
            {
                Console.WriteLine("Producto no encontrado. Intente nuevamente.");
                continue;
            }

            // Solicitar cantidad
            while (true)
            {
                try
                {
                    Console.WriteLine("¿Cuántas unidades desea comprar?");
                    int cantidadPedida = int.Parse(Console.ReadLine());

                    if (cantidadPedida <= 0)
                    {
                        Console.WriteLine("Ingrese una cantidad mayor a 0.");
                    }
                    else if (cantidadPedida > stock[indexProducto])
                    {
                        Console.WriteLine("No hay suficiente stock disponible.");
                    }
                    else
                    {
                        // ✅ Compra válida: actualizar stock y guardar historial
                        stock[indexProducto] -= cantidadPedida;

                        double subtotal = precios[indexProducto] * cantidadPedida;

                        historialProductos.Add(productos[indexProducto]);
                        historialCantidades.Add(cantidadPedida);
                        historialSubtotales.Add(subtotal);

                        Console.WriteLine($"Se agregó {cantidadPedida} de {productos[indexProducto]} al carrito. Subtotal: ${subtotal}");
                        break; // salir del bucle de cantidad
                    }
                }
                catch
                {
                    Console.WriteLine("Ingrese un número válido.");
                }
            }
        }

        return stock;
    }

    //Preguntar si se quiere seguir comprando
    public static bool seguirComprando()
    {
        Console.WriteLine("\n¿Desea seguir comprando? (sí/no)");
        while (true)
        {
            string respuesta = Console.ReadLine().ToLower();

            if (respuesta == "sí" || respuesta == "si")
                return true;
            else if (respuesta == "no")
                return false;
            else
                Console.WriteLine("Respuesta no válida. Escriba 'sí' o 'no'.");
        }
    }

}