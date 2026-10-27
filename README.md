# Prog1-Parcial2-2doCuatri-CallebautUriel


## Cómo levantar la API
1. Abrir una terminal y posicionarse en la carpeta de la API:
   ```bash
   cd Talleres.Api
   ```
2. Ejecutar la API:
   ```bash
   dotnet run
   ```
3. La API iniciará en `http://localhost:5050`.
4. Acceder a Swagger en el navegador: `http://localhost:5050/swagger`.
5. Al iniciar la API, si el archivo `talleres.json` no existe, se crea automáticamente con los cuatro talleres iniciales en el directorio de ejecución.


## Cómo correr los tests
Desde la raíz del repositorio, ejecutar:
```bash
dotnet test
```


## Cómo abrir el frontend
1. Asegurarse de que la API esté en ejecución en `http://localhost:5050`.
2. Abrir la carpeta del proyecto en VS Code.
3. Hacer clic derecho sobre `Frontend/index.html` y abrir con Live Server (o abrir directamente en el navegador).
