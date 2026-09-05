# SkynetProtocol

Prototipo funcional de videojuego 2D desarrollado en Unity para el Hito 1 de la asignatura Desarrollo de Videojuegos.

## Concepto

Un pequeño robot con orugas explora una instalación tecnológica. El escenario se construye con un kit modular de Prefabs para paredes, plataformas, arquitectura, decoración, iluminación y elementos de gameplay.

## Controles

- `A` / flecha izquierda: moverse a la izquierda.
- `D` / flecha derecha: moverse a la derecha.
- `Espacio`: saltar.
- `R`: reiniciar el nivel actual.

## Prologo jugable

`Level00_Despertar` es la primera escena del juego. Presenta el arranque de la IA dentro del robot, bloquea temporalmente el movimiento, enciende su visor y entrega el control con el objetivo de abandonar la sala de pruebas. La puerta derecha conduce a `Level01`.

En `Level01`, el robot debe empujar una caja física sobre una placa de presión para abrir la puerta de seguridad. Cruzar la puerta completa el prototipo y muestra la opción de reinicio.

## Tecnología

- Unity 6.3 LTS (`6000.3.22f1`).
- Universal Render Pipeline 2D.
- C# e Input System.
- Física mediante `Rigidbody2D` y `Collider2D`.

## Estructura principal

- `Assets/Scenes`: escenas del juego.
- `Assets/Scripts`: control y animación del robot.
- `Assets/Prefabs/Environment`: kit modular de construcción.
- `Assets/Art`: sprites y conceptos visuales.

## Kit de laboratorio

Los accesorios están separados por tipo para que puedan arrastrarse directamente a la escena:

- `Furniture`: escritorios, silla, banco, estantería y casillero.
- `Electronics`: laptops, monitores, teclado, tableta, cámara y consolas.
- `Science`: microscopio, tubos, químicos, camilla, brazo robótico y generador.
- `Clutter`: cables, papeles, herramientas, baterías, barriles y piezas rotas.
- `Signs`: señalización y terminal de acceso.

Son decorativos y no tienen colisión, por lo que se pueden superponer libremente. Cada prefab incluye un componente `Sorting Group`; modifica `Order in Layer` para colocarlo delante o detrás de otros objetos. El kit completo puede reconstruirse desde `Tools > Skynet Protocol > Regenerar kit de accesorios del laboratorio`.
