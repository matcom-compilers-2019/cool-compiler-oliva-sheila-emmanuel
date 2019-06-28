# Compilador de COOL a MIPS

## Integrantes:
* Sheila Mederos Miranda
* Alejandro Oliva González
* Emmanuel de la Rosa Pita

```C411```

## Requerimientos:
* Sistema Operativo: ```Windows```
* Compilador de ```C#``` (Visual Studio)
* Antlr

## Aplicación:
Para compilar y ejecutar la aplicación se abre en Visual Studio (u otro compilador de C#) y se ejecuta la aplicación de consola ```Compiler```, la cual tiene una clase ```Program``` con un método ```main``` que da inicio al programa.

Si solo se quiere ejecutar la aplicación se abre el acceso directo a ```Compiler.exe```.

El código de COOL a compilar se encuentra en el archivo ```cool.cl```, si se desea probar algún otro, debe copiarse en este mismo archivo.

## Salida:

En la consola se podrá apreciar los siguientes elementos:

* El código de COOL a compilar.
* El código correspondiente en CIL.

Además en el archivo de salida ``` mips.s ``` se encontrará el código MIPS que le corresponde al CIL mostrado en consola.

Para ejecutar el código de MIPS se recomienda usar el interpreter ```QtSpin```. 




