# Bash
A C# command line application that simulates some bash commands.


## Commands available:
### cd
Changes the directory to a specified location. If you just enter cd without any parameters, it will go to the parent directory. <br>
Usage: `cd <path>`

### ls
Lists the folders and files of the current directory. <br>
Usage: `ls`

### copy
Copies a file into a directory or into another file (by overwriting it), or a directory into another directory (by creating a subfolder). <br>
Usage: `copy <source_path> <destination_path>`

### move
Moves a file into a directoro or into another file (by overwriting it), or a directory into another directory (by creating a subfolder). <br> 
Usage: `move <source_path> <destination_path>`

### rm
Removes a file or a directory and all of its subfolders. <br>
Usage: `rm <target_path>`

### rename
Renames a file or a directory by moving to themselves. <br>
Usage `rename <source> <target_name>`

### mkdir
Creates a directory in the specified path. <br>
Usage: `mkdir <directory_path>`

### mkfile
Creates a file in the specified path. <br>
Usage: `mkfile <file_path>` 

### ps
Shows the current processes ordered by memory usage (in Megabytes). <br>
Usage: `ps`

### myip
Shows the local and public ip of the computer.
Usage: `myip`

