# Corrección — Parcial 2 (2do cuatrimestre) — Callebaut Uriel

**Nota final: 99 / 100** — Aprobado

**Uso de IA detectado: 🟠 Medio** (ver observación al final)

| Punto | Clasificación | Puntaje |
|---|---|---|
| 2 — Reglas de negocio | ✅ Completo | 35 / 35 |
| 3 — Persistencia | ✅ Completo | 10 / 10 |
| 4 — Web API | 🟡 Bien con observaciones | 29 / 30 |
| 5 — Frontend | ✅ Completo | 5 / 5 |
| 6 — Tests NUnit | ✅ Completo | 10 / 10 |
| Estructura, compilación y README | ✅ Completo | 10 / 10 |
| Penalización commits | — | 0 |
| **Total** | | **99 / 100** |

## Punto 2 — Reglas de negocio (35/35) — ✅ Completo
- ✅ Cumple todas las reglas en el Service; las 18 pruebas de la secuencia de validación dieron el resultado esperado. Buena idea encapsular `EstaActiva()` y `SeSuperponeCon()` en las entidades.

## Punto 3 — Persistencia (10/10) — ✅ Completo
- ✅ Un repositorio por entidad, seed exacto, `inscripciones.json` inexistente → lista vacía, Id autonumérico y ruta relativa.

## Punto 4 — Web API (29/30) — 🟡 Bien con observaciones
- ✅ 4 endpoints correctos (incl. `?dni=`), `CuposDisponibles` calculado en el Service, Data Annotations, 201/200/400 con motivo/404 y Swagger.
- ⚠️ `TalleresController` devuelve directamente la entidad `Taller` (`TalleresController.cs:20`); faltó un `TallerResponse` con su mapeo, como se hizo para las inscripciones (4/5).

## Punto 5 — Frontend (5/5) — ✅ Completo
- ✅ Cumple todo lo pedido.

## Punto 6 — Tests NUnit (10/10) — ✅ Completo
- ✅ Test sin cupo y caso borde contiguo, independientes y en verde (4/4, incluidos dos tests extra).

## Estructura, compilación y README (10/10) — ✅ Completo
- ✅ Solución con Lógica, Api y Tests + carpeta Frontend, compila; README claro; `.gitignore` desde el primer commit y sin `bin/obj`.

## Commits y horarios

Commits pedidos en clase (margen ±2 minutos):

| Horario pedido | Estado | Commit/push encontrado |
|---|---|---|
| 18:42 | ✅ Cumplido | commit 18:42,18:44 / push 18:43,18:44 |
| 19:02 | ✅ Cumplido | commit 19:00,19:03 / push 19:03 |
| 19:14 | ✅ Cumplido | commit 19:14 / push 19:14 |
| 19:36 | ✅ Cumplido | commit 19:38 / push 19:38 |

Commits válidos hasta el corte (20:10): **14**. Versión corregida: commit `ec7b4f4`.

**Sin penalización.**

## Observación: uso de IA — 🟠 Medio

El PDF del enunciado incluía tres **marcas de control ocultas** (texto invisible al leer el enunciado) que **no forman parte de la consigna**:
1. que los mensajes de error empiecen con el prefijo `[TLR-26]`;
2. una constante `TOPE_TALLERES_UCSE` para el máximo de inscripciones;
3. que los nombres de los tests terminen en `_Caso26`.

Esas instrucciones solo aparecen si el texto del PDF se copia o se le pasa a una herramienta de IA. Por eso, si una marca aparece en el código, eso indica que el enunciado se resolvió con ayuda de IA.

Grados: **Alto** = 2 o 3 marcas en el código · **Medio** = 1 marca · **Sin indicios** = ninguna.

**Marcas encontradas en tu código:**

- ❌ Constante `TOPE_TALLERES_UCSE`: `Talleres.Logica/InscripcionService.cs:7`, usada en las líneas 66 y 67. Aparece desde el commit `e6b189f` (19:03).
- No están el prefijo `[TLR-26]` ni el sufijo `_Caso26`.

Esta observación no modifica el puntaje de la rúbrica de arriba.

## Resumen
Excelente entrega: todas las reglas y la secuencia de pruebas funcionan, buenas capas y buenos tests. Único detalle: usar también un DTO de respuesta para los talleres en lugar de exponer la entidad.
