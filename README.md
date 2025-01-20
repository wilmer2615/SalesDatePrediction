# SalesDatePrediction


Este repositorio contiene el proyecto **backend** desarrollado en .NET para la aplicación Sales Date Prediction. Proporciona servicios REST para gestionar órdenes, empleados, productos, transportistas, y predicciones basadas en datos históricos.

## Requisitos Previos

- **.NET SDK** (v8.0).
- **SQL Server** (local).
- Editor de código como Visual Studio.

## Estructura del Proyecto

```plaintext
SalesDatePrediction/
|-- Entities/          # Definición de entidades de base de datos
|-- Logic/             # Lógica de negocio
|-- Models/            # Modelos de datos para comunicación
|-- Repository/        # Acceso a datos y patrones de repositorio
|-- SalesDatePrediction/ # Proyecto principal de la API
|-- TestLogic/         # Pruebas unitarias de la lógica de negocio
|-- TestSalesDatePrediction/ # Pruebas unitarias de la API
```

### Principales Componentes

- **Entities**: Representa tablas de base de datos como `Orders`, `Customers`, y `Employees`.
- **Logic**: Contiene la lógica para predicciones y cálculos relacionados.
- **Repository**: Implementa el acceso a datos utilizando patrones de repositorio para desacoplar la lógica de negocio.
- **SalesDatePrediction**: Define los controladores REST.
- **Tests**: Incluye pruebas unitarias para garantizar calidad y funcionalidad.

## Configuración Inicial

1. Clona este repositorio:
   ```bash
   git clone <url-del-repositorio-backend>
   cd SalesDatePrediction
   ```

2. Configura la cadena de conexión en `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=StoreSample;User Id=tuUsuario;Password=tuContrasena;"
     }
   }
   ```

3. Ejecutar los script SQL para configurar la base de datos:
  - Se deben ejecutar los scripts en la carpeta DBScripts que esta en la raiz del proyecto, para crear el store procedure.

4. Construye y ejecuta el proyecto:
   ```bash
   dotnet build
   dotnet run
   ```



## Servicios REST Disponibles

1. **Clientes**:
   - `GET /api/Customers`: Obtiene todos los clientes.
   - `GET /api/Customers/{id}`: Obtiene un cliente por ID.

2. **Órdenes**:
   - `GET /api/Orders`: Lista todas las órdenes.
   - `POST /api/orders`: Crea una nueva orden.

3. **Productos**:
   - `GET /api/Products`: Lista todos los productos.

4. **Empleados**:
   - `GET /api/Employees`: Lista todos los empleados.
   
5. **Transportistas**:
   - `GET /api/Shippers`: Lista todos los Transportistas.   


## Buenas Prácticas

1. **Principios SOLID**:
   - Inyección de dependencias para facilitar pruebas y modularidad.
   - Implemantacion de interfaces
   - Separación de responsabilidad
2. **Separation of Concerns**:
   - Lógica de negocio separada de los controladores y el acceso a datos.
3. **Pruebas Unitarias**:
   - Pruebas unitarias con XUnit
    


---


