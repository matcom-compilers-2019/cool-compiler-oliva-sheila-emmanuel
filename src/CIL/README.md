# <p align="center">Especificaciones de MIPS

Las propiedades `Args` y `Locals` de la clase `CIL_Function` son dos diccionarios de `string` contra `int`. En las llaves van los nombres de las variables y en los valores van identificadores que comienzan en 0 e incrimentan de 1 en 1.
Los valores de ambas propiedades estan unos a continuacion de otros

La clase `CIL_OneType` representa los tipos de cool. Tiene una propiedad `Attributes` donde van las propiedas de todos sus padres y las suyas propias en orden, empezando por las de sus ancestros mas lejanos y terminando en las de ellos. Lo mismo para la propiedad `Methods` lo que en esta se guardan los metodos del tipo y de todos sus ancestros en el mismo orden.