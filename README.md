# Bases 2. Proyecto Final.

## Restaurant Proyect (QuickBIte)

```bash
/TuProyecto
├── /Presentation                 // Capa de Presentación (UI)
│   ├── Program.cs                // Punto de entrada
│   ├── MainForm.cs               // Formulario principal
│   ├── ReportForm.cs             // Formulario para reportes
│   ├── LoginForm.cs              // Formulario de inicio de sesión
│   ├── /Views                    // Vistas adicionales (tablas, gráficos)
│   ├── /Resources                // Imágenes, íconos y otros recursos
│   └── /Styles                   // Estilos (CSS, XAML para WPF)
│
├── /BusinessLogic                // Capa de Lógica de Negocio
│   ├── /Services
│   │   ├── ReportService.cs      // Servicio para gestionar reportes
│   │   ├── AuthService.cs        // Servicio de autenticación
│   │   └── PaymentService.cs     // Servicio de pagos
│   └── /Interfaces               // Definición de interfaces de servicios
│       ├── IReportService.cs
│       ├── IAuthService.cs
│       └── IPaymentService.cs
│
├── /DataAccess                   // Capa de Acceso a Datos
│   ├── /Context
│   │   └── DatabaseContext.cs    // Contexto de base de datos (Entity Framework)
│   ├── /Repositories
│   │   ├── ReportRepository.cs   // Repositorio para reportes
│   │   ├── UserRepository.cs     // Repositorio para usuarios
│   │   └── PaymentRepository.cs  // Repositorio para pagos
│   ├── /Entities                 // Clases que representan las tablas de la BD
│   │   ├── Report.cs             // Entidad Reporte
│   │   ├── User.cs               // Entidad Usuario
│   │   └── Payment.cs            // Entidad Pago
│   └── /Migrations               // Migraciones para EF (opcional)
│
├── /Utilities                    // Capa de Utilidades (opcional)
│   ├── LogHelper.cs              // Clase para manejo de logs
│   ├── ErrorHandler.cs           // Manejo global de errores
│   ├── ConfigHelper.cs           // Utilidad para leer configuraciones
│   └── Constants.cs              // Constantes reutilizables
│
├── /Tests                        // Capa de Pruebas
│   ├── /BusinessLogicTests       // Pruebas unitarias de lógica de negocio
│   │   ├── ReportServiceTests.cs
│   │   └── AuthServiceTests.cs
│   ├── /DataAccessTests          // Pruebas de acceso a datos
│   │   └── ReportRepositoryTests.cs
│   ├── /IntegrationTests         // Pruebas de integración
│   └── /MockData                 // Datos de prueba
│
└── /Properties                   // Archivos de configuración del proyecto
    └── launchSettings.json       // Configuraciones de inicio
```

### **1. Capa de presentación**

- *Interacción con el usuario:* Capa donde se presenta la interfaz gráfica o visual donde las capas interactúan
- *Validación básica:* Valida entradas el usuario, por ejemplo que las entradas estén completas  
- *Comunicación con la capa de lógica de negocios:* Está capa envía las solicitudes al usuario a la lógica de negocios y recibe respuestas para mostrar la interfaz.  
### **2. Capa lógica de negocios**

- *Reglas de negocio:* Toda la lógica del negocio, como se calculan precios etc.
- *Procesamiento de datos:* Procesa los datos recibidos de la capa de presentación antes de enviarlos a la capa de acceso a datos, o recibir datos de la capa de acceso a datos para pasarlos a la capa de presentación.
- *Comunicación entre la UI y la base de datos:* Recibe solicitudes de la capa de presentación, aplicando las reglas de negocio necesarias, y luego envía solicitudes en la capa de acceso a datos.

