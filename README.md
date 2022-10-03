# image-braille-art-gen
A very small CLI tool to generate braille-unicode ASCII* arts.

*by now the term ASCII-Art generally describes text-art consisting of any
set of characters from the unicode standard. Not just ASCII.

## Building
The .NET 6.0 SDK ([Official site](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)) is required to be installed on the system.
Afterwards the command `dotnet build` with its working directory being the repository folder does the rest.

## Usage
```
image-braille-art-gen <width in px> <height in px> <color threshold 0 - 255> <input file> <output file>
```

## Results
Input image

![Image](/assets/ok.jpg)

The resulting text file can be found [here](/assets/result.txt).
Supplied arguments:
```
Width: 256
Height: 256
Color Threshold: 128
Input File: assets/ok.jpg
Output File: assets/result.txt
```


## Note
This is not intended to be of huge use or an actual tool anybody should use.
There are better and more conveniant alternatives.
It may fulfill the purpose of code reference for anybody else wishing to implement an
*image to braille* generator in their software with decreased effort.