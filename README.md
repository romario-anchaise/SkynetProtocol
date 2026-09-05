# SkynetProtocol

Prototipo funcional de videojuego 2D desarrollado en Unity para el Hito 1 de la asignatura Desarrollo de Videojuegos.

## Concepto

Un pequeño robot con orugas explora una instalación tecnológica. El escenario se construye con un kit modular de Prefabs para paredes, plataformas, arquitectura, decoración, iluminación y elementos de gameplay.

## Controles

- `A` / flecha izquierda: moverse a la izquierda.
- `D` / flecha derecha: moverse a la derecha.
- `Espacio`: saltar.

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
