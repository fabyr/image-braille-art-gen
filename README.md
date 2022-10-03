# image-braille-art-gen
A very small tool to generate braille-unicode ASCII* arts.

*by now the term ASCII-Art generally describes text-art consisting of any
set of characters from the unicode standard. Not just ASCII.

## Building
The .NET 6.0 SDK is required to be installed on the system.
Afterwards the command `dotnet build` with its working directory being the repository folder does the rest.

## Usage
```
image-braille-art-gen <width in px> <height in px> <color threshold 0 - 255> <input file> <output file>
```

## Note
This is not intended to be of huge use or an actual tool anybody should use.
There are better and more conveniant alternatives.
It may fulfill the purpose of code reference for somebody else to easily implement an
*image to braille* generator in their software.