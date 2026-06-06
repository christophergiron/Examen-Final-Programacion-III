# Examen Final Programacion III

# Serie II

# 1. Como la estructura de colas utilizada a nivel de infrastructura de aplicaciones puede ayudar a crear aplicaciones altamente escalables y defina un ejemplo concreto. 

- La estructura de colas sirve mucho en la infrasestructura porque nos permite llevar un control de flujo mas ordenado gracias a la asyncronia procesando cada orden de manera paralela haciendo mas eficiente el proceso

- Ejemplo: queremos realizar multiples ordenes de cafe gracias a la estructura de colas podemos ir orden por orden realizando todos los pedidos paralelamente de esta forma es mas ordenado y evitamos sobrecargar el sistema por culpa de las multiples ordenes. 

# 2. Elabore un diagrama de como la herramienta utilizada para su proyecto (Kafka) puede contribuir a que una aplicacion de transferencias monetarias pueda recibir una mayor cantidad de solicitudes simultaneas.

<img width="655" height="591" alt="Diagrama serie II drawio" src="https://github.com/user-attachments/assets/21ce3b9c-1cdf-41fb-a4ef-142c437456f9" />

# Serie III

## Decisiones de Diseño

Se utilizaron entidades simples basadas en clases de C# que representan directamente las tablas en la base de datos mediante Entity Framework Core.

* `Product` → representa los productos disponibles
* `Cart` → representa el carrito del usuario
* `CartItem` → relación entre carrito y productos

Se eligió esta estructura porque:

* Permite una relación clara entre entidades
* Facilita el uso de ORM (Entity Framework)
* Hace más sencillo el mantenimiento y escalabilidad

---

# Campos de Entidades

**Product**

* `Id` → identificador único
* `Name` → nombre del producto
* `Stock` → cantidad disponible
* `Price` → precio del producto

**Cart**

* `Id` → identificador del carrito
* `UserId` → usuario asociado

**CartItem**

* `Id`
* `CartId`
* `ProductId`
* `Quantity`

Se eligieron estos campos porque representan lo necesario para un sistema de carrito funcional tambien asi podemos manejar de mejor manera el stock 
---

# Separación de Archivos

Se utilizó una arquitectura en capas:

* **Controllers**

  * Manejan las peticiones HTTP
  * No contienen lógica de negocio

* **Services**

  * Contienen la lógica principal (ej: agregar al carrito, validar stock)
  * Facilitan reutilización y pruebas

* **Data (DbContext)**

  * Maneja la conexión con la base de datos

* **Models**

  * Representan las entidades
---

## Migraciones 

### Crear migración

```bash
dotnet ef migrations add InitialCreate
```

### 🔹 Aplicar migración

```bash
dotnet ef database update
```
Estas migraciones permiten versionar la base de datos y mantener consistencia entre el modelo y la BD.

---

##  Comandos Docker Utilizados

### 🔹 Levantar PostgreSQL

```bash
docker run --name postgres-db \
-e POSTGRES_PASSWORD=1234 \
-e POSTGRES_DB=productsdb \
-p 5432:5432 \
-d postgres
```

---

### 🔹 Ver contenedores activos

```bash
docker ps
```

---

### 🔹 Detener contenedor

```bash
docker stop postgres-db
```

---

### 🔹 Iniciar contenedor

```bash
docker start postgres-db
```

---

### 🔹 Eliminar contenedor

```bash
docker rm postgres-db
```

### 🔹 Desarrollo Propio

El desarrollo principal del sistema fue realizado de forma individual, incluyendo:

* Diseño y creación de la API REST
* Definición de entidades (`Product`, `Cart`, `CartItem`)
* Implementación de controladores (Controllers)
* Lógica de negocio en servicios (Services)
* Integración con Entity Framework Core
* Configuración de la base de datos PostgreSQL
* Implementación del flujo completo:

  * agregar productos al carrito
  * consultar carrito
  * validación de stock
  * proceso de compra (checkout)
* Pruebas funcionales mediante Postman
* Pruebas de carga utilizando k6

---

### 🔹 Uso de Asistencia (ChatGPT)

Se utilizó asistencia como herramienta de apoyo principalmente para:

* Resolución de errores específicos:

  * problemas con migraciones
  * errores de Entity Framework
  * manejo de concurrencia (stock)
  * errores de configuración del DbContext

* Optimización del sistema:

  * mejoras en manejo de asincronía (`async/await`)
  * recomendaciones para estructura de servicios
  * mejoras en pruebas de carga (k6)

* Apoyo en documentación:

  * redacción del README
  * organización de la explicación técnica
---

# Evidencias fotograficas
<img width="1730" height="1077" alt="Captura de pantalla 2026-06-06 080732" src="https://github.com/user-attachments/assets/07d62f5b-6a38-4b77-a5fc-498704a9ef7d" />
<img width="1918" height="1078" alt="Captura de pantalla 2026-06-06 080656" src="https://github.com/user-attachments/assets/9b0d01a7-6bd8-4907-84fe-15ab5206fa21" />
<img width="1272" height="755" alt="Captura de pantalla 2026-06-06 080635" src="https://github.com/user-attachments/assets/ed6d6786-61b5-4c5a-81a0-adf36511a00c" />
<img width="1776" height="832" alt="Captura de pantalla 2026-06-06 080342" src="https://github.com/user-attachments/assets/126b0158-2efb-4556-8ffe-0b9b6b156c94" />
<img width="1918" height="1011" alt="Captura de pantalla 2026-06-06 080749" src="https://github.com/user-attachments/assets/039fa6bb-0df9-4a43-b260-137d4dcad912" />
<img width="1835" height="430" alt="Captura de pantalla 2026-06-06 080426" src="https://github.com/user-attachments/assets/a592b939-d60e-41fb-9b88-8d0badd6cc1d" />
<img width="1181" height="851" alt="Captura de pantalla 2026-06-06 080616" src="https://github.com/user-attachments/assets/1e556387-b8ad-44e2-b224-f35f65396758" />
<img width="1325" height="872" alt="Captura de pantalla 2026-06-06 080459" src="https://github.com/user-attachments/assets/4fb48b81-7ac3-4b5b-be65-67d32ab8fc8d" />
